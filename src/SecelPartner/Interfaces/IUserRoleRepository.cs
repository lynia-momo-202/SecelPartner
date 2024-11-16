using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using SecelPartner.UI.Areas.Identity.Data;

namespace SecelPartner.UI.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IdentityUserRole<string>> GetById(string id);
        Task<IEnumerable<IdentityUserRole<string>>> GetAll();
        List<IdentityUserRole<string>> ListItems();
        Task Add(IdentityUserRole<string> userRole);
        Task Delete(string id);
    }
}
