using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.ViewModel;
using OpenAdm.Cep.Domain.Exceptions;
using System.Text.Json;

namespace OpenAdm.Cep.Application.Services;

public sealed class CepService : ICepService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
    public CepService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<EnderecoViewModel> ConsultarCepAsync(string cep)
    {
        var client = _httpClientFactory.CreateClient("VIA_CEP");
        var response = await client.GetAsync($"{cep}/json");
        if (!response.IsSuccessStatusCode)
        {
            throw new CepException("Não foi possível consultar o cep!");
        }

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<EnderecoViewModel>(content, _options) ?? new();
    }
}
