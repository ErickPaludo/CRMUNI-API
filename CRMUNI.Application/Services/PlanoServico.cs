using CRMUNI.Application.DTOs.Cadastro;
using CRMUNI.Application.Excecoes;
using CRMUNI.Application.Interfaces;
using CRMUNI.Domain.Entidades.Planos;
using CRMUNI.Domain.Entidades.Usuarios;
using CRMUNI.Domain.ObjetosValor.Descricoes;
using CRMUNI.Domain.ObjetosValor.Nomes;
using CRMUNI.Domain.Servicos;

namespace CRMUNI.Application.Services;

public sealed class PlanoServico : IPlanoServico
{
    private readonly IUnityOfWork _unityOfWork;

    public PlanoServico(IUnityOfWork unityOfWork)
        => _unityOfWork = unityOfWork;


    public async Task<PlanoDto> Cadatro(string idUsuario, PlanoCadastroDto dto)
    {
        Usuario? usuario = await _unityOfWork.UsuarioRepositorio.BuscarObjetoUnico(x
            => x.Id.ToString() == idUsuario);

        if (usuario is null)
            throw new NaoPermitidoExcecao("Você não possui permissão para esta ação.");

        NomePlano nome = new NomePlano(dto.Nome);
        DescricaoPlano descricao = new DescricaoPlano(dto.Descricao);
        Plano plano = new Plano(usuario.Empresa, nome, descricao);

        await _unityOfWork.PlanoRepositorio.Insere(plano);
        await _unityOfWork.Commit();

        return MapeamentoCadastro(plano);
    }

    private PlanoDto MapeamentoCadastro(Plano plano)
        => new PlanoDto(plano.Id, plano.Empresa.Id, plano.Nome.Completo, plano.Descricao.Texto);
}