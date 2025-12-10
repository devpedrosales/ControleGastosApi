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
        // O Include diz: "Quando buscar a transação, traga junto os dados da Categoria dela"
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .ToListAsync();
    }

    public async Task<Transacao?> GetById(int id)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria) 
            .FirstOrDefaultAsync(t => t.Id == id);
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
            .Include(t => t.Categoria)
            .Where(t => t.Data.Month == mes && t.Data.Year == ano)
            .ToListAsync();
    }
}
