using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class BrandCateRel
    {
        public int BID { get; set; }
        public string Brand_Name { get; set; }
        public List<BCRel> bcrel { get; set; }
    }
    public class BCRel
    {
        public int BCID { get; set; }
        public int CID { get; set; }
        public int BID { get; set; }
        public string Category_Name { get; set; }
        public string Cate_Des { get; set; }
        public string ImgUrl { get; set; }
        public string IconUrl { get; set; }

    }
}