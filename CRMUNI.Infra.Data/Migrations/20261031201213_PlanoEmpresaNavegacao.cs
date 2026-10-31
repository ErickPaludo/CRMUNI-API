using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlanoEmpresaNavegacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planos_tb_empresas_EmpresaId",
                table: "tb_planos");

            // Planos cadastrados antes do mapeamento da navegação ficaram sem EmpresaId
            migrationBuilder.Sql(
                "UPDATE tb_contatos SET PlanoId = NULL WHERE PlanoId IN (SELECT Id FROM tb_planos WHERE EmpresaId IS NULL);");
            migrationBuilder.Sql("DELETE FROM tb_planos WHERE EmpresaId IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "tb_planos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planos_tb_empresas_EmpresaId",
                table: "tb_planos",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planos_tb_empresas_EmpresaId",
                table: "tb_planos");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "tb_planos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planos_tb_empresas_EmpresaId",
                table: "tb_planos",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id");
        }
    }
}
