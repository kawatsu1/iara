using Iara.Domain.Entities;
using Iara.Infra.Context;
using Iara.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Iara.Infra.Repositories
{
    public class CotacaoRepository : BaseRepository<Cotacao>, ICotacaoRepository
    {
        private readonly IaraContext _context;
        public CotacaoRepository(IaraContext context) : base(context)
        {
            _context = context;
        }

        public new virtual async Task<Cotacao> GetAsnyc(long id)
        {
            var obj = await _context.Set<Cotacao>()
                                    .AsNoTracking()
                                    .Include(x => x.CotacaoItem)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

            return obj;
        }

        public new virtual async Task<IList<Cotacao>> GetAllAsync()
        {
            return await _context.Set<Cotacao>()
                                 .AsNoTracking()
                                 .Include(x => x.CotacaoItem)
                                 .ToListAsync();
        }

        public virtual async Task CreateOrUpdate(IList<CotacaoItem> items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }


        public new virtual async Task<Cotacao> UpdateAsync(Cotacao obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            if (obj.CotacaoItem.Any()) await CreateOrUpdate(obj.CotacaoItem.ToList());
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
