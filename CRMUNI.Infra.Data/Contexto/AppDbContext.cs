using CRMUNI.DOMAIN.Entidades.Empresas;
using Microsoft.EntityFrameworkCore;

namespace CRMUNI.Infra.Data.Contexto;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Empresa> Empresas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}