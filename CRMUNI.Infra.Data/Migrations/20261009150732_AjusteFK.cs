using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId",
                table: "tb_mensagens");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId1",
                table: "tb_mensagens");

            migrationBuilder.DropIndex(
                name: "IX_tb_mensagens_FuncionarioId1",
                table: "tb_mensagens");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "tb_mensagens");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId",
                table: "tb_mensagens",
                column: "FuncionarioId",
                principalTable: "tb_funcionarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId",
                table: "tb_mensagens");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId1",
                table: "tb_mensagens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_mensagens_FuncionarioId1",
                table: "tb_mensagens",
                column: "FuncionarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId",
                table: "tb_mensagens",
                column: "FuncionarioId",
                principalTable: "tb_funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId1",
                table: "tb_mensagens",
                column: "FuncionarioId1",
                principalTable: "tb_funcionarios",
                principalColumn: "Id");
        }
    }
}
