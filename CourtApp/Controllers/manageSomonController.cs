using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourtApp.halper;
using CourtApp.Models;
using CourtApp.Models.ViewModel;
using Microsoft.Reporting.WebForms;

namespace CourtApp.Controllers
{
    public class manageSomonController : Controller
    {
        private COURTDATABASEEntities db = new COURTDATABASEEntities();


       
        // GET: manageSomon
        public ActionResult Index()
        {
            var i = 1;
            commonWork();
            //create a list 
            List<SomonVM> svmList = new List<SomonVM>();

            var sMINFs = db.SMINFs.Include(s => s.COURTINF).OrderByDescending(a=>a.SMID);

            //there a lop for add data in list
            foreach (var sm in sMINFs) {
                SomonVM svm = new SomonVM();
                //formet the date 
                string cdt = sm.CASEDATE.ToString("dd-MMM-yyyy");
                svm.CASENUM = convertDate.toBangla(cdt);

                string sdt = sm.SMDAT.ToString("dd-MMM-yyyy");
                svm.SMDAT = convertDate.toBangla(sdt);

                svm.SMID = sm.SMID;
                svm.SMNUM = sm.SMNUM;
                svm.courtName = sm.COURTINF.COURTNAME;
                svm.SerialNum =convertDate.toBangla(i++.ToString("00.")); //00.1
                svmList.Add(svm);
            }
           
            return View(svmList);
        }

        // GET: manageSomon/Details/5
        public ActionResult Details(long? id)
        {
            var i = 1;
            commonWork();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sMINF = db.SMINFs.FirstOrDefault(a => a.SMID == id);
            if (sMINF == null)
            {
                return HttpNotFound();
            }
            else
            {
                SomonVM svm = new SomonVM();
                string cdt = sMINF.CASEDATE.ToString("dd-MMM-yyyy");
                svm.CASENUM =convertDate.toBangla(cdt);
                svm.COURTINF = sMINF.COURTINF;
                svm.SMNUM = sMINF.SMNUM;
                svm.SMID = sMINF.SMID;
                string sdt = sMINF.SMDAT.ToString("dd-MMM-yyyy");
                svm.SMDAT =convertDate.toBangla(sdt);

                //get all somon person detials
                var smp = db.SMINFPs.Where(a => a.SMID == svm.SMID).Include(a => a.AREAINF).OrderByDescending(p => p.PSL).ToList();

                //cretae a list
                List<SumonDVM> sdvmList = new List<SumonDVM>();
               
                foreach (var s in smp) {
                    //create the SommonDVM Object
                    SumonDVM sdvm = new SumonDVM();

                    sdvm.PSL = s.PSL;
                    sdvm.SMTYPE = s.SMTYPE;
                    sdvm.PRSADDRESS = s.PRSADDRESS;
                    sdvm.PRSNAME = s.PRSNAME;
                    sdvm.SMID = s.SMID;
                    sdvm.AreaName = s.AREAINF.AREANAM;
                    sdvm.SerialNum = convertDate.toBangla(i++.ToString("00."));
                    sdvmList.Add(sdvm);

                }

                svm.SomonPList = sdvmList;
               
                return View(svm);
            }

        }

        // GET: manageSomon/Create
        public ActionResult Create()
        {
            commonWork();
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME");
            return View();
        }

        // POST: manageSomon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SMINF sMINF)
        {
            if (ModelState.IsValid)
            {
                if (matchSMNUM(sMINF.SMNUM)) {
                    db.SMINFs.Add(sMINF);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = sMINF.SMID });
                }
                
             
            }
            ViewBag.warning = "Somon Number is Duplicat. Try agine";
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", sMINF.COURTID);
            commonWork();
            return View(sMINF);
        }

        // GET: manageSomon/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMINF sMINF = db.SMINFs.FirstOrDefault(a=>a.SMID == id);
            if (sMINF == null)
            {
                return HttpNotFound();
            }
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", sMINF.COURTID);
            commonWork();
            return View(sMINF);
        }

        // POST: manageSomon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SMINF sMINF)
        {
            if (ModelState.IsValid && matchSMNUM(sMINF.SMNUM))
            {
                db.Entry(sMINF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = sMINF.SMID });
            }
            ViewBag.warning = "Somon Number is Duplicat. Try agine";
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", sMINF.COURTID);
            commonWork();
            return View(sMINF);
        }

        // GET: manageSomon/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMINF sMINF = db.SMINFs.Find(id);
            if (sMINF == null)
            {
                return HttpNotFound();
            }
            return View(sMINF);
        }

        // POST: manageSomon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SMINF sMINF = db.SMINFs.Find(id);
            db.SMINFs.Remove(sMINF);
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


        [HttpGet]
        public ActionResult CreateSp(int? id) {
            if (id == null)
            {
                return HttpNotFound();
            }
            var s = db.SMINFs.Where(a => a.SMID == id).FirstOrDefault();
            if (s == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM");
            ViewBag.SMNUM = s.SMNUM;
            ViewBag.SMID = s.SMID;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSp(SMINFP smp) {
            if (ModelState.IsValid)
            {
                smp.PSL = PSLID(smp.SMID);
                db.SMINFPs.Add(smp);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = smp.SMID });
            }
            else {
                var s = db.SMINFs.Where(a => a.SMID == smp.SMID).FirstOrDefault();
                if (s == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM");
                ViewBag.WRID = s.SMNUM;
                return PartialView(s);

            }
        }

        [HttpGet]
        public ActionResult EditSp(int? srid, int? id) {
            if (id == null && srid == null)
            {
                return HttpNotFound();
            }
            else
            {
                var WP = db.SMINFPs.Where(p => p.PSL == id && p.SMID == srid).FirstOrDefault();
                if (WP == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM", WP.AREAID);
                    ViewBag.wrid = WP.SMID;
                    return PartialView(WP);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSp(SMINFP smp) {
            if (ModelState.IsValid)
            {
                db.UpdateSMP(smp.SMID, smp.PSL, smp.PRSNAME, smp.PRSADDRESS, smp.AREAID, smp.SMTYPE);
                return RedirectToAction("Details", new { id = smp.SMID });
            }
            else {
                ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM", smp.AREAID);
                ViewBag.wrid = smp.SMID;
                return PartialView(smp);
            }
        }


        //match the WarrantNumber
        private bool matchSMNUM(string SMNUM)
        {

            //var s = db.SMINFs.ToList();
            //foreach (var match in s)
            //{
            //    if (match.SMNUM == SMNUM)
            //    {
            //        return false;

            //    }

            //}
            return true;
        }

        private int PSLID(long smID)
        {
            var wr = db.SMINFPs.Where(a => a.SMID == smID).ToList().Count;
            if (wr > 0)
            {

                return wr += 1;

            }
            else
            {
                return 1;
            }
        }

        //this function for commonWork
        public void commonWork()
        {
            ViewBag.action = this.ControllerContext.RouteData.Values["action"].ToString().ToLower();
            ViewBag.controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            ViewBag.TitleText = "সমন";
        }

        //this is reporting
        public ActionResult report(string ReportType, string Report) {


            LocalReport localreport = new LocalReport();
           
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string[] streams;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + reportType + "</OutputFormat>" +
                "</DeviceInfo>";
            Warning[] warning;
            string fileNameExtention;

            //this condition for createing full or hafe report
            if (Report == "full")
            {
                //there initilage report dataset

                
                localreport.ReportPath = Server.MapPath("~/Reporting/SomonDetails.rdlc");

                ReportDataSource ds1 = new ReportDataSource();
                ds1.Name = "SomonDetails";
                List<SomonDetialsVM> sdvmList = new List<SomonDetialsVM>();

                var data = db.SomonWithDetials();
                foreach (var d in data) {
                    SomonDetialsVM sdvm = new SomonDetialsVM();
                    sdvm.casedate = convertDate.toBangla(d.casedate.ToString("dd-MMM-yyyy"));
                    sdvm.smdate = convertDate.toBangla(d.smdate.ToString("dd-MMM-yyyy"));
                    sdvm.prsdesc = d.prsdesc;
                    sdvm.psl = convertDate.toBangla(Convert.ToString(d.psl));
                    sdvm.slnum = convertDate.toBangla(Convert.ToString(d.slnum));
                    sdvm.smnum = d.smnum;
                    sdvm.smtype = d.smtype;
                    sdvm.smid = d.smid;

                    sdvmList.Add(sdvm);
                }
                ds1.Value = sdvmList;

                localreport.DataSources.Add(ds1);
                if (reportType == "pdf")
                {
                   fileNameExtention = "pdf";
                    var r = localreport.Render(reportType, deviceInfo,
                   out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                    //Response.AddHeader("content-disposition", "attachment;filename = somon_report" + DateTime.Today + "." + fileNameExtention);
                    return File(r, mimeType);
                }
                else 
                {
                    fileNameExtention = "xlsx";

                    var r = localreport.Render(reportType, null,
                  out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                    Response.AddHeader("content-disposition", "attachment;filename = somon_report" + DateTime.Today + "." + fileNameExtention);
                    return File(r, fileNameExtention);
                }
               

               

            }
            else {




                //this is hafe report
         

                localreport.ReportPath = Server.MapPath("~/Reporting/SomonListr.rdlc");
                ReportDataSource ds = new ReportDataSource();
                ds.Name = "somonL";
                List<SommonList_Result> sl = new List<SommonList_Result>();

                var t = db.SommonList();
                foreach (var st in t)
                {
                    SommonList_Result sr = new SommonList_Result();
                    sr.CaseDate = convertDate.toBangla(st.CaseDate);
                    sr.SomonDate = convertDate.toBangla(st.SomonDate);
                    //there are a problem
                    //sr.SlNum = convertDate.toBangla(st.SlNum.ToString().Trim());
                    sr.SlNum = convertDate.toBangla(st.SlNum);
                    sr.SMID = st.SMID;
                    sr.SMNUM = st.SMNUM;
                    sr.COURTNAME = st.COURTNAME;

                    sl.Add(sr);
                }
                ds.Value = sl;

                localreport.DataSources.Add(ds);

                //there are condition
                if (reportType == "pdf")
                {
                    fileNameExtention = "pdf";
                    var r = localreport.Render(reportType, deviceInfo,
                  out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                   
                    return File(r, mimeType);
                }
                else 
                {
                    fileNameExtention = "xlsx";

                    var r = localreport.Render(reportType, null,
                   out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                    Response.AddHeader("content-disposition", "attachment;filename = somon_report" + DateTime.Today + "." + fileNameExtention);
                    return File(r, fileNameExtention);
                }
               

 
            }
           

        }
        
        

    }
}
