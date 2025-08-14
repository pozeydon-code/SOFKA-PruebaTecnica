
using System.Net;
using System.Net.Http.Json;
using AccountService.Application.Dtos;
using AccountService.Application.Interfaces;

namespace AccountService.Infrastructure.Gateways;

public class ClienteGateway : IClienteGateway
{
    private readonly HttpClient _http;
    public ClienteGateway(HttpClient http) => _http = http;

    public async Task<ClienteMiniDto?> GetByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"/Clientes/{id}");
        if (resp.StatusCode == HttpStatusCode.NotFound) return null;

        resp.EnsureSuccessStatusCode();
        var dto = await resp.Content.ReadFromJsonAsync<ClienteMiniDto>();
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
        => (await GetByIdAsync(id)) is not null;
}
