<%@ Page Language="C#" MasterPageFile="~/Views/shared/Site.Master" AutoEventWireup="true"
    CodeBehind="WeeklyOffer.aspx.cs" Inherits="MarcomTrade.Views.Products.WeeklyOffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Resources/Styles/Products/WeeklyOffer.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="MasterWeeklyOffer_table" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="#" class="SiteMap_link"><span class="SiteMap_span">Home ></span></a> <a
                    href="#" class="SiteMap_link"><span class="SiteMap_span">Weekly Offer </span>
                </a>
            </td>
        </tr>
        <tr>
            <td class="MasterWeeklyOffer_table_td1">
                <img src="<%=Page.ResolveClientUrl("~/Resources/Images/Uploads/News/News1.png")%>"
                    width='250' height='187.5' id='Img1' />
            </td>
            <td class="MasterWeeklyOffer_table_td2">
                <table class="Offer_table">
                    <tr>
                        <td class="Offer_table_td1">
                            <table class="OfferHeader_table">
                                <tr>
                                    <td class="OfferHeader_table_td1">
                                        <div class="HeaderText_div">
                                            <span class="HeaderText_span">product1</span></div>
                                    </td>
                                    <td class="OfferHeader_table_td2">
                                        <div class="Range_div">
                                            <span class="HeaderText_Range">Brand</span></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td2">
                            <p class="DescriptionText_p">
                                test products test products test products test products test products test products
                                test products test products test products test products test products test products
                                test products test products
                                <br />
                                <br />
                                Features:<br />
                                -test test test test test .<br />
                                -test test test test
                                <br />
                                -test test test test
                                <br />
                                -test test test test
                                <br />
                                -test test test test
                                <br />
                                -test test test test
                            </p>
                            <br />
                            <span>For more details click <a href="#">here</a></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="Offer_table_td3">
                            <a class="cart" id="Addcartlink" itemname="" itemid=""></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
