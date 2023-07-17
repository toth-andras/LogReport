namespace LogReport.Models.Reports.Services;

using Logs;

public class ServiceReport : IServiceReport
{
    private int _rotationsCount;
    
    public string ServiceName { get;}
    
    public ILogReport LogReport { get; }

    /// <summary>
    /// Number of service's log rotations.
    /// </summary>
    /// <exception cref="ArgumentException">Is thrown if rotations count is negative.</exception>
    public int RotationsCount
    {
        get => _rotationsCount;
        set => _rotationsCount = value >= 0
            ? value
            : throw new ArgumentException("The number of rotations must be non-negative");
    }

    public ServiceReport(string serviceName, ILogReport logReport, int rotationsCount)
    {
        ServiceName = serviceName;
        LogReport = logReport;
        RotationsCount = rotationsCount;
    }
}