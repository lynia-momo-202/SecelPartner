using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class NiveauPartenariatRepository : GenericRepository<NiveauPartenariat>, INiveauPartenariatRepository
    {
        public NiveauPartenariatRepository(SecelPartnerDataContext Context) : base(Context)
        {
        }
        public async Task Update(NiveauPartenariat niveauPartenariat)
        {
            var np = await GetById(niveauPartenariat.Id);
            if (np != null)
            {
                np.Designation = niveauPartenariat.Designation;
            }
        }
    }
}
