using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
using Marcom.Infrastructure.Notification;

namespace Marcom.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult LangChange(string url)
        {
            string newurl = "";
            if (url == "/")
            {
                return RedirectToAction("Home", "Ar_Home");
            }
            else
            {
                int count = 0;
                int charcount = 0;
                foreach (char c in url)
                {
                    count++;
                    if (c == '/')
                    {
                        charcount++;
                    }
                    if (charcount == 1)
                    {
                        newurl = url.Insert(count, "Ar_");
                        return Redirect(newurl);
                    }
                }
            }
            return RedirectToAction("Home", "Home");
        }

        public ActionResult ArabicLangChange(string url)
        {
            string newurl = "/";
            if (url.Contains("/Ar_Home/Home"))
            {
                return Redirect(newurl);
            }
            else
            {
                int count = 0;
                int charcount = 0;
                foreach (char c in url)
                {
                    count++;
                    if (c == '/')
                    {
                        charcount++;
                    }
                    if (charcount == 1)
                    {
                        newurl = url.Remove(count,3);
                        return Redirect(newurl);
                    }
                }
            }
            return RedirectToAction("Home", "Home");
        }

        public ActionResult Home(string message, MessageType? MsgType)
        {
            if (message != null && !message.Trim().Equals(""))
                this.ShowMessage(MsgType.Value, message,true);
            using (MarcomEntities context = new MarcomEntities())
            {
                var AboutUs =context.CompanyAboutUs.Count()>0? context.CompanyAboutUs.ToList().ToList().LastOrDefault():null;
                ViewData["AboutUs"] = AboutUs==null?"":AboutUs.HomeTxt_Eng;
                 return View();
            }
        }

        public PartialViewResult SliderImage()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return PartialView(context.ImagesGallery.Take(4).ToList());
            }
        }

        public ActionResult WeeklyOffer(int id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                WeeklyOffer LstObj = context.WeeklyOffer.Include("Products").Include("Products.Brands").Where(a => a.id == id).SingleOrDefault();
                return PartialView(LstObj);
            }
        }

        public PartialViewResult WeeklyOffers()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<WeeklyOffer> LstObj = context.WeeklyOffer.Include("Products").Include("Products.Brands").Where(a => !a.IsDelete.Value).OrderByDescending(a => a.id).Take(1).ToList();
                return PartialView(LstObj);
            }
        }

        public PartialViewResult PrtialNews()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<News> LstObj = context.News.OrderByDescending(a => a.News_Datetime).Take(10).ToList();
                return PartialView(LstObj);
            }
        }

        public PartialViewResult PrtialTopOffer()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                List<Products> LstObj = context.Products.Include("Brands").Where(x => x.IsMostVisted==true).Take(4).ToList();
                return PartialView(LstObj);
            }
        }


        public ActionResult SiteMap()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return View();
            }
        }

    }
}
