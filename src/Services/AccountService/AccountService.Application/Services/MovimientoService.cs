using System;
using System.Threading.Tasks;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Services;
public class MovimientoService
{
  private readonly IMovimientoRepository _repository;

  public MovimientoService(IMovimientoRepository repository)
   {
       _repository = repository;
   }

  public async Task CreateAsync(Movimiento entity)
   {
       // Agregar validaciones aquí si se requiere
       await _repository.AddAsync(entity);
   }
}