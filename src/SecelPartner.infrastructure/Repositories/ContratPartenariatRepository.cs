using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class ContratPartenariatRepository
        : GenericRepository<ContratPartenariat>,
            IContratPartenariatRepository
    {
        public ContratPartenariatRepository(SecelPartnerDataContext Context)
            : base(Context) { }

        public async Task Update(ContratPartenariat contratPartenariat)
        {
            var cp = await GetById(contratPartenariat.Id);
            if (cp != null)
            {
                cp.DateSign = contratPartenariat.DateSign;
                cp.DateExpiration = contratPartenariat.DateExpiration;
                cp.Titre = contratPartenariat.Titre;
                cp.Montant = contratPartenariat.Montant;
                cp.Partenariat = contratPartenariat.Partenariat;
                cp.Partenariat = contratPartenariat.Partenariat;
                cp.PartenaireId = contratPartenariat.PartenaireId;
                cp.PartenaireId = contratPartenariat.PartenaireId;
            }
        }

        public new async Task<IEnumerable<ContratPartenariat>> GetAll()
        {
            var All = _context
                .ContratPartenariats.Include(i => i.Partenariat)
                .Include(i => i.Partenaire);
            return await All.ToListAsync();
        }
    }
}
