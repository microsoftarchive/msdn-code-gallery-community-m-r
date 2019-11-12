
// Houssem Dellai
// houssem.dellai@live.com
// @HoussemDellai
// +216 95 325 964

using System.Threading.Tasks;
using MVC5Demo.Models;
using System;
using System.Web.Mvc;
using Repositories;

namespace MVC5Demo.Controllers
{
    public class PersonsController : Controller
    {

        GenericRepository<Person> _genericRepository = new GenericRepository<Person>(new PersonsContext()); 

        //private PersonsContext db = new PersonsContext();

        //
        // GET: /Persons/
        public ActionResult Index()
        {
            return View(_genericRepository.GetAll());
        }

        //
        // GET: /Persons/Details/5
        public async Task<ActionResult> Details(Int32 id)
        {
            Person person = await _genericRepository.GetByIdAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Persons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _genericRepository.InsertAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        //
        // GET: /Persons/Edit/5
        public async Task<ActionResult> Edit(Int32 id)
        {
            Person person = await _genericRepository.GetByIdAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await _genericRepository.EditAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /Persons/Delete/5
        public async Task<ActionResult> Delete(Int32 id)
        {
            Person person = await _genericRepository.GetByIdAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Int32 id)
        {
            Person person = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.DeleteAsync(person);
            return RedirectToAction("Index");
        }
    }
}
