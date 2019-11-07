namespace MVCEF3TCS.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MVCEF3TCS.Business;
    using MVCEF3TCS.Entities;
        
    public class ProductController : Controller
    {
        private ProductManager productManager = new ProductManager();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var productlist = this.productManager.FindAll();

            return View(productlist.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = this.productManager.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(this.productManager.CategoryList, "Id", "Description");

            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                this.productManager.Save(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(this.productManager.CategoryList, "Id", "Description");
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = this.productManager.Find(id);

            if (product == null)
            {
                return HttpNotFound();            
            }

            ViewBag.CategoryId = new SelectList(this.productManager.CategoryList, "Id", "Description");
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                this.productManager.Modify(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(this.productManager.CategoryList, "Id", "Description");
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = this.productManager.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            this.productManager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.productManager.Dispose();
            base.Dispose(disposing);
        }
    }
}