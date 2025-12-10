using ControleGastos.Context;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ControleGastos.Models.Transacao;

namespace ControleGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ITransacaoService _service;

        public DashboardController(ITransacaoService service)
        {
            _service = service;
        }

        [HttpGet("resumo-mensal")]
        public async Task<IActionResult> GetResumo(int mes, int ano)
        {
            var resumo = await _service.ObterResumoMensal(mes, ano);
            return Ok(resumo);
        }
    }

}
