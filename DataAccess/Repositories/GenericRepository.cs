using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        public ECommerceDbContext _context;
        
        public GenericRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public virtual T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                throw new Exception("Not Found");
            return entity;
        }
        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
            _context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
