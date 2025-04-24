using Patient.Persistence;
using Patient.Application;
using System.Text.Json.Serialization;
using Patient.Api.Middlewares;
using Patient.Api;
using System.Reflection;

try
{
    const string version = "v1";
    const string appName = "Patient API v1";
    const string description = "Patient API provides an interface for managing patient data in a healthcare system";

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddSwaggerDescription(Assembly.GetExecutingAssembly(), appName, version, description)
        .AddPersistenceServices(builder.Configuration)
        .AddApplicationServices();

    var app = builder.Build();

    app.RunDbMigrations();

    app.UseExceptionHandlerMiddleware()
        .UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient API v1");
            c.RoutePrefix = string.Empty;
        });

    app.MapControllers();

    app.Run();
}
catch (Exception)
{

	throw;
}


