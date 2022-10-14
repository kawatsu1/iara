using Iara.Domain.Validators;

namespace Iara.Domain.Entities
{
    public class Cotacao : Base
    {
        public string CNPJComprador { get; set; }
        public string CNPJFornecedor { get; set; }
        public string NumeroCotacao { get; set; }
        public DateTime DataCotacao { get; set; }
        public DateTime DataEntregaCotacao { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<CotacaoItem> CotacaoItem { get; set; }

        protected Cotacao() { }

        public Cotacao(string cNPJComprador, string cNPJFornecedor, string numeroCotacao, DateTime dataCotacao, DateTime dataEntregaCotacao, string cEP, string logradouro, string complemento, string bairro, string uF, string observacao, ICollection<CotacaoItem> cotacaoItem)
        {
            CNPJComprador = cNPJComprador;
            CNPJFornecedor = cNPJFornecedor;
            NumeroCotacao = numeroCotacao;
            DataCotacao = dataCotacao;
            DataEntregaCotacao = dataEntregaCotacao;
            CEP = cEP;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            UF = uF;
            Observacao = observacao;
            CotacaoItem = cotacaoItem;
            _errors = new List<string>();
            Validate();
        }

        public bool Validate()
            => base.Validate(new CotacaoValidator(), this);
    }
}
