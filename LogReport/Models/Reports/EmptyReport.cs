namespace LogReport.Models.Reports;

/// <summary>
/// Special class for the case when no data can be collected.
/// </summary>
public class EmptyReport
{
    public static EmptyReport Empty { get; } = new EmptyReport();
}