using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class PartenaireRepository : GenericRepository<Partenaire>, IPartenaireRepository
    {
        public PartenaireRepository(SecelPartnerDataContext Context) : base(Context)
        {
        }

        public async Task Update(Partenaire partenaire)
        {
            var p = await GetById(partenaire.Id);
            if (p != null)
            {
                if (p.LogoPath != null)
                {
                    p.LogoName = partenaire.LogoName;
                    p.LogoPath = partenaire.LogoPath;
                }
                p.Nom = partenaire.Nom;
                p.Statut = partenaire.Statut;
                p.Adresse = partenaire.Adresse;
                p.Siteweb = partenaire.Siteweb;
                p.Email = partenaire.Email;
                p.SecteurAc = partenaire.SecteurAc;
                p.NumTel = partenaire.NumTel;
            }
        }
    }
}
