using ControleGastos.DTOs;
using ControleGastos.Models;
using ControleGastos.Repositories;
using static ControleGastos.Models.Transacao;

namespace ControleGastos.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _repository;

    public TransacaoService(ITransacaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Transacao>> ListarTodas()
    {
        return await _repository.GetAll();
    }

    public async Task<Transacao> Adicionar(TransacaoDto dto)
    {
        // 1. Regra de Negócio: Validação (Exemplo)
        if (dto.Data > DateTime.Now)
        {
            throw new Exception("Não é permitido lançar transações futuras!");
        }

        // 2. Conversão DTO -> Entidade (Tiramos isso do Controller)
        var transacao = new Transacao
        {
            Titulo = dto.Titulo,
            Valor = dto.Valor,
            Data = dto.Data,
            Tipo = (TipoTransacao)dto.Tipo,
            Categoria = dto.Categoria
        };

        // 3. Chama o Repository para salvar
        await _repository.Add(transacao);

        return transacao;
    }

    public async Task Remover(int id)
    {
        await _repository.Delete(id);
    }
}
