using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FuncionariosSetoresTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuarios_tb_empresas_empresa_id",
                table: "tb_usuarios");

            migrationBuilder.RenameColumn(
                name: "empresa_id",
                table: "tb_usuarios",
                newName: "id_empresa");

            migrationBuilder.RenameIndex(
                name: "IX_tb_usuarios_empresa_id",
                table: "tb_usuarios",
                newName: "IX_tb_usuarios_id_empresa");

            migrationBuilder.CreateTable(
                name: "tb_setores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false, comment: "Setores: 0-Comercial | 1-Financeiro | 2-Suporte"),
                    id_empresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dthr_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dthr_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_setores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_setores_tb_empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "tb_empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_setor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    primeiro_nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    segundo_nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    dthr_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dthr_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_funcionarios_tb_setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "tb_setores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_funcionarios_tb_setores_id_setor",
                        column: x => x.id_setor,
                        principalTable: "tb_setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_id_setor",
                table: "tb_funcionarios",
                column: "id_setor");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_SetorId",
                table: "tb_funcionarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_setores_id_empresa",
                table: "tb_setores",
                column: "id_empresa");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuarios_tb_empresas_id_empresa",
                table: "tb_usuarios",
                column: "id_empresa",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuarios_tb_empresas_id_empresa",
                table: "tb_usuarios");

            migrationBuilder.DropTable(
                name: "tb_funcionarios");

            migrationBuilder.DropTable(
                name: "tb_setores");

            migrationBuilder.RenameColumn(
                name: "id_empresa",
                table: "tb_usuarios",
                newName: "empresa_id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_usuarios_id_empresa",
                table: "tb_usuarios",
                newName: "IX_tb_usuarios_empresa_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuarios_tb_empresas_empresa_id",
                table: "tb_usuarios",
                column: "empresa_id",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
