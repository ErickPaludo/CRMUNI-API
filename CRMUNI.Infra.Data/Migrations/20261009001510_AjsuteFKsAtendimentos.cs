using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjsuteFKsAtendimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_atendimentos_AtendimentoId",
                table: "tb_funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_tb_funcionarios_AtendimentoId",
                table: "tb_funcionarios");

            migrationBuilder.DropColumn(
                name: "AtendimentoId",
                table: "tb_funcionarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtendimentoId",
                table: "tb_funcionarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_AtendimentoId",
                table: "tb_funcionarios",
                column: "AtendimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_atendimentos_AtendimentoId",
                table: "tb_funcionarios",
                column: "AtendimentoId",
                principalTable: "tb_atendimentos",
                principalColumn: "Id");
        }
    }
}
