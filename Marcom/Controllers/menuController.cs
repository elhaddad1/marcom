using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class menuController : Controller
    {
        //
        // GET: /menu/

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Product()
        {
            return View();
        }
        //
        // GET: /menu/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Brand()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return View();
            }
        }
        //
        // GET: /menu/

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Category()
        {
            return View();
        }

    }
}
