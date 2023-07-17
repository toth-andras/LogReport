namespace LogReport.Models.ReportCreators.UserRequestReports;

using ServiceReports;
using Reports.Services;
using System.Text.RegularExpressions;

public class UserRequestReportCreator : IUserRequestReportCreator
{
    private readonly IServiceReportCreator _reportCreator;
    
    public UserRequestReportCreator(IServiceReportCreator reportCreator) 
        => _reportCreator = reportCreator;

    public IDictionary<string, IServiceReport> CreateReport(string directoryPath, string request)
    {
        var directory = new DirectoryInfo(directoryPath);
        if (directory.Exists is false)
        {
            throw new ArgumentException($"No such directory: {directoryPath}");
        }

        var pattern = $"{request}.log";
        var serviceNames = directory
            .EnumerateFiles("*.log")
            .Where(info => Regex.IsMatch(info.Name, pattern))
            .Select(info => Path.GetFileNameWithoutExtension(Regex.Match(info.Name, pattern).Groups[0].Value))
            .ToList();

        var report = new Dictionary<string, IServiceReport>();
        foreach (var serviceName in serviceNames)
        {
            _reportCreator.CreateReport(directoryPath, serviceName).Switch(
                normalReport => report.Add(serviceName, normalReport),
                _ => { });
        }

        return report;
    }
}