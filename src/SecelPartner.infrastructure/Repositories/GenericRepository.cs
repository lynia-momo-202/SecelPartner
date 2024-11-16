using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class GenericRepository<C> : IGenericRepository<C>
        where C : class
    {
        public readonly SecelPartnerDataContext _context;

        public GenericRepository(SecelPartnerDataContext context)
        {
            _context = context;
        }

        public async Task Add(C entity)
        {
            await _context.Set<C>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<C> entities)
        {
            await _context.Set<C>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<C>> Find(Expression<Func<C, bool>> expression)
        {
            return _context.Set<C>().Where(expression);
        }

        public async Task<IEnumerable<C>> GetAll()
        {
            return await _context.Set<C>().ToListAsync();
        }

        public async Task<C> GetById(int id)
        {
            return await _context.Set<C>().FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var c = await GetById(id);
            if (c != null)
            {
                _context.Set<C>().Remove(c);
            }
        }

        public async Task RemoveRange(IEnumerable<C> entities)
        {
            _context.Set<C>().RemoveRange(entities);
        }

        public List<C> ListItems()
        {
            var query = from C in _context.Set<C>() select C;
            return query.ToList();
        }
    }
}
