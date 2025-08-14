using IdentityService.Application.Interfaces;
using IdentityService.Domain.Primitives;
using IdentityService.Domain.DomainErrors;

namespace IdentityService.Application.Commands.ClienteCommand.Delete;
internal sealed class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, ErrorOr<Unit>>
{
    private readonly IClienteRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClienteCommandHandler(IClienteRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteClienteCommand request, CancellationToken ct)
    {
        if (await _repo.GetByIdAsync(request.Id) is not Cliente cliente)
            return Errors.Cliente.NotFound;

        await _repo.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
