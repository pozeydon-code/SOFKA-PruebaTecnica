using IdentityService.Domain.Entities;
using IdentityService.Application.Interfaces;
using IdentityService.Infrastructure.Persistence;

namespace IdentityService.Infrastructure.Repositories;
public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync() => await _context.Set<Cliente>().ToListAsync();
    public async Task<Cliente?> GetByIdAsync(int id) => await _context.Set<Cliente>().SingleOrDefaultAsync(e => e.ClienteId == id);
    public async Task<Cliente?> GetByIdentificacionAsync(string identificacion) => await _context.Set<Cliente>().SingleOrDefaultAsync(e => e.Identificacion == identificacion);
    public async Task<bool> ExistsAsync(int id) => await _context.Set<Cliente>().AnyAsync(e => e.ClienteId == id);
    public async Task AddAsync(Cliente entity) => await _context.Set<Cliente>().AddAsync(entity);
    public void UpdateAsync(Cliente entity) => _context.Set<Cliente>().Update(entity);
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            _context.Set<Cliente>().Remove(entity);
    }
}
