using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunaEmpresaFuncionarios3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
