using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace PizzaShop.IRepository
{
    public class HomeController : Controller
    {
        private readonly IFoodType _foodtype;
        private readonly IFood _food;
        private readonly IProduct _product;
        private readonly ICustomer _customer;
        private readonly IOrder _bestallning;
        private readonly IOrderFoods _bestallningMatratt;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IFoodType foodtype, IFood Food,IProduct Product,ICustomer Customer,IHttpContextAccessor HttpContextAccessor, IOrder Bestallning, IOrderFoods BestallningMatratt)
        {
            _foodtype = foodtype;
            _food = Food;
            _product = Product;
            _customer = Customer;
            _httpContextAccessor = HttpContextAccessor;
            _bestallning = Bestallning;
            _bestallningMatratt = BestallningMatratt;
        }
        public IActionResult Index()
        {
            ViewFoodtypeAndDetials model = new ViewFoodtypeAndDetials();
            model.Foodtypes = _foodtype.GetAll().ToList();
            model.Foods = _food.GetAll().ToList();
            return View(model);
        }
        
        public IActionResult Addproduct( int food, int amount)
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies["Customerlogin"]))
            {
                return RedirectToAction("Login", "Login");

            }
            var food1= _food.Get(food);

            CartItem cart = new CartItem();
            cart.Amount = amount;
            cart.Item = food1;
            List<CartItem> shoopingCart;
            var selectedProudct = cart;
            
            if (HttpContext.Session.GetString("Cart") == null)
            {
                shoopingCart = new List<CartItem>();
                
            }
           
            else
            {

                var valuesJson = HttpContext.Session.GetString("Cart");
                shoopingCart = JsonConvert.DeserializeObject<List<CartItem>>(valuesJson);


            }               

            var check = shoopingCart.FirstOrDefault(x=>x.Item.MatrattId==selectedProudct.Item.MatrattId);
            shoopingCart.Remove(check);
            if (check!=null)
            {
                selectedProudct.Amount += check.Amount;
                shoopingCart.Add(selectedProudct);
            }
            else
            {
                shoopingCart.Add(selectedProudct);

            }

            var serlizedList = JsonConvert.SerializeObject(shoopingCart);

            HttpContext.Session.SetString("Cart", serlizedList);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Cart()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("Cart")))
            {
                
                return View();

            }
            else
            {
                var valuesJson = HttpContext.Session.GetString("Cart");
                var model = JsonConvert.DeserializeObject<List<CartItem>>(valuesJson);

                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Cart(int id)
        {   
                var valuesJson = HttpContext.Session.GetString("Cart");
                var model = JsonConvert.DeserializeObject<List<CartItem>>(valuesJson);
                var removed = model.FirstOrDefault(x => x.Item.MatrattId == id);
                model.Remove(removed);
                var serlizedList = JsonConvert.SerializeObject(model);

               HttpContext.Session.SetString("Cart", serlizedList);
               var valuesJson2 = HttpContext.Session.GetString("Cart");
               model = JsonConvert.DeserializeObject<List<CartItem>>(valuesJson2);
            return View(model);
            
        }



        [HttpPost]
        public IActionResult SaveOrder(int customerid, string orderfood,int total )
        {
           var orderfoods = JsonConvert.DeserializeObject<List<CartItem>>(orderfood);

            Bestallning b = new Bestallning();
            b.BestallningDatum = DateTime.Now;
            b.KundId = customerid;
            b.Totalbelopp = total;
            b.Levererad = false;
            _bestallning.Add(b);

            BestallningMatratt matratts = new BestallningMatratt();
            foreach(var order in orderfoods)
            {
                matratts.Antal = order.Amount;
                matratts.MatrattId = order.Item.MatrattId;
                matratts.BestallningId = b.BestallningId;
                _bestallningMatratt.Add(matratts);

            }
            HttpContext.Session.Remove("Cart");
            return View();
        }


    }
}