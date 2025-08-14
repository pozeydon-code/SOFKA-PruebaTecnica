using AccountService.Application.Dtos;

namespace AccountService.Application.Queries.CuentaQuery.GetAll;

public record GetAllCuentaQuery : IRequest<ErrorOr<IReadOnlyList<CuentaDto>>>;
