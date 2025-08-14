using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;
using AccountService.Domain.DomainErrors;

namespace AccountService.Application.Queries.MovimientoQuery.Reporte;

public class GetEstadoCuentaQueryHandler : IRequestHandler<GetEstadoCuentaQuery, ErrorOr<List<EstadoCuentaDto>>>
{
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IClienteGateway _clienteRepository;

    public GetEstadoCuentaQueryHandler(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository, IClienteGateway clienteRepository)
    {
        _movimientoRepository = movimientoRepository;
        _cuentaRepository = cuentaRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<List<EstadoCuentaDto>>> Handle(GetEstadoCuentaQuery request, CancellationToken ct)
    {
        if (await _clienteRepository.GetByIdAsync(request.NumeroCliente) is not ClienteMiniDto cliente)
            return Errors.Cliente.NotFound;

        DateTime fechaInicio = DateTime.SpecifyKind(request.FechaInicio, DateTimeKind.Utc);
        DateTime fechaFin = DateTime.SpecifyKind(request.FechaFin, DateTimeKind.Utc);

        if (fechaFin < fechaInicio) (fechaInicio, fechaFin) = (fechaFin, fechaInicio);

        var hastaIncl = fechaFin;

        //
        // decimal? lastPrevSaldo = await _repository.GetLastSaldoPosteriorBeforeAsync(fechaInicio, request.NumeroCuenta);
        // decimal saldoInicialRango = lastPrevSaldo ?? (decimal)cuenta.SaldoInicial;
        //
        List<Cuenta> cuentas = await _cuentaRepository.GetByClienteIdAsync(request.NumeroCliente);
        List<EstadoCuentaDto> response = new List<EstadoCuentaDto>();

        foreach (Cuenta cuenta in cuentas)
        {

            List<EstadoCuenta_MovimientoDto> movimientos = await _movimientoRepository.GetMovimientosByRangeAsync(fechaInicio, hastaIncl, cuenta.NumeroCuenta);

            foreach (var m in movimientos)
            {
                var movConSigno = (m.Tipo == TipoMovimientoMap.Retiro) ? -m.Valor : m.Valor;
                response.Add(new EstadoCuentaDto(
                    Fecha: m.FechaUtc,
                    Cliente: cliente.Nombre,
                    NumeroCuenta: cuenta.NumeroCuenta,
                    Tipo: cuenta.Tipo.ToString(),
                    SaldoInicial: cuenta.SaldoInicial,
                    Estado: cuenta.Estado,
                    Movimiento: movConSigno,
                    SaldoDisponible: m.SaldoPosterior
                ));
            }
        }

        return response.OrderByDescending(f => f.Fecha)
            .ToList();
    }
}
