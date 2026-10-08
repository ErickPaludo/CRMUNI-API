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
        
        builder.Property(c => c.DthrCriacao)
            .HasColumnName("dthr_criacao")
            .IsRequired();

        builder.Property(c => c.DthrAlteracao)
            .HasColumnName("dthr_alteracao");
    }
}