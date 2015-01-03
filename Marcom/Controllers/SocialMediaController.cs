using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class SocialMediaController : Controller
    {
        //
        // GET: /SocialMedia/

        public ActionResult Index()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return View(context.LinkTable.ToList());
            }
        }


    }
}
