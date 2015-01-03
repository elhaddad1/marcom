using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Marcom.Models
{
    public class clsGlobel
    {

        #region Fields



        private static byte[] key = { };

        private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };

        private static string stringKey = "!5663a#KN";



        #endregion

        #region Public Methods


        public static string Encrypt(string text)
        {

            try
            {

                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));



                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] byteArray = Encoding.UTF8.GetBytes(text);



                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,

                    des.CreateEncryptor(key, IV), CryptoStreamMode.Write);



                cryptoStream.Write(byteArray, 0, byteArray.Length);

                cryptoStream.FlushFinalBlock();



                return Convert.ToBase64String(memoryStream.ToArray());

            }

            catch (Exception ex)
            {


            }



            return string.Empty;

        }



        public static string Decrypt(string text)
        {

            try
            {

                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));



                DESCryptoServiceProvider des = new DESCryptoServiceProvider();


                Byte[] byteArray = Convert.FromBase64String(text);



                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,

                    des.CreateDecryptor(key, IV), CryptoStreamMode.Write);



                cryptoStream.Write(byteArray, 0, byteArray.Length);

                cryptoStream.FlushFinalBlock();



                return Encoding.UTF8.GetString(memoryStream.ToArray());


            }

            catch (Exception ex)
            {


            }



            return string.Empty;

        }
        #endregion
        public static string SrcbrandsDomain = "http://admin.eg-mt.com/images/brands/";
        public static string SrcCategorysDomain = "http://admin.eg-mt.com/images/Categorys/";
        public static string SrcDepartmentsDomain = "http://admin.eg-mt.com/images/Departments/";
        public static string SrcNewsDomain = "http://admin.eg-mt.com/images/News/";
        public static string SrcServiceDomain = "http://admin.eg-mt.com/images/Services/";
        public static string SrcProductDomain = "http://admin.eg-mt.com/images/Product/";
        public static string SrcProductGalleryDomain = "http://admin.eg-mt.com/images/ProductGallery/";
        public static string SrcWeeklyOfferDomain = "http://admin.eg-mt.com/images/WeeklyOffer/";
        public static string SrcimgDomain = "http://admin.eg-mt.com/images/img/";
        public static string SrcAboutUsDomain = "http://admin.eg-mt.com/images/AboutUs/";
        public static string SrcCertificateDomain = "http://admin.eg-mt.com/images/Certificate/";
        public static string SrcLiksDomain = "http://admin.eg-mt.com/images/Liks/";
        public static string SrcWebSitesDomain = "http://admin.eg-mt.com/images/WebSites/";
        public static string SrcLogoDomain = "http://admin.eg-mt.com/images/MarcomSetting/";


        //public static string SrcbrandsDomain = "http://localhost:4455/images/brands/";
        //public static string SrcCategorysDomain = "http://localhost:4455/images/Categorys/";
        //public static string SrcDepartmentsDomain = "http://localhost:4455/images/Departments/";
        //public static string SrcNewsDomain = "http://localhost:4455/images/News/";
        //public static string SrcNewsDomain = "http://localhost:4455/images/Services/";
        //public static string SrcProductDomain = "http://localhost:4455/images/Product/";
        //public static string SrcProductGalleryDomain = "http://localhost:4455/images/ProductGallery/";
        //public static string SrcWeeklyOfferDomain = "http://localhost:4455/images/WeeklyOffer/";
        //public static string SrcimgDomain = "http://localhost:4455/images/img/";
        //public static string SrcAboutUsDomain = "http://localhost:4455/images/AboutUs/";
        //public static string SrcCertificateDomain = "http://localhost:4455/images/Certificate/";
        //public static string SrcLiksDomain = "http://localhost:4455/images/Liks/";

        public static int getTotalNewsRecords()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return context.News.Count();
            }
        }
        public static int getTotalServiceRecords()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return context.Service.Count();
            }
        }


        public static int getTotalProductsRecords()
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return context.Products.Count();
            }
        }

        public static string GetUserName(int id )
        {
            using (MarcomEntities context = new MarcomEntities())
            {
                return context.UsersData.Where(a => a.User_id == id).SingleOrDefault().User_name;
            }
        }
    }
}