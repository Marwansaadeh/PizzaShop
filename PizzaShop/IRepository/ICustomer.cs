using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.IRepository
{
    public interface ICustomer: IRepository<Kund>
    {
        public Kund Edit(Kund editcustomer);
        public bool VerifyUser(Kund customer);

    }
}
