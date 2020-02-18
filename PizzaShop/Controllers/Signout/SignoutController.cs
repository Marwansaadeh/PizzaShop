using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers.Signout
{
    public class SignoutController : Controller
    {
       
        public IActionResult Signout()
        {
            Response.Cookies.Delete("Customerlogin");
            return RedirectToAction("Index", "Home");

        }
    }
}