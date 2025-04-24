using Microsoft.EntityFrameworkCore;
using Patient.Application.Abstractions.Persistence;

namespace Patient.Persistence
{
    internal sealed class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly PatientDbContext _dbApplicationDbContext;

        public DatabaseMigrator(PatientDbContext dbApplicationDbContext)
        {
            _dbApplicationDbContext = dbApplicationDbContext;
        }

        public Task MigrateAsync(CancellationToken cancellationToken)
        {
            return _dbApplicationDbContext.Database.MigrateAsync(cancellationToken);
        }

        public void Migrate()
        {
            _dbApplicationDbContext.Database.Migrate();
        }

        public IEnumerable<string> GetPendingMigrations()
        {
            return _dbApplicationDbContext.Database.GetPendingMigrations();
        }
    }
}