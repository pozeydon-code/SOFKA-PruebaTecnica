using AccountService.Application.Commands.MovimientoCommand.Create;
using AccountService.Application.Commands.MovimientoCommand.Delete;
using AccountService.Application.Commands.MovimientoCommand.Update;
using AccountService.Application.Queries.MovimientoQuery.GetAll;
using AccountService.Application.Queries.MovimientoQuery.GetById;
namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    public class MovimientosController : ApiBaseController
    {
        private readonly ISender _mediator;

        public MovimientosController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovimientoCommand command)
        {
            var createdResult = await _mediator.Send(command);
            return createdResult.Match(
                created => Ok(created),
                error => Problem(error)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovimientoCommand command)
        {
            if (command.NumeroCuenta != id)
            {
                List<Error> errors = new()
                {
                    Error.Validation("Movimiento.UpdateInvalid", "The request Id does not match with the url Id.")
                };
                return Problem(errors);
            }
            var updatedResult = await _mediator.Send(command);
            return updatedResult.Match(
                updated => Ok(updated),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var itemsResult = await _mediator.Send(new GetAllMovimientoQuery());

            return itemsResult.Match(
                items => Ok(items),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var itemResult = await _mediator.Send(new GetMovimientoByIdQuery(id));

            return itemResult.Match(
                item => Ok(item),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteMovimientoCommand(id));

            return deleteResult.Match(
                customerId => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
