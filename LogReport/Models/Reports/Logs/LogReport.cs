namespace LogReport.Models.Reports.Logs;

public record LogReport (
    DateTime EarliesRecordDateTime,
    DateTime LatestRecordDateTime,
    Dictionary<string, int> RecordsPerCategory) : ILogReport
{
    /// <summary>
    /// The date and time of the earliest record in log.
    /// </summary>
    public DateTime EarliesRecordDateTime { get; private set; } = EarliesRecordDateTime;

    /// <summary>
    /// The date and time of the latest record in log.
    /// </summary>
    public DateTime LatestRecordDateTime { get; private set; } = LatestRecordDateTime;

    /// <summary>
    /// Number of records for all categories.
    /// </summary>
    public Dictionary<string, int> RecordsPerCategory { get; } = RecordsPerCategory;
    
    public void Update(ILogReport report)
    {
        if (report is not LogReport otherReport)
        {
            throw new ArgumentException("Cannot compare LogReport with any other type.");
        }

        if (otherReport.EarliesRecordDateTime < EarliesRecordDateTime)
        {
            EarliesRecordDateTime = otherReport.EarliesRecordDateTime;
        }
        if (otherReport.LatestRecordDateTime > LatestRecordDateTime)
        {
            LatestRecordDateTime = otherReport.LatestRecordDateTime;
        }

        foreach (var pair in otherReport.RecordsPerCategory)
        {
            if (RecordsPerCategory.ContainsKey(pair.Key) is false)
            {
                RecordsPerCategory[pair.Key] = 0;
            }

            RecordsPerCategory[pair.Key] += pair.Value;
        }
    }
}