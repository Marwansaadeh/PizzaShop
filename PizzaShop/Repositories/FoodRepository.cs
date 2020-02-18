using Microsoft.EntityFrameworkCore;
using PizzaShop.IRepository;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repositories
{
    public class FoodRepository:Repository<Matratt>, IFood
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public FoodRepository(TomasosContext context)

            : base(context) { }



        public Matratt Edit(Matratt editedFood)
        {
            var OldFood = Get(editedFood.MatrattId);
            Db.Entry(OldFood).CurrentValues.SetValues(editedFood);
            Db.SaveChanges();
            return editedFood;
        }

        public List<Produkt> FoodProducts(Matratt food)
        {

            List<Produkt> pro = new List<Produkt>();
            var FooProducts = Db.MatrattProdukt.ToList();
            var Products = Db.Produkt.ToList();
            foreach(var productfood in FooProducts)
            {
                if(productfood.MatrattId==food.MatrattId)
                {
                    Produkt p = Products.First(x => x.ProduktId == productfood.ProduktId);
                    pro.Add(p);
                }
            }
            return pro;

        }
      


    }
}
