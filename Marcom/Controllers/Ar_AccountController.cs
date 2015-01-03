using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Marcom.Models;
using Marcom.Infrastructure.Notification;

namespace Marcom.Controllers
{

    public class Ar_AccountController : Controller
    {

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public PartialViewResult LogOn()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    string message = "";
                    MessageType MsgType = MessageType.Success;
                    UsersData Obj = context.UsersData.Where(a => a.User_Email == model.UserName || a.User_name == model.UserName).SingleOrDefault();
                    if (Obj != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        Session["UserId"] = Obj.User_id;
                        message = "login successful";
                        MsgType = MessageType.Success;
                    }
                    else
                    {
                        Session["UserId"] = null;
                        message = "failed to login";
                        MsgType = MessageType.Error;
                    }
                    this.ShowMessage(MsgType, message, true);
                }
            }
            return RedirectToAction("Home", "Home");
            // If we got this far, something failed, redisplay form
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            return RedirectToAction("Home", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string message = "";
                MessageType MsgType = MessageType.Success;
                List<UsersData> Obj = context.UsersData.Where(a => a.User_Email == model.Email || a.User_name == model.UserName).ToList();
                if (Obj.Count == 0)
                {
                    UsersData NewObj = new UsersData();
                    NewObj.User_name = model.UserName;
                    NewObj.User_IsActive = true;
                    NewObj.User_Password = model.Password;
                    NewObj.User_Email = model.Email;
                    context.AddToUsersData(NewObj);
                    context.SaveChanges();
                    Session["UserId"] = NewObj.User_id;
                    message = "Register successful";
                    MsgType = MessageType.Success;
                }
                else
                {
                    Session["UserId"] = null;
                    message = "failed to Register (email or username already exist";
                    MsgType = MessageType.Error;
                }
                this.ShowMessage(MsgType, message, true);
                return RedirectToAction("Home", "Home");
            }
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        //// **************************************
        //// URL: /Account/ChangePasswordSuccess
        //// **************************************

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}

    }
}
