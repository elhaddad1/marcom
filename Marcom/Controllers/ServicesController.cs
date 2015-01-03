using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;

namespace Marcom.Controllers
{
    public class ServicesController : Controller
    {
        //

        public ActionResult Service(int? page)
        {
            if (page == null) page = 1;
            using (MarcomEntities context = new MarcomEntities())
            {
                List<Service> LstService = context.Service.OrderByDescending(a => a.Service_Datetime).Skip((page.Value - 1) * 4).Take(4).ToList();
                return View(LstService);
            }
        }

        public ActionResult ServiceDetail(int? id)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                Service Service = context.Service.Where(a => a.Service_id == id).SingleOrDefault(); ;
                return View(Service);
            }
        }

    }
}
