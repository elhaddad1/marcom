using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marcom.Models;
using Marcom.Infrastructure.Notification;

namespace Marcom.Controllers
{
    public class Ar_ContactUsController : Controller
    {
        //
        // GET: /ContactUs/

        public ActionResult ContactUs(string message, MessageType? MsgType)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                if (message != null && !message.Trim().Equals(""))
                    this.ShowMessage(MsgType.Value, message, true);
                CompanyContactUs Obj = context.CompanyContactUs.Count() > 0 ? context.CompanyContactUs.ToList().LastOrDefault() : null;
                return View(Obj);
            }
        }

        [HttpPost]
        public ActionResult ContactUs(string name, string company, string email, string phone, string message)
        {
            using (MarcomEntities context = new MarcomEntities())
            {


                if (clsSendingEmail.SendContactUs(email, name, company, phone, message))
                {
                    MessageType MsgType = MessageType.Success;
                    message = "تم ارسال رسالتك بنجاح";
                    MsgType = MessageType.Success;
                    this.ShowMessage(MsgType, message, true);
                }
                else
                {
                    MessageType MsgType = MessageType.Success;
                    message = "يرجى التحقق من البريد الخاص بك، البريد الإلكتروني غير صحيح.";
                    MsgType = MessageType.Error;
                    this.ShowMessage(MsgType, message, true);
                }
                return RedirectToAction("ContactUs", "Ar_ContactUs");
            }
        }

    }
}
