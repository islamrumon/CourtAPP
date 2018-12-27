using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourtApp.Models.ViewModel
{
    public class WarrantDVM
    {
        public long WRID { get; set; }

        public string serialNum { get; set; }
        public int PSL { get; set; }
        public string PRSNAME { get; set; }
        public string PRSADDRESS { get; set; }
        public int AREAID { get; set; }

        public virtual AREAINF AREAINF { get; set; }

        public string areaName { get; set; }
    }
}