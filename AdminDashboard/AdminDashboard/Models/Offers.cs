using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class Offers
    {
        [Key]
        public int OID { get; set; }
        public string Offer_Name { get; set; }
        public string ImgUrl { get; set; }
        public string IconUrl { get; set; }
        public string DefaultUrl { get; set; }
        public string Offer_Des { get; set; }
        public string Offer_Type { get; set; }
        public string Offer_on { get; set; }
        public string Offer_onID { get; set; }
        public string Offer_Dis { get; set; }

    }
}