using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class MarktingEmailController : Controller
    {
        //
        // GET: /MarktingEmail/

        public ActionResult Index(int? catgid, int? id1, int? id2, int? id3, int? id4,int?UseId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                ViewData["UserDataId"] = clsGlobel.Encrypt(UseId.ToString());
                List<Products> LstProd = new List<Products>();
                ViewData["WeeklyOffer"] = context.WeeklyOffer.Include("Products").Where(a => !a.IsDelete.Value).OrderBy(a => a.Products.OrderIndex).FirstOrDefault();
                if (id1 != null && id1 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id1).SingleOrDefault());
                if (id2 != null && id2 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id2).SingleOrDefault());
                if (id3 != null && id3 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id3).SingleOrDefault());
                if (id4 != null && id4 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id4).SingleOrDefault());
                return View(LstProd);
            }
        }


        public ActionResult IndexSpecUser(int? id1, int? id2, int? id3, int? id4, int? UseId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                ViewData["UserDataId"] = clsGlobel.Encrypt(UseId.ToString());
                List<Products> LstProd = new List<Products>();
                ViewData["WeeklyOffer"] = context.WeeklyOffer.Include("Products").Where(a => !a.IsDelete.Value).OrderBy(a => a.Products.OrderIndex).FirstOrDefault();
                if (id1 != null && id1 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id1).SingleOrDefault());
                if (id2 != null && id2 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id2).SingleOrDefault());
                if (id3 != null && id3 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id3).SingleOrDefault());
                if (id4 != null && id4 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id4).SingleOrDefault());
                return View(LstProd);
            }
        }
        public ActionResult Ar_Index(int? catgid, int? id1, int? id2, int? id3, int? id4, int? UseId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                ViewData["UserDataId"] = clsGlobel.Encrypt(UseId.ToString());
                List<Products> LstProd = new List<Products>();
                ViewData["WeeklyOffer"] = context.WeeklyOffer.Include("Products").Where(a => !a.IsDelete.Value).OrderBy(a => a.Products.OrderIndex).FirstOrDefault();
                if (id1 != null && id1 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id1).SingleOrDefault());
                if (id2 != null && id2 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id2).SingleOrDefault());
                if (id3 != null && id3 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id3).SingleOrDefault());
                if (id4 != null && id4 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id4).SingleOrDefault());
                return View(LstProd);
            }
        }


        public ActionResult Ar_IndexSpecUser(int? id1, int? id2, int? id3, int? id4, int? UseId)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                ViewData["UserDataId"] = clsGlobel.Encrypt(UseId.ToString());
                List<Products> LstProd = new List<Products>();
                ViewData["WeeklyOffer"] = context.WeeklyOffer.Include("Products").Where(a => !a.IsDelete.Value).OrderBy(a => a.Products.OrderIndex).FirstOrDefault();
                if (id1 != null && id1 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id1).SingleOrDefault());
                if (id2 != null && id2 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id2).SingleOrDefault());
                if (id3 != null && id3 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id3).SingleOrDefault());
                if (id4 != null && id4 > 0)
                    LstProd.Add(context.Products.Include("CategoryProducr").Where(a => a.Product_id == id4).SingleOrDefault());
                return View(LstProd);
            }
        }

        public ActionResult Encart(int usrid,int[] arrayId, int[] arrayQuamtity, string CommentText)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var model = context.UsersData.Where(a => a.User_id == usrid).SingleOrDefault();
                clsAddToCard ModelObj = new clsAddToCard();
                ModelObj.UserNamer = model.User_name;
                ModelObj.UserEmail = model.User_Email;
                ModelObj.Telephonenumber = model.User_Mobile;
                ModelObj.CustomerComment = CommentText;
                ModelObj.Password = "";
                List<CardData> LstCardData = new List<CardData>();
                for (int x = 0; x < arrayId.Count(); x++)
                {
                    CardData CardDataObj = new CardData();
                    int id = arrayId[x];
                    Carts Obj = context.Carts.Where(a => a.Cart_id == id).SingleOrDefault();
                    Obj.Amount = arrayQuamtity[x];
                    context.SaveChanges();
                    CardDataObj.ProductNamer = Obj.Products.Product_Title_Eng;
                    CardDataObj.Amount = Obj.Amount.ToString();
                    LstCardData.Add(CardDataObj);
                }
                ModelObj.card = new List<CardData>();
                ModelObj.card.AddRange(LstCardData);
                return View(ModelObj);
            }
        }
        
        public ActionResult Arcart(int usrid, int[] arrayId, int[] arrayQuamtity, string CommentText)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var model = context.UsersData.Where(a => a.User_id == usrid).SingleOrDefault();
                clsAddToCard ModelObj = new clsAddToCard();
                ModelObj.UserNamer = model.User_name;
                ModelObj.UserEmail = model.User_Email;
                ModelObj.Telephonenumber = model.User_Mobile;
                ModelObj.CustomerComment = CommentText;
                ModelObj.Password = "";
                List<CardData> LstCardData = new List<CardData>();
                for (int x = 0; x < arrayId.Count(); x++)
                {
                    CardData CardDataObj = new CardData();
                    int id = arrayId[x];
                    Carts Obj = context.Carts.Where(a => a.Cart_id == id).SingleOrDefault();
                    Obj.Amount = arrayQuamtity[x];
                    context.SaveChanges();
                    CardDataObj.ProductNamer = Obj.Products.Product_Title_Ar;
                    CardDataObj.Amount = Obj.Amount.ToString();
                    LstCardData.Add(CardDataObj);
                }
                ModelObj.card = new List<CardData>();
                ModelObj.card.AddRange(LstCardData);
                return View(ModelObj);
            }
        }

        public ActionResult EnForgetEmail(string Password)
        {
            clsAddToCard ModelObj = new clsAddToCard();
            ModelObj.Password = Password;
            return View(ModelObj);
        }

        public ActionResult ArForgetEmail(string Password)
        {
                clsAddToCard ModelObj = new clsAddToCard();
                ModelObj.Password = Password;
                return View(ModelObj);
        }


        public ActionResult Ar_RegisterEmail()
        {
            return View();
        }


        public ActionResult EN_RegisterEmail()
        {
            return View();
        }
    }
}
