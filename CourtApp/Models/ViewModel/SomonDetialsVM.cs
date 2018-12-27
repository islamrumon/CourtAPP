using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class SomonDetialsVM
    {
        public string slnum { get; set; }
        public long? smid { get; set; }
        public string smnum { get; set; }
        public string psl { get; set; }
        public string prsdesc { get; set; }
        public string smdate { get; set; }
        public string casedate { get; set; }
        public string smtype { get; set; }
    }
}