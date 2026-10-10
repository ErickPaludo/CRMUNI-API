using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaAutenticacao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "tb_autenticacao",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_contatos_ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.DropIndex(
                name: "IX_tb_autenticacao_ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.DropColumn(
                name: "ContatoId",
                table: "tb_autenticacao");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "tb_autenticacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
