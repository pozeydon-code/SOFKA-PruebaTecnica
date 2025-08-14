using AccountService.Application.Dtos;

namespace AccountService.Application.Queries.MovimientoQuery.Reporte;

public record GetEstadoCuentaQuery(
    int NumeroCliente,
    DateTime FechaInicio,
    DateTime FechaFin
    ) : IRequest<ErrorOr<List<EstadoCuentaDto>>>;
