using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Models;
using PizzaShop.IRepository;
namespace PizzaShop.Repositories
{
    public class OrderFoodsRepository : Repository<BestallningMatratt>, IOrderFoods
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public OrderFoodsRepository(TomasosContext context)

            : base(context) { }

    }
}
