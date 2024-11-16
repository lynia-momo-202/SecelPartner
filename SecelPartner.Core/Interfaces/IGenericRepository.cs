using System.Linq.Expressions;
using System.Runtime.InteropServices;
using SecelPartner.Core.Entities;

namespace SecelPartner.Core.Interfaces
{
    public interface IGenericRepository<C>
        where C : class
    {
        Task<C> GetById(int id);
        Task<IEnumerable<C>> GetAll();
        List<C> ListItems();
        Task<IEnumerable<C>> Find(Expression<Func<C, bool>> expression);
        Task Add(C Class);
        Task AddRange(IEnumerable<C> entities);

        Task Delete(int id);
        Task RemoveRange(IEnumerable<C> entities);
    }
}
