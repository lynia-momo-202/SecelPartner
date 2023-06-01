using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;
using System.Linq.Expressions;

namespace SecelPartner.UI.Repositories
{
    public class UserRoleRepository:IUserRoleRepository
    {
        private readonly SecelPartnerUIContext _context;

        public UserRoleRepository(SecelPartnerUIContext context)
        {
            _context = context;
        }
        public async Task Add(IdentityUserRole<string> IdentityUserRole)
        {
            await _context.UserRoles.AddAsync(IdentityUserRole);
            await Save();
        }

        public async Task<IEnumerable<IdentityUserRole<string>>> GetAll()
        {
            var all = _context.UserRoles;
            return await all.ToListAsync();
        }

        public async Task<IdentityUserRole<string>> GetById(string id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task Delete(string id)
        {
            var g = await GetById(id);
            if (g != null)
            {
                _context.UserRoles.Remove(g);
            }
            await Save();
        }

        public List<IdentityUserRole<string>> ListItems()
        {
            return _context.UserRoles.ToList();
        }
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
