using AccountService.Application.Interfaces;
using AccountService.Domain.Primitives;
using AccountService.Domain.DomainErrors;

namespace AccountService.Application.Commands.MovimientoCommand.Create
{
    public class CreateMovimientoCommandHandler : IRequestHandler<CreateMovimientoCommand, ErrorOr<Unit>>
    {
        private readonly IMovimientoRepository _repo;
        private readonly ICuentaRepository _cuentaRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMovimientoCommandHandler(IMovimientoRepository repo, IUnitOfWork unitOfWork, ICuentaRepository cuentaRepo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _cuentaRepo = cuentaRepo ?? throw new ArgumentNullException(nameof(cuentaRepo));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateMovimientoCommand request, CancellationToken ct)
        {
            try
            {

                if (await _cuentaRepo.GetByIdAsync(request.NumeroCuenta) is not Cuenta cuenta)
                    return Errors.Cuenta.NotFound;

                if (Saldo.Create(request.Monto) is not Saldo saldo)
                    return Errors.Cuenta.InvalidSaldo;

                if (request.Tipo == TipoMovimiento.Deposito) cuenta.Depositar(saldo);
                else cuenta.Retirar(saldo);

                await _unitOfWork.SaveChangesAsync(ct);
                return Unit.Value;

            }
            catch (Exception ex)
            {
                return Error.Validation("Movimiento", ex.Message);
            }
        }
    }
}
