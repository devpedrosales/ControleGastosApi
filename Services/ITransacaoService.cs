using ControleGastos.DTOs;
using ControleGastos.Models;

namespace ControleGastos.Services;

public interface ITransacaoService
{
    Task<IEnumerable<Transacao>> ListarTodas();
    Task<Transacao> Adicionar(TransacaoDto dto); // O Service resolve a conversão
    Task Remover(int id);
}
