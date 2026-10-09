using CRMUNI.DOMAIN.Entidades.Mensagens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class MensagensConfig : IEntityTypeConfiguration<Mensagem>
{
    public void Configure(EntityTypeBuilder<Mensagem> builder)
    {
        builder.ToTable("tb_mensagens");
        builder.HasKey(p => p.Id);
        
        builder.HasOne(a => a.Contato)
            .WithMany()
            .HasForeignKey("ContatoId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.OwnsOne(e => e.Conteudo,
            conteudo
                =>
            {
                conteudo.Property(e => e.Texto)
                    .HasColumnName("Conteudo")
                    .IsRequired()
                    .HasMaxLength(1000);
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}