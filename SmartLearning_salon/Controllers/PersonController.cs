using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLearning_salon.Models;
using SmartLearning_salon.Services.Person;

namespace SmartLearning_salon.Controllers
{
    public class PersonController : Controller
    {
        //Reference
        private readonly IPersonService _personService;
        //Injecting the service
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        // GET: PersonController
        public IActionResult Index()
        {
            
            return View("Person");
        }
        public IActionResult Upload(string uploadtekst)
        {
            return View();
        }

        // GET: PersonController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            try
            {
                //using the Injected service
                _personService.AddItemAsync(person);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
