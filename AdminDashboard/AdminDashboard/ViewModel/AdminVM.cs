using AdminDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminDashboard.ViewModel
{
    public class AdminVM
    {
        public List<Vendor> vendorlist { get; set; }
        public List<Brand> brandlist { get; set; }
        public List<Category> categorylist { get; set; }
        public List<SubCategory> subcategorylist { get; set; }
        public List<Product> productlist { get; set; }
    }
}