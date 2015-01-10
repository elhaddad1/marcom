<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.WeeklyOffer>" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table class="MasterWeeklyOffer_table" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "Home")%>" class="SiteMap_link">
                    <h1>Home ></h1>
                </a><a
                    href="javascript:void(0);" class="SiteMap_link">
                    <h1>Weekly Offer </h1>
                </a>
            </td>
        </tr>
        <tr>
            <td class="MasterWeeklyOffer_table_td1">
                <img src="<%=Marcom.Models.clsGlobel.SrcWeeklyOfferDomain+Model.Image_Path %>"
                    width='250' height='187.5' id='Img1' alt="<%=Model.Product_Title_Ar %>" />
            </td>
            <td class="MasterWeeklyOffer_table_td2">
                <table class="Offer_table">
                    <tr>
                        <td class="Offer_table_td1">
                            <table class="OfferHeader_table">
                                <tr>
                                    <td class="OfferHeader_table_td1">
                                        <div class="HeaderText_div">
                                            <span class="HeaderText_span"><%=Model.Product_Title_Ar %></span>
                                        </div>
                                    </td>
                                    <td class="OfferHeader_table_td2">
                                        <div class="Range_div">
                                            <span class="HeaderText_Range"><%=Model.Products.Brands.Brand_Name_Ar %></span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td2">
                            <p class="DescriptionText_p">
                                <%=Model.Product_Highlight_Ar != null ? Model.Product_Highlight_Ar : Model.Product_Title_Ar%>
                            </p>
                            <br />
                            <span>For more details click  <a href="<%=Url.Action("ProductDetail", "Products", new {id =Model.Product_id ,name=Model.Product_Title_Ar})%>">here</a></span>
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
    <meta name="description" content="مــــــــــاركــوم تـريــــــد شــــــريــكك ومستشــــــــــارك الفنــــــــى للمـــــلاحــة الامنـــــــــة فى مصــــر، افرقيــــا، والشــــرق الاوســــــط ">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />
    <link href="../../Resources/Styles/Products/Ar_WeeklyOffer.css" rel="stylesheet" type="text/css" />
</asp:Content>
