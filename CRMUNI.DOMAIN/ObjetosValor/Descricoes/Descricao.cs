using CRMUNI.DOMAIN.Validacoes.ObjetosValor.Descricoes;
using CRMUNI.DOMAIN.Validacoes.Utilitarios;

namespace CRMUNI.DOMAIN.ObjetosValor.descricoes
{
    public abstract record Descricao
    {
        public virtual bool Obrigatorio { get; } = false;
        public virtual int TamanhoMinimo { get; } = 0;
        public virtual int TamanhoMaximo { get; } = 400;
        public string Texto { get; private set; }

        protected Descricao(string texto)
        {
            ValidaNulo.Verifica(texto, DescricaoMensagens.DescricaoNula);
            ValidaObrigatoriedade(texto);
            texto = Prepara(texto);
            ValidaTamanhoCaracteres(texto);
            Texto = texto;
        }

        private string Prepara(string texto)
        {
            return texto.Trim();
        }

        private void ValidaObrigatoriedade(string texto)
        {
            if (Obrigatorio)
                DescricaoValidacao.Verifica(string.IsNullOrWhiteSpace(texto),
                    DescricaoMensagens.DescricaoObrigatoria);
        }

        private void ValidaTamanhoCaracteres(string texto)
        {
            DescricaoValidacao.Verifica(texto.Length < TamanhoMinimo,
                DescricaoMensagens.DescricaoMaximo(TamanhoMinimo));
            DescricaoValidacao.Verifica(texto.Length > TamanhoMaximo,
                DescricaoMensagens.DescricaoMaximo(TamanhoMaximo));
        }
    }
}