namespace ControleGastosWeb.Models;

public class TransacaoViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public int Tipo { get; set; } // 0 = Receita, 1 = Despesa

    public int CategoriaId { get; set; }

    // Objeto aninhado para pegar o nome da categoria
    public CategoriaViewModel? Categoria { get; set; }
}