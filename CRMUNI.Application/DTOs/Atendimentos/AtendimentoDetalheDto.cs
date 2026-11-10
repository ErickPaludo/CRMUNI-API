using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Domain.Entidades.Atendimentos;
using CRMUNI.Domain.Entidades.Etapas;

namespace CRMUNI.Application.DTOs.Atendimentos;

public record AtendimentoEtapaDto(int Id, EEtapa Tipo, string Nome, int Ordem);

/// <summary>Setor = nome do setor do atendente (o atendimento pertence ao setor de quem o atende).</summary>
public record AtendenteDto(int Id, string NomeCompleto, string Email, string Setor);

public record AtendimentoDetalheDto(
    int Id,
    EAtendimentoSituacao Situacao,
    DateTime DataCriacao,
    AtendimentoEtapaDto Etapa,
    FunilEtapaDto Funil,
    AtendenteDto Atendente);

public record AtendimentosDto(List<AtendimentoDetalheDto> Atendimentos);
