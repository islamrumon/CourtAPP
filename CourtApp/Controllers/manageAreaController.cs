using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourtApp.Models;

namespace CourtApp.Controllers
{
    public class manageAreaController : Controller
    {
        private COURTDATABASEEntities db = new COURTDATABASEEntities();

        // GET: manageArea
        public ActionResult Index()
        {
            return View(db.AREAINFs.ToList());
        }

       

        // GET: manageArea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manageArea/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AREAINF aREAINF)
        {
            if (ModelState.IsValid)
            {
                db.AREAINFs.Add(aREAINF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aREAINF);
        }

        // GET: manageArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREAINF aREAINF = db.AREAINFs.Find(id);
            if (aREAINF == null)
            {
                return HttpNotFound();
            }
            return View(aREAINF);
        }

        // POST: manageArea/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AREAINF aREAINF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aREAINF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aREAINF);
        }

        // GET: manageArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREAINF aREAINF = db.AREAINFs.Find(id);
            if (aREAINF == null)
            {
                return HttpNotFound();
            }
            return View(aREAINF);
        }

        // POST: manageArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AREAINF aREAINF = db.AREAINFs.Find(id);
            db.AREAINFs.Remove(aREAINF);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
