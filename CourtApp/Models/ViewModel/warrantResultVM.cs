using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class warrantResultVM
    {
        public string  slnum { get; set; }
        public long? wrid { get; set; }
        public string psl { get; set; }
        public string wrnum { get; set; }
        public string wrdate { get; set; }
        public string casedate { get; set; }
        public string prsdesc { get; set; }
        public string courtid { get; set; }
      
        public string pregref { get; set; }
        public string dispose { get; set; }
    }
}