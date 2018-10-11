using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using transfer_centre.Models;
using System.Data.Entity;
using System.Net;

namespace transfer_centre.Controllers
{
    public class HomeController : Controller
    {
        private transfer_centreEntities db = new transfer_centreEntities();

        public ActionResult Index(string searchString)
        {
            var transfers = from m in db.transfers select m;
            if(!string.IsNullOrEmpty(searchString))
            {
                transfers = transfers.Where(x => x.Name.Contains(searchString));
            }
            return View(transfers);
        }
        public ActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(transfer Transfer)
        {
            if (ModelState.IsValid)
            {
                db.transfers.Add(Transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Transfer);
        }
    

        public ActionResult Edit(int ID)
        {
            
            transfer transfer = db.transfers.Find(ID);
            return View(transfer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(transfer Transfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Transfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Transfer);
        }
        public ActionResult Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transfer transfer = db.transfers.Find(ID);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }
        public ActionResult Delete(int ID)
        {
            transfer transfer = db.transfers.Find(ID);

            return View(transfer);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            transfer transfer = db.transfers.Find(ID);
            db.transfers.Remove(transfer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
     

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}