using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class Vendor
    {
        [Key]
        public int VID { get; set; }
        public string Vendor_Name { get; set; }
        public string Vendor_Des { get; set; }
        public List<Vendor> vlist { get; set; }
    }
}