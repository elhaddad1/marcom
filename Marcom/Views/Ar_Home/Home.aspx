<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="مــــــــــاركــوم تـريــــــد شــــــريــكك ومستشــــــــــارك الفنــــــــى للمـــــلاحــة الامنـــــــــة فى مصــــر، افرقيــــا، والشــــرق الاوســــــط ">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />
    <%-------------------Css-------------%>
    <!--[if IE 7]>
  <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Home/ieAr_Home_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if IE 8]>
  <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Home/ieAr_Home_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if IE 9]>
  <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Home/ieAr_Home_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if !IE]> -->
    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Home/Ar_Home_Style.css")%>"
        rel="stylesheet" type="text/css" />
    <!-- <![endif]-->
    <link href="<%=Page.ResolveClientUrl("~/Resources/JQuery/Slider/SlideShow1.2/css/rhinoslider-1.05.css")%>"
        rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/css/Ar_jquery.fancyNews-1.2.css")%>"
        rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/css/jquery.jscrollpane.css")%>"
        rel="stylesheet" type="text/css" />
    <%--------------------------------------------Js----------------%>
    <script async src="<%=Page.ResolveClientUrl("~/Resources/JQuery/Slider/SlideShow1.2/js/rhinoslider-1.05.min.js")%>"
        type="text/javascript"></script>
    <script async src="<%=Page.ResolveClientUrl("~/Resources/JQuery/Slider/SlideShow1.2/js/mousewheel.min.js")%>"
        type="text/javascript"></script>
    <script async src="<%=Page.ResolveClientUrl("~/Resources/JQuery/Slider/SlideShow1.2/js/easing.min.js")%>"
        type="text/javascript"></script>
    <!-- Plugin for scaling images in the posts -->
    <script async type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/js/jquery.jScale.js")%>"></script>
    <!-- A custom jQueriy UI plugin, which contains the color animation.-->
    <!-- Plugin for loading feeds -->
    <script async type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/js/Ar_jquery.fancyNews.min.js")%>"></script>
    <script async type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/js/jquery.jgfeed.js")%>"></script>
    <!-- Plugins needed for the scrollbar -->
    <%--    <script async type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/js/jquery.mousewheel.js")%>"></script>--%>
    <script async type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/JQuery/fancynews/js/jquery.jscrollpane.min.js")%>"></script>
    <!-- FancyNews plugin -->
    <script async src="<%=Page.ResolveClientUrl("~/Js/Home/ArHomeInitiat.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterHomeTable" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td class="MasterHomeTable_td1">
                <table class="Word-FlashTable">
                    <tr>
                        <td class="Word-FlashTable_td1">
                            <div class="Word_div">
                                <%if (ViewData["AboutUs"] != null)
                                  { %>
                                <%=ViewData["AboutUs"]%>
                                <%} %>
                            </div>
                        </td>
                        <td class="Word-FlashTable_td2">
                            <div class="PhotoGallary_div">
                                <ul id="PhotoGallary">
                                    <%=Html.Action("SliderImage", "Ar_Home")%>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="MasterHomeTable_td2">
                <table class="HomeTable" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="HomeTable_td1">
                            <div class="HomeTable_td1_div">
                                <div class="HomeHeaders_div">
                                </div>
                                <div class="Offers_div" align="center">
                                    <ul id="TopProducts">
                                        <%=Html.Action("PrtialTopOffer", "Ar_Home")%>
                                    </ul>
                                </div>
                            </div>
                        </td>
                        <td class="HomeTable_td2">
                            <div class="HomeTable_td2_div">
                                <div class="HomeHeaders_div">
                                </div>
                                <div class="Offers_div">
                                    <%=Html.Action("WeeklyOffers", "Ar_Home")%>
                                </div>
                            </div>
                        </td>
                        <td class="HomeTable_td3">
                            <div class="HomeTable_td3_div">
                                <div class="News_div">
                                    <div id="fancyNews" style="width: 100%">
                                        <%=Html.Action("PrtialNews", "Ar_Home")%>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
