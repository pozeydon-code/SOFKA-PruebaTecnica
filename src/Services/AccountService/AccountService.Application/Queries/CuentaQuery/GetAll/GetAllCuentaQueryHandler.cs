using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
namespace AccountService.Application.Queries.CuentaQuery.GetAll;
public class GetAllCuentaQueryHandler : IRequestHandler<GetAllCuentaQuery, ErrorOr<IReadOnlyList<CuentaDto>>>
{
    private readonly ICuentaRepository _repository;
    public GetAllCuentaQueryHandler(ICuentaRepository repository) => _repository = repository;

    public async Task<ErrorOr<IReadOnlyList<CuentaDto>>> Handle(GetAllCuentaQuery request, CancellationToken ct)
    {

        IReadOnlyList<Cuenta> cuentas = await _repository.GetAllAsync();

        return cuentas.Select(c => new CuentaDto(
            c.NumeroCuenta,
            c.Tipo,
            c.SaldoInicial,
            c.SaldoActual,
            c.Estado,
            c.ClienteId)).ToList();
    }
}
