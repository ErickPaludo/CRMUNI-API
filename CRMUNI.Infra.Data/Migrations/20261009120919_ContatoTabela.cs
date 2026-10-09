using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContatoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situacao: 0-Ativo | 1-Inativo | 2-Bloqueado"),
                    Origem = table.Column<int>(type: "int", nullable: false, comment: "Origem: 0-Instagram | 1-Facebook | 2-Twitter | 3-LinkedIn | 4-Google | 5-Youtube | 6-Outros"),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_contatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_contatos_tb_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_contatos_EmpresaId",
                table: "tb_contatos",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_contatos");
        }
    }
}
