using CRMUNI.Domain.Entidades.Funis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class FunilConfig : IEntityTypeConfiguration<Funil>
{
    public void Configure(EntityTypeBuilder<Funil> builder)
    {
        builder.ToTable("tb_funils");
        builder.HasKey(f => f.Id);
        
        builder.Property(u => u.Tipo)
            .HasComment("Funis: 0-Curioso | 1-Potencial Cliente | 2-Vendido")
            .IsRequired();
       
        builder.OwnsOne(e => e.Nome,
            nome
                =>
            {
                nome.Property(e => e.Primeiro)
                    .HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(25);
            }
        );
        
        builder.OwnsOne(e => e.Descricao,
            descricao
                =>
            {
                descricao.Property(e => e.Texto)
                    .HasColumnName("Descricao")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}