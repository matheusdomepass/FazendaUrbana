using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FazendaUrbana.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTransacaoEmVendasModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransacaoId",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TransacaoId",
                table: "Vendas",
                column: "TransacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Transacoes_TransacaoId",
                table: "Vendas",
                column: "TransacaoId",
                principalTable: "Transacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Transacoes_TransacaoId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_TransacaoId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "TransacaoId",
                table: "Vendas");
        }
    }
}
