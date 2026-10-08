using CRMUNI.DOMAIN.Entidades.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMUNI.Infra.Data.ConfigTabela;

public class EmpresaConfig : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("tb_empresas");
        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.Email,
            endereco
                =>
            {
                endereco.Property(e => e.Endereco)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(256);;
            }
        );
        
        builder.OwnsOne(n => n.Nome,
            nome
                =>
            {
                nome.Property(e => e.Primeiro)
                    .HasColumnName("nome")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );
        
        builder.OwnsOne(t => t.Telefone,
            telefone
                =>
            {
                telefone.Property(e => e.Numero)
                    .HasColumnName("telefone")
                    .IsRequired()
                    .HasMaxLength(12);
            }
        );
        
        builder.OwnsOne(c => c.Cnpj,
            cnpj
                =>
            {
                cnpj.Property(e => e.Codigo)
                    .HasColumnName("cnpj")
                    .IsRequired()
                    .HasMaxLength(14);;
            }
        );

        builder.Property(c => c.DthrCriacao)
            .HasColumnName("dthr_criacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("dthr_alteracao");
    }
}