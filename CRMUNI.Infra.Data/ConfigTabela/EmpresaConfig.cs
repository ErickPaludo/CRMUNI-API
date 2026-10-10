using CRMUNI.Domain.Entidades.Empresas;
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
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(256);;
            }
        );
        
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
        
        builder.OwnsOne(t => t.Telefone,
            telefone
                =>
            {
                telefone.Property(e => e.Numero)
                    .HasColumnName("Telefone")
                    .IsRequired()
                    .HasMaxLength(12);
            }
        );
        
        builder.OwnsOne(c => c.Cnpj,
            cnpj
                =>
            {
                cnpj.Property(e => e.Codigo)
                    .HasColumnName("Cnpj")
                    .IsRequired()
                    .HasMaxLength(14);;
            }
        );

        builder.Property(c => c.DthrCriacao)
            .HasColumnName("DthrCriacao")
            .IsRequired();
        
        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("DthrAlteracao");
    }
}