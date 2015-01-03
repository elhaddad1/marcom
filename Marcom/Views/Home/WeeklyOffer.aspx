<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.WeeklyOffer>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterWeeklyOffer_table" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "Home")%>" class="SiteMap_link">
                    <h1>Home ></h1>
                </a><a href="javascript:void(0);" class="SiteMap_link">
                    <h1>Weekly Offer </h1>
                </a>
            </td>
        </tr>
        <tr>
            <td class="MasterWeeklyOffer_table_td1">
                <img src="<%=Marcom.Models.clsGlobel.SrcWeeklyOfferDomain+Model.Image_Path %>" width='250'
                    height='187.5' id='Img1' alt="<%=Model.Product_Title_Eng %>" />
            </td>
            <td class="MasterWeeklyOffer_table_td2">
                <table class="Offer_table">
                    <tr>
                        <td class="Offer_table_td1">
                            <table class="OfferHeader_table">
                                <tr>
                                    <td class="OfferHeader_table_td1">
                                        <div class="HeaderText_div">
                                            <h2>
                                                <%=Model.Product_Title_Eng %></h2>
                                        </div>
                                    </td>
                                    <td class="OfferHeader_table_td2">
                                        <img src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+Model.Products.Brands.Image_Path %>"
                                            style="max-height: 70px; max-width: 200px;" alt="<%=Model.Product_Title_Eng %>" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td2">
                            <p class="DescriptionText_p">
                                <%=Model.Product_Highlight_Eng != null ? Model.Product_Highlight_Eng : Model.Product_Title_Eng%>
                            </p>
                            <span style="font-family: Calibri;">For more details <a href="<%=Url.Action("ProductDetail", "Products", new {id =Model.Product_id ,name=Model.Product_Title_Eng})%>">click here</a></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td3">
                            <a class="cart" id="Addcartlink" itemname="<%=Model.Product_Title_Eng %>" itemid="<%=Model.Product_id%>"></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" Marcom Trade Your Partner & Technical Advisor For safe Navigation at Sea Egypt, Africa, and Middle East">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />

    <link href="../../Resources/Styles/Products/WeeklyOffer.css" rel="stylesheet" type="text/css" />
</asp:Content>
