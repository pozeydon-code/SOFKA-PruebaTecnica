namespace AccountService.Application.Dtos;
public record CuentaDto
(
    int NumeroCuenta,
    TipoCuenta Tipo,
    decimal SaldoInicial,
    decimal SaldoActual,
    bool Estado,
    int ClienteId);
