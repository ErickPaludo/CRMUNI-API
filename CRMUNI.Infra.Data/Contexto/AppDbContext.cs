using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Contatos;
using CRMUNI.Domain.Entidades.Empresas;
using CRMUNI.Domain.Entidades.Funcionarios;
using CRMUNI.Domain.Entidades.Funis;
using CRMUNI.Domain.Entidades.Setores;
using CRMUNI.Domain.Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Contexto;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Setor> Setores { get; set; }
    public DbSet<Funil> Funis { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }
    public DbSet<Contato> Contatos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}