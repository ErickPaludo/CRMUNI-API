using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaPlano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situacao: 0-Ativo | 1-Inativo"),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_planos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_planos_tb_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_planos_EmpresaId",
                table: "tb_planos",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_planos");
        }
    }
}
