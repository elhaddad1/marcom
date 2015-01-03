<%@ Page Title="Marcom Trade"  Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="divSiteMap"  style="display:inline-block; text-align:right;width: 100%; height: auto; min-height: 400px;  margin-top:10px; margin-right:10px; background-color:#ffffff;">
        <ul id="browser" class="filetree">
            <li>
                    <span class="file"><a href="<%=Url.Action("Home","Ar_Home") %>"> الرئيسية</a></span>
               
            </li>
            <li class="closed clsDepartmentSM ">
                                                
            </li>
            <li class="closed clsCategoriesSM">
                                               
            </li>
             <li class="closed clsBrandSM">
            </li>
            <li><span class="file"><a href="<%=Url.Action("News","Ar_News") %>"> الاخبار </a></span></li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <link href="../../Resources/JQuery/jquery-treeview-master/jquery.treeview.rtl.css" rel="stylesheet"
        type="text/css" />
     <script async src="../../Resources/JQuery/jquery-treeview-master/demo/jquery.cookie.js"
        type="text/javascript"></script>
     <script async src="../../Resources/JQuery/jquery-treeview-master/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        jQuery(document).ready(function () {
            $('.divSiteMap').hide();
            $.ajax({
                type: "POST",
                url: "../../ArMarcomService.asmx/ArGetSitMapBrand",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                async:false,
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">الماركات</span>";
                    $('.clsBrandSM').append(f).append(data);
                },
                failure: function (errMsg) {
                    $('.message').text(errMsg);
                }
            });

            $.ajax({
                type: "POST",
                url: "../../ArMarcomService.asmx/ArGetSitMapCategory",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                async:false,
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">تصنيف بالسفينة</span>";
                    $('.clsCategoriesSM').append(f).append(data);

                },
                failure: function (errMsg) {
                    $('.message').text(errMsg);
                }
            });

            $.ajax({
                type: "POST",
                url: "../../ArMarcomService.asmx/ArGetSitMapDepartment",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">المنتجات</span>";
                    $('.clsDepartmentSM').append(f).append(data);
                    jQuery("#browser").treeview({
                        animated: "fast",
                        collapsed: true,
                        unique: true
                    });
                    $('.divSiteMap').show('slow');
                },
                failure: function (errMsg) {
                    $('.message').text(errMsg);
                }
            });
        });
    </script>
</asp:Content>
