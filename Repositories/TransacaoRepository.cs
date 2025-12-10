using ControleGastos.Context;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transacao>> GetAll()
    {
        return await _context.Transacoes.ToListAsync();
    }

    public async Task<Transacao?> GetById(int id)
    {
        return await _context.Transacoes.FindAsync(id);
    }

    public async Task Add(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao != null)
        {
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Transacao>> GetByPeriod(int mes, int ano)
    {
        return await _context.Transacoes
            .Where(t => t.Data.Month == mes && t.Data.Year == ano)
            .ToListAsync();
    }
}
