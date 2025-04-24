using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Patient.Api
{
    /// <summary>
    /// Class for configuring dependencies in the application
    /// Contains methods for adding Swagger and other services to the dependency container
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the Swagger description to the dependency container
        /// </summary>
        /// <param name="services">The collection of services to configure</param>
        /// <param name="apiAssembly">The API assembly to get the name of the XML documentation file</param>
        /// <param name="appName">The application name</param>
        /// <param name="version">The API version</param>
        /// <param name="description">The API description</param>
        /// <returns>The updated collection of services</returns>
        public static IServiceCollection AddSwaggerDescription(
        this IServiceCollection services,
        Assembly apiAssembly,
        string appName,
        string version,
        string description)
        {
            return services.AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = appName,
                        Description = description
                    });

                    var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
        }
    }
}
