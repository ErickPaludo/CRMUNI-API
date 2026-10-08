using CRMUNI.DOMAIN.Entidades.Etapas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class EtapaConfig : IEntityTypeConfiguration<Etapa>
{
    public void Configure(EntityTypeBuilder<Etapa> builder)
    {
        builder.ToTable("tb_etapas");
        builder.HasKey(x => x.Id);

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
        
        builder.Property(u => u.Tipo)
            .HasComment(
                "Etapas: 0-Inicial | 1-PrimeiroContato | 2-ApresentacaoPlanos | 3-AguardandoDecicao | 4-Conversao | 5-Concluido | 6-Feedback | 7-Perdido")
            .IsRequired();
        
        builder.OwnsOne(e => e.Ordem,
            ordem
                =>
            {
                ordem.Property(e => e.Valor)
                    .HasColumnName("Ordem")
                    .IsRequired()
                    .HasColumnType("decimal(18)");
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();

        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}