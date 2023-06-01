using SecelPartner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Interfaces
{
    public interface IPartenariatRepository
    {
        Task<Partenariat> GetById(string id);
        Task<IEnumerable<Partenariat>> GetAll();
        List<Partenariat> ListPartenariats();
        Task<IEnumerable<Partenariat>> Find(Expression<Func<Partenariat, bool>> expression);
        Task Add(Partenariat partenariat);
        Task AddRange(IEnumerable<Partenariat> entities);
        //Task Update(C Class);
        Task Delete(string id);
        Task RemoveRange(IEnumerable<Partenariat> partenariat);
        Task Update(Partenariat partenariat);
    }
}
