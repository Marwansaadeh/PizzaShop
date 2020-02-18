using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Models;

namespace PizzaShop.ViewModels
{
    public class CartItem
    {
        public Matratt Item { get; set; }
        public int Amount { get; set; }
    }
}
