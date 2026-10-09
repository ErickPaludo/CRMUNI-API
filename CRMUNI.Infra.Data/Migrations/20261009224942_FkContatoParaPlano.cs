using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkContatoParaPlano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanoId",
                table: "tb_contatos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_contatos_PlanoId",
                table: "tb_contatos",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_contatos_tb_planos_PlanoId",
                table: "tb_contatos",
                column: "PlanoId",
                principalTable: "tb_planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_contatos_tb_planos_PlanoId",
                table: "tb_contatos");

            migrationBuilder.DropIndex(
                name: "IX_tb_contatos_PlanoId",
                table: "tb_contatos");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "tb_contatos");
        }
    }
}
