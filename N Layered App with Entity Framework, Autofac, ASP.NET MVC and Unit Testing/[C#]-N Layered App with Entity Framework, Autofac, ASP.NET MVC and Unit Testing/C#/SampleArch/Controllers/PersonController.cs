using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SampleArch.Model;
using SampleArch.Service;

namespace SampleArch.Controllers
{
    public class PersonController : Controller
    {
        IPersonService _PersonService;
        ICountryService _CountryService;
        public PersonController(IPersonService PersonService, ICountryService CountryService)
        {
            _PersonService = PersonService;
            _CountryService = CountryService;
        }

        // GET: /Person/
        public ActionResult Index()
        {
            return View(_PersonService.GetAll());
        }

        // GET: /Person/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _PersonService.GetById(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: /Person/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_CountryService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Address,State,CountryId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _PersonService.Create(person);
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_CountryService.GetAll(), "Id", "Name", person.CountryId);
            return View(person);
        }

        // GET: /Person/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _PersonService.GetById(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_CountryService.GetAll(), "Id", "Name", person.CountryId);
            return View(person);
        }

        // POST: /Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,State,CountryId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _PersonService.Update(person);
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_CountryService.GetAll(), "Id", "Name", person.CountryId);
            return View(person);
        }

        // GET: /Person/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _PersonService.GetById(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: /Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Person person = _PersonService.GetById(id);
            _PersonService.Delete(person);
            return RedirectToAction("Index");
        }       
    }
}
