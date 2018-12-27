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
    public class manageCourtController : Controller
    {
        private COURTDATABASEEntities db = new COURTDATABASEEntities();

        // GET: manageCourt
        public ActionResult Index()
        {
            return View(db.COURTINFs.ToList());
        }


        // GET: manageCourt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manageCourt/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( COURTINF cOURTINF)
        {
            if (ModelState.IsValid)
            {
                db.COURTINFs.Add(cOURTINF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOURTINF);
        }

        // GET: manageCourt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURTINF cOURTINF = db.COURTINFs.Find(id);
            if (cOURTINF == null)
            {
                return HttpNotFound();
            }
            return View(cOURTINF);
        }

        // POST: manageCourt/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( COURTINF cOURTINF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOURTINF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOURTINF);
        }

        // GET: manageCourt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURTINF cOURTINF = db.COURTINFs.Find(id);
            if (cOURTINF == null)
            {
                return HttpNotFound();
            }
            return View(cOURTINF);
        }

        // POST: manageCourt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COURTINF cOURTINF = db.COURTINFs.Find(id);
            db.COURTINFs.Remove(cOURTINF);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
