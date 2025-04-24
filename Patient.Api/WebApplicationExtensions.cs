using Patient.Application.Abstractions.Persistence;

namespace Patient.Api;

/// <summary>
/// Extensions for Patient.Api
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Run a database migration
    /// </summary>
    /// <returns></returns>
    public static WebApplication RunDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();

        var migrationAttemptsCount = 0;
        while (dataContext.GetPendingMigrations().Any())
        {
            migrationAttemptsCount++;
            try
            {
                if (dataContext.GetPendingMigrations().Any())
                {
                    dataContext.Migrate();
                }
            }
            catch (Exception ex)
            {

                if (migrationAttemptsCount == 10)
                {
                    throw;
                }

                app.Logger.LogWarning("{ex.Message}", ex);
                Thread.Sleep(2000);
            }
        }
        return app;
    }
}