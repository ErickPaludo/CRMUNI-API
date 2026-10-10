using CRMUNI.Domain.Entidades.Autenticacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class AutenticacaoConfig : IEntityTypeConfiguration<Autenticacao>
{
    public void Configure(EntityTypeBuilder<Autenticacao> builder)
    {
        builder.ToTable("tb_autenticacao");

        builder.HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey("UsuarioId")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.Contato)
            .WithMany()
            .HasForeignKey("ContatoId")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasKey(a => a.Id);
    }
}