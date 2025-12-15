using ControleGastosWeb.Models;
using ControleGastosWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ControleGastosWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ITransacaoService _service;

    public IndexModel(ITransacaoService service)
    {
        _service = service;
    }

    // Lista que a tela vai ler
    public List<TransacaoViewModel> Transacoes { get; set; } = new();

    public ResumoViewModel Resumo { get; set; } = new();
    public async Task OnGetAsync()
    {
        // Quando a página carregar, busca na API
        Transacoes = await _service.GetAll();

        Resumo = await _service.GetResumo();
    }

    public async Task<IActionResult> OnPostDeletarAsync(int id)
    {
        await _service.Deletar(id);

        // Recarrega a página para atualizar a tabela
        return RedirectToPage();
    }
}
