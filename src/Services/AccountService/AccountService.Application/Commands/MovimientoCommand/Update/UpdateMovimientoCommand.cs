using AccountService.Application.Dtos;

namespace AccountService.Application.Commands.MovimientoCommand.Update;
public record UpdateMovimientoCommand(
    int Id,
    DateTime Fecha,
    TipoMovimiento Tipo,
    decimal Monto,
    decimal SaldoPosterior,
    int NumeroCuenta) : IRequest<ErrorOr<MovimientoDto>>;
