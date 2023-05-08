using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext pz = new PizzaContext()) {
                List<Pizza> list = pz.Pizzas.ToList();
                if (list == null)
                    return View("ErrorList");

                return View(list);
            }

        }
        public IActionResult Details(int id)
        {
            using (PizzaContext pz = new PizzaContext())
            {
                Pizza pizza = pz.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                return View(pizza);
            }
        }

        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(Pizza data)
        {

            if(!ModelState.IsValid)
            {
                return View("Create", data);
            }
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pz = new Pizza { Name = data.Name, Description = data.Description, Image = data.Image, Price = data.Price };
                context.Pizzas.Add(pz);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public IActionResult Edit(int id)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                Pizza pizza = ctx.Pizzas.Where(p => p.Id == id).First();

            return View(pizza);
            }
       

        }

        [HttpPost]
        public IActionResult Edit(Pizza pizza)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
               if(!ModelState.IsValid)
                {
                    return View("Edit");
                }
               Pizza pz = ctx.Pizzas.Where(p => p.Id == pizza.Id).First();
                pz.Image = pizza.Image;
                pz.Name = pizza.Name;
                pz.Description = pizza.Description;
                pz.Price = pizza.Price;
                ctx.Pizzas.Update(pz);
                ctx.SaveChanges();

                return RedirectToAction("index");
            }


        }


    }
}
