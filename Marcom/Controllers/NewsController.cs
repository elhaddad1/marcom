using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class NewsController : Controller
    {
        //

        public ActionResult News(int? page)
        {
            if (page == null) page = 1;
            using (MarcomEntities context = new MarcomEntities())
            {
                List<News> LstNews = context.News.OrderByDescending(a => a.News_Datetime).Skip((page.Value - 1) * 4).Take(4).ToList();
                return View(LstNews);
            }
        }

        public ActionResult NewsDetail(int? id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                News News = context.News.Where(a => a.News_id == id).SingleOrDefault(); ;
                return View(News);
            }
        }

    }
}
