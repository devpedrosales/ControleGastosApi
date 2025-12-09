using ControleGastos.Context;
using ControleGastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleGastos.DTOs;
using static ControleGastos.Models.Transacao;

namespace ControleGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacaoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Transacoes (Mostra todas as contas salvas)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var listaDto = await _context.Transacoes
        .Select(t => new TransacaoDto  
        {
            Id = t.Id, 
            Titulo = t.Titulo,
            Valor = t.Valor,
            Data = t.Data,
            Categoria = t.Categoria
            // Não expomos dados internos ou sensíveis aqui
        })
        .ToListAsync();

        return Ok(listaDto);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransacaoDto dto)
    {
        if (dto == null)
            return BadRequest("Dados inválidos: JSON vazio.");

        var transacaoReal = new Transacao
        {
            Titulo = dto.Titulo,
            Valor = dto.Valor,
            Data = dto.Data,
            Tipo = (TipoTransacao)dto.Tipo, 
            Categoria = dto.Categoria
            
        };

        // 1. Adiciona na memória
        _context.Transacoes.Add(transacaoReal);

        // 2. Comita (salva) no Banco SQL Server de verdade
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = transacaoReal.Id }, transacaoReal);
    }

    // DELETE: api/Transacoes/5 (Para apagar se errar)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao == null) return NotFound();

        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();



        return NoContent();
    }
}
