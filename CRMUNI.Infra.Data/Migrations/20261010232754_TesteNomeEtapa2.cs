using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMUNI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TesteNomeEtapa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_funils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false, comment: "Funis: 0-Curioso | 1-Potencial Cliente | 2-Vendido"),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_funils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_funils_tb_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_empresas",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "tb_setores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false, comment: "Setores: 0-Comercial | 1-Financeiro | 2-Suporte"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_setores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_setores_tb_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_usuarios_tb_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_etapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false, comment: "Etapas: 0-Inicial | 1-PrimeiroContato | 2-ApresentacaoPlanos | 3-AguardandoDecicao | 4-Conversao | 5-Concluido | 6-Feedback | 7-Perdido"),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Ordem = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    FunilId = table.Column<int>(type: "int", nullable: true),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_etapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_etapas_tb_funils_FunilId",
                        column: x => x.FunilId,
                        principalTable: "tb_funils",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_tb_contatos_tb_planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "tb_planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DthrCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_funcionarios_tb_setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "tb_setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_autenticacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationRefresh = table.Column<long>(type: "bigint", nullable: false),
                    Revoke = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContatoId = table.Column<int>(type: "int", nullable: true),
                    DthrAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_autenticacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_autenticacao_tb_contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "tb_contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContatoId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_tb_atendimentos_tb_contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "tb_contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "tb_mensagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContatoId = table.Column<int>(type: "int", nullable: true),
                    Conteudo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AtendimentoId = table.Column<int>(type: "int", nullable: true),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_ContatoId",
                table: "tb_atendimentos",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_EtapaId",
                table: "tb_atendimentos",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_atendimentos_FuncionarioId",
                table: "tb_atendimentos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_autenticacao_ContatoId",
                table: "tb_autenticacao",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_autenticacao_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_contatos_EmpresaId",
                table: "tb_contatos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_contatos_PlanoId",
                table: "tb_contatos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_etapas_FunilId",
                table: "tb_etapas",
                column: "FunilId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionarios_SetorId",
                table: "tb_funcionarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funils_EmpresaId",
                table: "tb_funils",
                column: "EmpresaId");

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
                name: "IX_tb_planos_EmpresaId",
                table: "tb_planos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_setores_EmpresaId",
                table: "tb_setores",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_EmpresaId",
                table: "tb_usuarios",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_autenticacao");

            migrationBuilder.DropTable(
                name: "tb_mensagens");

            migrationBuilder.DropTable(
                name: "tb_usuarios");

            migrationBuilder.DropTable(
                name: "tb_atendimentos");

            migrationBuilder.DropTable(
                name: "tb_contatos");

            migrationBuilder.DropTable(
                name: "tb_etapas");

            migrationBuilder.DropTable(
                name: "tb_funcionarios");

            migrationBuilder.DropTable(
                name: "tb_planos");

            migrationBuilder.DropTable(
                name: "tb_funils");

            migrationBuilder.DropTable(
                name: "tb_setores");

            migrationBuilder.DropTable(
                name: "tb_empresas");
        }
    }
}
