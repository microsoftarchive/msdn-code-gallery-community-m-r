using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace ContosoUniversity
{
    public partial class Courses1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Enrollment> coursesGrid_GetData([QueryString] int? studentID)
        {
            SchoolContext db = new SchoolContext();
            var query = db.Enrollments.Include(e => e.Course)
                .Where(e => e.StudentID == studentID);
            return query;
        }
    }
}