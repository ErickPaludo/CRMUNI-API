using CRMUNI.Domain.Entidades.Setores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class SetoresConfig : IEntityTypeConfiguration<Setor>
{
    public void Configure(EntityTypeBuilder<Setor> builder)
    {
        builder.ToTable("tb_setores");
        builder.HasKey(e => e.Id);
        
        builder.OwnsOne(n => n.Nome,
            nome
                =>
            {
                nome.Property(e => e.Primeiro)
                    .HasColumnName("Nome")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );
        
        builder.Property(u => u.Tipo)
            .HasComment("Setores: 0-Comercial | 1-Financeiro | 2-Suporte")
            .IsRequired();

        
        builder.OwnsOne(e => e.DescricaoSetor,
            descricao
                =>
            {
                descricao.Property(e => e.Texto)
                    .HasColumnName("Descricao")
                    .IsRequired()
                    .HasMaxLength(100);
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}