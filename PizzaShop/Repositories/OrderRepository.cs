using PizzaShop.IRepository;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repositories
{
    public class OrderRepository : Repository<Bestallning>, IOrder
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public OrderRepository(TomasosContext context)

            : base(context) { }



        public Bestallning Edit(Bestallning editedOrder)
        {
            var OldOrder = Get(editedOrder.BestallningId);
            Db.Entry(OldOrder).CurrentValues.SetValues(editedOrder);
            Db.SaveChanges();
            return editedOrder;
        }
    }
}
