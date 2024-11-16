using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IContratPartenariatRepository : IGenericRepository<ContratPartenariat>
    {
        Task Update(ContratPartenariat contratPartenariat);
    }
}
