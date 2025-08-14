using ErrorOr;

namespace IdentityService.Domain.DomainErrors;

public static partial class Errors
{
    public static class Cliente
    {
        public static Error NotFound => Error.NotFound("Cliente", "El cliente con el id proporcionado no ha sido encontrado.");
    }
}
