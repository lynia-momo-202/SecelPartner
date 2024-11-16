using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class ConditionRepository : GenericRepository<Condition>, IConditionRepository
    {
        public ConditionRepository(SecelPartnerDataContext Context)
            : base(Context) { }

        public async Task Update(Condition condition)
        {
            var c = await GetById(condition.Id);
            if (c != null)
            {
                c.Description = condition.Description;
            }
        }

        public new async Task<IEnumerable<Condition>> GetAll()
        {
            var All = _context.Conditions.Include(i => i.Partenariat);
            return await All.ToListAsync();
        }
    }
}
