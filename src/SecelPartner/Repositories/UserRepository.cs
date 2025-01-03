using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;
using SecelPartner.UI.Models;

namespace SecelPartner.UI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SecelPartnerUIContext _context;

        public UserRepository(SecelPartnerUIContext context)
        {
            _context = context;
        }

        public async Task Add(SecelPartnerUIUser SecelPartnerUIUser)
        {
            await _context.SecelPartnerUIUsers.AddAsync(SecelPartnerUIUser);
            await Save();
        }

        public async Task<IEnumerable<SecelPartnerUIUser>> Find(
            Expression<Func<SecelPartnerUIUser, bool>> expression
        )
        {
            return _context.SecelPartnerUIUsers.Where(expression);
        }

        public async Task<IEnumerable<SecelPartnerUIUser>> GetAll()
        {
            return await _context.SecelPartnerUIUsers.ToListAsync();
        }

        public async Task<SecelPartnerUIUser> GetById(string id)
        {
            return await _context.SecelPartnerUIUsers.FindAsync(id);
        }

        public async Task Delete(string id)
        {
            var g = await GetById(id);
            if (g != null)
            {
                _context.SecelPartnerUIUsers.Remove(g);
            }
            await Save();
        }

        public List<SecelPartnerUIUser> ListItems()
        {
            return _context.SecelPartnerUIUsers.ToList();
        }

        public async Task Update(SecelPartnerUIUser user)
        {
            var u = await GetById(user.Id);
            if (u != null)
            {
                if (user.ProfilPath != null)
                {
                    u.ProfilName = user.ProfilName;
                    u.ProfilPath = user.ProfilPath;
                }
                u.Email = user.Email;
                u.LastName = user.LastName;
                u.FirstName = user.FirstName;
            }
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
