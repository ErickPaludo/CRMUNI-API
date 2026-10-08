using CRMUNI.DOMAIN.Entidades.Funcionarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class FuncionarioConfig : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("tb_funcionarios");
        builder.HasKey(f => f.Id);
        
        builder.HasOne(s => s.Setor)
            .WithMany()
            .HasForeignKey("id_setor")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.OwnsOne(n => n.Nome,
            nome =>
            {
                nome.Property(n => n.Primeiro)
                    .HasColumnName("primeiro_nome")
                    .IsRequired()
                    .HasMaxLength(50);
                ;
            }
        );

        builder.OwnsOne(n => n.Nome,
            nome =>
            {
                nome.Property(n => n.Segundo)
                    .HasColumnName("segundo_nome")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );

        builder.OwnsOne(e => e.Email,
            endereco
                =>
            {
                endereco.Property(e => e.Endereco)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(256);
            }
        );

        builder.OwnsOne(s => s.Senha,
            salt
                =>
            {
                salt.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .IsRequired();
            }
        );

        builder.OwnsOne(s => s.Senha,
            salt
                =>
            {
                salt.Property(e => e.Hash)
                    .HasColumnName("hash")
                    .IsRequired();
            }
        );
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("dthr_criacao")
            .IsRequired();

        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("dthr_alteracao");
    }
}