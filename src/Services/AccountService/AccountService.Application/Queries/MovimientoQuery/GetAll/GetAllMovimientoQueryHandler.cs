using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
namespace AccountService.Application.Queries.MovimientoQuery.GetAll;
public class GetAllMovimientoQueryHandler : IRequestHandler<GetAllMovimientoQuery, ErrorOr<IReadOnlyList<MovimientoDto>>>
{
    private readonly IMovimientoRepository _repository;
    public GetAllMovimientoQueryHandler(IMovimientoRepository repository) => _repository = repository;

    public async Task<ErrorOr<IReadOnlyList<MovimientoDto>>> Handle(GetAllMovimientoQuery request, CancellationToken ct)
    {

        IReadOnlyList<Movimiento> movimientos = await _repository.GetAllAsync();

        return movimientos.Select(m => new MovimientoDto(
            m.Id,
            m.Fecha,
            m.Tipo,
            m.Valor,
            m.SaldoPosterior,
            m.NumeroCuenta))
            .ToList();
    }
}
