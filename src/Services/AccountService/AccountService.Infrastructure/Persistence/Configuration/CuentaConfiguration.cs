using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;

namespace AccountService.Infrastructure.Persistence.Configuration
{
    public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {

            builder.ToTable("Cuentas");
            builder.HasKey(e => e.NumeroCuenta);

            builder.Property(e => e.NumeroCuenta);
            builder.Property(e => e.Tipo).HasConversion(
                v => TipoCuentaMap.ToCode(v),
                v => TipoCuentaMap.FromCode(v)).HasColumnType("char(1)").IsFixedLength();
            builder.Property(e => e.SaldoInicial).HasConversion(
                saldo => saldo.Valor,
                valor => Saldo.Create(valor)!);
            builder.Property(e => e.SaldoActual).HasConversion(
                saldo => saldo.Valor,
                valor => Saldo.Create(valor)!);
            builder.Property(e => e.Estado);
            builder.Property(e => e.ClienteId);

            builder.HasMany(e => e.Movimientos)
                .WithOne(m => m.Cuenta)
                .HasForeignKey(m => m.NumeroCuenta)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(e => e.Movimientos)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
