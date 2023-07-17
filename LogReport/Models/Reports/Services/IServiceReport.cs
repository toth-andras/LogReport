using System.Text.Json.Serialization;
using LogReport.Models.Reports.Logs;

namespace LogReport.Models.Reports.Services;

/// <summary>
/// Represents basic report created for a single service.
/// </summary>
[JsonDerivedType(typeof(ServiceReport))]
public interface IServiceReport
{
    /// <summary>
    /// Name of service report is created for.
    /// </summary>
    public string ServiceName { get; }
    
    /// <summary>
    /// Contains report from all log files of service.
    /// </summary>
    public ILogReport LogReport { get; }
}