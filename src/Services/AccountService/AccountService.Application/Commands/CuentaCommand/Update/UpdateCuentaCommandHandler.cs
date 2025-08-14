using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
using AccountService.Domain.DomainErrors;
using AccountService.Domain.Primitives;
namespace AccountService.Application.Commands.CuentaCommand.Update;

public class UpdateCuentaCommandHandler : IRequestHandler<UpdateCuentaCommand, ErrorOr<CuentaDto>>
{
    private readonly ICuentaRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCuentaCommandHandler(ICuentaRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<CuentaDto>> Handle(UpdateCuentaCommand request, CancellationToken ct)
    {
        if (await _repo.GetByIdAsync(request.NumeroCuenta) is not Cuenta cuenta)
            return Errors.Cuenta.NotFound;

        if (Saldo.Create(request.Saldo) is not Saldo saldo)
            return Errors.Cuenta.InvalidSaldo;

        Cuenta entity = Cuenta.UpdateCuenta(
            request.NumeroCuenta,
            request.Tipo,
            cuenta.SaldoInicial,
            saldo,
            request.Estado,
            request.ClienteId);

        _repo.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(ct);

        return new CuentaDto(
            entity.NumeroCuenta,
            entity.Tipo,
            entity.SaldoInicial,
            entity.SaldoActual,
            entity.Estado,
            entity.ClienteId);
    }
}
