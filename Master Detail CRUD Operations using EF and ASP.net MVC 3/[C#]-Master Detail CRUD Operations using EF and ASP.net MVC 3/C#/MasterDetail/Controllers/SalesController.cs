using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterDetail.Models;
using System.Web.Helpers;
using System.Data.Objects;

namespace MasterDetail.Controllers
{ 
    public class SalesController : Controller
    {
        private MasterDetailContext db = new MasterDetailContext();

        //
        // GET: /Sales/
        public ViewResult Index()
        {
            return View(db.SalesMains.ToList());
        }

        //
        // GET: /Sales/Details/5

        public ViewResult Details(int id)
        {
            SalesMain salesmain = db.SalesMains.Find(id);
            return View(salesmain);
        }

        //
        // GET: /Sales/Create

        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        } 

        
        
        // POST: /Sales/Create
        /// <summary>
        /// This method is used for Creating and Updating  Sales Information 
        /// (Sales Contains: 1.SalesMain and *SalesSub )
        /// </summary>
        /// <param name="salesmain">
        /// </param>
        /// <returns>
        /// Returns Json data Containing Success Status, New Sales ID and Exeception 
        /// </returns>
        [HttpPost]
        public JsonResult Create(SalesMain salesmain)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // If sales main has SalesID then we can understand we have existing sales Information
                    // So we need to Perform Update Operation
                    
                    // Perform Update
                    if (salesmain.SalesId > 0)
                    {

                        var CurrentsalesSUb = db.SalesSubs.Where(p => p.SalesId == salesmain.SalesId);

                        foreach (SalesSub ss in CurrentsalesSUb)
                            db.SalesSubs.Remove(ss);

                        foreach (SalesSub ss in salesmain.SalesSubs)
                            db.SalesSubs.Add(ss);
                        
                        db.Entry(salesmain).State = EntityState.Modified;
                    }
                    //Perform Save
                    else
                    {
                        db.SalesMains.Add(salesmain);
                    }

                    db.SaveChanges();

                    // If Sucess== 1 then Save/Update Successfull else there it has Exception
                    return Json(new { Success = 1, SalesID = salesmain.SalesId, ex="" });
                }
            }
            catch (Exception ex) 
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }
            
            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }
        
        //
        // GET: /Sales/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
            SalesMain salesmain = db.SalesMains.Find(id);

            //Call Create View
            return View("Create", salesmain);
        }

        
        

        
        // GET: /Sales/Delete/5
        public ActionResult Delete(int id)
        {
            SalesMain salesmain = db.SalesMains.Find(id);
            return View(salesmain);
        }

        


        // POST: /Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SalesMain salesmain = db.SalesMains.Find(id);
            db.SalesMains.Remove(salesmain);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}