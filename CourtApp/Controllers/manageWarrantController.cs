using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CourtApp.halper;
using CourtApp.Models;
using CourtApp.Models.ViewModel;
using Microsoft.Reporting.WinForms;
using static System.Net.WebRequestMethods;

namespace CourtApp.Controllers
{
    public class manageWarrantController : Controller
    {
        private COURTDATABASEEntities db = new COURTDATABASEEntities();

        // GET: manageWarrant
      
        public ActionResult Index()
        {
            commonWork();
            var wRINFs = db.WRINFs.Include(w => w.COURTINF).OrderByDescending(a => a.WRID);
            var i = 1;


            List<WarrantVM> wvmList = new List<WarrantVM>();

            foreach (var wr in wRINFs)
            {
                WarrantVM wvm = new WarrantVM();

                wvm.WRNUM = wr.WRNUM;
                wvm.WRID = wr.WRID;
                string wdt = wr.WRDAT.ToString("dd-MMM-yyyy");
                wvm.WRDAT = convertDate.toBangla(wdt);
                string cdt = wr.CASEDATE.ToString("dd-MMM-yyyy");
                wvm.CASEDATE = convertDate.toBangla(cdt);

                wvm.PREGREF = wr.PREGREF;
                wvm.DISPOSE = wr.DISPOSE;
                wvm.COURTID = wr.COURTID;
                wvm.courtName = wr.COURTINF.COURTNAME;
                wvm.serialNum = convertDate.toBangla(i++.ToString("00")); // Convert serial No

                wvmList.Add(wvm);
            }






            return View(wvmList);


        }

        // GET: manageWarrant/Details/5
      
        public ActionResult Details(long? id)
        {
            commonWork();
            var i = 1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WRINF wRINF = db.WRINFs.Where(a => a.WRID == id).FirstOrDefault();
            if (wRINF == null)
            {
                return HttpNotFound();
            }
            else
            {

                WarrantVM wvm = new WarrantVM();
                wvm.WRID = wRINF.WRID;
                //     wvm.serialNum = DateCovert.DateToBangla(i++.ToString("00"));
                wvm.WRNUM = wRINF.WRNUM;
                string wrdat = wRINF.WRDAT.ToString("dd-MMM-yyyy"); // 10-Oct-2019
                wvm.WRDAT = convertDate.toBangla(wrdat); // ১০-অক্টো-২০১৮ 
                wvm.PREGREF = wRINF.PREGREF;
                wvm.DISPOSE = wRINF.DISPOSE;
                wvm.COURTINF = wRINF.COURTINF;
                string cdt = wRINF.CASEDATE.ToString("dd-MMM-yyyy");
                wvm.CASEDATE = convertDate.toBangla(cdt);

                // create all WRINFP list

                var wrinfp = db.WRINFPs.Where(a => a.WRID == wvm.WRID).Include(a => a.AREAINF).OrderBy(y => y.PSL).ToList();
                List<WarrantDVM> wDVMList = new List<WarrantDVM>();

                foreach (var w in wrinfp)
                {
                    WarrantDVM dVM = new WarrantDVM();

                    dVM.AREAID = w.AREAID;
                    dVM.areaName = w.AREAINF.AREANAM;
                    dVM.PRSADDRESS = w.PRSADDRESS;
                    dVM.PRSNAME = w.PRSNAME;
                    dVM.WRID = w.WRID;
                    dVM.serialNum = convertDate.toBangla(i++.ToString("00"));
                    dVM.PSL = w.PSL;
                    wDVMList.Add(dVM);
                }
                wvm.wRINFPsList = wDVMList;


                return View(wvm);
            }

        }

        // GET: manageWarrant/Create
        public ActionResult Create()
        {
            //ViewBag.COURTID = db.COURTINFs.ToList();
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID","COURTNAME");
            commonWork();
            return View();
        }

        // POST: manageWarrant/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WRINF wRINF)
        {
            if (ModelState.IsValid)
            {
                //wRINF.WRDAT = DateTime.Now;

                //match the WRNUM
                if (matchWRNUM(wRINF.WRNUM))
                {
                    db.WRINFs.Add(wRINF);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = wRINF.WRID });
                }
              
               
            }
            ViewBag.warning = "Warrent Number is Duplicat. Try agine";
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", wRINF.COURTID);
            commonWork();
            return View(wRINF);
        }

        // GET: manageWarrant/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WRINF wRINF = db.WRINFs.Where(a=>a.WRID == id).FirstOrDefault();
            if (wRINF == null)
            {
                return HttpNotFound();
            }
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", wRINF.COURTID);
            commonWork();
            return View(wRINF);
        }

        // POST: manageWarrant/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( WRINF wRINF)
        {
            if (ModelState.IsValid && matchWRNUM(wRINF.WRNUM))
            {
                db.Entry(wRINF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = wRINF.WRID });
            }
            ViewBag.warning = "Warrent Number is Duplicat. Try agine";
            ViewBag.COURTID = new SelectList(db.COURTINFs, "COURTID", "COURTNAME", wRINF.COURTID);
            commonWork();
            return View(wRINF);
        }

        

        // POST: manageWarrant/Delete/5
        [HttpPost]       
        public JsonResult DeleteConfirmed(long id)
        {
            WRINF wRINF = db.WRINFs.Where(a=>a.WRID == id).FirstOrDefault();
            db.WRINFs.Remove(wRINF);
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }


        //GET: manageWarrant/CreateWP/WR234
        [HttpGet]
        public ActionResult CreateWP(int? id) {
            if (id == null)
            {
                return HttpNotFound();
            }
            var w = db.WRINFs.Where(a => a.WRID == id ).FirstOrDefault();
            if (w == null) {
                return HttpNotFound();
            }
            ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM");
            ViewBag.WRNUM = w.WRNUM;
            ViewBag.WRID = w.WRID;
            return PartialView();
        }

        //POST: /manageWarrant/CreateWp/WR234
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWp(WRINFP wrp) {
            if (ModelState.IsValid) {

                wrp.PSL = PSLID(wrp.WRID);

                db.WRINFPs.Add(wrp);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = wrp.WRID});
            }
            var w = db.WRINFs.Where(a => a.WRID == wrp.WRID).FirstOrDefault();
            if (w == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM");
            ViewBag.WRID = w.WRNUM;
            return PartialView(wrp);
        }

        //GET: /manageWarrant/EditWp/1

        [HttpGet]
        public ActionResult EditWp(int? wrid, int? id) {
            if (id == null && wrid == null)
            {
                return HttpNotFound();
            }
            else {
                var WP = db.WRINFPs.Where(p => p.PSL == id && p.WRID == wrid).FirstOrDefault();
                if (WP == null)
                {
                    return HttpNotFound();
                }
                else {
                    ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM", WP.AREAID);
                    ViewBag.wrid = WP.WRID;
                    return PartialView(WP);
                }
            }
        }

        //POST:manageWarrant/EditWp/1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWp(WRINFP wrp) {
            if (ModelState.IsValid) {

                //db.Entry(wrp).State = EntityState.Modified;
                db.UpdateWRP(wrp.WRID,wrp.PSL,wrp.PRSNAME,wrp.PRSADDRESS,wrp.AREAID);
                //db.SaveChanges();
                return RedirectToAction("Details", new { id = wrp.WRID });
            }
            ViewBag.AREAID = new SelectList(db.AREAINFs, "AREAID", "AREANAM", wrp.AREAID);
            return PartialView(wrp);
        }

        //POST: manageWarrant/DeleteWp/1 type JSON
        [HttpPost]
        public JsonResult DeleteWp(int? id) {
            if (id == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else {
                var wp = db.WRINFPs.Where(p => p.PSL == id).FirstOrDefault();
                if (wp == null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.WRINFPs.Remove(wp);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }


        //match the WarrantNumber
        private bool matchWRNUM(string WRNUM) {

            //var w = db.WRINFs.ToList();
            //foreach (var match in w) {
            //    if (match.WRNUM == WRNUM) {
            //        return false;
                    
            //    }
                
            //}
            return true;
        }

        private int PSLID(long wrID) {
            var wr = db.WRINFPs.Where(a => a.WRID == wrID).ToList().Count;
            if (wr > 0)
            {
                
                return wr += 1;

            }
            else {
                return 1;
            }
        }

        public void commonWork()
        {
            ViewBag.action = this.ControllerContext.RouteData.Values["action"].ToString().ToLower();
            ViewBag.controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            ViewBag.TitleText = "ওয়ারেন্ট";
        }
        //create report
       

        public ActionResult report(string ReportType, string Report)
        {

        //Declear some variabls
        
        string[] streams;
        Warning[] warning;
        string reportType = ReportType;
        string mimeType;
        string encoding;
        string fileNameExtention;
            string deviceInfo =
               "<DeviceInfo>" +
               "  <OutputFormat>" + reportType + "</OutputFormat>" +
               "</DeviceInfo>";

            //Create local report object
            LocalReport locaReport = new LocalReport();


        if (Report == "full") {

            locaReport.ReportPath = Server.MapPath("~/Reporting/warrantdl.rdlc");
            ReportDataSource dl = new ReportDataSource();
            dl.Name = "warrantDL";

                //create a list for convert data in bangla
                List<warrantResultVM> wdrl = new List<warrantResultVM>();
            var warantde = db.WarrantWithDetials();

                foreach (var w in warantde) {
                    warrantResultVM wdr = new warrantResultVM();
                    //wdr.psl = w.psl;

                    wdr.psl =convertDate.toBangla(Convert.ToString(w.psl));
                    wdr.pregref = w.pregref;
                    wdr.prsdesc = w.prsdesc;
                    //wdr.slnum = w.slnum;

                    wdr.slnum = convertDate.toBangla(Convert.ToString(w.slnum));
                    wdr.wrdate = convertDate.toBangla(w.wrdate.ToString("dd-MMM-yyyy"));
                    wdr.wrid = w.wrid;
                    wdr.wrnum = w.wrnum;
                    wdr.dispose = w.dispose;
                    wdr.casedate = convertDate.toBangla(w.casedate.ToString("dd-MMM-yyyy"));
                    
                    wdrl.Add(wdr);
                }
                dl.Value = wdrl;
            locaReport.DataSources.Add(dl);

            if (reportType == "pdf")
                {
                fileNameExtention = "pdf";
                    var wrFull = locaReport.Render(reportType, deviceInfo,
                   out mimeType, out encoding, out fileNameExtention, out streams, out warning);
  
                    return File(wrFull, mimeType);

                }
                else  {
                fileNameExtention = "xlsx";

                    var wrFull = locaReport.Render(reportType, null,
                   out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                    Response.AddHeader("content-disposition", "attachment;filename = WarrantWithDetials_report" + DateTime.Now + "." + fileNameExtention);
                    return File(wrFull, fileNameExtention);




                }

                


            }
            else {

           
        locaReport.ReportPath = Server.MapPath("~/Reporting/warrant.rdlc");
        ReportDataSource ds = new ReportDataSource();
        ds.Name = "warrant";

                //this list for convert in bangla
                List<warrantL_Result> wlr = new List<warrantL_Result>();
                var warantl= db.warrantL();
                //assin the data in list

                foreach (var wl in warantl) {
                    warrantL_Result wr = new warrantL_Result();
                    wr.casedate = convertDate.toBangla(wl.casedate);
                    wr.COURTNAME = wl.COURTNAME;
                    wr.DISPOSE = wl.DISPOSE;
                    wr.PREGREF = wl.PREGREF;
                    wr.warrantdate = convertDate.toBangla(wl.warrantdate);
                    wr.WRNUM = wl.WRNUM;
                    wr.SLNum = convertDate.toBangla(wl.SLNum);

                    wlr.Add(wr);
                }


                ds.Value = wlr;            
                locaReport.DataSources.Add(ds);


                var r = locaReport.Render(reportType, deviceInfo,
                           out mimeType,
                           out encoding,
                           out fileNameExtention, out streams, out warning);

                if (reportType == "pdf")
        {
            fileNameExtention = "pdf";
                    var wrFull = locaReport.Render(reportType, deviceInfo,
                         out mimeType, out encoding, out fileNameExtention, out streams, out warning);

                    return File(wrFull, mimeType);
                }
        else 
        {
            fileNameExtention = "xlsx";
                    var wrFull = locaReport.Render(reportType, null,
                   out mimeType, out encoding, out fileNameExtention, out streams, out warning);
                    Response.AddHeader("content-disposition", "attachment;filename = warrant_report" + DateTime.Now + "." + fileNameExtention);
                    return File(r, fileNameExtention);
                }
                
              
            }

        }

        





    }

}
