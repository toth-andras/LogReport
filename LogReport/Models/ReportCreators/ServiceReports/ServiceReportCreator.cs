namespace LogReport.Models.ReportCreators.ServiceReports;

using OneOf;
using Reports;
using LogReports;
using Reports.Logs;
using Reports.Services;
using System.Text.RegularExpressions;

public class ServiceReportCreator : IServiceReportCreator
{
    private readonly ILogReportCreator _reportCreator;

    public ServiceReportCreator(ILogReportCreator reportCreator)
        => _reportCreator = reportCreator;
    
    public OneOf<IServiceReport, EmptyReport> CreateReport(string directoryPath, string serviceName)
    {
        var directory = new DirectoryInfo(directoryPath);
        if (directory.Exists is false)
        {
            throw new ArgumentException($"No such directory: {directoryPath}");
        }
        
        var pattern = @$"{serviceName}(\.\d+)?\.log";
        var filesToRead = directory
            .EnumerateFiles("*.log")
            .Where(fileInfo => Regex.IsMatch(fileInfo.Name, pattern))
            .Select(fileInfo => fileInfo.FullName)
            .ToArray();

        if (filesToRead.Length == 0)
        {
            return EmptyReport.Empty;
        }
        
        ILogReport? commonReport = null;
        foreach (var path in filesToRead)
        {
            var report = _reportCreator.CreateReport(path);
            report.Switch(
                logReport =>
                {
                    if (commonReport is null)
                    {
                        commonReport = logReport;
                    }
                    else
                    {
                        commonReport.Update(logReport);
                    }
                },
                _ => { }
            );
        }
        
        return commonReport is null 
            ? EmptyReport.Empty 
            // -1, because there is one log which is current.
            : new ServiceReport(serviceName, commonReport!, filesToRead.Length - 1);
    }
}