using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunasPadronizadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_setores_SetorId",
                table: "tb_funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_setores_id_setor",
                table: "tb_funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_setores_tb_empresas_id_empresa",
                table: "tb_setores");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuarios_tb_empresas_id_empresa",
                table: "tb_usuarios");

            migrationBuilder.DropIndex(
                name: "IX_tb_setores_id_empresa",
                table: "tb_setores");

            migrationBuilder.DropIndex(
                name: "IX_tb_funcionarios_id_setor",
                table: "tb_funcionarios");

            migrationBuilder.DropColumn(
                name: "id_empresa",
                table: "tb_setores");

            migrationBuilder.DropColumn(
                name: "id_setor",
                table: "tb_funcionarios");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "tb_usuarios",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "hash",
                table: "tb_usuarios",
                newName: "Hash");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "tb_usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dthr_criacao",
                table: "tb_usuarios",
                newName: "DthrCriacao");

            migrationBuilder.RenameColumn(
                name: "dthr_alteracao",
                table: "tb_usuarios",
                newName: "DthrAlteracao");

            migrationBuilder.RenameColumn(
                name: "segundo_nome",
                table: "tb_usuarios",
                newName: "SegundoNome");

            migrationBuilder.RenameColumn(
                name: "primeiro_nome",
                table: "tb_usuarios",
                newName: "PrimeiroNome");

            migrationBuilder.RenameColumn(
                name: "id_empresa",
                table: "tb_usuarios",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_usuarios_id_empresa",
                table: "tb_usuarios",
                newName: "IX_tb_usuarios_EmpresaId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "tb_setores",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "tb_setores",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "dthr_criacao",
                table: "tb_setores",
                newName: "DthrCriacao");

            migrationBuilder.RenameColumn(
                name: "dthr_alteracao",
                table: "tb_setores",
                newName: "DthrAlteracao");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "tb_funcionarios",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "hash",
                table: "tb_funcionarios",
                newName: "Hash");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "tb_funcionarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dthr_criacao",
                table: "tb_funcionarios",
                newName: "DthrCriacao");

            migrationBuilder.RenameColumn(
                name: "dthr_alteracao",
                table: "tb_funcionarios",
                newName: "DthrAlteracao");

            migrationBuilder.RenameColumn(
                name: "segundo_nome",
                table: "tb_funcionarios",
                newName: "SegundoNome");

            migrationBuilder.RenameColumn(
                name: "primeiro_nome",
                table: "tb_funcionarios",
                newName: "PrimeiroNome");

            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "tb_empresas",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "tb_empresas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "tb_empresas",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "tb_empresas",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "dthr_criacao",
                table: "tb_empresas",
                newName: "DthrCriacao");

            migrationBuilder.RenameColumn(
                name: "dthr_alteracao",
                table: "tb_empresas",
                newName: "DthrAlteracao");

            migrationBuilder.AlterColumn<Guid>(
                name: "SetorId",
                table: "tb_funcionarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_setores_SetorId",
                table: "tb_funcionarios",
                column: "SetorId",
                principalTable: "tb_setores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuarios_tb_empresas_EmpresaId",
                table: "tb_usuarios",
                column: "EmpresaId",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionarios_tb_setores_SetorId",
                table: "tb_funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuarios_tb_empresas_EmpresaId",
                table: "tb_usuarios");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "tb_usuarios",
                newName: "salt");

            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "tb_usuarios",
                newName: "hash");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DthrCriacao",
                table: "tb_usuarios",
                newName: "dthr_criacao");

            migrationBuilder.RenameColumn(
                name: "DthrAlteracao",
                table: "tb_usuarios",
                newName: "dthr_alteracao");

            migrationBuilder.RenameColumn(
                name: "SegundoNome",
                table: "tb_usuarios",
                newName: "segundo_nome");

            migrationBuilder.RenameColumn(
                name: "PrimeiroNome",
                table: "tb_usuarios",
                newName: "primeiro_nome");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "tb_usuarios",
                newName: "id_empresa");

            migrationBuilder.RenameIndex(
                name: "IX_tb_usuarios_EmpresaId",
                table: "tb_usuarios",
                newName: "IX_tb_usuarios_id_empresa");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_setores",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "tb_setores",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "DthrCriacao",
                table: "tb_setores",
                newName: "dthr_criacao");

            migrationBuilder.RenameColumn(
                name: "DthrAlteracao",
                table: "tb_setores",
                newName: "dthr_alteracao");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "tb_funcionarios",
                newName: "salt");

            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "tb_funcionarios",
                newName: "hash");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_funcionarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DthrCriacao",
                table: "tb_funcionarios",
                newName: "dthr_criacao");

            migrationBuilder.RenameColumn(
                name: "DthrAlteracao",
                table: "tb_funcionarios",
                newName: "dthr_alteracao");

            migrationBuilder.RenameColumn(
                name: "SegundoNome",
                table: "tb_funcionarios",
                newName: "segundo_nome");

            migrationBuilder.RenameColumn(
                name: "PrimeiroNome",
                table: "tb_funcionarios",
                newName: "primeiro_nome");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "tb_empresas",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_empresas",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_empresas",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "tb_empresas",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "DthrCriacao",
                table: "tb_empresas",
                newName: "dthr_criacao");

            migrationBuilder.RenameColumn(
                name: "DthrAlteracao",
                table: "tb_empresas",
                newName: "dthr_alteracao");

            migrationBuilder.AddColumn<Guid>(
                name: "id_empresa",
                table: "tb_setores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SetorId",
                table: "tb_funcionarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "id_setor",
                table: "tb_funcionarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_setores_id_empresa",
                table: "tb_setores",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_id_setor",
                table: "tb_funcionarios",
                column: "id_setor");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_setores_SetorId",
                table: "tb_funcionarios",
                column: "SetorId",
                principalTable: "tb_setores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionarios_tb_setores_id_setor",
                table: "tb_funcionarios",
                column: "id_setor",
                principalTable: "tb_setores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_setores_tb_empresas_id_empresa",
                table: "tb_setores",
                column: "id_empresa",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuarios_tb_empresas_id_empresa",
                table: "tb_usuarios",
                column: "id_empresa",
                principalTable: "tb_empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
