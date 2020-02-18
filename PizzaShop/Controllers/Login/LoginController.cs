using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.IRepository;
using PizzaShop.Models;

namespace PizzaShop.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly ICustomer _customer;
        public LoginController(ICustomer Customer)
        {
            _customer = Customer;
        }

        public IActionResult Login()
        {
            Kund c = new Kund();
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Kund customer)
        {
           var checkuser= _customer.VerifyUser(customer);
            if(checkuser)
            {
                CookieOptions option = new CookieOptions();
                option.HttpOnly = true;
                option.Expires = DateTime.Now.AddDays(20);

                Response.Cookies.Append("Customerlogin", customer.AnvandarNamn, option);
                return RedirectToAction("Index", "Home");

            }
            return View(customer);
        }
    }
}