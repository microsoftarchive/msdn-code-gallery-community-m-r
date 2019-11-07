using Microsoft.WindowsAzure.ActiveDirectory;
using System.Web.Mvc;


namespace TaskTracker
{
    public class TaskTrackerController : Controller
    {
        //Display app roles
        [HttpGet]
        [Authorize(Roles = "Admin, Observer, Writer, Approver")]
        public ActionResult TaskTracker()
        {
            ViewBag.Message = "TaskTracker";
            ViewData["tasks"] = XmlHelper.GetTaskElemsFromXml();
            return View();
        }

        //Change app roles
        [HttpPost]
        [Authorize(Roles = "Admin, Writer, Approver")]
        public ActionResult TaskSubmit (FormCollection formCollection)
        {
            ActionResult result = RedirectToAction("TaskTracker", "TaskTracker");
            if (User.IsInRole("Admin") || User.IsInRole("Writer"))
            {
                //add new task
                if (formCollection["newTask"] != null && formCollection["newTask"].Length != 0)
                {
                    XmlHelper.AppendTaskElemToXml(formCollection);
                }
            }

            if (User.IsInRole("Admin") || User.IsInRole("Approver"))
            {
                //change status of existing task
                XmlHelper.ChangeTaskAttribute(formCollection);
            }

            return result;
        }
    }
}
