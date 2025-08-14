namespace AccountService.Application.Commands.MovimientoCommand.Create;
public record CreateMovimientoCommand(
    int NumeroCuenta,
    TipoMovimiento Tipo,
    decimal Monto
    ) : IRequest<ErrorOr<Unit>>;

