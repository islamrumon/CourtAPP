using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class SomonVM
    {
     
        public string SerialNum { get; set; }
        public long SMID { get; set; }
        public string SMNUM { get; set; }
     
        public string SMDAT { get; set; }
       
        public string CASENUM { get; set; }
        public int COURTID { get; set; }
        public string courtName { get; set; }
        public virtual COURTINF COURTINF { get; set; }

        public List<SumonDVM> SomonPList { get; set; }
    }
}