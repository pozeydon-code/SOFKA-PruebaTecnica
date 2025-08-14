using AccountService.Application.Commands.CuentaCommand.Create;
using AccountService.Application.Commands.CuentaCommand.Delete;
using AccountService.Application.Commands.CuentaCommand.Update;
using AccountService.Application.Queries.CuentaQuery.GetAll;
using AccountService.Application.Queries.CuentaQuery.GetById;
namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    public class CuentasController : ApiBaseController
    {
        private readonly ISender _mediator;

        public CuentasController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCuentaCommand command)
        {
            var createdResult = await _mediator.Send(command);
            return createdResult.Match(
                created => Ok(created),
                error => Problem(error)
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCuentaCommand command)
        {
            if (command.NumeroCuenta != id)
            {
                List<Error> errors = new()
                {
                    Error.Validation("Cuenta.UpdateInvalid", "The request Id does not match with the url Id.")
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
            var itemsResult = await _mediator.Send(new GetAllCuentaQuery());

            return itemsResult.Match(
                items => Ok(items),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var itemResult = await _mediator.Send(new GetCuentaByIdQuery(id));

            return itemResult.Match(
                item => Ok(item),
                errors => Problem(errors)
            );
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteCuentaCommand(id));

            return deleteResult.Match(
                customerId => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
