using AccountService.Application.Interfaces;
using AccountService.Domain.Primitives;
using AccountService.Domain.DomainErrors;
using AccountService.Application.Dtos;

namespace AccountService.Application.Commands.CuentaCommand.Create
{
    public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, ErrorOr<Unit>>
    {
        private readonly ICuentaRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteGateway _clientes;
        public CreateCuentaCommandHandler(ICuentaRepository repo, IUnitOfWork unitOfWork, IClienteGateway clientes)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _clientes = clientes ?? throw new ArgumentNullException(nameof(clientes));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCuentaCommand request, CancellationToken ct)
        {
            if (await _clientes.GetByIdAsync(request.ClienteId) is not ClienteMiniDto cliente)
                return Errors.Cliente.NotFound;
            if (!cliente.Estado) return Errors.Cliente.Inactive;


            if (Saldo.Create(request.Saldo) is not Saldo saldo)
                return Errors.Cuenta.InvalidSaldo;

            Cuenta entity = new Cuenta(
                0,
                request.Tipo,
                saldo,
                saldo,
                request.Estado,
                request.ClienteId
            );
            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
