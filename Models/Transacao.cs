using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models
{
    public class Transacao
    {
        public enum TipoTransacao
        {
            Renda = 0,
            Despesas = 1
        }
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; } = string.Empty; // Ex: "Mercado Semanal"

        [Column(TypeName = "decimal(18,2)")] // Configura para aceitar dinheiro corretamente no banco
        public decimal Valor { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        public TipoTransacao Tipo { get; set; }

        public string Categoria { get; set; } = "Geral";
    }
}
