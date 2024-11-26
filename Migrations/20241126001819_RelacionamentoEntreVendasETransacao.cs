using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FazendaUrbana.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoEntreVendasETransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Transacoes_TransacaoModelId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_TransacaoModelId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "TransacaoModelId",
                table: "Vendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransacaoModelId",
                table: "Vendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TransacaoModelId",
                table: "Vendas",
                column: "TransacaoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Transacoes_TransacaoModelId",
                table: "Vendas",
                column: "TransacaoModelId",
                principalTable: "Transacoes",
                principalColumn: "Id");
        }
    }
}
