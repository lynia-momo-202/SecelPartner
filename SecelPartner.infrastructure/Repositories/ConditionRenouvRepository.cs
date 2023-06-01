using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class ConditionRenouvRepository : GenericRepository<ConditionRenouv>, IConditionRenouvRepository
    {
        public ConditionRenouvRepository(SecelPartnerDataContext Context) : base(Context)
        {
        }

        public async Task Update(ConditionRenouv conditionRenouv)
        {
            var c = await GetById(conditionRenouv.Id);
            if (c != null)
            {
                c.Description = conditionRenouv.Description;
                c.Partenariat=conditionRenouv.Partenariat;
            }
        }
        public new async Task<IEnumerable<ConditionRenouv>> GetAll()
        {
            var All = _context.ConditionRenouvs.Include(i => i.Partenariat);
            return await All.ToListAsync();
        }
    }
}