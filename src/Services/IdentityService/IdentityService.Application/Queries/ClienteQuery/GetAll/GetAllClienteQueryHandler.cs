using IdentityService.Application.Interfaces;
using IdentityService.Application.Dtos;
namespace IdentityService.Application.Queries.ClienteQuery.GetAll
{
    public class GetAllClienteQueryHandler : IRequestHandler<GetAllClienteQuery, ErrorOr<IReadOnlyList<ClienteDto>>>
    {
        private readonly IClienteRepository _repository;
        public GetAllClienteQueryHandler(IClienteRepository repository) => _repository = repository;

        public async Task<ErrorOr<IReadOnlyList<ClienteDto>>> Handle(GetAllClienteQuery request, CancellationToken ct)
        {
            IReadOnlyList<Cliente> clientes = await _repository.GetAllAsync();

            return clientes.Select(cliente => new ClienteDto(
                cliente.ClienteId,
                cliente.Estado,
                cliente.Nombre,
                cliente.Genero,
                cliente.Edad,
                cliente.Identificacion,
                cliente.Direccion,
                cliente.Telefono)).ToList();
        }
    }
}
