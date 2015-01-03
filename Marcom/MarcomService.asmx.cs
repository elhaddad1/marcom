using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Marcom.Models;
using System.Web.Script.Serialization;
using System.Text;

namespace Marcom
{
    /// <summary>
    /// Summary description for MarcomService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MarcomService : System.Web.Services.WebService
    {


        [WebMethod]
        public string GetSitMapBrand(int? num)
        {
            string tag = "";

            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                string StrCode = "tile_10";
                List<Brands> LstParent = context.Brands.Include("Products").Include("Products.Departments").Where(a => a.Products.Count() > 0).OrderBy(a => a.OrderIndex).ToList();
                string strindx = StrCode  ;
                List<Departments> DeptGObj = context.Departments.ToList();
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                Parntsb.Append(string.Format("<ul  class=\"{0}\" >", "filetree"));
                foreach (var itm in LstParent)
                {
                    if (!LstObj.ContainsKey(itm.Brand_id))
                    {
                        LstObj.Add(itm.Brand_id, new List<int>());
                    }
                    if (!LstFLObj.ContainsKey(itm.Brand_id))
                    {
                        LstFLObj.Add(itm.Brand_id, new List<int>());
                    }
                    List<Departments> lstctgry = itm.Products.Select(a => a.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList();
                    foreach (var catg in lstctgry)
                    {
                        int? deptId = catg.Department_id;
                        while (DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Brand_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Brand_id].Add(Nullprnt);
                            }
                            if (deptId == null)
                            {
                                if (!LstFLObj[itm.Brand_id].Contains(Nullprnt))
                                    LstFLObj[itm.Brand_id].Add(Nullprnt);
                                break;
                            }
                        }
                    }
                }
                foreach (var itm in LstParent)
                {
                     
                    string urlHref = "BrandId=" + itm.Brand_id;
                    string listrindx = StrCode ;
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", itm.Brand_Name_Eng));
                    Parntsb.Append("<ul>");
                    foreach (var x in LstFLObj[itm.Brand_id].ToList())
                    {
                        Departments Objdept = DeptGObj.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => a.Brand_id == itm.Brand_id && a.Department_id == Objdept.Department_id).Any())
                        {
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            Parntsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, Objdept.Department_Name_Eng));
                        }
                        else
                        {
                             
                            string listrindx2 = StrCode ;
                            Parntsb.Append(string.Format(" <li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                            Parntsb.Append(GetChildBrnd(itm.Brand_id, x, LstObj[itm.Brand_id], StrCode, urlHref));
                            Parntsb.Append("</li>");
                        }
                    }
                    Parntsb.Append("</ul>");
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");
                 

                JavaScriptSerializer jss = new JavaScriptSerializer();
                tag = jss.Serialize(Parntsb.ToString());

                return tag;
            }
        }
        public StringBuilder GetChildBrnd(int brnid, int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                //int Dept = ;
                string strindx = StrCode ;
                StringBuilder PrntChild = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                    PrntChild.Append("<ul>");
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                             
                            string listrindx2 = StrCode ;

                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format(" <li><span class='folder'>{0}</span>", catg.Department_Name_Eng));
                                PrntChild.Append(GetChildBrnd(brnid, catg.Department_id, lstint, StrCode, url));
                                PrntChild.Append("</li>");
                            }
                            else
                            {
                                string href = url + "&DeptId=" + catg.Department_id;
                                PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, catg.Department_Name_Eng));
                            }
                        }
                    }
                    else
                    {
                        string href = url + "&DeptId=" + prntid;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, ObjDept.Department_Name_Eng));
                    }
                    PrntChild.Append("</ul>");
                }
                catch
                {
                    throw;
                }
                return PrntChild;
            }
        }


        [WebMethod]
        public string GetSitMapCategory(int? num)
        {
            string tag = "";

            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                List<Categories> LstParent = context.Products.Include("Categories").Where(a => !a.IsDelete.Value).Select(a => a.Categories).Where(a => !a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                List<int> LstInt = LstParent.Select(a => a.Category_id).ToList();
                LstParent.AddRange(context.CategoryProducr.Include("Categories").Include("Categories.CategoryProducr").Include("Categories.CategoryProducr.Products").Where(a => !LstInt.Contains(a.CategoryPID.Value)).Select(a => a.Categories).Where(a => !a.IsDelete.Value).ToList());
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                List<Departments> DeptGObj = context.Departments.ToList();
                Parntsb.Append("<ul>");
                foreach (var itm in LstParent)
                {
                    if (!LstObj.ContainsKey(itm.Category_id))
                    {
                        LstObj.Add(itm.Category_id, new List<int>());
                    }
                    if (!LstFLObj.ContainsKey(itm.Category_id))
                    {
                        LstFLObj.Add(itm.Category_id, new List<int>());
                    }
                    List<Departments> lstctgry = new List<Departments>();
                    if (itm.Products.Count > 0)
                        lstctgry.AddRange(itm.Products.Select(a => a.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    if (itm.CategoryProducr.Count > 0)
                        lstctgry.AddRange(itm.CategoryProducr.Select(a => a.Products.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    foreach (var catg in lstctgry)
                    {
                        int? deptId = catg.Department_id;
                        while (DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Category_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Category_id].Add(Nullprnt);
                            }
                            if (deptId == null)
                            {
                                if (!LstFLObj[itm.Category_id].Contains(Nullprnt))
                                    LstFLObj[itm.Category_id].Add(Nullprnt);
                                break;
                            }
                        }
                    }
                }
                foreach (var itm in LstParent)
                {
                     
                    string urlHref = "CatgId=" + itm.Category_id;
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", itm.Category_Name_Eng));
                    Parntsb.Append("<ul>");
                    foreach (var x in LstFLObj[itm.Category_id].ToList())
                    {
                        Departments Objdept = DeptGObj.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => (a.Category_id == itm.Category_id || a.CategoryProducr.Where(e => e.CategoryPID == itm.Category_id).Any()) && a.Department_id == Objdept.Department_id).Any())
                        {
                             
                            Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                            List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(itm.Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                            Parntsb.Append("<ul>");
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            foreach (var brndItm in LstBrnd)
                            {
                                string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                Parntsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Eng));
                            }
                            Parntsb.Append("</ul>");
                            Parntsb.Append("</li>");
                        }
                        else
                        {
                             
                            Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                            Parntsb.Append(GetChildCtgory(itm.Category_id, x, LstObj[itm.Category_id], urlHref));
                            Parntsb.Append("</li>");
                        }
                    }
                    Parntsb.Append("</ul>");
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");
                 

                JavaScriptSerializer jss = new JavaScriptSerializer();
                tag = jss.Serialize(Parntsb.ToString());

                return tag;
            }
        }
        public StringBuilder GetChildCtgory(int Category_id, int prntid, List<int> lstint, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                StringBuilder PrntChild = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                    PrntChild.Append("<ul>");
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                             
                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", catg.Department_Name_Eng));
                                PrntChild.Append(GetChildCtgory(Category_id, catg.Department_id, lstint, url));
                            }
                            else
                            {
                                Departments Objdept = catg;
                                string href = url + "&DeptId=" + Objdept.Department_id;
                                 
                                PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                                List<Brands> LstBrnd = context.Products.Include("Brands").Where(a => a.Category_id == Category_id && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                                LstBrnd.AddRange(context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList());
                                PrntChild.Append("<ul>");
                                foreach (var brndItm in LstBrnd)
                                {
                                    string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                    PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Eng));
                                }
                                PrntChild.Append("</ul>");
                                PrntChild.Append("</li>");
                            }
                        }
                    }
                    else
                    {
                        Departments Objdept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                        string href = url + "&DeptId=" + prntid;
                         
                        PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                        List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                        PrntChild.Append("<ul>");
                        foreach (var brndItm in LstBrnd)
                        {
                            string href2 = href + "&BrandId=" + brndItm.Brand_id;
                            PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Eng));
                        }
                        PrntChild.Append("</ul>");
                        PrntChild.Append("</li>");
                    }
                    PrntChild.Append("</ul>");

                }
                catch
                {
                    throw;
                }
                return PrntChild;
            }
        }


        [WebMethod]
        public string GetSitMapDepartment(int num)
        {
            string tag = "";

            using (MarcomEntities context = new MarcomEntities())
            {
                List<int> LstObj = new List<int>();
                List<Departments> LstFLObj = new List<Departments>();
                List<Departments> LstParent = context.Products.Include("Departments").Select(a => a.Departments).Where(a => !a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                List<Departments> DeptGObj = context.Departments.ToList();
                Parntsb.Append("<ul>");
                foreach (var catg in LstParent)
                {
                    int? deptId = catg.Department_id;
                    while (DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Any())
                    {
                        int Nullprnt = deptId.Value;
                        deptId = DeptGObj.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                        if (!LstObj.Contains(Nullprnt) && deptId.HasValue)
                        {
                            LstObj.Add(Nullprnt);
                        }
                        if (deptId == null)
                        {
                            if (LstFLObj.Count == 0 || !LstFLObj.Select(a => a.Department_id).Contains(Nullprnt))
                                LstFLObj.Add(DeptGObj.Where(a => a.Department_id == Nullprnt).SingleOrDefault());
                            break;
                        }
                    }
                }
                foreach (var x in LstFLObj.OrderBy(a => a.OrderIndex).ToList())
                {
                    Departments Objdept = DeptGObj.Where(a => a.Department_id == x.Department_id).SingleOrDefault();
                     
                    string urlHref = "DeptId=" + Objdept.Department_id;
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Eng));
                    Parntsb.Append(GetChildDept(x.Department_id, LstObj, urlHref));
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");

                JavaScriptSerializer jss = new JavaScriptSerializer();
                tag = jss.Serialize(Parntsb.ToString());

                return tag;
            }
        }
        public StringBuilder GetChildDept(int prntid, List<int> lstint, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                StringBuilder PrntChild = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList();
                    List<Brands> BrndLst = context.Products.Include("Brands").Where(a => a.Department_id == prntid).Select(s => s.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                    PrntChild.Append("<ul>");
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                             
                            PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", catg.Department_Name_Eng));
                            PrntChild.Append(GetChildDept(catg.Department_id, lstint, ("DeptId=" + catg.Department_id)));
                            PrntChild.Append("</li>");
                        }
                    }
                    foreach (var itm in BrndLst)
                    {
                        string href = url + "&BrandId=" + itm.Brand_id;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itm.Brand_Name_Eng));
                    }
                    PrntChild.Append("</ul>");
                }
                catch
                {
                    throw;
                }
                return PrntChild;
            }
        }
    }
}
