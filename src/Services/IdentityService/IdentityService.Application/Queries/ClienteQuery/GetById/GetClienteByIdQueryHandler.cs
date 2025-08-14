using IdentityService.Application.Interfaces;
using IdentityService.Application.Dtos;
using IdentityService.Domain.DomainErrors;
namespace IdentityService.Application.Queries.ClienteQuery.GetById
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ErrorOr<ClienteDto>>
    {
        private readonly IClienteRepository _repository;
        public GetClienteByIdQueryHandler(IClienteRepository repository) => _repository = repository;

        public async Task<ErrorOr<ClienteDto>> Handle(GetClienteByIdQuery request, CancellationToken ct)
        {
            if (await _repository.GetByIdAsync(request.ClienteId) is not Cliente cliente)
                return Errors.Cliente.NotFound;

            return new ClienteDto(
                cliente.ClienteId,
                cliente.Estado,
                cliente.Nombre,
                cliente.Genero,
                cliente.Edad,
                cliente.Identificacion,
                cliente.Direccion,
                cliente.Telefono);
        }
    }
}
