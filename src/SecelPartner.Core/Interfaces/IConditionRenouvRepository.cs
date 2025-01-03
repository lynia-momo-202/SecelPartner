using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IConditionRenouvRepository : IGenericRepository<ConditionRenouv>
    {
        Task Update(ConditionRenouv conditionRenouv);
    }
}
