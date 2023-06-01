using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task Update(Contact contact);
    }
}
