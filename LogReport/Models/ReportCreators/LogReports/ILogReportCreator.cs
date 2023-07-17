namespace LogReport.Models.ReportCreators.LogReports;

using OneOf;
using Reports;
using Reports.Logs;

/// <summary>
/// Represents a class that creates report for a log file.
/// </summary>
public interface ILogReportCreator
{
    /// <summary>
    /// Creates report for the given log file.
    /// </summary>
    /// <param name="path">Full path to log.</param>
    /// <returns>Report.</returns>
    public OneOf<ILogReport, EmptyReport> CreateReport(string path);
}