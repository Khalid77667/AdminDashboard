using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class Product
    {
        [Key]
        public int PID { get; set; }
        public int VID { get; set; }
        public int BID { get; set; }
        public int CID { get; set; }
        public int SCID { get; set; }
      //  public string Product_code { get; set; }
        public string Product_Name { get; set; }
        public string Psize { get; set; }
        public string Product_Dec { get; set; }
        //public string Product_Quantity { get; set; }
        public string Product_Price { get; set; }
        public string ImgUrl { get; set; }
        public string IconUrl { get; set; }
        public string SquareUrl { get; set; }

    }
}