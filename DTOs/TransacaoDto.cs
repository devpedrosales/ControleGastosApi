using static ControleGastos.Models.Transacao;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs
{
    public class TransacaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty; // Ex: "Mercado Semanal"
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public TipoTransacao Tipo { get; set; }
        public string Categoria { get; set; } = "Geral";
    }
}
