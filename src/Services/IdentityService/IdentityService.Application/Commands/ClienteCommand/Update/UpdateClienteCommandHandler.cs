using IdentityService.Application.Dtos;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.DomainErrors;
using IdentityService.Domain.Primitives;

namespace IdentityService.Application.Commands.ClienteCommand.Update;

public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, ErrorOr<ClienteDto>>
{
    private readonly IClienteRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateClienteCommandHandler(IClienteRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ClienteDto>> Handle(UpdateClienteCommand request, CancellationToken ct)
    {
        if (!await _repo.ExistsAsync(request.ClienteId))
            return Errors.Cliente.NotFound;

        Cliente entity = Cliente.UpdateCliente(
            request.ClienteId,
            request.Password,
            request.Estado,
            request.Nombre,
            request.Genero,
            request.Edad,
            request.Identificacion,
            request.Direccion,
            request.Telefono);

        _repo.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(ct);
        return new ClienteDto(
            entity.ClienteId,
            entity.Estado,
            entity.Nombre,
            entity.Genero,
            entity.Edad,
            entity.Identificacion,
            entity.Direccion,
            entity.Telefono);
    }
}
