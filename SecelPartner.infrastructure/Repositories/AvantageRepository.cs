using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class AvantageRepository :GenericRepository<Avantage>, IAvantageRepository
    {
        public AvantageRepository( SecelPartnerDataContext Context) : base(Context)
        {
        }

        public async Task Update(Avantage avantage)
        {
            var a = await GetById(avantage.Id);
            if (a != null)
            {
                a.Description = avantage.Description;
                a.Partenariat = avantage.Partenariat;
            }
        }
        public new async Task<IEnumerable<Avantage>> GetAll()
        {
            var All = _context.Avantages.Include(i => i.Partenariat);
            return await All.ToListAsync();
        }
    }
}
