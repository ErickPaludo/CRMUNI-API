using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EtapaFunilNavegacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas");

            migrationBuilder.AlterColumn<int>(
                name: "FunilId",
                table: "tb_etapas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas",
                column: "FunilId",
                principalTable: "tb_funils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas");

            migrationBuilder.AlterColumn<int>(
                name: "FunilId",
                table: "tb_etapas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_etapas_tb_funils_FunilId",
                table: "tb_etapas",
                column: "FunilId",
                principalTable: "tb_funils",
                principalColumn: "Id");
        }
    }
}
