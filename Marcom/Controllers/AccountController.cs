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

    public class AccountController : Controller
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
                    UsersData Obj = context.UsersData.Where(a =>(( a.User_Email == model.UserName || a.User_name == model.UserName)&&a.User_Password == model.Password)).SingleOrDefault();
                    if (Obj != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        Session["UserId"] = Obj.User_id;
                        message = "You have successfully logged in";
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
            string message = "";
            MessageType MsgType = MessageType.Success;
            message = "Thank you for your time with Marcom Trade";
            MsgType = MessageType.Warning;
            this.ShowMessage(MsgType, message, true);
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            return RedirectToAction("Home", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var list = new SelectList(new[]
                                          {
                                              new {ID="Mr",Name="Mr"},
                                              new{ID="Ms",Name="Ms"}
                                          },
                           "ID", "Name", 1);
                ViewBag.genderlist = list;
                List<int> lstInt = new List<int>();
                if (Session["UserId"] != null)
                {
                    int userid = (int)Session["UserId"];
                    lstInt.AddRange(context.UserCategory.Where(a => a.UserId == userid).Select(a=>a.CategoryId).ToList());
                }
                ViewBag.UserCategoryInt = lstInt;
                ViewBag.Categorylist = context.Categories.Where(a => !a.IsDelete.Value && a.Category_Parent_id == null).OrderBy(a => a.Category_Name_Eng).Where(a => a.Category_Parent_id == null).ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, int? RegUser_id, List<int> ctgcheckbox)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string message = "";
                MessageType MsgType = MessageType.Success;
                if (RegUser_id != null && RegUser_id > 0)
                {
                    List<UsersData> Obj = context.UsersData.Where(a =>a.User_id!=RegUser_id && a.User_Email == model.Email).ToList();
                    if (Obj.Count == 0)
                    {
                        UsersData NewObj = context.UsersData.Where(a => a.User_id == RegUser_id).SingleOrDefault();
                        NewObj.User_name = model.UserName;
                        NewObj.User_IsActive = true;
                        NewObj.IsArabic = false;
                        NewObj.User_Password = model.Password;
                        NewObj.User_Email = model.Email;
                        NewObj.User_Address = model.User_Address;
                        NewObj.User_Company = model.User_Company;
                        NewObj.User_Mobile = model.User_Mobile;
                        NewObj.Gender = model.Gender;
                        //NewObj.User_InteristIn = model.User_InteristIn;
                        NewObj.User_Subscripe = model.User_Subscripe;
                        NewObj.User_LastName = model.LastName;
                        context.UsersData.ApplyCurrentValues(NewObj);
                        context.SaveChanges();
                        List<UserCategory> LstObj = context.UserCategory.Where(a => a.UserId == NewObj.User_id).ToList();
                        foreach (var item in LstObj)
                        {
                            context.DeleteObject(item);
                            context.SaveChanges();
                        }
                        if (model.User_Subscripe == true)
                        {
                            foreach (var item in ctgcheckbox)
                            {
                                context.UserCategory.AddObject(new UserCategory { UserId = NewObj.User_id, CategoryId = item });
                                context.SaveChanges();
                            }
                        }
                        Session["UserId"] = NewObj.User_id;
                        message = "Data Updated successful";
                        MsgType = MessageType.Success;
                    }
                    else
                    {
                        message = "email already exist";
                        MsgType = MessageType.Error;
                    }
                }
                else
                {
                    List<UsersData> Obj = context.UsersData.Where(a => a.User_Email == model.Email ).ToList();
                    if (Obj.Count == 0 && clsSendingEmail.SendRegistration(model.Email))
                    {
                        UsersData NewObj = new UsersData();
                        NewObj.User_name = model.UserName;
                        NewObj.User_IsActive = true;
                        NewObj.IsArabic = false;
                        NewObj.User_Password = model.Password;
                        NewObj.User_Email = model.Email;
                        NewObj.User_Address = model.User_Address;
                        NewObj.User_Company = model.User_Company;
                        NewObj.User_Mobile = model.User_Mobile;
                        NewObj.Gender = model.Gender;
                        //NewObj.User_InteristIn = model.User_InteristIn;
                        NewObj.User_Subscripe = model.User_Subscripe;
                        NewObj.User_LastName = model.LastName;
                        context.AddToUsersData(NewObj);
                        context.SaveChanges();
                        if (model.User_Subscripe == true)
                        {
                            foreach (var item in ctgcheckbox)
                            {
                                context.UserCategory.AddObject(new UserCategory { UserId = NewObj.User_id, CategoryId = item });
                                context.SaveChanges();
                            }
                        }
                        Session["UserId"] = NewObj.User_id;
                        message = "Register successful";
                        MsgType = MessageType.Success;
                    }
                    else
                    {
                        if (Obj.Count > 0)
                        {
                            message = "failed to Register ,email already exist";
                        }
                        else
                        {
                            message = "failed to Register (Invalid email )";
                        }
                        Session["UserId"] = null;
                        MsgType = MessageType.Error;
                    }
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


        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public PartialViewResult Ar_LogOn()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Ar_LogOn(LogOnArModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    string message = "";
                    MessageType MsgType = MessageType.Success;
                    UsersData Obj = context.UsersData.Where(a => ((a.User_Email == model.UserName || a.User_name == model.UserName) && a.User_Password == model.Password)).SingleOrDefault();
                    if (Obj != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        Session["UserId"] = Obj.User_id;
                        message = "تم الدخول بنجاح";
                        MsgType = MessageType.Success;
                    }
                    else
                    {
                        Session["UserId"] = null;
                        message = "يرجى مراجعة بيانات الدخول";
                        MsgType = MessageType.Error;
                    }
                    this.ShowMessage(MsgType, message, true);
                }
            }
            return RedirectToAction("Home", "Ar_Home");
            // If we got this far, something failed, redisplay form
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult Ar_LogOff()
        {
            string message = "";
            MessageType MsgType = MessageType.Success;
            message = "نشكرك على تعاملك مع ماركوم تريد";
            MsgType = MessageType.Warning;
            this.ShowMessage(MsgType, message, true);
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            return RedirectToAction("Home", "Ar_Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Ar_Register()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                var list = new SelectList(new[]
                                          {
                                              new {ID="Mr",Name="السيد"},
                                              new{ID="Ms",Name="السيده"}
                                          },
                           "ID", "Name", 1);
                ViewBag.genderlist = list;
                List<int> lstInt = new List<int>();
                if (Session["UserId"] != null)
                {
                    int userid = (int)Session["UserId"];
                    lstInt.AddRange(context.UserCategory.Where(a => a.UserId == userid).Select(a => a.CategoryId).ToList());
                }
                ViewBag.UserCategoryInt = lstInt;
                ViewBag.Categorylist = context.Categories.Where(a => !a.IsDelete.Value && a.Category_Parent_id == null).OrderBy(a => a.Category_Name_Eng).Where(a => a.Category_Parent_id == null).ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Ar_Register(RegisterArModel model, int RegUser_id, List<int> ctgcheckbox)
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                string message = "";
                MessageType MsgType = MessageType.Success;
                if (RegUser_id != null && RegUser_id > 0)
                {
                    UsersData NewObj = context.UsersData.Where(a => a.User_id == RegUser_id).SingleOrDefault();
                    NewObj.User_name = model.UserName;
                    NewObj.User_IsActive = true;
                    NewObj.User_Password = model.Password;
                    NewObj.User_Email = model.Email;
                    NewObj.User_Address = model.Email;
                    NewObj.User_Company = model.User_Company;
                    NewObj.User_Mobile = model.User_Mobile;
                    NewObj.Gender = model.Gender;
                    NewObj.IsArabic = true;
                    //NewObj.User_InteristIn = model.User_InteristIn;
                    NewObj.User_Subscripe = model.User_Subscripe;
                    NewObj.User_LastName = model.LastName;
                    context.UsersData.ApplyCurrentValues(NewObj);
                    context.SaveChanges();
                    List<UserCategory> LstObj = context.UserCategory.Where(a => a.UserId == NewObj.User_id).ToList();
                    foreach (var item in LstObj)
                    {
                        context.DeleteObject(item);
                        context.SaveChanges();
                    }
                    if (model.User_Subscripe == true)
                    {
                        foreach (var item in ctgcheckbox)
                        {
                            context.UserCategory.AddObject(new UserCategory { UserId = NewObj.User_id, CategoryId = item });
                            context.SaveChanges();
                        }
                    }
                    Session["UserId"] = NewObj.User_id;
                    message = "تم تعديل البيانات بنجاح";
                    MsgType = MessageType.Success;
                }
                else
                {
                    List<UsersData> Obj = context.UsersData.Where(a => a.User_Email == model.Email || a.User_name == model.UserName).ToList();
                    if (Obj.Count == 0 && clsSendingEmail.SendArRegistration(model.Email))
                    {
                        UsersData NewObj = new UsersData();
                        NewObj.User_name = model.UserName;
                        NewObj.User_IsActive = true;
                        NewObj.User_Password = model.Password;
                        NewObj.User_Email = model.Email;
                        NewObj.User_Address = model.User_Address;
                        NewObj.User_Company = model.User_Company;
                        NewObj.User_Mobile = model.User_Mobile;
                        NewObj.Gender = model.Gender;
                        NewObj.IsArabic = true;
                        //NewObj.User_InteristIn = model.User_InteristIn;
                        NewObj.User_Subscripe = model.User_Subscripe;
                        context.AddToUsersData(NewObj);
                        context.SaveChanges();
                        if (model.User_Subscripe == true)
                        {
                            foreach (var item in ctgcheckbox)
                            {
                                context.UserCategory.AddObject(new UserCategory { UserId = NewObj.User_id, CategoryId = item });
                                context.SaveChanges();
                            }
                        }
                        Session["UserId"] = NewObj.User_id;
                        message = "تم التسجيل بنجاح";
                        MsgType = MessageType.Success;
                    }
                    else
                    {
                        if (Obj.Count > 0)
                        {
                            message = "فشل فى تسجيل (البريد الإلكتروني أو اسم المستخدم موجود مسبقا)";
                        }
                        else
                        {
                            message = "فشل فى تسجيل ,البريد الإلكتروني غير صحيح";
                        }
                        Session["UserId"] = null;
                        MsgType = MessageType.Error;
                    }
                }
                this.ShowMessage(MsgType, message, true);
                return RedirectToAction("Home", "Ar_Home");
            }
        }

    }
}
