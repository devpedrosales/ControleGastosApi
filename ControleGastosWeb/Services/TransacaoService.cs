using ControleGastosWeb.Models;
using System.Text.Json;

namespace ControleGastosWeb.Services;

public class TransacaoService : ITransacaoService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7219/api/Transacoes";

    public TransacaoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TransacaoViewModel>> GetAll()
    {
        // Faz o GET na API
        var response = await _httpClient.GetAsync(_baseUrl);

        if (!response.IsSuccessStatusCode)
            return new List<TransacaoViewModel>();

        var json = await response.Content.ReadAsStringAsync();

        // Converte o JSON da API para a nossa Classe C#
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<List<TransacaoViewModel>>(json, options) ?? new List<TransacaoViewModel>();
    }

    public async Task Adicionar(TransacaoViewModel transacao)
    {
        // Envia como JSON para a API
        await _httpClient.PostAsJsonAsync(_baseUrl, transacao);
    }
    public async Task Deletar(int id)
    {
        // Chama DELETE https://localhost:xxxx/api/Transacoes/5
        await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
    }

    public async Task<ResumoViewModel> GetResumo()
    {
        var dataAtual = DateTime.Now;
       
        string urlApi = $"https://localhost:7219/api/Dashboard/resumo-mensal?mes={dataAtual.Month}&ano={dataAtual.Year}";

        var response = await _httpClient.GetAsync(urlApi);

        if (!response.IsSuccessStatusCode)
            return new ResumoViewModel();

        var json = await response.Content.ReadAsStringAsync();

        // Deserialização simples (sem options)
        return JsonSerializer.Deserialize<ResumoViewModel>(json) ?? new ResumoViewModel();
    }
    public async Task<List<CategoriaViewModel>> GetCategorias()
    {
        // Ajuste a porta se necessário (7219)
        var response = await _httpClient.GetAsync("https://localhost:7219/api/Categorias");

        if (!response.IsSuccessStatusCode) return new List<CategoriaViewModel>();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<List<CategoriaViewModel>>(json, options) ?? new List<CategoriaViewModel>();
    }

}

