using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add (TEntity entitiy); 
        bool Delete (TEntity entitiy);

        bool Update (TEntity entitiy);

        IQueryable<TEntity> GetAll (); // OUT OF MEMEORY QUERY 

        TEntity GetById (int id); 

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);


    }
}
