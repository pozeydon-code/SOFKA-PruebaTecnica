namespace IdentityService.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;