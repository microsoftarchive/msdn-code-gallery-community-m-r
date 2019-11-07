using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContosoUniversity
{
    public partial class AddStudent1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void addStudentForm_InsertItem()
        {
            var item = new Student();

            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                using (SchoolContext db = new SchoolContext())
                {
                    db.Students.Add(item);
                    db.SaveChanges();
                }
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Students1.aspx");
        }

        protected void addStudentForm_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Redirect("~/Students1.aspx");
        }
    }
}