using CRMUNI.DOMAIN.Entidades.Contatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class ContatoConfig : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("tb_contatos");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(n => n.Cpf,
            cpf =>
            {
                cpf.Property(n => n.Codigo)
                    .HasColumnName("Cpf")
                    .HasMaxLength(11);
            }
        );

        builder.HasOne(c => c.Plano)
            .WithMany()
            .HasForeignKey("PlanoId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(n => n.Nome,
            nome =>
            {
                nome.Property(n => n.Primeiro)
                    .HasColumnName("PrimeiroNome")
                    .IsRequired()
                    .HasMaxLength(50);
                ;
            }
        );

        builder.OwnsOne(n => n.Nome,
            nome =>
            {
                nome.Property(n => n.Segundo)
                    .HasColumnName("SegundoNome")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );

        builder.OwnsOne(e => e.Email,
            endereco
                =>
            {
                endereco.Property(e => e.Endereco)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(256);
                ;
            }
        );

        builder.OwnsOne(n => n.Celular,
            nome =>
            {
                nome.Property(n => n.Numero)
                    .HasColumnName("Telefone")
                    .IsRequired()
                    .HasMaxLength(12 + 1);
            }
        );

        builder.Property(u => u.Situacao)
            .HasComment("Situacao: 0-Ativo | 1-Inativo | 2-Bloqueado")
            .IsRequired();

        builder.Property(u => u.Origem)
            .HasComment("Origem: 0-Instagram | 1-Facebook | 2-Twitter | 3-LinkedIn | 4-Google | 5-Youtube | 6-Outros")
            .IsRequired();

        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();

        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}