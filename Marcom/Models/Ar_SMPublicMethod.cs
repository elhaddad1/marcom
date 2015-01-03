using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace Marcom.Models
{
    public class Ar_SMPublicMethod
    {
        public static int publiccount = 0;

        public MvcHtmlString CreateBrndMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                Ar_SMPublicMethod.publiccount = num;
                string StrCode = "tile_10";
                List<Brands> LstParent = context.Brands.Include("Products").Include("Products.Departments").Where(a => a.Products.Count() > 0).OrderBy(a => a.OrderIndex).ToList();
                string strindx = StrCode + num;
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
                        while (context.Departments.Where(a => a.Department_id == deptId).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = context.Departments.Where(a => a.Department_id == deptId).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Brand_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Brand_id].Add(Nullprnt);
                            }
                            if (!deptId.HasValue)
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
                    Ar_SMPublicMethod.publiccount += 1;
                    string urlHref = "BrandId=" + itm.Brand_id;
                    string listrindx = StrCode + (Ar_SMPublicMethod.publiccount);
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", itm.Brand_Name_Ar));
                    Parntsb.Append("<ul>");
                    foreach (var x in LstFLObj[itm.Brand_id].ToList())
                    {
                        Departments Objdept = context.Departments.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => a.Brand_id == itm.Brand_id && a.Department_id == Objdept.Department_id).Any())
                        {
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            Parntsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, Objdept.Department_Name_Ar));
                        }
                        else
                        {
                            Ar_SMPublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
                            Parntsb.Append(string.Format(" <li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                            Parntsb.Append(GetChildBrnd(itm.Brand_id, x, LstObj[itm.Brand_id], StrCode, urlHref));
                            Parntsb.Append("</li>");
                        }
                    }
                    Parntsb.Append("</ul>");
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");
                Ar_SMPublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
            }
        }
        public StringBuilder GetChildBrnd(int brnid, int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                //int Deptnum = num;
                string strindx = StrCode + (Ar_SMPublicMethod.publiccount);
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
                            Ar_SMPublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);

                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format(" <li><span class='folder'>{0}</span>", catg.Department_Name_Ar));
                                PrntChild.Append(GetChildBrnd(brnid, catg.Department_id, lstint, StrCode, url));
                                PrntChild.Append("</li>");
                            }
                            else
                            {
                                string href = url + "&DeptId=" + catg.Department_id;
                                PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, catg.Department_Name_Ar));
                            }
                        }
                    }
                    else
                    {
                        string href = url + "&DeptId=" + prntid;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, ObjDept.Department_Name_Ar));
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



        public MvcHtmlString CreateCtgoryMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                Dictionary<int, List<int>> LstObj = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> LstFLObj = new Dictionary<int, List<int>>();
                Ar_SMPublicMethod.publiccount = num;
                List<Categories> LstParent = context.Products.Include("Categories").Where(a=>!a.IsDelete.Value).Select(a => a.Categories).Where(a=>!a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                List<int> LstInt = LstParent.Select(a => a.Category_id).ToList();
                LstParent.AddRange(context.CategoryProducr.Include("Categories").Include("Categories.CategoryProducr").Include("Categories.CategoryProducr.Products").Where(a => !LstInt.Contains(a.CategoryPID.Value)).Select(a => a.Categories).Where(a => !a.IsDelete.Value).ToList());
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
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
                    if(itm.Products.Count>0)
                       lstctgry.AddRange( itm.Products.Select(a => a.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    if (itm.CategoryProducr.Count > 0)
                        lstctgry.AddRange(itm.CategoryProducr.Select(a => a.Products.Departments).OrderBy(a => a.OrderIndex).Distinct().ToList());
                    foreach (var catg in lstctgry)
                    {
                        int? deptId = catg.Department_id;
                        while (context.Departments.Where(a => a.Department_id == deptId).Any())
                        {
                            int Nullprnt = deptId.Value;
                            deptId = context.Departments.Where(a => a.Department_id == deptId).Select(a => a.Department_Parent_id).SingleOrDefault();
                            if (!LstObj[itm.Category_id].Contains(Nullprnt) && deptId.HasValue)
                            {
                                LstObj[itm.Category_id].Add(Nullprnt);
                            }
                            if (!deptId.HasValue)
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
                    Ar_SMPublicMethod.publiccount += 1;
                    string urlHref = "CatgId=" + itm.Category_id;
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", itm.Category_Name_Ar));
                    Parntsb.Append("<ul>");
                    foreach (var x in LstFLObj[itm.Category_id].ToList())
                    {
                        Departments Objdept = context.Departments.Where(a => a.Department_id == x).SingleOrDefault();
                        if (context.Products.Where(a => (a.Category_id == itm.Category_id || a.CategoryProducr.Where(e=>e.CategoryPID == itm.Category_id).Any()) && a.Department_id == Objdept.Department_id).Any())
                        {
                            Ar_SMPublicMethod.publiccount += 1;
                            Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                            List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(itm.Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                            Parntsb.Append("<ul>");
                            string href = urlHref + "&DeptId=" + Objdept.Department_id;
                            foreach (var brndItm in LstBrnd)
                            {
                                string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                Parntsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Ar));
                            }
                            Parntsb.Append("</ul>");
                            Parntsb.Append("</li>");
                        }
                        else
                        {
                            Ar_SMPublicMethod.publiccount += 1;
                            Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                            Parntsb.Append(GetChildCtgory(itm.Category_id, x, LstObj[itm.Category_id], urlHref));
                            Parntsb.Append("</li>");
                        }
                    }
                    Parntsb.Append("</ul>");
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");
                Ar_SMPublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
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
                            Ar_SMPublicMethod.publiccount += 1;
                            List<Departments> SBrndLst = context.Departments.Where(a => a.Department_Parent_id == catg.Department_id && lstint.Contains(a.Department_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Departments>();
                            if (SBrndLst.Count > 0)
                            {
                                PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", catg.Department_Name_Ar));
                                PrntChild.Append(GetChildCtgory(Category_id, catg.Department_id, lstint, url));
                            }
                            else
                            {
                                Departments Objdept = catg;
                                string href = url + "&DeptId=" + Objdept.Department_id;
                                Ar_SMPublicMethod.publiccount += 1;
                                PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                                List<Brands> LstBrnd = context.Products.Include("Brands").Where(a => a.Category_id == Category_id && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                                LstBrnd.AddRange(context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList());
                                PrntChild.Append("<ul>");
                                foreach (var brndItm in LstBrnd)
                                {
                                    string href2 = href + "&BrandId=" + brndItm.Brand_id;
                                    PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Ar));
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
                        Ar_SMPublicMethod.publiccount += 1;
                        PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                        List<Brands> LstBrnd = context.Products.Include("CategoryProducr").Include("Brands").Where(a => a.CategoryProducr.Select(e => e.CategoryPID.Value).Contains(Category_id) && a.Department_id == Objdept.Department_id).Select(a => a.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList();
                        PrntChild.Append("<ul>");
                        foreach (var brndItm in LstBrnd)
                        {
                            string href2 = href + "&BrandId=" + brndItm.Brand_id;
                            PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href2, brndItm.Brand_Name_Ar));
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
        #region //Hashed Method
        //public MvcHtmlString CreateDeptMenu(int num)
        //{
        //    using (MarcomEntities context = new MarcomEntities())
        //    {
        //        Ar_SMPublicMethod.publiccount = num;
        //        string StrCode = "tile_1";
        //        List<Departments> LstParent = context.Departments.Include("Departments1").Where(a => a.Department_Parent_id == null).OrderBy(a => a.OrderIndex).Distinct().ToList();
        //        string strindx = StrCode + num;
        //        clsBrandMenu obj = new clsBrandMenu();

        //        StringBuilder Parntsb = new StringBuilder();
        //        StringBuilder Childsb = new StringBuilder();
        //        StringBuilder ChildOfChsb = new StringBuilder();
        //        Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_111_tiles", "/Resources/Images/Uploads/Products/Products.jpg"));
        //        foreach (var itm in LstParent)
        //        {
        //            if (itm.Departments1.Count > 0 || itm.Products.Count > 0)
        //            {
        //                Ar_SMPublicMethod.publiccount += 1;
        //                string urlHref = "DeptId=" + itm.Department_id;
        //                string listrindx = StrCode + (Ar_SMPublicMethod.publiccount);
        //                Parntsb.Append(string.Format("<li rel=\"{0}\">{1}</li>", listrindx, itm.Department_Name_Ar));
        //                List<Departments> lstctgry = itm.Departments1.OrderBy(a => a.OrderIndex).ToList();
        //                List<Brands> lstBrnd = context.Products.Include("Brands").Where(a => a.Department_id == itm.Department_id).Select(a => a.Brands).OrderBy(a => a.OrderIndex).Distinct().ToList();

        //                Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx, "jGlide_111_tiles", itm.Image_Path != null ? !itm.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + itm.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
        //                foreach (var catg in lstctgry)
        //                {
        //                    if ((context.Departments.Where(a => a.Department_Parent_id == catg.Department_id).ToList().Count > 0)||(context.Products.Where(a => a.Department_id == catg.Department_id).ToList().Count > 0))
        //                    {
        //                        Ar_SMPublicMethod.publiccount += 1;
        //                        string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
        //                        Childsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Department_Name_Ar));
        //                        ChildOfChsb.Append(GetChildDept(catg.Department_id, StrCode, ("DeptId=" + catg.Department_id)));
        //                    }
        //                }
        //                foreach (var itmB in lstBrnd)
        //                {
        //                    string href = urlHref + "&BrandId=" + itmB.Brand_id;
        //                    Childsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itmB.Brand_Name_Ar));
        //                }
        //                Childsb.Append("</ul>");

        //            }
        //        }
        //        Parntsb.Append("</ul>");
        //        Parntsb.Append(Childsb.Append(ChildOfChsb));
        //        Ar_SMPublicMethod.publiccount = 0;
        //        return MvcHtmlString.Create(Parntsb.ToString());
        //    }
        //}
        //public StringBuilder GetChildDept(int deptId, string StrCode, string url)
        //{
        //    using (MarcomEntities context = new MarcomEntities())
        //    {
        //        //int Deptnum = num;
        //        string strindx = StrCode + Ar_SMPublicMethod.publiccount;
        //        StringBuilder PrntChild = new StringBuilder();
        //        StringBuilder Child = new StringBuilder();
        //        try
        //        {
        //            Departments ObjDept = context.Departments.Where(a => a.Department_id == deptId).SingleOrDefault();
        //            List<Departments> DeptLst = context.Departments.Include("Departments1").Include("Departments1.Products").Include("Products").Where(a => a.Department_Parent_id == deptId).Distinct().ToList<Departments>();
        //            List<Brands> BrndLst = context.Products.Include("Brands").Where(a => a.Department_id == deptId).Select(s => s.Brands).Distinct().ToList<Brands>();
        //            if (DeptLst.Count == 0 && BrndLst.Count == 0)
        //                return PrntChild;
        //            PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_111_tiles", ObjDept.Image_Path != null ? !ObjDept.Image_Path.Trim().Equals("") ? clsGlobel.SrcDepartmentsDomain + ObjDept.Image_Path : "/Resources/Images/Uploads/Products/Products.JPG" : "/Resources/Images/Uploads/Products/Products.JPG"));
        //            foreach (var itm in DeptLst)
        //            {
        //                if ((context.Departments.Where(a => a.Department_Parent_id == itm.Department_id).ToList().Count > 0) || (context.Products.Where(a => a.Department_id == itm.Department_id).ToList().Count > 0))
        //                {
        //                    Ar_SMPublicMethod.publiccount += 1;
        //                    string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
        //                    PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, itm.Department_Name_Ar));
        //                    List<Departments> ChildDeptLst = context.Departments.Include("Departments1").Include("Departments1.Products").Include("Products").Where(a => a.Department_Parent_id == itm.Department_id).Distinct().ToList<Departments>();
        //                    foreach (var catg in ChildDeptLst)
        //                    {
        //                        Child.Append(GetChildDept(itm.Department_id, StrCode, ("DeptId=" + catg.Department_id)));
        //                    }
        //                }
        //            }
        //            foreach (var itm in BrndLst)
        //            {
        //                string href = url + "&BrandId=" + itm.Brand_id;
        //                PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itm.Brand_Name_Ar));
        //            }
        //            PrntChild.Append("</ul>");
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return PrntChild.Append(Child);
        //    }
        //}

        //public  MvcHtmlString CreateMenu(int num)
        //{
        //    using (MarcomEntities context = new MarcomEntities())
        //    {
        //        Ar_SMPublicMethod.publiccount = num;
        //        string StrCode = "tile_0";
        //        List<Categories> LstParent = context.Categories.Include("Products").Include("Categories1").Where(a => a.Category_Parent_id == null).OrderBy(a => a.OrderIndex).Distinct().ToList();
        //        string strindx = StrCode + Ar_SMPublicMethod.publiccount;
        //        clsBrandMenu obj = new clsBrandMenu();

        //        StringBuilder Parntsb = new StringBuilder();
        //        StringBuilder Childsb = new StringBuilder();
        //        StringBuilder ChildOfChsb = new StringBuilder();
        //        Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_050_tiles", "/Resources/Images/Uploads/Products/Category.jpg"));
        //        foreach (var itm in LstParent)
        //        {
        //            if (itm.Categories1.Count > 0 || itm.Products.Count>0)
        //            {
        //                Ar_SMPublicMethod.publiccount += 1;
        //                string urlHref = "CatgId=" + itm.Category_id;
        //                string listrindx = StrCode + (Ar_SMPublicMethod.publiccount);
        //                Parntsb.Append(string.Format("<li rel=\"{0}\">{1}</li>", listrindx, itm.Category_Name_Ar));
        //                List<Categories> lstctgry = itm.Categories1.OrderBy(a => a.OrderIndex).ToList();
        //                List<Brands> lstBrnd = context.Products.Include("Brands").Where(a => a.Category_id == itm.Category_id).Select(a => a.Brands).OrderBy(a => a.OrderIndex).Distinct().ToList();
        //                Childsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", listrindx, "jGlide_050_tiles", itm.Image_Path != null ? !itm.Image_Path.Trim().Equals("") ? clsGlobel.SrcCategorysDomain + itm.Image_Path : "/Resources/Images/Uploads/Products/Category.JPG" : "/Resources/Images/Uploads/Products/Category.JPG"));
        //                foreach (var catg in lstctgry)
        //                {
        //                    if ((context.Products.Where(a => a.Category_id == catg.Category_id).ToList().Count > 0)||(context.Categories.Where(a => a.Category_Parent_id == catg.Category_id).ToList().Count > 0))
        //                    {
        //                        Ar_SMPublicMethod.publiccount += 1;
        //                        string listrindx2 = StrCode + Ar_SMPublicMethod.publiccount;
        //                        Childsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Category_Name_Ar));
        //                        ChildOfChsb.Append(GetChild(catg.Category_id, StrCode, ("CatgId=" + catg.Category_id)));
        //                    }
        //                }
        //                foreach (var itmB in lstBrnd)
        //                {
        //                    string href = urlHref + "&BrandId=" + itmB.Brand_id;
        //                    Childsb.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itmB.Brand_Name_Ar));
        //                }
        //                Childsb.Append("</ul>");
        //            }
        //        }
        //        Parntsb.Append("</ul>");
        //        Parntsb.Append(Childsb.Append(ChildOfChsb));
        //        Ar_SMPublicMethod.publiccount = 0;
        //        return MvcHtmlString.Create(Parntsb.ToString());
        //    }
        //}
        //public  StringBuilder GetChild(int Id, string StrCode, string url)
        //{
        //    using (MarcomEntities context = new MarcomEntities())
        //    {
        //        string strindx = StrCode + Ar_SMPublicMethod.publiccount;
        //        StringBuilder PrntChild = new StringBuilder();
        //        StringBuilder Child = new StringBuilder();
        //        try
        //        {
        //            Categories ObjCtg = context.Categories.Where(a => a.Category_id == Id).SingleOrDefault();
        //            List<Categories> Lst = context.Categories.Include("Categories1").Include("Categories1.Products").Include("Products").Where(a => a.Category_Parent_id == Id).Distinct().ToList<Categories>();
        //            List<Brands> BrndLst = context.Products.Include("Brands").Where(a => a.Category_id == Id).Select(s => s.Brands).Distinct().ToList<Brands>();
        //            if (Lst.Count == 0 && BrndLst.Count == 0)
        //                return PrntChild;
        //            PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_050_tiles", ObjCtg.Image_Path != null ? !ObjCtg.Image_Path.Trim().Equals("") ? clsGlobel.SrcCategorysDomain + ObjCtg.Image_Path : "/Resources/Images/Uploads/Products/Category.JPG" : "/Resources/Images/Uploads/Products/Category.JPG"));
        //            foreach (var itm in Lst)
        //            {
        //                if ((context.Products.Where(a => a.Category_id == itm.Category_id).ToList().Count > 0) || (context.Categories.Where(a => a.Category_Parent_id == itm.Category_id).ToList().Count > 0))
        //                {
        //                    Ar_SMPublicMethod.publiccount += 1;
        //                    string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
        //                    PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, itm.Category_Name_Ar));
        //                    List<Categories> ChildLst = context.Categories.Include("Categories1").Include("Categories1.Products").Include("Products").Where(a => a.Category_Parent_id == itm.Category_id).Distinct().ToList<Categories>();
        //                    foreach (var catg in ChildLst)
        //                    {
        //                        Child.Append(GetChild(itm.Category_id, StrCode, ("CatgId=" + catg.Category_id)));
        //                    }
        //                }
        //            }
        //            foreach (var itm in BrndLst)
        //            {
        //                string href  = url + "&BrandId=" + itm.Brand_id;
        //                PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itm.Brand_Name_Ar));
        //            }
        //            PrntChild.Append("</ul>");
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return PrntChild.Append(Child);
        //    }
        //}
        #endregion

        public MvcHtmlString CreateDeptMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<int> LstObj = new List<int>();
                List<Departments> LstFLObj = new List<Departments>();
                Ar_SMPublicMethod.publiccount = num;
                List<Departments> LstParent = context.Products.Include("Departments").Select(a => a.Departments).Where(a => !a.IsDelete.Value).Distinct().OrderBy(a => a.OrderIndex).ToList();
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                Parntsb.Append("<ul>");
                foreach (var catg in LstParent)
                {
                    int? deptId = catg.Department_id;
                    while (context.Departments.Where(a => a.Department_id == deptId).Any())
                    {
                        int Nullprnt = deptId.Value;
                        deptId = context.Departments.Where(a => a.Department_id == deptId).Select(a => a.Department_Parent_id).SingleOrDefault();
                        if (!LstObj.Contains(Nullprnt) && deptId.HasValue)
                        {
                            LstObj.Add(Nullprnt);
                        }
                        if (!deptId.HasValue)
                        {
                            if (LstFLObj.Count==0||!LstFLObj.Select(a=>a.Department_id).Contains(Nullprnt))
                                LstFLObj.Add(context.Departments.Where(a => a.Department_id == Nullprnt).SingleOrDefault()) ;
                            break;
                        }
                    }
                }
                foreach (var x in LstFLObj.OrderBy(a=>a.OrderIndex).ToList())
                {
                    Departments Objdept = context.Departments.Where(a => a.Department_id == x.Department_id).SingleOrDefault();
                    Ar_SMPublicMethod.publiccount += 1;
                    string urlHref = "DeptId=" + Objdept.Department_id;
                    Parntsb.Append(string.Format("<li><span class='folder'>{0}</span>", Objdept.Department_Name_Ar));
                    Parntsb.Append(GetChildDept(x.Department_id, LstObj, urlHref));
                    Parntsb.Append("</li>");
                }
                Parntsb.Append("</ul>");
                Ar_SMPublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
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
                            Ar_SMPublicMethod.publiccount += 1;
                            PrntChild.Append(string.Format("<li><span class='folder'>{0}</span>", catg.Department_Name_Ar));
                            PrntChild.Append(GetChildDept(catg.Department_id, lstint, ("DeptId=" + catg.Department_id)));
                            PrntChild.Append("</li>");
                        }
                    }
                    foreach (var itm in BrndLst)
                    {
                        string href = url + "&BrandId=" + itm.Brand_id;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itm.Brand_Name_Ar));
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

        public MvcHtmlString CreateCtgMenu(int num)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<int> LstObj = new List<int>();
                List<int> LstFLObj = new List<int>();
                Ar_SMPublicMethod.publiccount = num;
                string StrCode = "tile_0";
                List<Categories> LstParent = context.Products.Include("Categories").Select(a => a.Categories).Distinct().OrderBy(a => a.OrderIndex).ToList();
                string strindx = StrCode + num;
                clsBrandMenu obj = new clsBrandMenu();
                StringBuilder Parntsb = new StringBuilder();
                StringBuilder ChildOfChsb = new StringBuilder();
                Parntsb.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_050_tiles", "/Resources/Images/Uploads/Products/Category.jpg"));
                foreach (var catg in LstParent)
                {
                    int? CtgId = catg.Category_id;
                    while (context.Categories.Where(a => a.Category_id == CtgId).Any())
                    {
                        int Nullprnt = CtgId.Value;
                        CtgId = context.Categories.Where(a => a.Category_id == CtgId).Select(a => a.Category_Parent_id).SingleOrDefault();
                        if (!LstObj.Contains(Nullprnt) && CtgId.HasValue)
                        {
                            LstObj.Add(Nullprnt);
                        }
                        if (!CtgId.HasValue)
                        {
                            if (!LstFLObj.Contains(Nullprnt))
                                LstFLObj.Add(Nullprnt);
                            break;
                        }
                    }
                }
                foreach (var x in LstFLObj.ToList())
                {
                    Categories ObjCtg = context.Categories.Where(a => a.Category_id == x).SingleOrDefault();
                    Ar_SMPublicMethod.publiccount += 1;
                    string urlHref = "CatgId=" + ObjCtg.Category_id;
                    string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
                    Parntsb.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, ObjCtg.Category_Name_Ar));
                    ChildOfChsb.Append(GetChildCtg(x, LstObj, StrCode, urlHref));
                }
                Parntsb.Append("</ul>");
                Parntsb.Append(ChildOfChsb);
                Ar_SMPublicMethod.publiccount = 0;
                return MvcHtmlString.Create(Parntsb.ToString());
            }
        }
        public StringBuilder GetChildCtg(int prntid, List<int> lstint, string StrCode, string url)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string strindx = StrCode + (Ar_SMPublicMethod.publiccount);
                StringBuilder PrntChild = new StringBuilder();
                StringBuilder Child = new StringBuilder();
                try
                {
                    Categories ObjCtg = context.Categories.Where(a => a.Category_id == prntid).SingleOrDefault();
                    List<Categories> ChildCtgLst = context.Categories.Where(a => a.Category_Parent_id == ObjCtg.Category_id && lstint.Contains(a.Category_id)).Distinct().OrderBy(a => a.OrderIndex).ToList<Categories>();
                    List<Brands> BrndLst = context.Products.Include("Brands").Where(a => a.Category_id == prntid).Select(s => s.Brands).Distinct().OrderBy(a => a.OrderIndex).ToList<Brands>();
                    PrntChild.Append(string.Format("<ul id=\"{0}\" class=\"{1}\" title=\"{2}\" alt=\"\">", strindx, "jGlide_050_tiles", ObjCtg.Image_Path != null ? !ObjCtg.Image_Path.Trim().Equals("") ? clsGlobel.SrcCategorysDomain + ObjCtg.Image_Path : "/Resources/Images/Uploads/Products/Category.JPG" : "/Resources/Images/Uploads/Products/Category.JPG"));
                    if (ChildCtgLst.Count > 0 || ObjCtg.Category_Parent_id == null)
                    {
                        foreach (var catg in ChildCtgLst)
                        {
                            Ar_SMPublicMethod.publiccount += 1;
                            string listrindx2 = StrCode + (Ar_SMPublicMethod.publiccount);
                            PrntChild.Append(string.Format(" <li rel=\"{0}\">{1}</li>", listrindx2, catg.Category_Name_Ar));
                            Child.Append(GetChildCtg(catg.Category_id, lstint, StrCode, ("CatgId=" + catg.Category_id)));
                        }
                    }
                    foreach (var itm in BrndLst)
                    {
                        string href = url + "&BrandId=" + itm.Brand_id;
                        PrntChild.Append(string.Format(" <li><a href=\"/Products/Products?{0}\"><span class='file'>{1}</span></a>", href, itm.Brand_Name_Ar));
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