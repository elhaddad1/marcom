using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Marcom.Models;
using Marcom.Infrastructure.Notification;


namespace Marcom.Controllers
{
    public class SearchAutoCmpltController : Controller
    {
        //
        // GET: /SearchAutoCmplt/

        #region //English

        public ActionResult Index(string Str)
        {
            if (Str != null)
                ViewData["Str"] = Str;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Searchresult(string featureClass, string style, int maxRows, string name_startsWith)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<string> LstStr = new List<string>();
                LstStr.AddRange(context.Categories.Where(a => a.Category_Name_Eng.Contains(name_startsWith)).Select(a => a.Category_Name_Eng).ToList());
                LstStr.AddRange(context.Brands.Where(a => a.Brand_Name_Eng.Contains(name_startsWith)).Select(a => a.Brand_Name_Eng).ToList());
                LstStr.AddRange(context.Products.Where(a => a.Product_Highlight_Eng.Contains(name_startsWith) || a.Product_Title_Eng.Contains(name_startsWith) || a.Product_Spec_Eng.Contains(name_startsWith) || a.Product_Short_Eng.Contains(name_startsWith)).Select(a => a.Product_Title_Eng).ToList());
                LstStr.AddRange(context.Departments.Where(a => a.Department_Name_Eng.Contains(name_startsWith)).Select(a => a.Department_Name_Eng).ToList());
                return Json(LstStr.Take(maxRows), JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AllCategAndBrandItems(int? page, string StrName)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                ViewData["StrName"] = StrName;
                List<Products> ItmLst = new List<Products>();
                List<int> LstSubItmCategory = new List<int>();
                if (page == null)
                    page = 1;
                List<int> BrandIdLst = context.Brands.Where(a => a.Brand_Name_Eng.Contains(StrName)).Select(a => a.Brand_id).ToList();
                List<int> CategIdLst = context.Categories.Where(a => a.Category_Name_Eng.Contains(StrName)).Select(a => a.Category_id).ToList();
                List<int> DeptIdLst = context.Departments.Where(a => a.Department_Name_Eng.Contains(StrName)).Select(a => a.Department_id).ToList();
                ItmLst.AddRange(context.Products.Where(a => a.Product_Title_Eng.Contains(StrName)).ToList());
                if (CategIdLst.Count > 0)
                {
                    ItmLst.AddRange(context.Products.Where(a => CategIdLst.Contains(a.Category_id.Value)).OrderByDescending(a => a.OrderIndex).ToList());
                }
                if (BrandIdLst.Count > 0)
                {
                    ItmLst.AddRange(context.Products.Where(a => BrandIdLst.Contains(a.Brand_id.Value)).OrderByDescending(a => a.OrderIndex).ToList());
                }
                if (DeptIdLst.Count > 0)
                {
                    ItmLst.AddRange(context.Products.Where(a => DeptIdLst.Contains(a.Department_id.Value)).OrderByDescending(a => a.OrderIndex).ToList());
                }
                ViewData["ListCount"] = ItmLst.Distinct().Count();
                ItmLst = ItmLst.Distinct().OrderByDescending(a => a.OrderIndex).Skip((page.Value - 1) * 10).Take(10).ToList();
                return View(ItmLst);
            }
        }

        #endregion //English

    }
}
