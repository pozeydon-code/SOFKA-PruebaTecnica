using AccountService.Application.Interfaces;
using AccountService.Domain.Primitives;
using AccountService.Domain.DomainErrors;

namespace AccountService.Application.Commands.CuentaCommand.Delete;

internal sealed class DeleteCuentaCommandHandler : IRequestHandler<DeleteCuentaCommand, ErrorOr<Unit>>
{
    private readonly ICuentaRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCuentaCommandHandler(ICuentaRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCuentaCommand request, CancellationToken ct)
    {
        if (await _repo.GetByIdAsync(request.Id) is not Cuenta cuenta)
            return Errors.Cuenta.NotFound;

        await _repo.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
