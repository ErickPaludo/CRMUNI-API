using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunaEmpresaFuncionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuarios_tb_empresas_EmpresaId",
                table: "tb_usuarios");

            migrationBuilder.DropIndex(
                name: "IX_tb_usuarios_EmpresaId",
                table: "tb_usuarios");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "tb_usuarios");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "tb_funcionarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_EmpresaId",
                table: "tb_funcionarios",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_empresas_EmpresaId",
                table: "tb_funcionarios",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_empresas_EmpresaId",
                table: "tb_funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_tb_funcionarios_EmpresaId",
                table: "tb_funcionarios");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "tb_funcionarios");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "tb_usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_EmpresaId",
                table: "tb_usuarios",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuarios_tb_empresas_EmpresaId",
                table: "tb_usuarios",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
