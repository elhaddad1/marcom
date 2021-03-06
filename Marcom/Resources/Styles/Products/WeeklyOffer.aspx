﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.WeeklyOffer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Marcom
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterWeeklyOffer_table" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "ar_Home")%>" class="SiteMap_link">
                    <h1>الرئيسية ></h1>
                </a><a href="javascript:void(0);" class="SiteMap_link">
                    <h1>عرض الاسبوع </h1>
                </a>
            </td>
        </tr>
        <tr>
            <td class="MasterWeeklyOffer_table_td1">
                <img src="<%=Marcom.Models.clsGlobel.SrcWeeklyOfferDomain+Model.Image_Path %>" width='250'
                    height='187.5' id='Img1' />
            </td>
            <td class="MasterWeeklyOffer_table_td2">
                <table class="Offer_table">
                    <tr>
                        <td class="Offer_table_td1">
                            <table class="OfferHeader_table">
                                <tr>
                                    <td class="OfferHeader_table_td1">
                                        <div class="HeaderText_div">
                                            <span class="HeaderText_span">
                                                <%=Model.Product_Title_Ar %></span>
                                        </div>
                                    </td>
                                    <td class="OfferHeader_table_td2">
                                        <img src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+Model.Products.Brands.Image_Path %>" style="max-height: 70px; max-width: 200px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td2">

                            <%=Model.Product_Highlight_Ar != null ? Model.Product_Highlight_Ar : Model.Product_Title_Ar%>

                            <br />
                            <span style="font-family: Calibri;">لمزيد من المعلومات  <a href="<%=Url.Action("ProductDetail", "Ar_Products", new {id =Model.Product_id ,name=Model.Product_Title_Ar})%>">أضغط هنا</a></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td3">
                            <a class="cart" id="Addcartlink" itemname="<%=Model.Product_Title_Ar %>" itemid="<%=Model.Product_id%>"></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="../../Resources/Styles/Products/Ar_WeeklyOffer.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
