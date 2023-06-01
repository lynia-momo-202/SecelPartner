using Microsoft.AspNetCore.Identity;
using SecelPartner.UI.Areas.Identity.Data;
using System.Linq.Expressions;

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
