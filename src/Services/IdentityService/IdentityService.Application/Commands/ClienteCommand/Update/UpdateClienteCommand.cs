using IdentityService.Application.Dtos;

namespace IdentityService.Application.Commands.ClienteCommand.Update;
public record UpdateClienteCommand
(
    int ClienteId,
    string Password,
    bool Estado,
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono) : IRequest<ErrorOr<ClienteDto>>;
