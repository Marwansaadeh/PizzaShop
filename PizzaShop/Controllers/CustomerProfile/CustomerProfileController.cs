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
    public class CustomerProfileController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICustomer _customer;

        public CustomerProfileController(ICustomer Customer, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;

            _customer = Customer;
        }
        public IActionResult EditeProfile()
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies["Customerlogin"]))
            {
                return RedirectToAction("Login", "Login");

            }
          Kund k = new Kund();
           
          var cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["Customerlogin"];
          var editedcustomer = _customer.GetAll().FirstOrDefault(x => x.AnvandarNamn == cookieValueFromContext);
          return View(editedcustomer);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditeProfile(Kund customer)
        {
            var customercheck = _customer.GetAll().FirstOrDefault(c => c.Email == customer.Email || c.AnvandarNamn == customer.AnvandarNamn);

            if (!ModelState.IsValid)
            {

                Kund model = new Kund();
                return View(model);
            }
            else
            {

                _customer.Edit(customer);

                return RedirectToAction("Index", "Home");
            }
        }

    }
}