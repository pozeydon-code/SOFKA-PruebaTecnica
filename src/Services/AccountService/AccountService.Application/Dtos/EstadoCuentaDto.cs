namespace AccountService.Application.Dtos;

public record EstadoCuenta_CuentaDto
(
     string NumeroCuenta,
     string Tipo,
     decimal SaldoInicial,
     List<EstadoCuenta_MovimientoDto> Movimientos
);

public record EstadoCuenta_MovimientoDto
(
     DateTime FechaUtc,
     string Tipo,
     decimal Valor,
     decimal SaldoPosterior
);


public record CuentaSimple(
    int NumeroCuenta,
    TipoCuenta Tipo,
    Decimal SaldoInicial
);

public record EstadoCuentaDto
(
    DateTime Fecha,
    string Cliente,
    int NumeroCuenta,
    string Tipo,
    decimal SaldoInicial,
    bool Estado,
    decimal Movimiento,
    decimal SaldoDisponible
);
