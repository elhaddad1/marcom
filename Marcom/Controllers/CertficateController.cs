using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
namespace Marcom.Controllers
{
    public class CertficateController : Controller
    {
        //
        // GET: /Certficate/


        public ActionResult Index()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return View(context.CertificateTable.ToList());
            }
        }

    }
}
