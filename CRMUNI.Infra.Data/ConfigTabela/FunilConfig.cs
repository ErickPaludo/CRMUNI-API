using CRMUNI.DOMAIN.Entidades.Funis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class FunilConfig : IEntityTypeConfiguration<Funil>
{
    public void Configure(EntityTypeBuilder<Funil> builder)
    {
        builder.ToTable("tb_funis");
        builder.HasKey(f => f.Id);
        
        builder.Property(u => u.Tipo)
            .HasComment("Setores: 0-Comercial | 1-Financeiro | 2-Suporte")
            .IsRequired();

        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}