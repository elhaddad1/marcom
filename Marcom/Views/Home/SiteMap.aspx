<%@ Page Title="Marcom Trade"  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divSiteMap" style="display: inline-block; width: 100%; height: auto; min-height: 400px; background-color: White;">
        <ul id="browser" class="filetree">
            <li><span class="file"><a href="<%=Url.Action("Home","Home") %>">Home</a></span>
            </li>
            <li class="closed clsDepartmentSM "></li>
            <li class="closed clsCategoriesSM"></li>
            <li class="closed clsBrandSM"></li>
            <li><span class="file"><a href="<%=Url.Action("News","News") %>">News </a></span>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" Marcom Trade Your Partner & Technical Advisor For safe Navigation at Sea Egypt, Africa, and Middle East">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />
    <link href="../../Resources/JQuery/jquery-treeview-master/jquery.treeview.css" rel="stylesheet"
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
                url: "../../MarcomService.asmx/GetSitMapBrand",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                async: false,
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">Brands</span>";
                    $('.clsBrandSM').append(f).append(data);
                },
                failure: function (errMsg) {
                    $('.message').text(errMsg);
                }
            });

            $.ajax({
                type: "POST",
                url: "../../MarcomService.asmx/GetSitMapCategory",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                async: false,
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">Categories</span>";
                    $('.clsCategoriesSM').append(f).append(data);

                },
                failure: function (errMsg) {
                    $('.message').text(errMsg);
                }
            });

            $.ajax({
                type: "POST",
                url: "../../MarcomService.asmx/GetSitMapDepartment",
                contentType: "application/json; charset=utf-8",
                data: '{"num":0}',
                dataType: "json",
                success: function (response) {
                    var data = $.parseJSON(response.d);
                    var f = "<span class=\"folder\">Product Range</span>";
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
