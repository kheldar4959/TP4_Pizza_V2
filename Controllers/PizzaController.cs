using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP4_Pizza_V2.BO;
using TP4_Pizza_V2.Models;
using static TP4_Pizza_V2.BO.FakeDB;

namespace TP4_Pizza_V2.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            //Récupère dans la DB simulée une instance de la liste de pizza
            return View(FakeDb.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            //Remonte les données stockés pour l'identifiant descendu dans l'url
            PizzaViewModel vm = new PizzaViewModel();
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            return View(vm);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {

            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible
                .Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible
                .Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid && ValidateVM(vm))
                {
                    Pizza pizza = vm.Pizza;

                    pizza.Pate = FakeDb.Instance.PatesDisponible.FirstOrDefault(x => x.Id == vm.IdPate);

                    pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(
                        x => vm.IdsIngredients.Contains(x.Id))
                        .ToList();

                    pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(x => x.Id) + 1;

                    FakeDb.Instance.Pizzas.Add(pizza);

                    return RedirectToAction("Index");
                }
                else
                {
                    vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

                    vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                        x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                        .ToList();

                    return View(vm);
                }
            }
            catch
            {
                vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

                vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                    x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                    .ToList();

                return View(vm);
            }
        }

        private bool ValidateVM(PizzaViewModel vm)
        {
            bool result = true;
            return result;
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdsIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }

            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaViewModel vm)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == vm.Pizza.Id);
                pizza.Nom = vm.Pizza.Nom;
                pizza.Pate = FakeDb.Instance.PatesDisponible.FirstOrDefault(x => x.Id == vm.IdPate);
                pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(x => vm.IdsIngredients.Contains(x.Id)).ToList();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();
            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
                FakeDb.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
