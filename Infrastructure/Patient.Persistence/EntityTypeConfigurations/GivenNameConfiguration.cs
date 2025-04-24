using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patient.Domain;

namespace Patient.Persistence.EntityTypeConfigurations
{
    public class GivenNameConfiguration : IEntityTypeConfiguration<GivenName>
    {
        public void Configure(EntityTypeBuilder<GivenName> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasMany(g => g.Names)
                .WithMany(n => n.GivenNames)
                .UsingEntity(j => j.ToTable("NameGivenNames"));
        }
    }
}
