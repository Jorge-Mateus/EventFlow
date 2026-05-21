using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoClienteProposta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Propostas_ClienteId",
                table: "Propostas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propostas_Clientes_ClienteId",
                table: "Propostas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propostas_Clientes_ClienteId",
                table: "Propostas");

            migrationBuilder.DropIndex(
                name: "IX_Propostas_ClienteId",
                table: "Propostas");
        }
    }
}
