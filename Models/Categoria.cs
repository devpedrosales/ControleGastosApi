using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty; // Ex: Alimentação

    public string Icone { get; set; } = ""; // Ex: 🍔

    // Relacionamento inverso (Uma categoria tem VÁRIAS transações)
    [JsonIgnore]
    public ICollection<Transacao>? Transacoes { get; set; }
}
