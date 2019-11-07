using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MVCCRUDKnockout.Models;

namespace MVCCRUDKnockout.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrialDBEntities _db = new TrialDBEntities();

        // GET: Home
        public ActionResult Read()
        {
            return View();
        }

        //GET All Courses
        public JsonResult ListCourses()
        {
            return Json(_db.Courses.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public string Create(Course course)
        {
            if (!ModelState.IsValid) return "Model is invalid";
            _db.Courses.Add(course);
            _db.SaveChanges();
            return "Cource is created";
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var course = _db.Courses.Find(id);
            if (course == null)
                return HttpNotFound();
            var serializer = new JavaScriptSerializer();
            ViewBag.SelectedCourse = serializer.Serialize(course);
            return View();
        }

        // POST: Home/Update/5
        [HttpPost]
        public string Update(Course course)
        {
            if (!ModelState.IsValid) return "Invalid model";
            _db.Entry(course).State = EntityState.Modified;
            _db.SaveChanges();
            return "Updated successfully";
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var course = _db.Courses.Find(id);
            if (course == null)
                return HttpNotFound();
            var serializer = new JavaScriptSerializer();
            ViewBag.SelectedCourse = serializer.Serialize(course);
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        public string Delete(Course course)
        {
            if (course == null) return "Invalid data";
            var getCourse = _db.Courses.Find(course.CourseID);
            _db.Courses.Remove(getCourse);
            _db.SaveChanges();
            return "Deleted successfully";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
