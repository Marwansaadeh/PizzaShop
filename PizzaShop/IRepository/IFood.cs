using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.IRepository
{
    public interface IFood : IRepository<Matratt>
    {
        List<Produkt> FoodProducts(Matratt food);

    }
}
