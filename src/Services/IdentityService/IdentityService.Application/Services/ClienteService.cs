using System;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Services;
public class ClienteService
{
  private readonly IClienteRepository _repository;

  public ClienteService(IClienteRepository repository)
   {
       _repository = repository;
   }

  public async Task CreateAsync(Cliente entity)
   {
       // Agregar validaciones aquí si se requiere
       await _repository.AddAsync(entity);
   }
}