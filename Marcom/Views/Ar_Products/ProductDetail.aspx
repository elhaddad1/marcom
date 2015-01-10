<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.Products>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Meta" runat="server">
    <%=Model.Meta_SocialTags_Ar %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterPtoductDetails_table" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "Ar_Home")%>" class="SiteMap_link">
                    <h1>الرئيسية ></h1>
                </a>
                <%if ((int)ViewData["DeptId"] > 0 || (int)ViewData["BrandId"] > 0 || (int)ViewData["CatgId"] > 0)
                  { %>
                <a href="<%=Url.Action("Products", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] })%>"
                    class="SiteMap_link">
                    <h1>
                        <%if ((int)ViewData["DeptId"] > 0)
                          {%>
                        المنتجات -
                        <%= Model.Departments.Department_Name_Ar %>
                        |
                        <% } %>
                        <%if ((int)ViewData["BrandId"] > 0)
                          {%>
                        الماركات -
                        <%= Model.Brands.Brand_Name_Ar %>
                        |
                        <% } %>
                        <%if ((int)ViewData["CatgId"] > 0)
                          {%>
                        الفئات -
                        <%= Model.Categories.Category_Name_Ar %>
                        <% } %>
                        ></h1>
                </a>
                <%} %>
                <a href="#" class="SiteMap_link">
                    <h1>
                        <%=Model.Product_Title_Ar %></h1>
                </a>
            </td>
        </tr>
        <tr>
            <td class="ProductDetails_td">
                <table class="ProductDetails_table" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="ProductDetailsTable_td1">
                            <table class="Text_table" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="Text_table_header_range" colspan="2">
                                        <div class="Range_div">
                                            <h1>
                                                <%=Model.Departments.Department_Name_Ar %></h1>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Text_table_header_td">
                                        <div class="Text_table_header_td_div">
                                            <h2>
                                                <%=Model.Product_Title_Ar %></h2>
                                        </div>
                                        <!-- AddThis Button BEGIN -->
                                        <a class="addthis_button" href="http://www.addthis.com/bookmark.php?v=300&amp;pubid=ra-52ad6af574c1238d">
                                            <img src="http://s7.addthis.com/static/btn/v2/lg-share-en.gif" width="125" height="16"
                                                alt="Bookmark and Share" style="border: 0" /></a>
                                        <script type="text/javascript">                                            var addthis_config = { "data_track_addressbar": true };</script>
                                        <script async type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-52ad6af574c1238d"></script>
                                        <!-- AddThis Button END -->
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Text_table_description_td">
                                        <%=Model.Product_Highlight_Ar %>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="Text_table_AddCart_td">
                                        <a class="cart" id="Addcartlink" itemname="<%=Model.Product_Title_Ar %>" itemid="<%=Model.Product_id%>"></a></td>
                                </tr>
                            </table>
                        </td>
                        <td class="ProductDetailsTable_td2">
                            <table class="ImagesTable" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="Text_table_Brand_td">
                                        <img class="Brand_img" src="<%=Marcom.Models.clsGlobel.SrcbrandsDomain+Model.Brands.Image_Path %>" alt="Brand" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ImagesTable_td1">
                                        <div id="product-image">
                                            <a id="Image-Link" href="<%=Marcom.Models.clsGlobel.SrcProductDomain+Model.Image_Path%>"
                                                target="_blank">
                                                <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath =Marcom.Models.clsGlobel.SrcProductDomain+Model.Image_Path, width = 250, height = 187 }) %>"
                                                    id='product-img-display' alt="<%=Model.Product_Title_Ar %>" />
                                            </a>
                                        </div>
                                        <div id="product-imageicons">
                                            <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcProductDomain+Model.Image_Path, width = 250, height = 187 }) %>"
                                                width='50' height='37.5' alt="Marcom Trade" />
                                            <%
                                                foreach (var Img in Model.ProductGallery)
                                                {%>
                                            <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcProductGalleryDomain+Img.Image_Path, width = 250, height = 187 })%>"
                                                width='50' height='37.5' alt="Marcom Trade" />
                                            <%
                                                } %>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%if (Model.RelatedProduct.Count > 0 || (Model.Product_Spec_Ar != null && Model.Product_Spec_Ar.Trim() != "") || (Model.Product_Ftr_Ar != null && Model.Product_Ftr_Ar.Trim() != "") || Model.ProductComponents.Count > 0 || Model.AccessoriesProducr.Count > 0)
                      {%>
                    <tr>
                        <td class="ProductDetailsTable_td3" colspan="2">
                            <div id="tabs" style="max-height: 864px;">
                                <ul>
                                    <li class="clstabs-3"><a href="#tabs-3">خصائص</a></li>
                                    <li class="clstabs-2"><a href="#tabs-2">مواصفات</a></li>
                                    <li class="clstabs-1"><a href="#tabs-1">أكسسوارات</a></li>
                                    <li class="clstabs-4"><a href="#tabs-4">الأجهزة المتعلقة</a></li>
                                </ul>
                                <div id="tabs-1">
                                    <%if (Model.ProductComponents.Count > 0 || Model.AccessoriesProducr.Count > 0)
                                      { %>
                                    <div id="product-slide" style="width: 850px">
                                        <div id="slider-left">
                                            &lt;
                                        </div>
                                        <div id="slider-container">
                                            <div id="slider" class="slider">
                                                <%foreach (var item in Model.ProductComponents)
                                                  { %>
                                                <div class="section">
                                                    <div>
                                                        <a class="newproduct" href="" alt="G142C" title="G142C">
                                                            <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath =Marcom.Models.clsGlobel.SrcProductDomain + item.Image_Path }) %>"
                                                                width='133' height='99.75' alt="Marcom Trade" />
                                                        </a>
                                                    </div>
                                                    <p>
                                                        <a href="" alt="G142C" title="G142C">
                                                            <%=item.Component_Highlight_Ar%>
                                                        </a>
                                                    </p>
                                                </div>
                                                <%} %>
                                                <%foreach (var item in Model.AccessoriesProducr)
                                                  { %>
                                                <div class="section">
                                                    <div>
                                                        <a class="newproduct" href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =item.Products1.Product_id ,name=item.Products1.Product_Title_Ar})%>"
                                                            alt="G142C" title="G142C">
                                                            <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath =Marcom.Models.clsGlobel.SrcProductDomain + item.Products1.Image_Path, width = 133, height = 100 }) %>"
                                                                width='133' height='99.75' alt="marcom trade" />
                                                        </a>
                                                    </div>
                                                    <p>
                                                        <a class="newproduct" href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =item.Products1.Product_id ,name=item.Products1.Product_Title_Ar})%>"
                                                            alt="G142C" title="G142C">
                                                            <%=item.Products1.Product_Short_Ar%>
                                                        </a>
                                                    </p>
                                                </div>
                                                <%} %>
                                            </div>
                                        </div>
                                        <div id="slider-right">
                                            &gt;
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                                <div id="tabs-2">
                                    <p>
                                        <%=Model.Product_Spec_Ar%>
                                    </p>
                                </div>
                                <div id="tabs-3">
                                    <p>
                                        <%=Model.Product_Ftr_Ar%>
                                    </p>
                                </div>
                                <div id="tabs-4">
                                    <%if (Model.RelatedProduct.Count > 0)
                                      { %>
                                    <div id="product-slide" style="width: 850px">
                                        <div id="slider-left">
                                            &lt;
                                        </div>
                                        <div id="slider-container">
                                            <div id="slider" class="slider">
                                                <%foreach (var RtItm in Model.RelatedProduct)
                                                  { %>
                                                <div class="section">
                                                    <div>
                                                        <a class="newproduct" href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =RtItm.Products1.Product_id ,name=RtItm.Products1.Product_Title_Ar})%>"
                                                            alt="G142C" title="G142C">
                                                            <img src="<%=Url.Action("GetPhotoThumbnail","WebSites", new { ImgPath = Marcom.Models.clsGlobel.SrcProductDomain+RtItm.Products1.Image_Path, width = 133, height = 100 }) %>"
                                                                width='133' height='99.75' alt="marcom trade" />
                                                        </a>
                                                    </div>
                                                    <p>
                                                        <a class="newproduct" href="<%=Url.Action("ProductDetail", "Ar_Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =RtItm.Products1.Product_id ,name=RtItm.Products1.Product_Title_Ar})%>"
                                                            alt="G142C" title="G142C">
                                                            <%=RtItm.Products1.Product_Title_Ar%>
                                                        </a>
                                                    </p>
                                                </div>
                                                <%} %>
                                            </div>
                                        </div>
                                        <div id="slider-right">
                                            &gt;
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td class="ProductDetailsTable_td4" colspan="2">
                            <div>
                                <div class="fb-comments" data-href="http://marcomtrade.com<%=Url.Action("ProductDetail", "Products", new { DeptId = (int)ViewData["DeptId"], BrandId = (int)ViewData["BrandId"], CatgId = (int)ViewData["CatgId"] ,id =Model.Product_id ,name=Model.Product_Title_Eng})%>" data-width="500" data-numposts="5"
                                    data-colorscheme="light">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <% Page.Title = Model.Categories.Category_Name_Ar + "-" + Model.Product_Title_Ar; %>
    <meta name="description" content="<%= Model.Meta_KeyWords_Ar %>" />
    <meta name="keywords" content="<%= Model.Meta_KeyWords_Ar%>" />
    <meta name="Title" content="<%= Model.Meta_Title_Ar %>" />

    <!--[if IE 7]>
 <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/ieAr_ProductDetalis_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if IE 8]>
 <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/ieAr_ProductDetalis_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if IE 9]>
 <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/ieAr_ProductDetalis_Style.css")%>"
        rel="stylesheet" type="text/css" />
<![endif]-->
    <!--[if !IE]> -->
    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/Ar_ProductDetalis_Style.css")%>"
        rel="stylesheet" type="text/css" />
    <!-- <![endif]-->
    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/Products/Ar_products.css")%>"
        rel="stylesheet" type="text/css" />
    <script async src='http://ajax.googleapis.com/ajax/libs/mootools/1.11/mootools-yui-compressed.js'
        type='text/javascript'></script>
    <script async src="<%=Page.ResolveClientUrl("~/Resources/JQuery/products.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var slideactive = 6;
    </script>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#tabs").tabs({

            });

            if (jQuery.trim(jQuery("#tabs-1").html()) == "") {
                jQuery("#tabs-1").css("display", "none");
                jQuery(".clstabs-1").css("display", "none");
            }
            if (jQuery.trim(jQuery("#tabs-2").html()) == "") {
                jQuery("#tabs-2").css("display", "none");
                jQuery(".clstabs-2").css("display", "none");
            }
            if (jQuery.trim(jQuery("#tabs-3").html()) == "") {
                jQuery("#tabs-3").css("display", "none");
                jQuery(".clstabs-3").css("display", "none");
            }
            if (jQuery.trim(jQuery("#tabs-4").html()) == "") {
                jQuery("#tabs-4").css("display", "none");
                jQuery(".clstabs-4").css("display", "none");
            }

        });
    </script>
</asp:Content>
