using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
using AccountService.Domain.DomainErrors;
namespace AccountService.Application.Queries.CuentaQuery.GetById;
public class GetCuentaByIdQueryHandler : IRequestHandler<GetCuentaByIdQuery, ErrorOr<CuentaDto>>
{
    private readonly ICuentaRepository _repository;
    public GetCuentaByIdQueryHandler(ICuentaRepository repository) => _repository = repository;

    public async Task<ErrorOr<CuentaDto>> Handle(GetCuentaByIdQuery request, CancellationToken ct)
    {
        if (await _repository.GetByIdAsync(request.Id) is not Cuenta cuenta)
            return Errors.Cuenta.NotFound;

        return new CuentaDto(
            cuenta.NumeroCuenta,
            cuenta.Tipo,
            cuenta.SaldoInicial,
            cuenta.SaldoActual,
            cuenta.Estado,
            cuenta.ClienteId);
    }
}
