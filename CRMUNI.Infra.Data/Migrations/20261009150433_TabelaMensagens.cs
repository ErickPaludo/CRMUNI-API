using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaMensagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_mensagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
                    ContatoId = table.Column<int>(type: "int", nullable: true),
                    Conteudo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AtendimentoId = table.Column<int>(type: "int", nullable: true),
                    FuncionarioId1 = table.Column<int>(type: "int", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mensagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_mensagens_tb_atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "tb_atendimentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_mensagens_tb_contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "tb_contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "tb_funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_mensagens_tb_funcionarios_FuncionarioId1",
                        column: x => x.FuncionarioId1,
                        principalTable: "tb_funcionarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_mensagens_AtendimentoId",
                table: "tb_mensagens",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_mensagens_ContatoId",
                table: "tb_mensagens",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_mensagens_FuncionarioId",
                table: "tb_mensagens",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_mensagens_FuncionarioId1",
                table: "tb_mensagens",
                column: "FuncionarioId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_mensagens");
        }
    }
}
