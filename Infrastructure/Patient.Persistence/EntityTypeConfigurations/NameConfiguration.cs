using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patient.Domain;

namespace Patient.Persistence.EntityTypeConfigurations
{
    public class NameConfiguration : IEntityTypeConfiguration<Name>
    {
        public void Configure(EntityTypeBuilder<Name> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Family)
                .IsRequired();

            builder.HasMany(n => n.GivenNames)
                .WithMany(g => g.Names)
                .UsingEntity(j => j.ToTable("NameGivenNames"));

            builder.Navigation(n => n.GivenNames).AutoInclude();
        }
    }
}
