using Bogus;
using Bogus.DataSets;
using Iara.Domain.Entities;
using Iara.Services.DTOS;

namespace Iara.Testes.Fixtures
{
    public class CotacaoFixture
    {
        public static Cotacao CreateValidCotacao()
        {
            ICollection<CotacaoItem> cotacaoItems = CreateValidListCotacaoItem();

            return new Cotacao("59409266000135", "57569132000156", new Randomizer().Int(0, 1000).ToString(), DateTime.Now, DateTime.Now, "78710-265", "", "", "", "", new Lorem().Sentence(12), cotacaoItems);
        }

        public static CotacaoItem CreateValidCotacaoItem()
        {
            return new CotacaoItem(0, new Lorem().Word(), new Randomizer().Int(0, 1000).ToString(), new Random().NextDouble(), new Randomizer().Int(0, 1000), new Lorem().Word(), "UN");
        }

        public static List<CotacaoItem> CreateValidListCotacaoItem(int limit = 5)
        {
            var list = new List<CotacaoItem>();
            for (int i = 0; i < limit; i++)
            {
                list.Add(CreateValidCotacaoItem());
            }

            return list;
        }

        public static List<Cotacao> CreateListValidCotacao(int limit = 5)
        {
            var list = new List<Cotacao>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidCotacao());

            return list;
        }

        public static CotacaoDto CreateValidCotacaoDTO(bool newId = false)
        {
            return new CotacaoDto
            {
                Id = newId ? new Randomizer().Int(0, 1000) : 0,
                CNPJComprador = "59409266000135",
                CNPJFornecedor = "57569132000156",
                NumeroCotacao = new Randomizer().Int(0, 10000).ToString(),
                CEP = "57080-860",
                DataCotacao = DateTime.Now,
                DataEntregaCotacao = DateTime.UtcNow,
                Observacao = new Lorem().Paragraph(1)
            };
        }

        public static CotacaoDto CreateInvalidCotacaoDTO()
        {
            return new CotacaoDto
            {
                Id = 0,
                CNPJComprador = new Randomizer().Int(0, 1100).ToString(),
                CNPJFornecedor = new Randomizer().Int(0, 1100).ToString(),
                NumeroCotacao = new Randomizer().Int(0, 10000).ToString(),
                CEP = "57080-860",
                DataCotacao = DateTime.Now,
                DataEntregaCotacao = DateTime.UtcNow,
                Observacao = new Lorem().Paragraph(1)
            };
        }
    }
}
