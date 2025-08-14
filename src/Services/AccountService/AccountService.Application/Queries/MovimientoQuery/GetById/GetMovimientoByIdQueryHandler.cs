using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
using AccountService.Domain.DomainErrors;
namespace AccountService.Application.Queries.MovimientoQuery.GetById;
public class GetMovimientoByIdQueryHandler : IRequestHandler<GetMovimientoByIdQuery, ErrorOr<MovimientoDto>>
{
    private readonly IMovimientoRepository _repository;
    public GetMovimientoByIdQueryHandler(IMovimientoRepository repository) => _repository = repository;

    public async Task<ErrorOr<MovimientoDto>> Handle(GetMovimientoByIdQuery request, CancellationToken ct)
    {
        if (await _repository.GetByIdAsync(request.Id) is not Movimiento movimiento)
            return Errors.Movimiento.NotFound;

        return new MovimientoDto(
            movimiento.Id,
            movimiento.Fecha,
            movimiento.Tipo,
            movimiento.Valor,
            movimiento.SaldoPosterior,
            movimiento.NumeroCuenta);
    }
}
