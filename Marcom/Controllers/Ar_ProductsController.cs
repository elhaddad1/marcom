using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class Ar_ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Products(int? page, int? DeptId, int? BrandId, int? CatgId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var LstObj = context.Products.Include("CategoryProducr").Where(a => a.IsDelete == false && a.ParentId == null).AsQueryable();
                if (page == null) page = 1;
                if (DeptId == null) DeptId = 0;
                if (BrandId == null) BrandId = 0;
                if (CatgId == null) CatgId = 0;
                if (DeptId > 0) {
                    LstObj = LstObj.Where("Department_id == @0", DeptId).AsQueryable();
                    ViewData["Deptstr"] = context.Departments.Where(a=>a.Department_id==DeptId).SingleOrDefault().Department_Name_Ar;
                }
                if (BrandId > 0)
                {
                    LstObj = LstObj.Where("Brand_id == @0", BrandId).AsQueryable();
                    ViewData["Brandstr"] = context.Brands.Where(a=>a.Brand_id==BrandId).SingleOrDefault().Brand_Name_Ar;
                }
                if (CatgId > 0) 
                {
                    List<int> LstInt = context.CategoryProducr.Include("Products").Where(e => e.CategoryPID == CatgId).Select(a => a.Products.Product_id).ToList();
                    LstObj = LstObj.ToList().Where(a =>a.Category_id==CatgId|| LstInt.Contains(a.Product_id)).AsQueryable();
                    ViewData["Catgstr"] = context.Categories.Where(a=>a.Category_id==CatgId).SingleOrDefault().Category_Name_Ar;
                }
                ViewData["ListCount"] = LstObj.Count();
                ViewData["DeptId"] = DeptId;
                ViewData["BrandId"] = BrandId;
                ViewData["CatgId"] = CatgId;

                
                
                return View(LstObj.OrderBy(a=>a.OrderIndex).Skip((page.Value - 1) * 8).Take(8).ToList());
            }
        }

        public ActionResult ProductDetail(int? id, int? DeptId, int? BrandId, int? CatgId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                if (DeptId == null) DeptId = 0;
                if (BrandId == null) BrandId = 0;
                if (CatgId == null) CatgId = 0;
                ViewData["DeptId"] = DeptId;
                ViewData["BrandId"] = BrandId;
                ViewData["CatgId"] = CatgId;
                Products Obj = context.Products.Include("Categories").Include("Departments").Include("Brands").Include("ProductGallery").Include("ProductComponents").Include("AccessoriesProducr").Include("AccessoriesProducr.Products1").Include("RelatedProduct").Include("RelatedProduct.Products1").Where(a => a.Product_id == id).SingleOrDefault();
                Obj.Product_ViewCount = Obj.Product_ViewCount.HasValue ? (Obj.Product_ViewCount + 1) : 1;
                context.SaveChanges();
                return View(Obj);
            }
        }

    }
}
