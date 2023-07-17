using System.Reflection;
using LogReport.Models.ReportCreators.LogReports;
using LogReport.Models.ReportCreators.ServiceReports;
using LogReport.Models.ReportCreators.UserRequestReports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ILogReportCreator, LogReportCreator>();
builder.Services.AddTransient<IServiceReportCreator, ServiceReportCreator>();
builder.Services.AddTransient<IUserRequestReportCreator, UserRequestReportCreator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();