using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class SubCategory
    {
        [Key]
        public int SCID { get; set; }
        public int CID { get; set; }
        public string SubCategory_Name { get; set; }
        public string SCcreationDt { get; set; }
        public string SCcreatedBy { get; set; }
    }
}