using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Models;
using System.Linq.Expressions;

namespace SecelPartner.UI.Interfaces
{
    public interface IUserRepository
    {
        Task<SecelPartnerUIUser> GetById(string id);
        Task<IEnumerable<SecelPartnerUIUser>> GetAll();
        List<SecelPartnerUIUser> ListItems();
        Task<IEnumerable<SecelPartnerUIUser>> Find(Expression<Func<SecelPartnerUIUser, bool>> expression);
        Task Add(SecelPartnerUIUser user);
        Task Delete(string id);
        Task Update(SecelPartnerUIUser user);
    }
}
