using ControleGastos.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ControleGastos.Models.Transacao;

namespace ControleGastos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("resumo-mensal")]
        public async Task<IActionResult> ObterResumoMensal()
        {
            // Pega o mês e ano atuais
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            // 1. Busca no banco apenas as transações deste mês
            var transacoes = await _context.Transacoes
                .Where(t => t.Data.Month == mesAtual && t.Data.Year == anoAtual)
                .ToListAsync();

            // 2. Calcula Totais
            var totalReceitas = transacoes
                .Where(t => t.Tipo == TipoTransacao.Renda)
                .Sum(t => t.Valor);

            var totalDespesas = transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesas)
                .Sum(t => t.Valor);

            var saldo = totalReceitas - totalDespesas;

            // 3. Agrupamento por Categoria (A "Mágica")
            // Ex: Retorna quanto gastou só com "Lazer" e a porcentagem disso no total
            var gastosPorCategoria = transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesas)
                .GroupBy(t => t.Categoria)
                .Select(grupo => new
                {
                    Categoria = grupo.Key,
                    TotalGasto = grupo.Sum(t => t.Valor),
                    Porcentagem = Math.Round((grupo.Sum(t => t.Valor) / (totalDespesas == 0 ? 1 : totalDespesas)) * 100, 1)
                })
                .OrderByDescending(x => x.TotalGasto);

            // 4. Monta o objeto final para enviar ao Front/Mobile
            var dashboard = new
            {
                Periodo = $"{mesAtual}/{anoAtual}",
                SaldoAtual = saldo,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                DetalhamentoPorCategoria = gastosPorCategoria
            };

            return Ok(dashboard);
        }

    }
}
