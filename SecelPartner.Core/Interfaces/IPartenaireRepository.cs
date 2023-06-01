using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IPartenaireRepository : IGenericRepository<Partenaire>
    {
        Task Update(Partenaire partenaire);
    }
}
