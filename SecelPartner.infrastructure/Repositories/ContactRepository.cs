using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(SecelPartnerDataContext Context)
            : base(Context) { }

        public async Task Update(Contact contact)
        {
            var c = await GetById(contact.Id);
            if (c != null)
            {
                if (contact.PhotoPath != null)
                {
                    c.PhotoName = contact.PhotoName;
                    c.PhotoPath = contact.PhotoPath;
                }
                c.Email = contact.Email;
                c.Prenom = contact.Prenom;
                c.Poste = contact.Poste;
                c.Tel = contact.Tel;
                c.PartenaireId = contact.PartenaireId;
                c.Partenaire = contact.Partenaire;
            }
        }

        public new async Task<IEnumerable<Contact>> GetAll()
        {
            var All = _context.Contacts.Include(i => i.Partenaire);
            return await All.ToListAsync();
        }
    }
}
