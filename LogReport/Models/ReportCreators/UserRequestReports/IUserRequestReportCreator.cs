namespace LogReport.Models.ReportCreators.UserRequestReports;

using Reports.Services;

/// <summary>
/// Represents a class that creates report for user request.
/// </summary>
public interface IUserRequestReportCreator
{
    /// <summary>
    /// Returns report for user request.
    /// </summary>
    /// <param name="directoryPath">Path to directory with logs.</param>
    /// <param name="request">Text of user request.</param>
    /// <returns>Reports for each service of request.</returns>
    public IDictionary<string, IServiceReport> CreateReport(string directoryPath, string request);
}