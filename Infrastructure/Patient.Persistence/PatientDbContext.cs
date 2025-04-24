using Microsoft.EntityFrameworkCore;
using Patient.Domain;
using System.Reflection;

namespace Patient.Persistence
{
    public sealed class PatientDbContext : DbContext
    {
        #region Patients
        internal DbSet<Domain.Patient> Patients { get; } = default!;
        internal DbSet<Name> Names { get; } = default!;
        internal DbSet<GivenName> GivenNames { get; } = default!;
        #endregion

        #region EF
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
