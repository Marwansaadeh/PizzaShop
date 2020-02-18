using Microsoft.EntityFrameworkCore;
using PizzaShop.IRepository;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repositories
{
    public class CustomerRepository : Repository<Kund>, ICustomer
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public CustomerRepository(TomasosContext context)

            : base(context) { }
        


        public Kund Edit(Kund editcustomer)
        {
            var OldCustomer = Get(editcustomer.KundId);
            Db.Entry(OldCustomer).CurrentValues.SetValues(editcustomer);
            Db.SaveChanges();
            return editcustomer;
        }
        public bool VerifyUser(Kund customer)
        {
            var verifiedcustomer = Db.Kund.FirstOrDefault(x => x.AnvandarNamn == customer.AnvandarNamn && x.Losenord == customer.Losenord);

            if(verifiedcustomer!=null)
            {
                return true;
            }
            return false;
        }
    }
}
