using AccountService.Application.Queries.MovimientoQuery.Reporte;

namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportesController : ApiBaseController
    {
        private readonly ISender _mediator;

        public ReportesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int numeroCuenta, [FromQuery] string fecha)
        {
            if (!TryParseRange(fecha, out var d1, out var d2))
                return BadRequest("Formato inválido. Use 'YYYY-MM-DD..YYYY-MM-DD'.");

            var result = await _mediator.Send(new GetEstadoCuentaQuery(numeroCuenta, d1, d2));
            return result.Match(Ok, Problem);
        }

        private static bool TryParseRange(string rango, out DateTime fechaInicio, out DateTime fechaFin)
        {
            fechaInicio = fechaFin = default;
            string[] partes = (rango ?? "").Split("..", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (partes.Length != 2) return false;
            if (!DateTime.TryParse(partes[0], out var a)) return false;
            if (!DateTime.TryParse(partes[1], out var b)) return false;
            fechaInicio = DateTime.SpecifyKind(a, DateTimeKind.Utc);
            fechaFin = DateTime.SpecifyKind(b, DateTimeKind.Utc);
            return true;
        }

    }
}
