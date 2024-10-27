using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;

namespace FazendaUrbana.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<VendasModel> Vendas { get; set; }
        public DbSet<TransacaoModel> Transacoes { get; set; }
    }
}
