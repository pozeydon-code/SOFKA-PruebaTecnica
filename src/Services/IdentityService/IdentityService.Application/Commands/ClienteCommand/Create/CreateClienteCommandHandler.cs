using IdentityService.Application.Interfaces;
using IdentityService.Domain.Primitives;

namespace IdentityService.Application.Commands.ClienteCommand.Create
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ErrorOr<Unit>>
    {
        private readonly IClienteRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateClienteCommandHandler(IClienteRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateClienteCommand request, CancellationToken ct)
        {
            Cliente entity = new Cliente(
                0,
                request.Password,
                request.Estado,
                request.Nombre,
                request.Genero,
                request.Edad,
                request.Identificacion,
                request.Direccion,
                request.Telefono);
            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
