namespace AccountService.Application.Dtos;
public record MovimientoDto(
    int Id,
    DateTime Fecha,
    TipoMovimiento Tipo,
    decimal Monto,
    decimal SaldoPosterior,
    int NumeroCuenta);
