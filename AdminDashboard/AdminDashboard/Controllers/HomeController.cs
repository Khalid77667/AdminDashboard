using AdminDashboard.DAL;
using AdminDashboard.Models;
using AdminDashboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    public class HomeController : Controller
    {
        SqlHelper dal = new SqlHelper();
        public ActionResult Index()
        {

            DataTable dt = dal.GetData("select count(VID) from YOL.VendorMaster");
            if (dt.Rows.Count > 0)
            {
                ViewBag.Vendorcnt = Convert.ToString(dt.Rows[0][0]);
            }
            else
            { ViewBag.Vendorcnt = 0; }


            dt = dal.GetData("select count(PID) from YOL.ProductMaster");
            if (dt.Rows.Count > 0)
            {
                ViewBag.Prodcnt = Convert.ToString(dt.Rows[0][0]);
            }
            else
            { ViewBag.Prodcnt = 0; }


            dt = dal.GetData("select count(BID) from YOL.BrandMaster");
            if (dt.Rows.Count > 0)
            {
                ViewBag.Brandcnt = Convert.ToString(dt.Rows[0][0]);
            }
            else
            { ViewBag.Brandcnt = 0; }

            dt = dal.GetData("select count(CID) from YOL.CategoryMaster");
            if (dt.Rows.Count > 0)
            {
                ViewBag.Catecnt = Convert.ToString(dt.Rows[0][0]);
            }
            else
            { ViewBag.Catecnt = 0; }

            return View();
        }

        [HttpGet]
        public ActionResult Vendor()
        {

            List<Vendor> v = new List<Vendor>();

            DataTable list = dal.GetData("exec YOL.Sp_selVendor");
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Vendor obj = new Vendor();
                    obj.VID = Convert.ToInt32(list.Rows[i]["VID"]);
                    obj.Vendor_Name = Convert.ToString(list.Rows[i]["Vendor_Name"]);
                    obj.Vendor_Des = Convert.ToString(list.Rows[i]["Vendor_Des"]);
                    v.Add(obj);
                }
            }

            return View(v);
        }


        [HttpPost]
        public ActionResult Vendor(Vendor v, string IUDFlag)
        {
            if (IUDFlag == "N")
            {
                DataTable vendor = dal.GetData(" exec YOL.SP_InsVendormaster " + v.VID + ",'" + v.Vendor_Name + "','','','" + v.Vendor_Des + "'");
            }
            else if (IUDFlag == "U")
            {
                DataTable vendor = dal.GetData(" exec YOL.SP_InsVendormaster " + v.VID + ",'" + v.Vendor_Name + "','','','" + v.Vendor_Des + "'");
            }
            else if (IUDFlag == "D")
            {
                int vendor = dal.executeQury(" exec YOL.Sp_delVendor " + v.VID);
            }


            return RedirectToAction("Vendor");
        }

        [HttpGet]
        public ActionResult Brand()
        {

            List<Brand> b = new List<Brand>();

            DataTable brandlist = dal.GetData("exec YOL.Sp_selBrand");
            if (brandlist.Rows.Count > 0)
            {
                for (int i = 0; i < brandlist.Rows.Count; i++)
                {
                    Brand obj = new Brand();
                    obj.BID = Convert.ToInt32(brandlist.Rows[i]["BID"]);
                    obj.Brand_Name = Convert.ToString(brandlist.Rows[i]["Brand_Name"]);

                    obj.Brand_Des = Convert.ToString(brandlist.Rows[i]["Brand_Des"]);
                    b.Add(obj);
                }
            }
            return View(b);
        }


        [HttpPost]
        public ActionResult Brand(Brand b, string IUDFlag)
        {
            //string VID = Request.Form["VENDOR_NAME"].ToString();
            if (IUDFlag == "N")
            {
                DataTable brand = dal.GetData(" exec YOL.SP_InsBrandmaster " + Convert.ToString(b.BID) + ",'" + Convert.ToString(b.Brand_Name) + "'," + Convert.ToString(b.VID) + ",'','','" + Convert.ToString(b.Iconurl) + "','" + Convert.ToString(b.Imgurl) + "','" + Convert.ToString(b.Brand_Des) + "'");
            }
            else if (IUDFlag == "U")
            {
                DataTable brand = dal.GetData(" exec YOL.SP_InsBrandmaster " + Convert.ToString(b.BID) + ",'" + Convert.ToString(b.Brand_Name) + "'," + Convert.ToString(b.VID) + ",'','','" + Convert.ToString(b.Iconurl) + "','" + Convert.ToString(b.Imgurl) + "','" + Convert.ToString(b.Brand_Des) + "'");
            }
            else if (IUDFlag == "D")
            {
                int brand = dal.executeQury(" exec YOL.Sp_delBrand " + Convert.ToString(b.BID));
            }

            return RedirectToAction("Brand");
        }

        [HttpGet]
        public ActionResult Category()
        {

            List<BrandCateRel> c = new List<BrandCateRel>();
            DataTable categorylist = dal.GetData("exec [YOL].[Sp_selBrand]");
            if (categorylist.Rows.Count > 0)
            {
                for (int i = 0; i < categorylist.Rows.Count; i++)
                {
                    BrandCateRel obj = new BrandCateRel();
                    obj.BID = Convert.ToInt32(categorylist.Rows[i]["BID"]);
                    obj.Brand_Name = Convert.ToString(categorylist.Rows[i]["Brand_Name"]);

                    List<BCRel> bcrel = new List<BCRel>();

                    DataTable dt = GetBrandcaterel(obj.BID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            BCRel br = new BCRel();
                            br.BCID = Convert.ToInt32(dr["BCID"]);
                            br.CID = Convert.ToInt32(dr["CID"]);
                            br.Category_Name = Convert.ToString(dr["Category_Name"]);
                            br.Cate_Des = Convert.ToString(dr["Cate_Des"]);

                            bcrel.Add(br);
                        }
                    }
                    obj.bcrel = bcrel;


                    c.Add(obj);
                }
            }
            return View(c);
        }

        public DataTable GetBrandcaterel(int BID)
        {

            return dal.GetData("exec YOL.Sp_GetBrandCateRel " + BID);
        }
        public DataTable GetSubcaterel(int CID)
        {
            return dal.GetData("exec [YOL].[Sp_GetSubCateRel]" + CID);
        }
        [HttpPost]
        public ActionResult Category(BCRel c, string IUDFlag)
        {
            //string BID = Request.Form["BRAND_NAME"].ToString();
            if (IUDFlag == "N")
            {
                DataTable cat = dal.GetData(" exec YOL.SP_InsCategorydmaster " + c.CID + ",'" + c.Category_Name + "','','','" + c.ImgUrl + "','" + c.IconUrl + "','" + c.Cate_Des + "'");
                if (cat.Rows.Count > 0)
                {
                    if (Convert.ToInt32(cat.Rows[0][0]) > 0)
                    {
                        DataTable brand = dal.GetData(" exec YOL.SP_InsBrandCateRel " + c.BCID + "," + Convert.ToInt32(cat.Rows[0][0]) + "," + c.BID + "");
                    }
                }
            }
            else if (IUDFlag == "U")
            {
                DataTable cat = dal.GetData(" exec YOL.SP_InsCategorydmaster " + c.CID + ",'" + c.Category_Name + "','','','" + c.ImgUrl + "','" + c.IconUrl + "','" + c.Cate_Des + "'");
                if (cat.Rows.Count > 0)
                {
                    if (Convert.ToInt32(cat.Rows[0][0]) > 0)
                    {
                        DataTable brand = dal.GetData(" exec YOL.SP_InsBrandCateRel " + c.BCID + "," + Convert.ToInt32(cat.Rows[0][0]) + "," + c.BID + "");
                    }
                }
            }
            else if (IUDFlag == "D")
            {
                int brand = dal.executeQury(" exec YOL.sp_delBrandCaterel " + c.BCID + "," + c.CID);
            }

            return RedirectToAction("Category");
        }

        [HttpGet]
        public ActionResult SubCategory()
        {
            List<SubCateRel> c = new List<SubCateRel>();
            DataTable categorylist = dal.GetData("exec [YOL].[Sp_selCategory]");
            if (categorylist.Rows.Count > 0)
            {
                for (int i = 0; i < categorylist.Rows.Count; i++)
                {
                    SubCateRel obj = new SubCateRel();
                    obj.CID = Convert.ToInt32(categorylist.Rows[i]["CID"]);
                    obj.Category_Name = Convert.ToString(categorylist.Rows[i]["Category_Name"]);

                    List<SCRel> bcrel = new List<SCRel>();

                    DataTable dt = GetSubcaterel(obj.CID);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            SCRel br = new SCRel();
                            br.CCID = Convert.ToInt32(dr["CCID"]);
                            br.SCID = Convert.ToInt32(dr["SCID"]);
                            br.SubCategory_Name = Convert.ToString(dr["SubCategory_Name"]);
                            br.SubCate_Des = Convert.ToString(dr["SubCate_Des"]);

                            bcrel.Add(br);
                        }
                    }
                    obj.SCrel = bcrel;


                    c.Add(obj);
                }
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult Subcategory(SCRel sc, string IUDFlag)
        {
            if (IUDFlag == "N")
            {
                DataTable cat = dal.GetData(" exec YOL.SP_InsSubCategorydmaster " + sc.SCID + ",'" + sc.SubCategory_Name + "','','','" + sc.ImgUrl + "','" + sc.IconUrl + "','" + sc.SubCate_Des + "'");
                if (cat.Rows.Count > 0)
                {
                    if (Convert.ToInt32(cat.Rows[0][0]) > 0)
                    {
                        DataTable brand = dal.GetData(" exec YOL.SP_InsSubCateRel " + sc.CCID + "," + Convert.ToInt32(cat.Rows[0][0]) + "," + sc.CID + "");
                    }
                }
            }
            else if (IUDFlag == "U")
            {
                DataTable cat = dal.GetData(" exec YOL.SP_InsSubCategorydmaster " + sc.SCID + ",'" + sc.SubCategory_Name + "','','','" + sc.ImgUrl + "','" + sc.IconUrl + "','" + sc.SubCate_Des + "'");
                if (cat.Rows.Count > 0)
                {
                    if (Convert.ToInt32(cat.Rows[0][0]) > 0)
                    {
                        DataTable brand = dal.GetData(" exec YOL.SP_InsSubCateRel " + sc.CCID + "," + Convert.ToInt32(cat.Rows[0][0]) + "," + sc.CID + "");
                    }
                }
            }
            else if (IUDFlag == "D")
            {
                int brand = dal.executeQury(" exec YOL.sp_delSubCaterel " + sc.CCID + "," + sc.SCID);
            }
            return RedirectToAction("SubCategory");
        }

        [HttpGet]
        public ActionResult Product()
        {
            List<Product> p = new List<Product>();
            DataTable productlist = dal.GetData("exec YOL.Sp_selProduct");
            if (productlist.Rows.Count > 0)
            {
                for (int i = 0; i < productlist.Rows.Count; i++)
                {
                    Product obj = new Product();
                    obj.PID = Convert.ToInt32(productlist.Rows[i]["PID"]);
                    obj.Product_Name = Convert.ToString(productlist.Rows[i]["Product_Name"]);
                    p.Add(obj);
                }
            }
            return View(p);
        }

        [HttpPost]
        public ActionResult Product(Product p, String IUDFlag)
        {
            //string SCID = Request.Form["SUBCATEGORY_NAME"].ToString();
            if (IUDFlag == "N")
            {
                DataTable brand = dal.GetData(" exec YOL.SP_InsProductmaster @PID=" + p.PID + ",@Product_code='',@Product_Name='" + p.Product_Name + "',@Psize='" + p.Psize + "',@Product_Quantity='',@Product_Dec='" + p.Product_Dec + "',@CID=" + p.CID + ",@BID=" + p.BID + ",@SCID=" + p.SCID + ",@VID=" + p.VID + ",@Product_Price='" + p.Product_Price + "',@ImgUrl='" + p.ImgUrl + "',@IconUrl='" + p.IconUrl + "',@SquareUrl='" + p.SquareUrl + "',@PcreationDt='',@PcreatedBy=''");
            }
            else if (IUDFlag == "U")
            {
                DataTable brand = dal.GetData(" exec YOL.SP_InsProductmaster @PID=" + p.PID + ",@Product_code='',@Product_Name='" + p.Product_Name + "',@Psize='" + p.Psize + "',@Product_Quantity='',@Product_Dec='" + p.Product_Dec + "',@CID=" + p.CID + ",@BID=" + p.BID + ",@SCID=" + p.SCID + ",@VID=" + p.VID + ",@Product_Price='" + p.Product_Price + "',@ImgUrl='" + p.ImgUrl + "',@IconUrl='" + p.IconUrl + "',@SquareUrl='" + p.SquareUrl + "',@PcreationDt='',@PcreatedBy=''");
            }
            else if (IUDFlag == "D")
            {
                int brand = dal.executeQury(" exec YOL.Sp_delProduct " + p.PID);
            }

            return RedirectToAction("Product");
        }


        public ActionResult VendorDetail(string IUDFlag, string VID)
        {
            Vendor v = new Vendor();

            ViewBag.Flag = IUDFlag;
            if (IUDFlag == "N")
            {
                //v.Vendor_Name= Convert.ToString("Vendor_Name");
                ViewBag.Action = "Save";

            }
            else if (IUDFlag == "U")
            {
                // get data from db depend on VID and assign VID and Vendorname to vendor obj 

                DataTable dt = dal.GetData("exec YOL.sp_selVendorName " + VID);

                if (dt.Rows.Count > 0)
                {
                    v.Vendor_Name = Convert.ToString(dt.Rows[0]["Vendor_Name"]);
                    v.VID = Convert.ToInt32(VID);
                    v.Vendor_Des = Convert.ToString(dt.Rows[0]["Vendor_Des"]);
                }



                ViewBag.Action = "Update";
            }

            else if (IUDFlag == "D")
            {
                // get data from db depend on VID and assign VID and Vendorname to vendor obj 
                DataTable dt = dal.GetData("exec YOL.sp_selVendorName " + VID);

                if (dt.Rows.Count > 0)
                {
                    v.Vendor_Name = Convert.ToString(dt.Rows[0]["Vendor_Name"]);
                    v.VID = Convert.ToInt32(VID);
                    v.Vendor_Des = Convert.ToString(dt.Rows[0]["Vendor_Des"]);
                }
                ViewBag.Action = "Delete";
            }

            return View(v);
        }


        public ActionResult BrandDetail(string IUDFlag, string BID)
        {
            Brand b = new Brand();

            ViewBag.Flag = IUDFlag;
            List<Vendor> v = new List<Vendor>();
            DataTable list = dal.GetData("exec YOL.Sp_selVendor");
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Vendor obj = new Vendor();
                    obj.VID = Convert.ToInt32(list.Rows[i]["VID"]);
                    obj.Vendor_Name = Convert.ToString(list.Rows[i]["Vendor_Name"]);
                    v.Add(obj);
                }
            }
            ViewBag.Vendor = v;

            if (IUDFlag == "N")
            {
                //v.Vendor_Name= Convert.ToString("Vendor_Name");
                ViewBag.Action = "Save";
            }

            else if (IUDFlag == "U")
            {
                // get data from db depend on BID and assign BID and Brandname to brand obj 

                DataTable dt = dal.GetData("exec YOL.sp_selBrandName " + BID);

                if (dt.Rows.Count > 0)
                {
                    b.Brand_Name = Convert.ToString(dt.Rows[0]["Brand_Name"]);
                    b.BID = Convert.ToInt32(BID);
                    b.VID = Convert.ToInt32(dt.Rows[0]["VID"]);
                    b.Iconurl = Convert.ToString(dt.Rows[0]["Iconurl"]);
                    b.Imgurl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                    b.Brand_Des = Convert.ToString(dt.Rows[0]["Brand_Des"]);
                }
                ViewBag.Action = "Update";
            }

            else if (IUDFlag == "D")
            {
                // get data from db depend on BID and assign BID and Brandname to Brand obj 
                DataTable dt = dal.GetData("exec YOL.sp_selBrandName " + BID);

                if (dt.Rows.Count > 0)
                {
                    b.Brand_Name = Convert.ToString(dt.Rows[0]["Brand_Name"]);
                    b.BID = Convert.ToInt32(BID);
                    b.VID = Convert.ToInt32(dt.Rows[0]["VID"]);
                    b.Iconurl = Convert.ToString(dt.Rows[0]["Iconurl"]);
                    b.Imgurl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                    b.Brand_Des = Convert.ToString(dt.Rows[0]["Brand_Des"]);
                }
                ViewBag.Action = "Delete";
            }

            return View(b);
        }


        public ActionResult CategoryDetail(string IUDFlag, int BCID)
        {
            BCRel c = new BCRel();

            ViewBag.Flag = IUDFlag;
            List<Brand> b = new List<Brand>();
            DataTable list = dal.GetData("exec YOL.Sp_selBrand");
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Brand obj = new Brand();
                    obj.BID = Convert.ToInt32(list.Rows[i]["BID"]);
                    obj.Brand_Name = Convert.ToString(list.Rows[i]["Brand_Name"]);
                    b.Add(obj);
                }
            }
            ViewBag.Brand = b;

            if (IUDFlag == "N")
            {
                //v.Vendor_Name= Convert.ToString("Vendor_Name");
                ViewBag.Action = "Save";
            }

            else if (IUDFlag == "U")
            {
                // get data from db depend on VID and assign BID and Brandname to brand obj 

                DataTable dt = dal.GetData("exec YOL.sp_selBrandCaterel " + BCID);

                if (dt.Rows.Count > 0)
                {
                    c.Category_Name = Convert.ToString(dt.Rows[0]["Category_Name"]);
                    c.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    c.BID = Convert.ToInt32(dt.Rows[0]["BID"]);
                    c.Cate_Des = Convert.ToString(dt.Rows[0]["Cate_Des"]);
                    c.IconUrl = Convert.ToString(dt.Rows[0]["Iconurl"]); ;
                    c.ImgUrl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                }
                ViewBag.Action = "Update";
            }

            else if (IUDFlag == "D")
            {
                // get data from db depend on VID and assign VID and Vendorname to vendor obj 
                DataTable dt = dal.GetData("exec YOL.sp_selBrandCaterel " + BCID);

                if (dt.Rows.Count > 0)
                {
                    c.Category_Name = Convert.ToString(dt.Rows[0]["Category_Name"]);
                    c.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    c.BID = Convert.ToInt32(dt.Rows[0]["BID"]);
                    c.Cate_Des = Convert.ToString(dt.Rows[0]["Cate_Des"]);
                    c.IconUrl = Convert.ToString(dt.Rows[0]["Iconurl"]); ;
                    c.ImgUrl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                }
                ViewBag.Action = "Delete";
            }

            return View(c);
        }

        public ActionResult SubCategoryDetail(string IUDFlag, int CCID)
        {
            SCRel sc = new SCRel();

            ViewBag.Flag = IUDFlag;
            List<Category> c = new List<Category>();
            DataTable list = dal.GetData("exec YOL.Sp_selCategory");
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Category obj = new Category();
                    obj.CID = Convert.ToInt32(list.Rows[i]["CID"]);
                    obj.Category_Name = Convert.ToString(list.Rows[i]["Category_Name"]);
                    c.Add(obj);
                }
            }
            ViewBag.Category = c;

            if (IUDFlag == "N")
            {
                //v.Vendor_Name= Convert.ToString("Vendor_Name");
                ViewBag.Action = "Save";
            }

            else if (IUDFlag == "U")
            {
                // get data from db depend on VID and assign BID and Brandname to brand obj 

                DataTable dt = dal.GetData("exec [YOL].[sp_selSubCaterel] " + CCID);

                if (dt.Rows.Count > 0)
                {
                    sc.SubCategory_Name = Convert.ToString(dt.Rows[0]["SubCategory_Name"]);
                    sc.SCID = Convert.ToInt32(dt.Rows[0]["SCID"]);
                    sc.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    sc.SubCate_Des = Convert.ToString(dt.Rows[0]["SubCate_Des"]);
                    sc.IconUrl = Convert.ToString(dt.Rows[0]["Iconurl"]); ;
                    sc.ImgUrl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                }
                ViewBag.Action = "Update";
            }

            else if (IUDFlag == "D")
            {
                // get data from db depend on VID and assign VID and Vendorname to vendor obj 
                DataTable dt = dal.GetData("exec [YOL].[sp_selSubCaterel] " + CCID);

                if (dt.Rows.Count > 0)
                {
                    sc.SubCategory_Name = Convert.ToString(dt.Rows[0]["SubCategory_Name"]);
                    sc.SCID = Convert.ToInt32(dt.Rows[0]["SCID"]);
                    sc.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    sc.SubCate_Des = Convert.ToString(dt.Rows[0]["SubCate_Des"]);
                    sc.IconUrl = Convert.ToString(dt.Rows[0]["Iconurl"]); ;
                    sc.ImgUrl = Convert.ToString(dt.Rows[0]["Imgurl"]);
                }
                ViewBag.Action = "Delete";
            }

            return View(sc);
        }


        public ActionResult ProductDetail(string IUDFlag, int PID)
        {
            Product p = new Product();

            ViewBag.Flag = IUDFlag;

            List<Vendor> v = new List<Vendor>();
            DataTable list = dal.GetData("exec YOL.Sp_selVendor");
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Vendor obj = new Vendor();
                    obj.VID = Convert.ToInt32(list.Rows[i]["VID"]);
                    obj.Vendor_Name = Convert.ToString(list.Rows[i]["Vendor_Name"]);
                    v.Add(obj);
                }
            }
            ViewBag.Vendor = v;

            if (IUDFlag == "N")
            {
                //v.Vendor_Name= Convert.ToString("Vendor_Name");
                ViewBag.Action = "Save";
            }

            else if (IUDFlag == "U")
            {
                // get data from db depend on VID and assign BID and Brandname to brand obj 

                DataTable dt = dal.GetData("exec YOL.sp_selProductName " + PID);

                if (dt.Rows.Count > 0)
                {
                    p.Product_Name = Convert.ToString(dt.Rows[0]["Product_Name"]);
                    p.PID = Convert.ToInt32(PID);
                    p.BID = Convert.ToInt32(dt.Rows[0]["BID"]);
                    p.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    p.VID = Convert.ToInt32(dt.Rows[0]["VID"]);
                    p.IconUrl = Convert.ToString(dt.Rows[0]["IconUrl"]);
                    p.ImgUrl = Convert.ToString(dt.Rows[0]["ImgUrl"]);
                    p.SquareUrl = Convert.ToString(dt.Rows[0]["SquareUrl"]);
                    p.Product_Price = Convert.ToString(dt.Rows[0]["Product_Price"]);
                    p.Product_Dec = Convert.ToString(dt.Rows[0]["Product_Dec"]);
                    p.SCID = Convert.ToInt32(dt.Rows[0]["SCID"]);
                    p.Psize = Convert.ToString(dt.Rows[0]["Psize"]);
                }

                ViewBag.Action = "Update";
            }

            else if (IUDFlag == "D")
            {
                // get data from db depend on VID and assign VID and Vendorname to vendor obj 
                DataTable dt = dal.GetData("exec YOL.sp_selProductName " + PID);

                if (dt.Rows.Count > 0)
                {
                    p.Product_Name = Convert.ToString(dt.Rows[0]["Product_Name"]);
                    p.PID = Convert.ToInt32(PID);
                    p.BID = Convert.ToInt32(dt.Rows[0]["BID"]);
                    p.CID = Convert.ToInt32(dt.Rows[0]["CID"]);
                    p.VID = Convert.ToInt32(dt.Rows[0]["VID"]);
                    p.IconUrl = Convert.ToString(dt.Rows[0]["IconUrl"]);
                    p.ImgUrl = Convert.ToString(dt.Rows[0]["ImgUrl"]);
                    p.SquareUrl = Convert.ToString(dt.Rows[0]["SquareUrl"]);
                    p.Product_Price = Convert.ToString(dt.Rows[0]["Product_Price"]);
                    p.Product_Dec = Convert.ToString(dt.Rows[0]["Product_Dec"]);
                    p.SCID = Convert.ToInt32(dt.Rows[0]["SCID"]);
                    p.Psize = Convert.ToString(dt.Rows[0]["Psize"]);
                }
                ViewBag.Action = "Delete";
            }
            return View(p);
        }

        [HttpGet]
        public JsonResult GetBrandByVendor(int VID)
        {
            List<Brand> b = new List<Brand>();
            DataTable list = dal.GetData("exec YOL.Sp_GetBrandByVendor " + VID);
            if (list.Rows.Count > 0)
            {
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Brand obj = new Brand();
                    obj.BID = Convert.ToInt32(list.Rows[i]["BID"]);
                    obj.Brand_Name = Convert.ToString(list.Rows[i]["Brand_Name"]);
                    b.Add(obj);
                }
            }
            return Json(b, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            var isSuccess = true;
            var errorMsg = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string bname = Request["brandname"].ToString();
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        if (file != null)
                        {
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            // Get the complete folder path and store the file inside it.  
                            fname = Server.MapPath(Path.Combine("~/Images/", bname + "_IMG.png"));
                            file.SaveAs(fname);

                        }
                    }
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    errorMsg = ex.Message;
                }
            }
            var jsonData = new { isSuccess, errorMsg };

            // after successfully uploading redirect the user
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UploadIcon()
        {
            var isSuccess = true;
            var errorMsg = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string bname = Request["brandname"].ToString();
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        if (file != null)
                        {
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            // Get the complete folder path and store the file inside it.  
                            fname = Server.MapPath(Path.Combine("~/Icon/", bname + "_Icon.png"));
                            file.SaveAs(fname);

                        }
                    }
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    errorMsg = ex.Message;
                }
            }
            var jsonData = new { isSuccess, errorMsg };

            // after successfully uploading redirect the user
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index1(HttpPostedFileBase file)
        {
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    //for checking uploaded image is an image or not
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                        || Path.GetExtension(file.FileName).ToLower() == ".png"
                        || Path.GetExtension(file.FileName).ToLower() == ".bmp"
                        || Path.GetExtension(file.FileName).ToLower() == ".ico")
                    {
                        path = Path.Combine(Server.MapPath("~/Images/"), file.FileName);
                        file.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                    }
                }
            }

            return View();
        }

        public ActionResult ImageUpload()
        {
            return View();
        }
        [HttpPost]
        public string ImageUpload(HttpPostedFileBase FileData)
        {

            FileData = Request.Files[0];
            if (FileData.ContentLength > 0)
            {
                var fileName = Path.GetFileName(FileData.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                FileData.SaveAs(path);
            }

            return "Files was uploaded successfully!";
        }

        [HttpGet]
        public ActionResult Offers(Offers o)
        {
            
            if (ModelState.IsValid)
            {
                
            }

            return View();
        }

        public ActionResult OfferBrand()
        {
           // string boffer = Request.Form["Brand"].ToString();
            return View();
        }

        public ActionResult OfferCategory()
        {
            //string coffer = Request.Form["Category"].ToString();
            return View();
        }

        public ActionResult OfferSubCategory()
        {
           // string scoffer = Request.Form["SubCategory"].ToString();
            return View();
        }

    }
}