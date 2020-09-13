using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLearning_salon.Models;
using SmartLearning_salon.Services.BlobStorage;
using SmartLearning_salon.Services.Person;

namespace SmartLearning_salon.Controllers
{
    public class PersonController : Controller
    {
        //Reference
        private readonly IPersonService _personService;
        private readonly IBlobStorage _blobStorageService;
        //Injecting the service
        public PersonController(IPersonService personService, IBlobStorage blobStorage)
        {
            _personService = personService;
            _blobStorageService = blobStorage;
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
        public async Task<ActionResult<Person>> Details(string id)
        {
            Person person = await _personService.GetItemAsync(id);

            return View(person);
        }

        // GET: PersonController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Create(Person person)
        {
            using (var stream = new MemoryStream())
            {
                try
                {
                    // assume a single file POST
                    await person.File.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    // now send blob up to Azure
                    await _blobStorageService.CreateBlobAsync(person.File.OpenReadStream(), person.File.FileName);

                    // send to Cosmos
                    await _personService.AddItemAsync(person);

                    //Ændre dette til at returnere et til detail view
                    return View(person);
                    //return Ok(new { fileuploaded = true });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
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
