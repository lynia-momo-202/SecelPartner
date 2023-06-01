using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface INiveauPartenariatRepository : IGenericRepository<NiveauPartenariat>
    {
        Task Update(NiveauPartenariat niveautPartenariat);
    }
}
