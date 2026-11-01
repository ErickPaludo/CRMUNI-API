using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContatoEmpresaNavegacaoEmailOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_contatos_tb_empresas_EmpresaId",
                table: "tb_contatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "tb_contatos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tb_contatos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_contatos_tb_empresas_EmpresaId",
                table: "tb_contatos",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_contatos_tb_empresas_EmpresaId",
                table: "tb_contatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "tb_contatos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tb_contatos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_contatos_tb_empresas_EmpresaId",
                table: "tb_contatos",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id");
        }
    }
}
