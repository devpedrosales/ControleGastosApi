using ControleGastosWeb.Models;
using ControleGastosWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ControleGastosWeb.Pages;

public class CriarModel : PageModel
{
    private readonly ITransacaoService _service;

    public CriarModel(ITransacaoService service)
    {
        _service = service;
    }

    // Guarda os dados que o usuário digitou
    [BindProperty]
    public TransacaoViewModel Transacao { get; set; } = new();

    // Guarda a lista de categorias para preencher o Dropdown (Select)
    public List<CategoriaViewModel> ListaCategorias { get; set; } = new();

    // MUDANÇA 1: Agora é Async para buscar dados na API
    public async Task OnGetAsync()
    {
        // Carrega as categorias assim que a página abre
        ListaCategorias = await _service.GetCategorias();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // MUDANÇA 2: Se o usuário errou algo, recarregamos a lista.
            // Se não fizermos isso, o dropdown volta vazio e dá erro.
            ListaCategorias = await _service.GetCategorias();
            return Page();
        }

        await _service.Adicionar(Transacao);
        return RedirectToPage("./Index");
    }
}
