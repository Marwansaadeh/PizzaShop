using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.IRepository;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICustomer _customer;

        public RegistrationController( ICustomer Customer)
        {
            _customer = Customer;
        }
        public IActionResult RegistarUser()
        {
            Kund customer = new Kund();

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistarUser(Kund customer)
        {

           var customercheckemail= _customer.GetAll().FirstOrDefault(c => c.Email == customer.Email );
           var customercheckusername = _customer.GetAll().FirstOrDefault(x=>x.AnvandarNamn == customer.AnvandarNamn);
                

            if (!ModelState.IsValid)
            {

                Kund model = new Kund();
                return View(model);
            }
            else if(customercheckemail != null|| customercheckusername!=null)

            {
                if (customercheckusername != null)
                {
                    ViewBag.ErrorUsername = "Please chooce another username, this username already exsist";

                }
                if (customercheckemail != null)
                {
                    ViewBag.ErrorEmail = "Please chooce another Email, this email already exsist";

                }
                Kund model = new Kund();
                return View(model);
            }

            else 
            {
                CookieOptions option = new CookieOptions();
                option.HttpOnly = true;
                option.Expires = DateTime.Now.AddDays(20);

                Response.Cookies.Append("CustomerInfo", customer.AnvandarNamn, option);

                _customer.Add(customer);

                return RedirectToAction("Index","Home");
            }

        }
    }
}