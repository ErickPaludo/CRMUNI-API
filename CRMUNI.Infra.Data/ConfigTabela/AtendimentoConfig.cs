using CRMUNI.DOMAIN.Entidades.Atendimentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class AtendimentoConfig : IEntityTypeConfiguration<Atendimento>
{
    public void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        builder.ToTable("tb_atendimentos");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Situacao)
            .HasComment(
                "Situacao: 0-Aguardando | 1-EmAndamento | 2-Finalizado")
            .IsRequired();

             
        builder.OwnsOne(e => e.Agendamento,
            agendamento
                =>
            {
                agendamento.Property(e => e.Data)
                    .HasColumnName("Agendamento");
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();

        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}