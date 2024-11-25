using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;

namespace FazendaUrbana.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendasModel>().
                HasOne(v => v.Produto).WithMany(p => p.Vendas).HasForeignKey( v => v.ProdutoId );
            modelBuilder.Entity<VendasModel>().
                HasOne(v => v.Transacao).WithMany().HasForeignKey(v => v.TransacaoId).HasConstraintName("FK_Vendas_Transacao").OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<VendasModel> Vendas { get; set; }
        public DbSet<TransacaoModel> Transacoes { get; set; }
        public DbSet<AlterarSenhaModel> AlterarSenha { get; set; }
    }
}
