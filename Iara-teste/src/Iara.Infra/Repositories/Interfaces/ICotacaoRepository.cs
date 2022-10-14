using Iara.Domain.Entities;

namespace Iara.Infra.Repositories.Interfaces
{
    public interface ICotacaoRepository : IBaseRepository<Cotacao>
    {
        public Task CreateOrUpdate(IList<CotacaoItem> items);
    }
}
