using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaAtendimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtendimentoId",
                table: "tb_funcionarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tb_atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    EtapaId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situacao: 0-Aguardando | 1-EmAndamento | 2-Finalizado"),
                    Agendamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_atendimentos_tb_etapas_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "tb_etapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_atendimentos_tb_funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "tb_funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_AtendimentoId",
                table: "tb_funcionarios",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_EtapaId",
                table: "tb_atendimentos",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_FuncionarioId",
                table: "tb_atendimentos",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_atendimentos_AtendimentoId",
                table: "tb_funcionarios",
                column: "AtendimentoId",
                principalTable: "tb_atendimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_atendimentos_AtendimentoId",
                table: "tb_funcionarios");

            migrationBuilder.DropTable(
                name: "tb_atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_tb_funcionarios_AtendimentoId",
                table: "tb_funcionarios");

            migrationBuilder.DropColumn(
                name: "AtendimentoId",
                table: "tb_funcionarios");
        }
    }
}
