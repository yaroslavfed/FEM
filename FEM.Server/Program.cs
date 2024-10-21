using System.Text.Json.Serialization;
using FEM.Server.Installers;
using NLog;
using NLog.Web;
using NSwag;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init fem server application");

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors();
services
    .AddControllers()
    .AddJsonOptions(
        e =>
        {
            e.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            e.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }
    );

services.AddOpenApiDocument(
    options =>
    {
        options.PostProcess = document =>
        {
            document.Info = new()
            {
                Version = "v1.0.0",
                Title = "FEM API",
                Description = "Vector FEM solver",
                TermsOfService = "https://example.com/terms",
                Contact = new OpenApiContact { Name = "Example Contact", Url = "https://example.com/contact" },
                License = new OpenApiLicense { Name = "Example License", Url = "https://example.com/license" }
            };
        };
    }
);

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
// if (app.Environment.IsDevelopment())
// {

// }

app.UseOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseReDoc(options => { options.Path = "/redoc"; });

app.UseHttpsRedirection();
app.UseRouting();

// Global cors policy
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();