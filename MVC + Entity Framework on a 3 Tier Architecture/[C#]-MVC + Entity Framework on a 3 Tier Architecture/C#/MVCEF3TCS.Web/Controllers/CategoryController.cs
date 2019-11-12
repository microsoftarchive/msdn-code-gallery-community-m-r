namespace MVCEF3TCS.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MVCEF3TCS.Business;
    using MVCEF3TCS.Entities;
    
    public class CategoryController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(this.categoryManager.FindAll().ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = this.categoryManager.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                this.categoryManager.Save(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = this.categoryManager.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                this.categoryManager.Modify(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = this.categoryManager.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.categoryManager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.categoryManager.Dispose();
            base.Dispose(disposing);
        }
    }
}