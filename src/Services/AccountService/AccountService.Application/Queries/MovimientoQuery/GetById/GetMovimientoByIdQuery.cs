using AccountService.Application.Dtos;

namespace AccountService.Application.Queries.MovimientoQuery.GetById;
public record GetMovimientoByIdQuery
(
     int Id
) : IRequest<ErrorOr<MovimientoDto>>;
