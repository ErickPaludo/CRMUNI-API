using CRMUNI.Domain.Entidades.Autenticacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class AutenticacaoConfig : IEntityTypeConfiguration<Autenticacao>
{
    public void Configure(EntityTypeBuilder<Autenticacao> builder)
    {
        builder.ToTable("tb_autenticacao");
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.RefreshToken).IsUnique();

        builder.HasOne(a => a.Usuario)
               .WithMany()
               .IsRequired();
    }
}