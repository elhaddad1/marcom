<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.Products>>" %>
<%@ Import Namespace="Marcom.Helpers" %>
   <link href="../../Resources/Styles/Products/Products_Style.css" rel="stylesheet"
        type="text/css" />
<table class="MasterProductsTable" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                 <a href="<%=Url.Action("Home", "Home")%>" class="SiteMap_link"><span class="SiteMap_span">Home ></span></a> <a
                    href="#" class="SiteMap_link"><span class="SiteMap_span">Search </span></a>
            </td>
        </tr>
        <tr>
            <td class="MasterProductsTable_td2">
            <%if (Model.Count() > 0)
              { %>
                <ul class="portfolio">
                  <%foreach (var item in Model)
                    {%>
                <li>
                        <div class="OneProduct_div">
                                        <table border="0" cellpadding="0" cellspacing="0" class="OneproductTable">
                                            <tr>
                                                <td class="ProductImg_td">
                                                    <img class="Product_img" src="<%=Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path%>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ProductTitle_td">
                                                
                                                    <a href="<%=Url.Action("ProductDetail", "Ar_Products", new {id =item.Product_id ,name=item.Product_Title_Ar})%>" class="ProductTitle_link"><span class="ProductsTitle_span"><%=item.Product_Title_Ar%></span></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ProductDescription_td">
                                                    <p class="ProductDesc_p">
                                                       <%=item.Product_Short_Ar != null ? item.Product_Short_Ar : item.Product_Title_Ar%></p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="AddTocart_td">
                                                    <a class="cart" href=""></a>
                                                </td>
                                            </tr>
                                        </table>
                        </div>
                    </li>
                    <%} %>
                 </ul>
                 <%}
              else
              { %>
              <div style="width: 500px; height: 25px; background-color: #005984; color:White;">لا نتائج مطابقة لكلمتك</div>
                 <%} %>
            </td>
        </tr>
        <tr>
            <td class="MasterProductsTable_td3">
                <%: Html.PagerAjax(Request.QueryString["Page"], 8, (int)ViewData["ListCount"], Url.Action("AllCategAndBrandItems", "SearchAutoCmplt", new { StrName = (string)ViewData["StrName"]}))%>
            </td>
        </tr>
    </table>
