using PizzaShop.IRepository;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repositories
{
    public class ProductRepository: Repository<Produkt>, IProduct
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public ProductRepository(TomasosContext context)

            : base(context) { }



        public Produkt Edit(Produkt editedProduct)
        {
            var OldProduct = Get(editedProduct.ProduktId);
            Db.Entry(OldProduct).CurrentValues.SetValues(editedProduct);
            Db.SaveChanges();
            return editedProduct;
        }
    }
}
