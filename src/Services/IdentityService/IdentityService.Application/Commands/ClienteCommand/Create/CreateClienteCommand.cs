
namespace IdentityService.Application.Commands.ClienteCommand.Create;
public record CreateClienteCommand
(
    string Password,
    bool Estado,
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono) : IRequest<ErrorOr<Unit>>;
