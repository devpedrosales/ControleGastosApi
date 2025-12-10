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
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao Tipo { get; set; }

        // --- MUDANÇA AQUI ---
        // Removemos: public string Categoria { get; set; }

        // Adicionamos a Chave Estrangeira:
        public int CategoriaId { get; set; }

        // Adicionamos o Objeto de Navegação (Para o Entity Framework popular)
        public Categoria? Categoria { get; set; }
    }
}
