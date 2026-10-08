using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkEntreFunilsEtapas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FunilId",
                table: "tb_etapas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_etapas_FunilId",
                table: "tb_etapas",
                column: "FunilId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas",
                column: "FunilId",
                principalTable: "tb_funils",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas");

            migrationBuilder.DropIndex(
                name: "IX_tb_etapas_FunilId",
                table: "tb_etapas");

            migrationBuilder.DropColumn(
                name: "FunilId",
                table: "tb_etapas");
        }
    }
}
