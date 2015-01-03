<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.WeeklyOffer>>" %>
<% foreach (var item in Model)
   { %>
<table border="0" cellpadding="0" cellspacing="0" class="OneproductTable">
    <tr>
    <td class="ProductBrand">
                    <img style="top:5px; float:right;right:5px; max-width:125px;" src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+item.Products.Brands.Image_Path %>">
                    </td>
        <td class="ProductImg_td">
            <a href="<%=Url.Action("WeeklyOffer", "Ar_Home", new {id =item.id})%>" class="ProductTitle_link">
            <img class="Product_img" 
                src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcWeeklyOfferDomain+item.Image_Path, width = 200, height = 150 }) %>"
                <%--src="<%=Marcom.Models.clsGlobel.SrcWeeklyOfferDomain+item.Image_Path %>"--%> />
            </a>
        </td>
    </tr>
    <tr>
        <td class="ProductTitle_td" colspan="2" style="width:auto;">
            <a href="<%=Url.Action("WeeklyOffer", "Ar_Home", new {id =item.id})%>" class="ProductTitle_link">
                <span class="ProductsTitle_span">
                    <%=item.Product_Title_Ar %></span></a>
        </td>
    </tr>
    <tr>
        <td class="ProductDescription_td" colspan="2" style="width:auto;">
           <span class="desc">
                <%=item.Products.Product_Short_Ar != null ? item.Products.Product_Short_Ar : item.Product_Title_Ar%>
        </span>
        </td>
    </tr>
    <tr>
        <td style="height:30px;width:auto;" colspan="2" >
            <table style="width: 100%; height: 100%;">
                <tr>
                    <td class='AddTocart_td'>
                        <a class="cart" id="Addcartlink" itemname="<%=item.Product_Title_Ar %>" itemid="<%=item.Product_id%>">
                        </a>
                    </td>
                    
                </tr>
            </table>
        </td>
    </tr>
</table>
<% } %>