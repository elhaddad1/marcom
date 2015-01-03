using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
using Marcom.Infrastructure.Notification;

namespace Marcom.Controllers
{

    public class Ar_HomeController : Controller
    {

        public ActionResult Home()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var AboutUs = context.CompanyAboutUs.Count() > 0 ? context.CompanyAboutUs.ToList().LastOrDefault() : null;
                ViewData["AboutUs"] = AboutUs ==null?"": AboutUs.HomeTxt_Ar;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Home(string message, MessageType? MsgType)
        {
            if (message != null && !message.Trim().Equals(""))
                this.ShowMessage(MsgType.Value, message,true);
            return View();
        }

        public PartialViewResult SliderImage()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return PartialView(context.ImagesGallery.Take(4).ToList());
            }
        }

        public ActionResult About()
        {
            return View();
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
