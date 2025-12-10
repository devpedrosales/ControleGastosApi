using ControleGastos.Models;

namespace ControleGastos.Repositories
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> GetAll();
        Task<Transacao?> GetById(int id);
        Task Add(Transacao transacao);
        Task Delete(int id);
        Task<IEnumerable<Transacao>> GetByPeriod(int mes, int ano);
    }
}
