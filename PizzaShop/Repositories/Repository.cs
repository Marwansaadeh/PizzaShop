using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShop.IRepository;

namespace PizzaShop.Repositories
{

    public abstract class Repository<TModel>: IRepository<TModel> where TModel : class
    {
        protected readonly DbContext DatabaseContext;

        public Repository(DbContext context)
        {
            this.DatabaseContext = context;
        }

        public void Add(TModel entity)
        {
            DatabaseContext.Set<TModel>().Add(entity);
            DatabaseContext.SaveChanges();

        }
        
        public TModel Get(int id)
        {
            return DatabaseContext.Set<TModel>().Find(id);
        }

        public IEnumerable<TModel> GetAll()
        {
            return DatabaseContext.Set<TModel>().ToList();
        }

        public void Remove(TModel entity)
        {
            DatabaseContext.Set<TModel>().Remove(entity);
            DatabaseContext.SaveChanges();

        }       
    }

    }


