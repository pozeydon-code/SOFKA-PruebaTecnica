namespace IdentityService.Domain.Entities;
public class Cliente : Persona
{
    public int ClienteId { get; private set; }

    public string Password { get; private set; }

    public bool Estado { get; private set; }

    public Cliente() { }
    public Cliente(int clienteId, string password, bool estado, string nombre, string genero, int edad, string identificacion, string direccion, string telefono)
    {
        ClienteId = clienteId;
        Password = password;
        Estado = estado;
        Nombre = nombre;
        Genero = genero;
        Edad = edad;
        Identificacion = identificacion;
        Direccion = direccion;
        Telefono = telefono;
    }

    public static Cliente UpdateCliente(int clienteId, string password, bool estado, string nombre, string genero, int edad, string identificacion, string direccion, string telefono)
    {
        return new Cliente(clienteId, password, estado, nombre, genero, edad, identificacion, direccion, telefono);
    }

}
