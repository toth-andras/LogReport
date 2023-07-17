namespace LogReport.Models.ReportCreators.ServiceReports;

using OneOf;
using Reports;
using Reports.Services;

/// <summary>
/// Represents a class that creates report for a service.
/// </summary>
public interface IServiceReportCreator
{
    /// <summary>
    /// Creates report for the given service from logs in given directory.
    /// </summary>
    public OneOf<IServiceReport, EmptyReport> CreateReport(string directoryPath, string serviceName);
}