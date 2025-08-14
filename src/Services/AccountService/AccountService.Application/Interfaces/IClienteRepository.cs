using AccountService.Application.Dtos;

namespace AccountService.Application.Interfaces;
public interface IClienteGateway
{
    Task<ClienteMiniDto?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
}
