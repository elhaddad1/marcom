using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace Marcom.Models
{
    public class PublicMethod
    {
        public static int publiccount = 0;

        public MvcHtmlString CreateBrndMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                PublicMethod.publiccount = num;
                string StrCode = "tile_10";
                List<Brands> LstParent = context.Brands.Include("Products").Include("Products.Departments").Where(a => a.Products.Count() > 0).OrderBy(a => a.OrderIndex).ToList();
                string strindx = StrCode + num;
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                StringBuilder Childsb = new StringBuilder();
                StringBuilder ChildOfChsb = new StringBuilder();
                List<Departments> PublicListDept = context.Departments.ToList();
                Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_1011_tiles", "/Resources/Images/Uploads/Products/BrandsN.jpg"));
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
                        while (PublicListDept.Where(a => a.Department_id == deptId && a.Department_Parent_id != a.Department_id).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = PublicListDept.Where(a => a.Department_id == deptId && a.Department_Parent_id != a.Department_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Brand_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Brand_id].Add(Nullprnt);
                            }
                            if (deptId==null)
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
                    PublicMethod.publiccount += 1;
                    string urlHref = "BrandId=" + itm.Brand_id;
                    string listrindx = StrCode + (PublicMethod.publiccount);
                    Parntsb.Append(string.Format("<li rel=\"{0}\">{1}</li>", listrindx, itm.Brand_Name_Eng));
                    Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx, "jGlide_1011_tiles", itm.Image_Path != null ? !itm.Image_Path.Trim().Equals("") ? clsGlobel.SrcbrandsDomain + itm.Image_Path : "/Resources/Images/Uploads/Products/BrandsN.JPG" : "/Resources/Images/Uploads/Products/BrandsN.JPG"));
                    foreach (var x in LstFLObj[itm.Brand_id].ToList())
                    {
                        Departments Objdept = PublicListDept.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => a.Brand_id == itm.Brand_id && a.Department_id == Objdept.Department_id).Any())
                        {
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            Childsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href, Objdept.Department_Name_Eng));
                        }
                        else
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);
                            Childsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                            ChildOfChsb.Append(GetChildBrnd(itm.Brand_id, x, LstObj[itm.Brand_id], StrCode, urlHref));
                        }
                    }
                    Childsb.Append("</ul>");
                }
                Parntsb.Append("</ul>");
                Parntsb.Append(Childsb.Append(ChildOfChsb));
                PublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
            }
        }
        public StringBuilder GetChildBrnd(int brnid, int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                //int Deptnum = num;
                string strindx = StrCode + (PublicMethod.publiccount);
                StringBuilder PrntChild = new StringBuilder();
                StringBuilder Child = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                    PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_1011_tiles", ObjDept.Image_Path != null ? !ObjDept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + ObjDept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);

                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Department_Name_Eng));
                                Child.Append(GetChildBrnd(brnid, catg.Department_id, lstint, StrCode, url));
                            }
                            else
                            {
                                string href = url + "&DeptId=" + catg.Department_id;
                                PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href, catg.Department_Name_Eng));
                            }
                        }
                    }
                    else
                    {
                        string href = url + "&DeptId=" + prntid;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href, ObjDept.Department_Name_Eng));
                    }
                    PrntChild.Append("</ul>");
                }
                catch
                {
                    throw;
                }
                return PrntChild.Append(Child);
            }
        }

        public MvcHtmlString CreateCtgoryMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                PublicMethod.publiccount = num;
                string StrCode = "tile_110";
                List<Categories> LstParent = context.Products.Include("Categories").Where(a=>!a.IsDelete.Value).Select(a => a.Categories).Where(a=>!a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                List<int> LstInt = LstParent.Select(a => a.Category_id).ToList();
                LstParent.AddRange(context.CategoryProducr.Include("Categories").Include("Categories.CategoryProducr").Include("Categories.CategoryProducr.Products").Where(a => !LstInt.Contains(a.CategoryPID.Value)).Select(a => a.Categories).Where(a => !a.IsDelete.Value).ToList());
                string strindx = StrCode + num;
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                StringBuilder Childsb = new StringBuilder();
                StringBuilder ChildOfChsb = new StringBuilder();
                List<Departments> PublicListDept = context.Departments.ToList();
                Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_11050_tiles", "/Resources/Images/Uploads/Products/Category.jpg"));
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
                    if(itm.Products.Count>0)
                       lstctgry.AddRange( itm.Products.Select(a => a.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    if (itm.CategoryProducr.Count > 0)
                        lstctgry.AddRange(itm.CategoryProducr.Select(a => a.Products.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    foreach (var catg in lstctgry)
                    {
                        int? deptId = catg.Department_id;
                        while (PublicListDept.Where(a => a.Department_id == deptId && a.Department_id != a.Department_Parent_id).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = PublicListDept.Where(a => a.Department_id == deptId&&a.Department_id!=a.Department_Parent_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Category_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Category_id].Add(Nullprnt);
                            }
                            if (deptId==null)
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
                    PublicMethod.publiccount += 1;
                    string urlHref = "CatgId=" + itm.Category_id;
                    string listrindx = StrCode + (PublicMethod.publiccount);
                    Parntsb.Append(string.Format("<li rel=\"{0}\">{1}</li>", listrindx, itm.Category_Name_Eng));
                    Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx, "jGlide_11050_tiles", itm.Image_Path != null ? !itm.Image_Path.Trim().Equals("") ? clsGlobel.SrcCategorysDomain + itm.Image_Path : "/Resources/Images/Uploads/Products/Category.JPG" : "/Resources/Images/Uploads/Products/Category.JPG"));
                    foreach (var x in LstFLObj[itm.Category_id].ToList())
                    {
                        Departments Objdept = PublicListDept.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => (a.Category_id == itm.Category_id || a.CategoryProducr.Where(e=>e.CategoryPID == itm.Category_id).Any()) && a.Department_id == Objdept.Department_id).Any())
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);
                            Childsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                            List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a => (a.Category_id == itm.Category_id || a.CategoryProducr.Where(e => e.CategoryPID == itm.Category_id).Any()) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                            ChildOfChsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx2, "jGlide_11050_tiles", Objdept.Image_Path != null ? !Objdept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + Objdept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            foreach (var brndItm in LstBrnd)
                            {
                                string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                ChildOfChsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href2, brndItm.Brand_Name_Eng));
                            }
                            ChildOfChsb.Append("</ul>");
                        }
                        else
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);
                            Childsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                            ChildOfChsb.Append(GetChildCtgory(itm.Category_id, x, LstObj[itm.Category_id], StrCode, urlHref));
                        }
                    }
                    Childsb.Append("</ul>");
                }
                Parntsb.Append("</ul>");
                Parntsb.Append(Childsb.Append(ChildOfChsb));
                PublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
            }
        }
        public StringBuilder GetChildCtgory(int Category_id, int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                //int Deptnum = num;
                string strindx = StrCode + (PublicMethod.publiccount);
                StringBuilder PrntChild = new StringBuilder();
                StringBuilder Child = new StringBuilder();
                StringBuilder Childsb = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                    PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_11050_tiles", ObjDept.Image_Path != null ? !ObjDept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + ObjDept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);

                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Department_Name_Eng));
                                Child.Append(GetChildCtgory(Category_id, catg.Department_id, lstint, StrCode, url));
                            }
                            else
                            {
                                Departments Objdept = catg;
                                string href = url + "&DeptId=" + Objdept.Department_id;
                                PublicMethod.publiccount += 1;
                                PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                                List<Brands> LstBrnd = context.Products.Include("Brands").Where(a => (a.Category_id == Category_id || a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id)) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                                LstBrnd.AddRange(context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList());
                                Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx2, "jGlide_11050_tiles", Objdept.Image_Path != null ? !Objdept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + Objdept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                                foreach (var brndItm in LstBrnd)
                                {
                                    string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                    Childsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href2, brndItm.Brand_Name_Eng));
                                }
                                Childsb.Append("</ul>");
                            }
                        }
                    }
                    else
                    {
                        Departments Objdept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                        string href = url + "&DeptId=" + prntid;
                        PublicMethod.publiccount += 1;
                        string listrindx2 = StrCode + (PublicMethod.publiccount);
                        PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                        List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a =>(a.Category_id == Category_id||a.CategoryProducr.Select(e=>e.CategoryPID.Value).Contains(Category_id)) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                        Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx2, "jGlide_11050_tiles", Objdept.Image_Path != null ? !Objdept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + Objdept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                        foreach (var brndItm in LstBrnd)
                        {
                            string href2 = href + "&BrandId=" + brndItm.Brand_id;
                            Childsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href2, brndItm.Brand_Name_Eng));
                        }
                        Childsb.Append("</ul>");
                    }
                    PrntChild.Append("</ul>").Append(Childsb.ToString());

                }
                catch
                {
                    throw;
                }
                return PrntChild.Append(Child);
            }
        }
        
        public MvcHtmlString CreateDeptMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<int> LstObj = new List<int>();
                List<Departments> LstFLObj = new List<Departments>();
                PublicMethod.publiccount = num;
                string StrCode = "tile_1";
                List<Departments> LstParent = context.Products.Include("Departments").Select(a => a.Departments).Where(a => !a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                string strindx = StrCode + num;
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                StringBuilder ChildOfChsb = new StringBuilder();
                List<Departments> PublicListDept = context.Departments.ToList();
                Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_111_tiles", "/Resources/Images/Uploads/Products/Products.jpg"));
                foreach (var catg in LstParent)
                {
                    int? deptId = catg.Department_id;
                    while (PublicListDept.Where(a => a.Department_id == deptId && a.Department_Parent_id != a.Department_id).Any())
                    {
                        int Nullprnt = deptId.Value;
                        deptId = PublicListDept.Where(a => a.Department_id == deptId&&a.Department_Parent_id!=a.Department_id).Select(a => a.Department_Parent_id).SingleOrDefault();
                        if (!LstObj.Contains(Nullprnt) && deptId.HasValue)
                        {
                            LstObj.Add(Nullprnt);
                        }
                        if (deptId==null)
                        {
                            if (LstFLObj.Count==0||!LstFLObj.Select(a=>a.Department_id).Contains(Nullprnt))
                                LstFLObj.Add(PublicListDept.Where(a => a.Department_id == Nullprnt).SingleOrDefault()) ;
                            break;
                        }
                    }
                }
                foreach (var x in LstFLObj.OrderBy(a=>a.OrderIndex).ToList())
                {
                    Departments Objdept = PublicListDept.Where(a => a.Department_id == x.Department_id).SingleOrDefault();
                    PublicMethod.publiccount += 1;
                    string urlHref = "DeptId=" + Objdept.Department_id;
                    string listrindx2 = StrCode + (PublicMethod.publiccount);
                    Parntsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, Objdept.Department_Name_Eng));
                    ChildOfChsb.Append(GetChildDept(x.Department_id, LstObj, StrCode, urlHref));
                }
                Parntsb.Append("</ul>");
                Parntsb.Append(ChildOfChsb);
                PublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
            }
        }
        public StringBuilder GetChildDept(int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string strindx = StrCode + (PublicMethod.publiccount);
                StringBuilder PrntChild = new StringBuilder();
                StringBuilder Child = new StringBuilder();
                try
                {
                    Departments ObjDept = context.Departments.Where(a => a.Department_id == prntid).SingleOrDefault();
                    List<Departments> ChildDeptLst = context.Departments.Where(a => a.Department_Parent_id == ObjDept.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList();
                    List<Brands> BrndLst = context.Products.Include("Brands").Where(a => a.Department_id == prntid).Select(s => s.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                    PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_111_tiles", ObjDept.Image_Path != null ? !ObjDept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + ObjDept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
                    if (ChildDeptLst.Count > 0 || ObjDept.Department_Parent_id == null)
                    {
                        foreach (var catg in ChildDeptLst)
                        {
                            PublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (PublicMethod.publiccount);
                            PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Department_Name_Eng));
                            Child.Append(GetChildDept(catg.Department_id, lstint, StrCode, ("DeptId=" + catg.Department_id)));
                        }
                    }
                    foreach (var itm in BrndLst)
                    {
                        string href = url + "&BrandId=" + itm.Brand_id;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\">{1}</a></li>", href, itm.Brand_Name_Eng));
                    }
                    PrntChild.Append("</ul>");
                }
                catch
                {
                    throw;
                }
                return PrntChild.Append(Child);
            }
        }

    }
}