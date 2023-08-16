namespace LogReport.Models.ReportCreators.LogReports;

using OneOf;
using Utils;
using Reports;
using Reports.Logs;
using System.Text.RegularExpressions;

public class LogReportCreator : ILogReportCreator
{
    public OneOf<ILogReport, EmptyReport> CreateReport(string path)
    {
        DateTime? earliestDateTime = null; 
        DateTime? latestDateTime = null;
        var categoryCount = new Dictionary<string, int>();
        
        // Pattern for a single log record.
        var pattern = @"(\[[0-9.:\s]+\])(\[[\w\s]+\]) ([\S\s]+)";

        using var reader = new StreamReader(path);
        var isEmptyLog = true;
        while (reader.EndOfStream is false)
        {
            var line = reader.ReadLine()!;
            isEmptyLog = false;
            var match = Regex.Match(line, pattern);

            var recordDate = DateTime.Parse(match.Groups[1].Value.Trim('[', ']'));
            var recordCategory = match.Groups[2].Value.Trim('[', ']');
            
            // I do not understand, what is the purpose of anonymizing,
            // as there is no saving log records' text.
            var recordMessage = EmailAnonymizerService.AnonymizeEmails(match.Groups[3].Value);

            earliestDateTime ??= recordDate;
            if (recordDate < earliestDateTime)
            {
                earliestDateTime = recordDate;
            }

            latestDateTime ??= recordDate;
            if (recordDate > latestDateTime)
            {
                latestDateTime = recordDate;
            }

            if (categoryCount.ContainsKey(recordCategory) is false)
            {
                categoryCount[recordCategory] = 0;
            }
            categoryCount[recordCategory]++;
        }
        
        return isEmptyLog 
            ? EmptyReport.Empty
            : new LogReport(earliestDateTime!.Value, latestDateTime!.Value, categoryCount);
    }
}