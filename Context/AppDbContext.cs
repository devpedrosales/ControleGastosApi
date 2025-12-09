using Microsoft.EntityFrameworkCore;
using ControleGastos.Models;

namespace ControleGastos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Representa a tabela "Transacoes" no banco de dados
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
