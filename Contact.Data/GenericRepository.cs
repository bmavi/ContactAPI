using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contact.Models;

namespace Contact.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _context;
        public GenericRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("Missing Insert information");
            }

            _context.Set<T>().Add(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("Missing Delete object");
            }

            _context.Set<T>().Remove(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                  .AsNoTracking()
                                  .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> searchCriteria)
        {
            return await _context.Set<T>()
                                    .Where(searchCriteria)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> searchCriteria)
        {
            return await _context.Set<T>()
                                    .Where(searchCriteria)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("Missing Update Information");
            }

            _context.Set<T>().Update(obj);
            return await _context.SaveChangesAsync();
        }
    }
}
