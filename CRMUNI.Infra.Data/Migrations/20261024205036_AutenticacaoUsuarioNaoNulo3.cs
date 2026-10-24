using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutenticacaoUsuarioNaoNulo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_contatos_ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.DropIndex(
                name: "IX_tb_autenticacao_ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.DropColumn(
                name: "ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "tb_autenticacao",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tb_autenticacao_RefreshToken",
                table: "tb_autenticacao",
                column: "RefreshToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_autenticacao_RefreshToken",
                table: "tb_autenticacao");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "tb_autenticacao",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ContatoId",
                table: "tb_autenticacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_autenticacao_ContatoId",
                table: "tb_autenticacao",
                column: "ContatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_contatos_ContatoId",
                table: "tb_autenticacao",
                column: "ContatoId",
                principalTable: "tb_contatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
