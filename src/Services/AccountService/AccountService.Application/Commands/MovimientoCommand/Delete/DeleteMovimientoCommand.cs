namespace AccountService.Application.Commands.MovimientoCommand.Delete;
public record DeleteMovimientoCommand(int Id) : IRequest<ErrorOr<Unit>>;
