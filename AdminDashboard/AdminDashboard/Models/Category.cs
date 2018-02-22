using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models
{
    public class Category
    {
        [Key]
        public int CID { get; set; }
        public int BID { get; set; }
        public string Category_Name { get; set; }
        public string Cate_Des { get; set; }
        public string Iconurl { get; set; }
        public string Imgurl { get; set; }
    }
    public class SubCateRel
    {
        public int CID { get; set; }
        public string Category_Name { get; set; }
        public List<SCRel> SCrel { get; set; }
    }
    public class SCRel
    {
        public int CCID { get; set; }
        public int SCID { get; set; }
        public int CID { get; set; }
        public string SubCategory_Name { get; set; }
        public string SubCate_Des { get; set; }
        public string ImgUrl { get; set; }
        public string IconUrl { get; set; }

    }
}