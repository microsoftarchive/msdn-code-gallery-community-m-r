using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity
{
    public partial class Students1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Student> studentsGrid_GetData([Control] AcademicYear? displayYear)
        {
            SchoolContext db = new SchoolContext();
            var query = db.Students.Include(s => s.Enrollments.Select(e => e.Course));

            if (displayYear != null)
            {
                query = query.Where(s => s.Year == displayYear);
            }

            return query;
        }

        public void studentsGrid_UpdateItem(int studentID)
        {
            using (SchoolContext db = new SchoolContext())
            {
                Student item = null;
                item = db.Students.Find(studentID);
                if (item == null)
                {
                    ModelState.AddModelError("",
                      String.Format("Item with id {0} was not found", studentID));
                    return;
                }

                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
            }
        }

        public void studentsGrid_DeleteItem(int studentID)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var item = new Student { StudentID = studentID };
                db.Entry(item).State = System.Data.EntityState.Deleted;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("",
                      String.Format("Item with id {0} no longer exists in the database.", studentID));
                }
            }
        }
    }
}