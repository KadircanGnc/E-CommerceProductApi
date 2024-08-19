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
    public class GenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : ECommerceDbContext, new()
    {
        public virtual T GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().Find(id);
            }
        }
        public virtual List<T> GetAll()
        {
            using (var context = new TContext())
            {
                return context.Set<T>().ToList();
            }
        }
        public virtual void Create(T entity)
        {
            using( var context = new TContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void Delete(int id)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Remove(GetById(id));
                context.SaveChanges();
            }
        }
        public virtual void Update(T entity)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
        }
    }
}
