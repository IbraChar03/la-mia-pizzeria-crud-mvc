﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _logger { get; set; }
        public PizzaController(ICustomLogger logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.WriteLog("Index page");
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
                Pizza pizza = pz.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
                return View(pizza);
            }
        }

        public IActionResult Create()
        {
            using(PizzaContext pz = new PizzaContext())
            {
                PizzaFormModel model = new PizzaFormModel();
                var categories = pz.Categories.ToList();
                model.Categories = categories;
                model.Pizza = new Pizza();
                return View(model);
            }
           

        }
        [HttpPost]
        public IActionResult Create(PizzaFormModel data)
        {

            if(!ModelState.IsValid)
            {
                using (PizzaContext pz = new PizzaContext())
                {
                    var categories = pz.Categories.ToList();
                    data.Categories = categories;
                    return View("Create", data);
                }
                    
            }
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pz = new Pizza { Name = data.Pizza.Name, Description = data.Pizza.Description, Image = data.Pizza.Image, Price = data.Pizza.Price, CategoryId = data.Pizza.CategoryId };
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
                var categories = ctx.Categories.ToList();
                PizzaFormModel form = new PizzaFormModel();
                form.Pizza = pizza;
                form.Categories = categories;

            return View(form);
            }
       

        }

        [HttpPost]
        public IActionResult Edit(int id,PizzaFormModel data)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
               if(!ModelState.IsValid)
                {
                    var categories = ctx.Categories.ToList();
                    data.Categories = categories;
                    return View("Edit",data);
                }
               Pizza pz = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                if (pz == null)
                    return NotFound();
                pz.Image = data.Pizza.Image;
                pz.Name = data.Pizza.Name;
                pz.Description = data.Pizza.Description;
                pz.Price =data.Pizza.Price;
                pz.CategoryId = data.Pizza.CategoryId;
                ctx.Pizzas.Update(pz);
                ctx.SaveChanges();

                return RedirectToAction("index");
            }


        }
        public IActionResult Delete(int id)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
               Pizza pz = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                if(pz == null)
                    return NotFound();
                ctx.Pizzas.Remove(pz);
                ctx.SaveChanges();
                return RedirectToAction("index");
                


            }
        }


    }
}
