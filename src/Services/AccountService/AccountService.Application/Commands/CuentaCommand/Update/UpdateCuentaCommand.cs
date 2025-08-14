using AccountService.Application.Dtos;

namespace AccountService.Application.Commands.CuentaCommand.Update;
public record UpdateCuentaCommand
(
    int NumeroCuenta,
    TipoCuenta Tipo,
    decimal Saldo,
    bool Estado,
    int ClienteId) : IRequest<ErrorOr<CuentaDto>>;
