using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Marcom.Helpers
{
    public static class Extension
    {
        public static MvcHtmlString Pager(this HtmlHelper helper, string pageRequest, int pageSize, int total, string urlPrefix)
        {
            int currentPage = 1;
            if (!Int32.TryParse(pageRequest, out currentPage)) currentPage = 1;
            UrlHelper u = new UrlHelper(helper.ViewContext.RequestContext);
            double totalPages = total * 1.0 / pageSize;
            // round total pages up to the nearest integer since we cant have something like 10.5 pages 
            totalPages = (int)Math.Ceiling(totalPages);
            if (totalPages == 1)
                return MvcHtmlString.Create("");

            int count = 0;
            int curIndex = 1;

            // needed for when we are toward the end of the page count 
            // the else ensures that the current page will usually showup in the middle 
            // with the exception of the first two pages 
            if (currentPage == totalPages)
                curIndex = currentPage - 4;
            else if (currentPage == totalPages - 1)
                curIndex = currentPage - 3;
            else
                curIndex = currentPage - 2;

            TagBuilder t = new TagBuilder("span");
            StringBuilder sb = new StringBuilder();

            while (count < 5 && curIndex <= totalPages)
            {
                if (curIndex < currentPage && curIndex > 0)
                {
                    sb.Append(CreateLink(curIndex, urlPrefix, u));
                    count++;
                }
                else if (curIndex > currentPage && curIndex <= totalPages)
                {
                    sb.Append(CreateLink(curIndex, urlPrefix, u));
                    count++;
                }
                else if (curIndex == currentPage)
                {
                    sb.AppendFormat("<span class='current'>{0}</span>", currentPage);
                    count++;
                }

                curIndex++;
            }


            t.InnerHtml = sb.ToString();

            return MvcHtmlString.Create(t.ToString());
        }



        private static string CreateLink(int currentPage, string urlPrefix, UrlHelper u)
        {


            TagBuilder t = new TagBuilder("a");
            string address = "";
            if (urlPrefix.Contains("?"))
            {
                address = urlPrefix + "&Page=" + currentPage;
            }
            else
            {
                address = urlPrefix + "?Page=" + currentPage;
            }
            t.MergeAttribute("href", address);
            t.MergeAttribute("class", "inactive");
            t.SetInnerText(currentPage.ToString());
            return t.ToString();


        }


        public static MvcHtmlString PagerAjax(this HtmlHelper helper, string pageRequest, int pageSize, int total, string urlPrefix)
        {
            int currentPage = 1;
            if (!Int32.TryParse(pageRequest, out currentPage)) currentPage = 1;
            UrlHelper u = new UrlHelper(helper.ViewContext.RequestContext);
            double totalPages = total * 1.0 / pageSize;
            // round total pages up to the nearest integer since we cant have something like 10.5 pages
            totalPages = (int)Math.Ceiling(totalPages);
            if (totalPages == 1)
                return MvcHtmlString.Create("");

            int count = 0;
            int curIndex = 1;

            // needed for when we are toward the end of the page count
            // the else ensures that the current page will usually showup in the middle
            // with the exception of the first two pages
            if (currentPage == totalPages)
                curIndex = currentPage - 4;
            else if (currentPage == totalPages - 1)
                curIndex = currentPage - 3;
            else
                curIndex = currentPage - 2;

            TagBuilder t = new TagBuilder("span");
            StringBuilder sb = new StringBuilder();

            while (count < 5 && curIndex <= totalPages)
            {
                if (curIndex < currentPage && curIndex > 0)
                {
                    sb.Append(CreateLinkAjax(curIndex, urlPrefix, u));
                    sb.Append("|");
                    count++;
                }
                else if (curIndex > currentPage && curIndex <= totalPages)
                {
                    sb.Append(CreateLinkAjax(curIndex, urlPrefix, u));
                    sb.Append("|");
                    count++;
                }
                else if (curIndex == currentPage)
                {
                    sb.AppendFormat("<span>{0}</span>|", currentPage);
                    count++;
                }

                curIndex++;
            }

            if (sb.Length > 1)
                sb = sb.Remove(sb.ToString().LastIndexOf('|'), 1);

            t.InnerHtml = sb.ToString();

            return MvcHtmlString.Create(t.ToString());
        }

        private static string CreateLinkAjax(int currentPage, string urlPrefix, UrlHelper u)
        {
            TagBuilder t = new TagBuilder("a");
            t.MergeAttribute("class", "AjaxLinkPaging");
            string address = "";
            if (!urlPrefix.Contains("?"))
            {
                address = urlPrefix + "?Page=" + currentPage;
            }
            else
            {
                address = urlPrefix + "&Page=" + currentPage;
            }
            t.MergeAttribute("alt", address);
            t.MergeAttribute("href", "#");
            t.SetInnerText(currentPage.ToString());
            return t.ToString();
        }
    }
}