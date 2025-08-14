using AccountService.Application.Dtos;

namespace AccountService.Application.Interfaces;

public interface ICuentaRepository
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int id);
    Task<List<CuentaSimple>> GetCuentasByNumeroCuentaAsync(int id);
    Task<List<Cuenta>> GetByClienteIdAsync(int clienteId);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Cuenta entity);
    void UpdateAsync(Cuenta entity);
    Task DeleteAsync(int id);
}
