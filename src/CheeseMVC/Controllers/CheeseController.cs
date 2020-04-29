using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Type = addCheeseViewModel.Type
                };

                CheeseData.Add(newCheese);

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }

            return Redirect("/");
        }

        public IActionResult Edit(int cheeseId)
        {
            var cheese = CheeseData.GetById(cheeseId);

            AddEditCheeseViewModel editCheese = new AddEditCheeseViewModel();
            editCheese.Name = cheese.Name;
            editCheese.Description = cheese.Description;
            editCheese.Type = cheese.Type;
            
            return View(editCheese);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel editCheese)
        {

            if (ModelState.IsValid)
            {
                var cheese = CheeseData.GetById(editCheese.CheeseId);
                cheese.Name = editCheese.Name;
                cheese.Description = editCheese.Description;
                cheese.Type = editCheese.Type;

                return Redirect("/");
            }
            
            return View();
        }
    }
}
