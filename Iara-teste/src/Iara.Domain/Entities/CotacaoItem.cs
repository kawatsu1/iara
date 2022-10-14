using Iara.Domain.Validators;

namespace Iara.Domain.Entities
{
    public class CotacaoItem : Base
    {
        public long CotacaoId { get; set; }
        public virtual Cotacao Cotacao { get; set; }
        public string Descricao { get; set; }
        public string NumeroItem { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public string Marca { get; set; }
        public string Unidade { get; set; }

        protected CotacaoItem() { }

        public CotacaoItem(long cotacaoId, string descricao, string numeroItem, double preco, int quantidade, string marca, string unidade)
        {
            CotacaoId = cotacaoId;
            Descricao = descricao;
            NumeroItem = numeroItem;
            Preco = preco;
            Quantidade = quantidade;
            Marca = marca;
            Unidade = unidade;
            Validate();
        }

        public bool Validate()
            => base.Validate(new CotacaoItemValidator(), this);
    }
}
