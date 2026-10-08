using CRMUNI.DOMAIN.Entidades.Empresas;
using CRMUNI.DOMAIN.Entidades.Funcionarios;
using CRMUNI.DOMAIN.Entidades.Funis;
using CRMUNI.DOMAIN.Entidades.Setores;
using CRMUNI.DOMAIN.Entidades.Usuarios;
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}