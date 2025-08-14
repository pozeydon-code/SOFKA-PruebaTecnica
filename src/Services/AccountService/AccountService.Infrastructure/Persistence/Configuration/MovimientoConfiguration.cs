using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;

namespace AccountService.Infrastructure.Persistence.Configuration
{
    public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimientos");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);
            builder.Property(e => e.Fecha);
            builder.Property(e => e.Tipo).HasConversion(v => TipoMovimientoMap.ToCode(v), v => TipoMovimientoMap.FromCode(v)).HasColumnType("char(1)").IsFixedLength();
            builder.Property(e => e.SaldoPosterior).HasConversion(saldo => saldo.Valor, saldo => Saldo.Create(saldo)!);
            builder.Property(e => e.Valor).HasConversion(saldo => saldo.Valor, saldo => Saldo.Create(saldo)!);
            builder.HasOne(m => m.Cuenta)
                .WithMany(e => e.Movimientos)
                .HasForeignKey(m => m.NumeroCuenta);
        }
    }
}
