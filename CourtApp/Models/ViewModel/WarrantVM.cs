using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class WarrantVM
    {

        public long WRID { get; set; }
        public string serialNum { get; set; }
        public string WRNUM { get; set; }

        public string WRDAT { get; set; }
 
        public string CASEDATE { get; set; }
        public int COURTID { get; set; }
        public string PREGREF { get; set; }
        public string DISPOSE { get; set; }
        public string courtName { get; set; }
        public virtual COURTINF COURTINF { get; set; }

        public List<WarrantDVM> wRINFPsList { get; set; }

    }
}