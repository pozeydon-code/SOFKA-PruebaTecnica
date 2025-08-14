namespace IdentityService.Application.Interfaces;
public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente?> GetByIdentificacionAsync(string identificacion);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Cliente entity);
    void UpdateAsync(Cliente entity);
    Task DeleteAsync(int id);
}
