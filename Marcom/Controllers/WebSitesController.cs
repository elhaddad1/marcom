using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
using System.Web.Helpers;
using System.IO;
using System.Net;

namespace Marcom.Controllers
{
    public class WebSitesController : Controller
    {
        //
        // GET: /SocialMedia/

        public ActionResult Index()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return View(context.WebSiteTable.ToList());
            }
        }

        public void GetPhotoThumbnail(string ImgPath,int width,int height)
        {
            WebRequest req = HttpWebRequest.Create(ImgPath);
            Stream stream = req.GetResponse().GetResponseStream();
            new WebImage(stream).Resize(width, height, true).Write();
            //.AddTextWatermark("Watermark", "white", 14, "Bold")
            // Resizing the image to 100x100 px on the fly... .Crop(1, 1) 
            // Cropping it to remove 1px border at top and left sides (bug in WebImage) .Write(); }
            // Loading a default photo for realties that don't have a Photo 
            //new WebImage(HostingEnvironment.MapPath(@"~/Content/images/no-photo100x100.png")).Write(); 
        }
    }
}
