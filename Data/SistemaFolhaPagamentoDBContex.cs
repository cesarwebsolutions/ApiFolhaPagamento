using ApiFolhaPagamento.Data.Map;
using ApiFolhaPagamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFolhaPagamento.Data
{
    public class SistemaFolhaPagamentoDBContex : DbContext
    {
        public SistemaFolhaPagamentoDBContex(DbContextOptions<SistemaFolhaPagamentoDBContex> options) : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
