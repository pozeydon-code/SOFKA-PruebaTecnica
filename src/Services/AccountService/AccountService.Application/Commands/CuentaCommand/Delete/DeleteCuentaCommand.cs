namespace AccountService.Application.Commands.CuentaCommand.Delete;
public record DeleteCuentaCommand(int Id) : IRequest<ErrorOr<Unit>>;
