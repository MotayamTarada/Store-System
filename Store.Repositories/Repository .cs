using Microsoft.EntityFrameworkCore;
using Store.DomainEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;
        private readonly StoreSystemContext context;

        public Repository() {
            this.context = new StoreSystemContext();
            this.dbSet = context.Set<TEntity>();
        }
        public void Add(TEntity entitiy)
        {
            if (entitiy == null)
            {

                throw new NotImplementedException();
            }
            dbSet.Add(entitiy);
            context.SaveChanges();  
        }

        public bool Delete(TEntity entitiy)
        {
            if (entitiy == null)
            {
                throw new NotImplementedException();
            }
            dbSet.Remove(entitiy);  
            context.SaveChanges();
            return true;

        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            foreach(var item in includes)
            {
                query = query.Include(item);
            }
            return query.Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public bool Update(TEntity entitiy)
        {
            if (entitiy == null)
            {

                throw new NotImplementedException();

            }

            dbSet.Update(entitiy);
            context.SaveChanges(true);
            return true;
        }
    }
    

    }

