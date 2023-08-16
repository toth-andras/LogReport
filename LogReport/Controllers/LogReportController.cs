namespace LogReport.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.ReportCreators.UserRequestReports;

/// <summary>
/// Controller for getting log reports.
/// </summary>
[ApiController]
[Route("[controller]")]
public class LogReportController : ControllerBase
{
    private readonly ILogger<LogReportController> _logger;
    private readonly IUserRequestReportCreator _reportCreator;

    public LogReportController(ILogger<LogReportController> logger, IUserRequestReportCreator reportCreator)
    {
        _logger = logger;
        _reportCreator = reportCreator;
    }
    
    /// <summary>
    /// Returns reports for every service of given directory that matches request.
    /// </summary>
    /// <param name="directoryPath">Path to directory with log files.</param>
    /// <param name="request">Service name(regex enabled).</param>
    [HttpGet("GetReport")]
    public async Task<ObjectResult> GetReport(string directoryPath, string request)
    {
        try
        {
            return Directory.Exists(directoryPath)
                ? StatusCode(200, _reportCreator.CreateReport(directoryPath, request))
                : StatusCode(400, $"No such directory: {directoryPath}");
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return StatusCode(500, e.Message);
        }
    }
}