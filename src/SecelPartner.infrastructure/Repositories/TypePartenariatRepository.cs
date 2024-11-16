using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class TypePartenariatRepository
        : GenericRepository<TypePartenariat>,
            ITypePartenariatRepository
    {
        public TypePartenariatRepository(SecelPartnerDataContext Context)
            : base(Context) { }

        public async Task Update(TypePartenariat typePartenariat)
        {
            var tp = await GetById(typePartenariat.Id);
            if (tp != null)
            {
                tp.Nom = typePartenariat.Nom;
            }
        }
    }
}
