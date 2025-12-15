using ControleGastosWeb.Models;

namespace ControleGastosWeb.Services
{
    public interface ITransacaoService
    {
        Task<List<TransacaoViewModel>> GetAll();
        Task Adicionar(TransacaoViewModel transacao);
        Task Deletar(int id);
        Task<ResumoViewModel> GetResumo();
        Task<List<CategoriaViewModel>> GetCategorias();
    }
}
