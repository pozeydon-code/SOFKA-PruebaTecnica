using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Persistence.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(e => e.ClienteId);

            builder.Property(e => e.ClienteId).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Estado).IsRequired();

            builder.Property(e => e.Nombre).IsRequired();
            builder.Property(e => e.Genero).IsRequired();
            builder.HasIndex(e => e.Identificacion).IsUnique();
            builder.Property(e => e.Direccion).IsRequired();
            builder.Property(e => e.Telefono).IsRequired();
        }
    }
}
