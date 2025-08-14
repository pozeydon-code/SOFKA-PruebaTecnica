using AccountService.Application.Interfaces;
using AccountService.Domain.Primitives;
using AccountService.Domain.DomainErrors;
using AccountService.Application.Dtos;
namespace AccountService.Application.Commands.MovimientoCommand.Update;

public class UpdateMovimientoCommandHandler : IRequestHandler<UpdateMovimientoCommand, ErrorOr<MovimientoDto>>
{
    private readonly IMovimientoRepository _repo;
    private readonly ICuentaRepository _cuentaRepo;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMovimientoCommandHandler(IMovimientoRepository repo, IUnitOfWork unitOfWork, ICuentaRepository cuentaRepo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _cuentaRepo = cuentaRepo ?? throw new ArgumentNullException(nameof(cuentaRepo));
    }

    public async Task<ErrorOr<MovimientoDto>> Handle(UpdateMovimientoCommand request, CancellationToken ct)
    {
        if (!await _repo.ExistsAsync(request.NumeroCuenta))
            return Errors.Cuenta.NotFound;

        if (!await _cuentaRepo.ExistsAsync(request.NumeroCuenta))
            return Errors.Cuenta.NotFound;

        if (Saldo.Create(request.Monto) is not Saldo monto)
            return Errors.Cuenta.InvalidSaldo;

        if (Saldo.Create(request.SaldoPosterior) is not Saldo saldoPosterior)
            return Errors.Cuenta.InvalidSaldo;
        Movimiento entity = Movimiento.UpdateMovimiento(
            request.Id,
            request.Fecha,
            request.Tipo,
            monto,
            saldoPosterior,
            request.NumeroCuenta
        );
        _repo.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(ct);
        return new MovimientoDto(
            entity.Id,
            entity.Fecha,
            entity.Tipo,
            entity.Valor.Valor,
            entity.SaldoPosterior.Valor,
            entity.NumeroCuenta);
    }
}
