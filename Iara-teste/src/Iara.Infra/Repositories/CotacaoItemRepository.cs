using Iara.Domain.Entities;
using Iara.Infra.Context;
using Iara.Infra.Repositories.Interfaces;

namespace Iara.Infra.Repositories
{
    public class CotacaoItemRepository : BaseRepository<CotacaoItem>, ICotacaoItemRepository
    {
        private readonly IaraContext _context;
        public CotacaoItemRepository(IaraContext context) : base(context)
        {
            _context = context;
        }
    }
}
