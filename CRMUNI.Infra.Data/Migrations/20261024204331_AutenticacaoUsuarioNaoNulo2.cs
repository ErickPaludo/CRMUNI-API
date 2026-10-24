using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutenticacaoUsuarioNaoNulo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
