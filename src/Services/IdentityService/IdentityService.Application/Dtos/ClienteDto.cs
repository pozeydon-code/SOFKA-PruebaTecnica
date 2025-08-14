namespace IdentityService.Application.Dtos;

public record ClienteDto(
    int ClientId,
    bool Estado,
    string Nombre,
    string Genero,
    int Edad,
    string Identificacion,
    string Direccion,
    string Telefono);
