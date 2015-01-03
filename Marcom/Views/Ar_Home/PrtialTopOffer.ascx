<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.Products>>" %>
<% foreach (var item in Model)
   { %>
<li>
    <table border='0' cellpadding='0' cellspacing='0' class='OneproductTable'>
        <tr>
         <td class="ProductBrand">
                            <img style="top:5px; float:left;right:5px; max-width:125px;"  src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+item.Brands.Image_Path %>">
                   </td>
            <td class='ProductImg_td'>
                <a href="<%=Url.Action("ProductDetail", "Ar_Products", new {id =item.Product_id ,name=item.Product_Title_Ar})%>"
                    class='ProductTitle_link'>
                <img class='Product_img' alt="<%=item.Image_Alt_Ar %>" 
                    src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path, width = 200, height = 150 }) %>"
                    <%--src="<%=Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path %>"--%> />
                    <%--src="<%=Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path %>" />--%>
                </a>
            </td>
        </tr>
        <tr>
            <td class='ProductTitle_td' colspan="2">
                <a href="<%=Url.Action("ProductDetail", "Ar_Products", new {id =item.Product_id ,name=item.Product_Title_Ar})%>"
                    class='ProductTitle_link'><span class='ProductsTitle_span'>
                        <%=item.Product_Title_Ar%>
                    </span></a>
            </td>
        </tr>
        <tr>
            <td class='ProductDescription_td' colspan="2">
                <span class="desc">
                    <%=item.Product_Short_Ar != null ? item.Product_Short_Ar : item.Product_Title_Ar%>
                    </span>
            </td>
        </tr>
        <tr>
            <td style="height: 30px;" colspan="2">
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td class='AddTocart_td'>
                            <a class="cart" id="A1" itemname="<%=item.Product_Title_Ar %>" itemid="<%=item.Product_id%>">
                            </a>
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</li>
<% } %>
