<%@ Page   Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Marcom.Models.Products>>" %>

<%@ Import Namespace="Marcom.Helpers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterProductsTable" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "Ar_Home")%>" class="SiteMap_link"><h1>
                    الرئيسية ></h1></a>
                <%if ((int)ViewData["DeptId"] > 0 || (int)ViewData["BrandId"] > 0 || (int)ViewData["CatgId"] > 0)
                  { %>
                <a href="javascript:void(0);" class="SiteMap_link"><h1>
                    <%if (ViewData["Deptstr"] != null && ViewData["Deptstr"] != "")
                      {%>
                    المنتجات -
                    <%= ViewData["Deptstr"]%>
                    |
                    <% } %>
                    <%if (ViewData["Brandstr"] != null && ViewData["Brandstr"] != "")
                      {%>
                    الماركات -
                    <%= ViewData["Brandstr"]%>
                    |
                    <% } %>
                    <%if (ViewData["Catgstr"] != null && ViewData["Catgstr"] != "")
                      {%>
                    الفئات -
                    <%= ViewData["Catgstr"]%>
                    <% } %>
                </h1></a>
                <%} %>
            </td>
        </tr>
        <tr>
            <td class="MasterProductsTable_td2">
                <ul class="portfolio">
                    <%foreach (var item in Model)
                      {%>
                    <li>
                        <div class="OneProduct_div">
                            <table border="0" cellpadding="0" cellspacing="0" class="OneproductTable">
                                <tr>
                                <td class="BrandImg_td">
                                        <img  src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+item.Brands.Image_Path %>" alt="Brand">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ProductImg_td">
                                        <a href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =item.Product_id ,name=item.Product_Title_Ar})%>"
                                            class="ProductTitle_link">
                                        <img class="Product_img" alt="<%=item.Image_Alt_Ar %>" 
                                            
                                            src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcProductDomain+item.Image_Path, width = 200, height = 150 }) %>"
                                            

                                    </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ProductTitle_td">
                                        <a href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =item.Product_id ,name=item.Product_Title_Ar})%>"
                                            class="ProductTitle_link">
                                            <h1><%=item.Product_Title_Ar%></h1></a>
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
                                        <a class="cart" id="Addcartlink" itemname="<%=item.Product_Title_Ar %>" itemid="<%=item.Product_id%>">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </li>
                    <%} %>
                </ul>
            </td>
        </tr>
        <tr>
            <td class="MasterProductsTable_td3">
                <%: Html.Pager(Request.QueryString["Page"], 8, (int)ViewData["ListCount"], Url.Action("Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] }))%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <% Page.Title = ViewData["Deptstr"] + "-" + ViewData["Brandstr"] + "-" + ViewData["Catgstr"];%>
    <meta name="description" content="<%= ViewData["Deptstr"]%>  <%= ViewData["Brandstr"]%> <%= ViewData["Catgstr"]%>"/>
    <meta name="keywords" content="<%= ViewData["Deptstr"]%> , <%= ViewData["Brandstr"]%> , <%= ViewData["Catgstr"]%> ,amp,marine,simrad,koden,radar"/>
    <meta name="Title" content="<%= ViewData["Deptstr"]%>  <%= ViewData["Brandstr"]%> <%= ViewData["Catgstr"]%>"/>

    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/Ar_Products_Style.css")%>"
        rel="stylesheet" type="text/css" />
</asp:Content>
