using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface ITypePartenariatRepository : IGenericRepository<TypePartenariat>
    {
        Task Update(TypePartenariat typePartenariat);
    }
}
