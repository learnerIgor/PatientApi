using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patient.Persistence.EntityTypeConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Domain.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.HasOne(p => p.Name)
                .WithOne(n => n.Patient)
                .HasForeignKey<Domain.Patient>(p => p.NameId);

            builder.Navigation(n => n.Name).AutoInclude();
        }
    }
}
