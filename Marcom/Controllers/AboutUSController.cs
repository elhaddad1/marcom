using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class AboutUSController : Controller
    {
        //
        // GET: /AboutUS/

        public ActionResult AboutUs()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                CompanyAboutUs Obj =context.CompanyAboutUs.Count()>0? context.CompanyAboutUs.ToList().LastOrDefault():null ;
                return View(Obj);
            }
        }

    }
}
