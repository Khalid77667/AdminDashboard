using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class Brand
    {
        [Key]
        public int BID { get; set; }
        public int VID { get; set; }
        public string Brand_Name { get; set; }
        public string Iconurl { get; set; }
        public string Imgurl { get; set; }
        public string Brand_Des { get; set; }

    }
}