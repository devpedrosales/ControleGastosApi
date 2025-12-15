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
        // 1. Regra de Negócio: Validação 
        if (dto.Data > DateTime.Now)
        {
            throw new Exception("Não é permitido lançar transações futuras!");
        }
        if (dto.Valor <= 0)
        {
            throw new Exception("Não é permitido que o valor de entrada seja menor ou igual a 0!");
        }

        var transacao = new Transacao
        {
            Titulo = dto.Titulo,
            Valor = dto.Valor,
            Data = dto.Data,
            Tipo = (TipoTransacao)dto.Tipo,

            // Agora mapeamos o ID
            CategoriaId = dto.CategoriaId
        };

        await _repository.Add(transacao);
        return transacao;
    }

    public async Task Remover(int id)
    {
        if (id <= 0)
        {
            throw new Exception("USUÁRIO NÃO ENCONTRADO! : NÃO É PERMITIDO ID 0 OU MENOR QUE 0!");
        }

        await _repository.Delete(id);
    }
    public async Task<object> ObterResumoMensal(int mes, int ano)
    {
        var transacoes = await _repository.GetByPeriod(mes, ano);

        var totalReceitas = transacoes.Where(t => t.Tipo == TipoTransacao.Renda).Sum(t => t.Valor);
        var totalDespesas = transacoes.Where(t => t.Tipo == TipoTransacao.Despesas).Sum(t => t.Valor);

        var gastosPorCategoria = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesas)
            .GroupBy(t => t.Categoria?.Nome ?? "Sem Categoria")
            .Select(g => new {
                Categoria = g.Key,
                Total = g.Sum(t => t.Valor)
            });

        return new
        {
            Saldo = totalReceitas - totalDespesas,
            Receitas = totalReceitas,
            Despesas = totalDespesas,
            Categorias = gastosPorCategoria
        };
    }
}
