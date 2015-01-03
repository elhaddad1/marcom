<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">

.ui-autocomplete-loading
{
    background: white url('/Content/images/ui-anim_basic_16x16.gif') right center no-repeat;
}
#tags
{
    width: 25em;
}
.HeaderText_span
{
    color:White;
    font-size:large;
    font-weight:bold;
}
</style>

    <script type="text/javascript">
        jQuery(function () {
            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            jQuery("#tags")
            // don't navigate away from the field on tab when selecting an item
			.bind("keydown", function (event) {
			    if (event.keyCode === jQuery.ui.keyCode.TAB &&
						jQuery(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        jQuery.getJSON("/SearchAutoCmplt/Searchresult", {
			            featureClass: "P",
			            style: "full",
			            maxRows: 12,
			            name_startsWith: request.term
			        }, response);
			    },
			    search: function () {
			        // custom minLength
			        var term = extractLast(this.value);
			        if (term.length < 1) {
			            return false;
			        }
			    },
			    focus: function () {
			        // prevent value inserted on focus
			        return false;
			    },
			    select: function (event, ui) {
			        var aquireStudentsURL = '/SearchAutoCmplt/AllCategAndBrandItems?StrName=' + ui.item.value + '';
			        // var aquireStudentsURL = '/OnlineShpngHome/Brands';
			        jQuery.get(aquireStudentsURL, function (result) {
			            // TODO: process the results of the server side call

			            jQuery("#log").html(result);

			        });
			    }
			});
        });
    </script>
    <br />
    <br />
    <div class="demo">
     <div style="width: 500px; height: 25px; background-color: #005984;">
                                            <span class="HeaderText_span">
                                               &nbsp;&nbsp; Search :&nbsp;&nbsp;</span>
            <input id="tags" class="ui-autocomplete-input" value="<%=ViewData["Str"]==null?(string)ViewData["Str"]:ViewData["Str"] %>" />
        </div>
        <br />
    </div>
    <div id="log">
    </div>
    <%if (ViewData["Str"] != null)
      {  %>
    <script type="text/javascript">
        var aquireStudentsURL2 = '/SearchAutoCmplt/AllCategAndBrandItems?StrName=' + jQuery('#tags').val() + '';
        
        jQuery.get(aquireStudentsURL2, function (result) {
            // TODO: process the results of the server side call

            jQuery("#log").html(result);

        });
    </script>
    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
