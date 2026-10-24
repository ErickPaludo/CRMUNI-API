using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutenticacaoUsuarioNaoNulo5 : Migration
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
                name: "FuncionarioId",
                table: "tb_autenticacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_autenticacao_FuncionarioId",
                table: "tb_autenticacao",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_funcionarios_FuncionarioId",
                table: "tb_autenticacao",
                column: "FuncionarioId",
                principalTable: "tb_funcionarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_funcionarios_FuncionarioId",
                table: "tb_autenticacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.DropIndex(
                name: "IX_tb_autenticacao_FuncionarioId",
                table: "tb_autenticacao");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
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
