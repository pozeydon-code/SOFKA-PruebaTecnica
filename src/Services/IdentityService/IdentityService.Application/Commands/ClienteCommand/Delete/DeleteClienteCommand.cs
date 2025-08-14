namespace IdentityService.Application.Commands.ClienteCommand.Delete;
public record DeleteClienteCommand(int Id) : IRequest<ErrorOr<Unit>>;
