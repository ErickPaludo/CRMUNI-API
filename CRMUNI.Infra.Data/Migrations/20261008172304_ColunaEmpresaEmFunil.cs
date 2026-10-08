using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunaEmpresaEmFunil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_funis",
                table: "tb_funis");

            migrationBuilder.RenameTable(
                name: "tb_funis",
                newName: "tb_funils");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "tb_funils",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_funils",
                table: "tb_funils",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funils_EmpresaId",
                table: "tb_funils",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funils_tb_empresas_EmpresaId",
                table: "tb_funils",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funils_tb_empresas_EmpresaId",
                table: "tb_funils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_funils",
                table: "tb_funils");

            migrationBuilder.DropIndex(
                name: "IX_tb_funils_EmpresaId",
                table: "tb_funils");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "tb_funils");

            migrationBuilder.RenameTable(
                name: "tb_funils",
                newName: "tb_funis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_funis",
                table: "tb_funis",
                column: "Id");
        }
    }
}
