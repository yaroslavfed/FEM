using System.Text.Json.Serialization;
using FEM.Server.Installers;
using NLog;
using NLog.Web;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init fem server application");

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors();
services.AddControllers().AddJsonOptions(e =>
{
    // Serialize enums as strings in api responses (e.g. Role)
    e.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // Ignore omitted parameters on models to enable optional params (e.g. User update)
    e.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Configure DI for application
services.AddServices();
services.AddStorages();

// Configure AutoMapper
services.AddAutoMapper();

// Setup Swagger/OpenAPI
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Building application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Global cors policy
app.UseCors(policyBuilder => policyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();