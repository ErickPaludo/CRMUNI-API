using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkDeEmpresaParaSetores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "tb_setores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_setores_EmpresaId",
                table: "tb_setores",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_setores_tb_empresas_EmpresaId",
                table: "tb_setores",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_setores_tb_empresas_EmpresaId",
                table: "tb_setores");

            migrationBuilder.DropIndex(
                name: "IX_tb_setores_EmpresaId",
                table: "tb_setores");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "tb_setores");
        }
    }
}
