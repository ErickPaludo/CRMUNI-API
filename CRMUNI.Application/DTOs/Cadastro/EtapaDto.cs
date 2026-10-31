using CRMUNI.Domain.Entidades.Etapas;

namespace CRMUNI.Application.DTOs.Cadastro;

public record EtapaDto(int Id, Guid IdEmpresa, EEtapa Tipo, FunilEtapaDto Funil, string Nome, int Ordem);
public record EtapasDto(List<EtapaDto> Etapas);
