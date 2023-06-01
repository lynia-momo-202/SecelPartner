using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IAvantageRepository : IGenericRepository<Avantage>
    {
        Task Update(Avantage avantage);
    }
}
