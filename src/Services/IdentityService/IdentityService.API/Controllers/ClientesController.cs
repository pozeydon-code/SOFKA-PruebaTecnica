using IdentityService.Application.Commands.ClienteCommand.Create;
using IdentityService.Application.Commands.ClienteCommand.Delete;
using IdentityService.Application.Commands.ClienteCommand.Update;
using IdentityService.Application.Queries.ClienteQuery.GetAll;
using IdentityService.Application.Queries.ClienteQuery.GetById;
using IdentityService.Domain.DomainErrors;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ApiController
    {
        private readonly ISender _mediator;

        public ClientesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteCommand command)
        {
            var createdResult = await _mediator.Send(command);
            return createdResult.Match(
                created => Ok(created),
                error => Problem(error)
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteCommand command)
        {
            if (command.ClienteId != id)
            {
                List<Error> errors = new()
                {
                  Errors.Cliente.NotFound
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
            var itemsResult = await _mediator.Send(new GetAllClienteQuery());

            return itemsResult.Match(
                items => Ok(items),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var itemResult = await _mediator.Send(new GetClienteByIdQuery(id));

            return itemResult.Match(
                item => Ok(item),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteClienteCommand(id));

            return deleteResult.Match(
                customerId => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
