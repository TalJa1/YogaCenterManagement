using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly YogaCenterContext _context = new YogaCenterContext();
        private readonly DbSet<T> _dbSet;
        public RepositoryBase()
        {
            _dbSet = _context.Set<T>();
        }
        public List<T> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include is not null)
            {
                query = include(query);
            }
            return query.ToList();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public void Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        public T getById(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
