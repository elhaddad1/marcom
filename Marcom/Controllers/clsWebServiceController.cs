using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
using System.Web.Services;
using Marcom.Infrastructure.Notification;
using System.IO;
using System.Web.UI;
using System.Net;
using System.Text;

namespace Marcom.Controllers
{
    public class clsWebServiceController : Controller
    {
        //

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UnsupscripeAccount(string urlpath)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                int UserIdDcrpt = int.Parse(clsGlobel.Decrypt(urlpath));
                if (UserIdDcrpt > 0)
                {
                    var Obj = context.UsersData.Where(a => a.User_id == UserIdDcrpt).SingleOrDefault();
                    Obj.User_Subscripe = false;
                    List<UserCategory> LstObj = context.UserCategory.Where(a => a.UserId == UserIdDcrpt).ToList();
                    foreach (var item in LstObj)
                    {
                        context.DeleteObject(item);
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("Home", "Home", new { message = "unsubscribe done successfully", MsgType = MessageType.Success });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ArUnsupscripeAccount(string urlpath)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                int UserIdDcrpt = int.Parse(clsGlobel.Decrypt(urlpath));
                if (UserIdDcrpt > 0)
                {
                    var Obj = context.UsersData.Where(a => a.User_id == UserIdDcrpt).SingleOrDefault();
                    Obj.User_Subscripe = false;
                    List<UserCategory> LstObj = context.UserCategory.Where(a => a.UserId == UserIdDcrpt).ToList();
                    foreach (var item in LstObj)
                    {
                        context.DeleteObject(item);
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("Home", "Ar_Home", new { message = "إلغاء الاشتراك تم بنجاح", MsgType = MessageType.Success });
            }
        }
        // GET: /clsWebService/
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult AddtocardList()
        {
            using(MarcomEntities context = new MarcomEntities())
            {
                List<Carts> Lst = new List<Carts>();
                if (Session["UserId"] != null)
                {
                    int userId = (int)Session["UserId"];
                    Lst = context.Carts.Include("Products").Where(a => a.User_id == userId&&(a.IsArchive == false||a.IsArchive == null)).ToList();
                }
                return PartialView(Lst);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddtocardAmount()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string strAmount = "0";
                var result = new JsonResult();
                if (Session["UserId"] != null)
                {
                    int userId = (int)Session["UserId"];
                    strAmount = context.Carts.Include("Products").Where(a => a.User_id == userId && (a.IsArchive == false||a.IsArchive == null)).Any() ? context.Carts.Include("Products").Where(a => a.User_id == userId && (a.IsArchive == false||a.IsArchive == null)).Select(a => a.Amount).Sum().ToString() : "0";
                }                
                result.Data = strAmount.ToString();
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddtocardDelete(int ProductId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string strAmount = "0";
                var result = new JsonResult();
                if (Session["UserId"] != null)
                {
                    int userId = (int)Session["UserId"];
                    Carts Obj = context.Carts.Where(a => a.Cart_id == ProductId).SingleOrDefault();
                    if (Obj != null)
                    {
                        context.Carts.DeleteObject(Obj);
                        context.SaveChanges();
                    }
                    strAmount = context.Carts.Include("Products").Where(a => a.User_id == userId && (a.IsArchive == false||a.IsArchive == null)).Select(a => a.Amount).Sum().ToString();
                }
                result.Data = strAmount;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ChangeProductAmount(int CrdId,int amount)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var result = new JsonResult();
                if (Session["UserId"] != null)
                {
                    Carts Obj = context.Carts.Where(a => a.Cart_id == CrdId).SingleOrDefault();
                    Obj.Amount = amount;
                    context.SaveChanges();
                    result.Data = "";
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                else
                {
                    result.Data = "error";
                    this.ShowMessage(MessageType.Error, "Please login first");
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                return result;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddtocardAddProduct(int ProductId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var result = new JsonResult();
                if (Session["UserId"] != null)
                {
                    int userId = (int)Session["UserId"];
                    Carts Obj = new Carts();
                    if (context.Carts.Where(a => a.User_id == userId && a.Product_id == ProductId && (a.IsArchive == false||a.IsArchive == null)).Any())
                    {
                        Obj = context.Carts.Where(a => a.User_id == userId && a.Product_id == ProductId && (a.IsArchive == false||a.IsArchive == null)).SingleOrDefault();
                    }
                    else
                    {
                        Obj.Amount = 1;
                    }
                    Obj.User_id = userId;
                    Obj.Product_id = ProductId;
                    Obj.Cart_Datetime = DateTime.Now;
                    Obj.IsArchive = false;
                    if (!context.Carts.Where(a => a.User_id == userId && a.Product_id == ProductId && (a.IsArchive == false||a.IsArchive == null)).Any())
                    {
                        context.AddToCarts(Obj);
                    }
                    context.SaveChanges();
                    result.Data = "";
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                else
                {
                    result.Data = "error";
                    this.ShowMessage(MessageType.Error, "Please login first");
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                return result;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddtocardCheckout(int[] arrayId, int[] arrayQuamtity,string CommentText)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var result = new JsonResult();
                try
                {
                    if (Session["UserId"] != null)
                    {
                        int usrid = (int)Session["UserId"];
                        var model = context.UsersData.Where(a => a.User_id == usrid).SingleOrDefault();
                        List<string> LstEmail = new List<string>() { model.User_Email, "webmaster@marcom.eg-mt.com" };
                        string PrmtrArr = "";
                        string PrmtrQun = "";
                        int count = arrayId.Count();
                        foreach (var Arr in arrayId)
                        {
                            if (count == 1)
                            {
                                PrmtrArr += ("arrayId="+Arr.ToString());
                            }
                            else
                            {
                                PrmtrArr += (("arrayId=" + Arr.ToString()) + "&&");
                            }
                            count--;
                        }
                        count = arrayQuamtity.Count();
                        foreach (var Arr in arrayQuamtity)
                        {
                            if (count == 1)
                            {
                                PrmtrQun += ("arrayQuamtity="+Arr.ToString());
                            }
                            else
                            {
                                PrmtrQun += (("arrayQuamtity=" + Arr.ToString()) + "&&");
                            }
                            count--;
                        }
                        clsSendingEmail.SendCheckOut(LstEmail, clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/Encart?usrid=" + usrid + "&&" + PrmtrArr + "&&" + PrmtrQun + "&&CommentText=" + CommentText + ""));
                        for (int x = 0; x < arrayId.Count(); x++)
                        {
                            int id = arrayId[x];
                            Carts Obj = context.Carts.Where(a => a.Cart_id == id).SingleOrDefault();
                            Obj.IsArchive = true;
                            context.SaveChanges();
                        }
                        this.ShowMessage(MessageType.Success, "", false);
                        result.Data = "Checkout done succefully ,Please Check your mail";
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    }
                    else
                    {
                        result.Data = "Please login first";
                        this.ShowMessage(MessageType.Error, "");
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    }
                }
                catch
                {
                    result.Data = "error";
                    this.ShowMessage(MessageType.Error, "");
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                return result;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddtocardArCheckout(int[] arrayId, int[] arrayQuamtity, string CommentText)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var result = new JsonResult();
                try
                {
                    if (Session["UserId"] != null)
                    {
                        int usrid = (int)Session["UserId"];
                        var model = context.UsersData.Where(a => a.User_id == usrid).SingleOrDefault();
                        List<string> LstEmail = new List<string>() { model.User_Email, "webmaster@marcom.eg-mt.com" };
                        string PrmtrArr = "";
                        string PrmtrQun = "";
                        int count = arrayId.Count();
                        foreach (var Arr in arrayId)
                        {
                            if (count == 1)
                            {
                                PrmtrArr += ("arrayId=" + Arr.ToString());
                            }
                            else
                            {
                                PrmtrArr += (("arrayId=" + Arr.ToString()) + "&&");
                            }
                            count--;
                        }
                        count = arrayQuamtity.Count();
                        foreach (var Arr in arrayQuamtity)
                        {
                            if (count == 1)
                            {
                                PrmtrQun += ("arrayQuamtity=" + Arr.ToString());
                            }
                            else
                            {
                                PrmtrQun += (("arrayQuamtity=" + Arr.ToString()) + "&&");
                            }
                            count--;
                        }
                        clsSendingEmail.SendArCheckOut(LstEmail, clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/Arcart?usrid=" + usrid + "&&" + PrmtrArr + "&&" + PrmtrQun + "&&CommentText=" + CommentText + ""));
                        for (int x = 0; x < arrayId.Count(); x++)
                        {
                            int id = arrayId[x];
                            Carts Obj = context.Carts.Where(a => a.Cart_id == id).SingleOrDefault();
                            Obj.IsArchive = true;
                            context.SaveChanges();
                        }
                        this.ShowMessage(MessageType.Success, "", false);
                        result.Data = "Checkout done succefully ,Please Check your mail";
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    }
                    else
                    {
                        result.Data = "Please login first";
                        this.ShowMessage(MessageType.Error, "");
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    }
                }
                catch
                {
                    result.Data = "error";
                    this.ShowMessage(MessageType.Error, "");
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                }
                return result;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ArForgetPassword(string email)
        {
            var result = new JsonResult();
            if (clsSendingEmail.SendArPassword(email))
            {
                this.ShowMessage(MessageType.Success, "برجاء مراجعة بريدك الالكترونى لمعرفة كلمة المرور", false);
                result.Data = "برجاء مراجعة بريدك الالكترونى لمعرفة كلمة المرور";
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            else
            {
                result.Data = "بريد الكترونى غير صحيح";
                this.ShowMessage(MessageType.Error, "Invalid Email");
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ForgetPassword(string email)
        {
            var result = new JsonResult();
            if (clsSendingEmail.SendPassword(email))
            {
                this.ShowMessage(MessageType.Success, "Please Check your mail for password", false);
                result.Data = "Please Check your mail for password";
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            else
            {
                result.Data = "Invalid Email";
                this.ShowMessage(MessageType.Error, "Invalid Email");
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            return result;
        }



        [OutputCache(VaryByParam="none",Duration=50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebLogo(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.LogoPath;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }
        [OutputCache(VaryByParam = "none", Duration = 50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebBG(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.BackgroundPath_Eng;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }

        [OutputCache(VaryByParam = "none", Duration = 50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebMV(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.BackgroundPath_Ar;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }


        [OutputCache(VaryByParam = "none", Duration = 50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebMVAr(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.DivBackGround_Ar;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }

        [OutputCache(VaryByParam = "none", Duration = 50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebPrm(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.DivPormBG_Eng;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }


        [OutputCache(VaryByParam = "none", Duration = 50000)]
        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetWebPrmAr(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string StrPath = "";
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                if (ObjBrnd != null)
                {
                    StrPath = clsGlobel.SrcLogoDomain + ObjBrnd.DivPormBG_Ar;
                }

                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public ActionResult GetShowPrm(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                bool StrPath =false;
                MarcomSetting ObjBrnd = context.MarcomSetting.ToList().LastOrDefault();
                // Define the key of the product to delete.
                // Get the object to delete with the specified key.
                StrPath=ObjBrnd.IsShowPrm.Value;
                var result = new JsonResult();
                result.Data = StrPath;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [WebMethod]
        public JsonResult GetUserData()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                UsersData Obj =new UsersData();
                if (Session["UserId"] != null)
                {
                    int UsrId = (int)Session["UserId"];
                    UsersData OrgObj = context.UsersData.Where(a => a.User_id == UsrId).SingleOrDefault();
                    Obj.User_id = OrgObj.User_id;
                    Obj.User_name = OrgObj.User_name;
                    Obj.User_Company = OrgObj.User_Company;
                    Obj.User_Email = OrgObj.User_Email;
                    Obj.User_Mobile = OrgObj.User_Mobile;
                    Obj.Gender = OrgObj.Gender;
                    Obj.User_Address = OrgObj.User_Address;
                    Obj.User_InteristIn = OrgObj.User_InteristIn;
                    Obj.User_LastName = OrgObj.User_LastName;
                    Obj.User_Password = OrgObj.User_Password;
                    Obj.User_Subscripe = OrgObj.User_Subscripe;
                }

                var result = new JsonResult();
                result.Data = Obj;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
            }
        }



        //////////////////////////////////////////////////////////////////////




        public static string GetHtmlFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url", "Parameter is null or empty");

            string html = "";
            HttpWebRequest request = GenerateHttpWebRequest(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                // Get the response stream.
                Stream responseStream = response.GetResponseStream();
                // Use a stream reader that understands UTF8.
                using (StreamReader reader =
                new StreamReader(responseStream, Encoding.UTF8))
                {
                    html = reader.ReadToEnd();
                }

            }
            return html;
        }
        public static HttpWebRequest GenerateHttpWebRequest(string UriString)
        {
            // Get a Uri object.
            Uri Uri = new Uri(UriString);
            // Create the initial request.
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Uri);
            // Return the request.
            return httpRequest;
        }
    }
}
