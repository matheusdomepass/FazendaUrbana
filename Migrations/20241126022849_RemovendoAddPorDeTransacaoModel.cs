using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FazendaUrbana.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoAddPorDeTransacaoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Add_Por",
                table: "Transacoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Add_Por",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
