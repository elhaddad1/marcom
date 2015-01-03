using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using Marcom.Controllers;

namespace Marcom.Models
{
    public class clsSendingEmail
    {
        public static bool SendContactUs(string email, string name, string company, string phone, string strmessage)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("webmaster@marcom.eg-mt.com");
                message.Subject = string.Format("From {0} ", email);
                message.To.Add("webmaster@marcom.eg-mt.com");
                message.Body = string.Format("From : {0},Company : {1},Name : {2},Phone : {3}, Message : {4}", email, company, name, phone, strmessage);
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.marcom.eg-mt.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SendPassword(string email)
        {
            try
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    UsersData UsrObj = context.UsersData.Where(a => a.User_Email == email).SingleOrDefault();
                    if (UsrObj != null)
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("webmaster@marcom.eg-mt.com");
                        message.Subject ="Forget password email";
                        message.To.Add(email);
                        message.Body = clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/EnForgetEmail?Password=" + UsrObj.User_Password + "");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "mail.marcom.eg-mt.com";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
                        smtp.Send(message);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void SendCheckOut( List<string> ToEmail, string Message)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("webmaster@marcom.eg-mt.com");
            message.Subject = string.Format("From {0} ", "marcom Trade");
            foreach (var em in ToEmail)
            {
                message.To.Add(em);
            }
            message.Body = Message;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.marcom.eg-mt.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
            smtp.Send(message);
        }
        public static bool SendRegistration(string email)
        {
            try
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    UsersData UsrObj = context.UsersData.Where(a => a.User_Email == email).SingleOrDefault();
                    if (UsrObj == null)
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("webmaster@marcom.eg-mt.com");
                        message.Subject = "Welcome To MarcomTrade.com";
                        message.To.Add(email);
                        message.Body = clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/EN_RegisterEmail");
                        message.IsBodyHtml = true;
                        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "mail.marcom.eg-mt.com";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
                        smtp.Send(message);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            // catching SmtpException 
            catch (SmtpException exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Arabic Area
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="phone"></param>
        /// <param name="strmessage"></param>
        /// <returns></returns>
        public static void SendArCheckOut(List<string> ToEmail, string Message)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("webmaster@marcom.eg-mt.com");
            message.Subject = string.Format("From {0} ", "marcom Trade");
            foreach (var em in ToEmail)
            {
                message.To.Add(em);
            }
            message.Body = Message;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.marcom.eg-mt.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
            smtp.Send(message);
        }
        public static bool SendArRegistration(string email)
        {
            try
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    UsersData UsrObj = context.UsersData.Where(a => a.User_Email == email).SingleOrDefault();
                    if (UsrObj == null)
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("webmaster@marcom.eg-mt.com");
                        message.Subject = "مرحبا بك فى ماركوم تراد";
                        message.To.Add(email);
                        message.Body = clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/Ar_RegisterEmail");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "mail.marcom.eg-mt.com";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
                        smtp.Send(message);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SendArPassword(string email)
        {
            try
            {
                using (MarcomEntities context = new MarcomEntities())
                {
                    UsersData UsrObj = context.UsersData.Where(a => a.User_Email == email).SingleOrDefault();
                    if (UsrObj != null)
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("webmaster@marcom.eg-mt.com");
                        message.Subject = "Forget password email";
                        message.To.Add(email);
                        message.Body = clsWebServiceController.GetHtmlFromUrl("http://marcomtrade.com/MarktingEmail/ArForgetEmail?Password=" + UsrObj.User_Password + "");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "mail.marcom.eg-mt.com";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("webmaster@marcom.eg-mt.com", "P@ssw0rd");
                        smtp.Send(message);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtmlFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url", "Parameter is null or empty");

            string html = "";
            HttpWebRequest request = GenerateHttpWebRequest(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                // Get the response stream.
                Stream responseStream = response.GetResponseStream();
                // Use a stream reader that understands UTF8.
                using (StreamReader reader =
                new StreamReader(responseStream, Encoding.UTF8))
                {
                    html = reader.ReadToEnd();
                }

            }
            return html;
        }
        public static HttpWebRequest GenerateHttpWebRequest(string UriString)
        {
            // Get a Uri object.
            Uri Uri = new Uri(UriString);
            // Create the initial request.
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Uri);
            // Return the request.
            return httpRequest;
        }
    }
}