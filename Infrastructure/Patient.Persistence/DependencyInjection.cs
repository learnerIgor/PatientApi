using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patient.Application.Abstractions.Persistence;
using Patient.Domain;

namespace Patient.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, PatientDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PatientConnection"));
            })
                .AddTransient<IBaseRepository<Domain.Patient>, BaseRepository<Domain.Patient>>()
                .AddTransient<IBaseRepository<GivenName>, BaseRepository<GivenName>>()
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>(); ;
        }
    }
}
