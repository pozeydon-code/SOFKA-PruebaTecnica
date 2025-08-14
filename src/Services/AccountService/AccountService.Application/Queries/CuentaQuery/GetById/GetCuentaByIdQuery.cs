using AccountService.Application.Dtos;

namespace AccountService.Application.Queries.CuentaQuery.GetById;
public record GetCuentaByIdQuery(
     int Id
) : IRequest<ErrorOr<CuentaDto>>;
