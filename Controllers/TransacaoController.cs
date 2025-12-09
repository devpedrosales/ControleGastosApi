using ControleGastos.DTOs;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service; 

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _service.ListarTodas();
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransacaoDto dto)
    {
        try
        {
            // O Controller só repassa a bola
            var transacaoCriada = await _service.Adicionar(dto);
            return CreatedAtAction(nameof(GetAll), new { id = transacaoCriada.Id }, transacaoCriada);
        }
        catch (Exception ex)
        {
            // Se a regra de negócio (data futura) falhar, retorna erro 400
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remover(id);
        return NoContent();
    }
}
