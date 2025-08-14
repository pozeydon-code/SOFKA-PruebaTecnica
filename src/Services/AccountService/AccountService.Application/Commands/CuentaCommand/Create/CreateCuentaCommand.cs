namespace AccountService.Application.Commands.CuentaCommand.Create;

public record CreateCuentaCommand
(
    TipoCuenta Tipo,
    decimal Saldo,
    bool Estado,
    int ClienteId) : IRequest<ErrorOr<Unit>>;
