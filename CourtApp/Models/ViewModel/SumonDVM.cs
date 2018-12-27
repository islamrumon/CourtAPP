using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class SumonDVM
    {
        public string SerialNum { get; set; }
        public long SMID { get; set; }
        public long PSL { get; set; }
        public string PRSNAME { get; set; }
        public string PRSADDRESS { get; set; }
        public int AREAID { get; set; }
        public string SMTYPE { get; set; }

        public string AreaName { get; set; }
        
    }
}