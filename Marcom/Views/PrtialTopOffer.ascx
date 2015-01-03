<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.Products>>" %>
<% foreach (var item in Model)
   { %>
<li>
    <table border='0' cellpadding='0' cellspacing='0' class='OneproductTable'>
        <tr>
            <td class='ProductImg_td'>
                <img class='Product_img' src="<%=Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path %>" />
            </td>
        </tr>
        <tr>
            <td class='ProductTitle_td'>
                <a href="<%=Url.Action("ProductDetail", "Ar_Products", new {id =item.Product_id ,name=item.Product_Title_Ar})%>"
                    class='ProductTitle_link'><span class='ProductsTitle_span'>
                        <%=item.Product_Title_Ar%>
                    </span></a>
            </td>
        </tr>
        <tr>
            <td class='ProductDescription_td'>
                <p class='ProductDesc_p'>
                    <%=item.Product_Short_Ar != null ? item.Product_Short_Ar : item.Product_Title_Ar%></p>
            </td>
        </tr>
        <tr>
            <td style="height: 30px;">
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td class='AddTocart_td'>
                            <a class="cart" id="A1" itemname="<%=item.Product_Title_Ar %>" itemid="<%=item.Product_id%>">
                            </a>
                        </td>
                        <td class="ProductBrand">
                            <img height="25px" src="http://admin.eg-mt.com/images/brands/320c02219-8f50-4fc1-b8ce-0d0ae84be88c.png">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</li>
<% } %>
