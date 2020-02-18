using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.ViewModels
{
    public class ViewFoodtypeAndDetials
    {
        public List<Matratt> Foods { get; set; }
        public List<MatrattTyp> Foodtypes { get; set; }
        public List<Produkt> Products { get; set; }

        public int Amount { get; set; }

     
    }
}
