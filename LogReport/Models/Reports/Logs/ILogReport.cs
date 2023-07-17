using System.Text.Json.Serialization;

namespace LogReport.Models.Reports.Logs;

/// <summary>
/// Represents a basic report which is created
/// for a service's single log file.
/// </summary>
[JsonDerivedType(typeof(LogReport))]
public interface ILogReport
{
    /// <summary>
    /// Compares data stored in current report with data stored
    /// in given report according to a certain logic and changes
    /// data stored in current report if needed.
    /// </summary>
    /// <param name="report">Report to compare with current.</param>
    public void Update(ILogReport report);
}