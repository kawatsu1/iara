using Iara.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iara.Infra.Context
{
    public class IaraContext : DbContext
    {
        public IaraContext()
        {

        }

        public IaraContext(DbContextOptions<IaraContext> options) : base(options) { }

        public virtual DbSet<Cotacao> Cotacao { get; set; }
        public virtual DbSet<CotacaoItem> CotacaoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IaraContext).Assembly);
        }
    }
}
