using AccountService.Application.Dtos;

namespace AccountService.Application.Queries.MovimientoQuery.GetAll;
public record GetAllMovimientoQuery : IRequest<ErrorOr<IReadOnlyList<MovimientoDto>>>;
