using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_empresa",
                table: "tb_empresa");

            migrationBuilder.RenameTable(
                name: "tb_empresa",
                newName: "tb_empresas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_empresas",
                table: "tb_empresas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tb_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    primeiro_nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    segundo_nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dthr_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dthr_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_usuarios_tb_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "tb_empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_empresa_id",
                table: "tb_usuarios",
                column: "empresa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_empresas",
                table: "tb_empresas");

            migrationBuilder.RenameTable(
                name: "tb_empresas",
                newName: "tb_empresa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_empresa",
                table: "tb_empresa",
                column: "Id");
        }
    }
}
