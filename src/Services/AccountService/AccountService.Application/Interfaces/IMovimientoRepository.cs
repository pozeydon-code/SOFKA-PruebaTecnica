using AccountService.Application.Dtos;

namespace AccountService.Application.Interfaces;

public interface IMovimientoRepository
{
    Task<List<Movimiento>> GetAllAsync();
    Task<Movimiento?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Movimiento entity);
    void UpdateAsync(Movimiento entity);
    Task DeleteAsync(int id);
    Task<List<EstadoCuenta_MovimientoDto>> GetMovimientosByRangeAsync(DateTime fechaInicio, DateTime fechaFin, int numeroCuenta);
    Task<decimal> GetSumaHastaAsync(DateTime fechaCorte, int numeroCuenta);
    Task<decimal?> GetLastSaldoPosteriorBeforeAsync(DateTime fechaCorte, int numeroCuenta);
}
