using IdentityService.Application.Dtos;

namespace IdentityService.Application.Queries.ClienteQuery.GetById;

public record GetClienteByIdQuery(int ClienteId) : IRequest<ErrorOr<ClienteDto>>;
