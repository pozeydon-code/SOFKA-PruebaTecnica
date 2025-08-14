using ErrorOr;

namespace AccountService.Domain.DomainErrors;

public static partial class Errors
{
    public static class Cuenta
    {
        public static Error NotFound => Error.NotFound("Cuenta", "La Cuenta con el id proporcionado no ha sido encontrado.");
        public static Error InvalidSaldo => Error.Validation("Cuenta.Saldo", "El saldo de la cuenta debe ser mayor o igual a cero.");
    }

    public static class Movimiento
    {
        public static Error NotFound => Error.NotFound("Movimiento", "El Movimiento con el id proporcionado no ha sido encontrado.");
    }

    public static class Cliente
    {
        public static Error NotFound => Error.NotFound("Cliente.NotFound", "El cliente no existe.");
        public static Error Inactive => Error.Validation("Cliente.Inactive", "El cliente está inactivo.");
    }
}
