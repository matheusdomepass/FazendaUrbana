using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FazendaUrbana.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCarrinhoEmVendasModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendasModelId",
                table: "Vendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendasModelId",
                table: "Vendas",
                column: "VendasModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Vendas_VendasModelId",
                table: "Vendas",
                column: "VendasModelId",
                principalTable: "Vendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Vendas_VendasModelId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_VendasModelId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "VendasModelId",
                table: "Vendas");
        }
    }
}
