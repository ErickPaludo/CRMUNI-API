using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FKContatoParaAtendimento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContatoId",
                table: "tb_atendimentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_ContatoId",
                table: "tb_atendimentos",
                column: "ContatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_atendimentos_tb_contatos_ContatoId",
                table: "tb_atendimentos",
                column: "ContatoId",
                principalTable: "tb_contatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_atendimentos_tb_contatos_ContatoId",
                table: "tb_atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_tb_atendimentos_ContatoId",
                table: "tb_atendimentos");

            migrationBuilder.DropColumn(
                name: "ContatoId",
                table: "tb_atendimentos");
        }
    }
}
