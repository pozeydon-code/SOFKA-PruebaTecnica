namespace AccountService.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
