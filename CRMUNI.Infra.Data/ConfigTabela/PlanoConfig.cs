using CRMUNI.Domain.Entidades.Planos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class PlanoConfig : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.ToTable("tb_planos");
        builder.HasKey(p => p.Id);
        
        builder.OwnsOne(n => n.Nome,
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
                    .HasMaxLength(400);
            }
        );

        builder.Property(u => u.Situacao)
            .HasComment("Situacao: 0-Ativo | 1-Inativo")
            .IsRequired();
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}