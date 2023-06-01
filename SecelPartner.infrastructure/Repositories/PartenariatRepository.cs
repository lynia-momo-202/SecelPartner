using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Interfaces;
using SecelPartner.Core.Entities;
using SecelPartner.Infrastructure.DefaultContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Infrastructure.Repositories
{
    public class PartenariatRepository : IPartenariatRepository
    {
        public readonly SecelPartnerDataContext _context;

        public PartenariatRepository(SecelPartnerDataContext context)
        {
            _context = context;
        }

        public async Task Add(Partenariat partenariat)
        {
            await _context.Set<Partenariat>().AddAsync(partenariat);
        }
        public async Task AddRange(IEnumerable<Partenariat> partenariats)
        {
            await _context.Set<Partenariat>().AddRangeAsync(partenariats);
        }
        public async Task<IEnumerable<Partenariat>> Find(Expression<Func<Partenariat, bool>> expression)
        {
            return _context.Set<Partenariat>().Where(expression);
        }
        public async Task<IEnumerable<Partenariat>> GetAll()
        {
            var All = _context.Partenariats
                .Include(i => i.TypePartenariat)
                .Include(i => i.NiveauPartenariat);
            return await All.ToListAsync();
        }
        public async Task<Partenariat> GetById(string id)
        {
            return await _context.Set<Partenariat>().FindAsync(id);
        }
        public async Task Delete(string id)
        {
            var c = await GetById(id);
            if (c != null)
            {
                _context.Set<Partenariat>().Remove(c);
            }

        }
        public async Task RemoveRange(IEnumerable<Partenariat> partenariat)
        {
            _context.Set<Partenariat>().RemoveRange(partenariat);
        }
        public async Task Update(Partenariat partenariat)
        {
            var a = await GetById(partenariat.Id);
            if (a != null)
            {
                a.Description = partenariat.Description;
            }
        }

        public List<Partenariat> ListPartenariats()
        {
            var query = from Partenariat in _context.Partenariats
                        select Partenariat;
            return query.ToList();
        }
    }
}
