using System;
using System.Threading.Tasks;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Services;
public class CuentaService
{
  private readonly ICuentaRepository _repository;

  public CuentaService(ICuentaRepository repository)
   {
       _repository = repository;
   }

  public async Task CreateAsync(Cuenta entity)
   {
       // Agregar validaciones aquí si se requiere
       await _repository.AddAsync(entity);
   }
}