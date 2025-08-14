using IdentityService.Application.Dtos;

namespace IdentityService.Application.Queries.ClienteQuery.GetAll;

public record GetAllClienteQuery : IRequest<ErrorOr<IReadOnlyList<ClienteDto>>>;
