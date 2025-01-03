using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IConditionRepository : IGenericRepository<Condition>
    {
        Task Update(Condition condition);
    }
}
