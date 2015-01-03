<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.Carts>>" %>



<% foreach (var item in Model) { %>

<div id="<%=item.Cart_id %>" class='item_wraper'>
 <table class='one_item_table' border='0' cellpadding='0' cellspacing='0'>
<tr>
<td>
<div class='ItemName_div'> <%=item.Products.Product_Title_Eng %>
</div>
</td>
<td>
<div class='Item_count_div'>
<%string divId = "Text_" + item.Cart_id;%>
<input id="<%=divId%>" value='<%=item.Amount %>' name="InPutQuantity" class='Quantity' onchange="CalculateAmountEvent(this);"/>
</div>
</td>
<td>

<a class='Remove' id="Remove" + <%=item.Cart_id %> + "" ItemID="<%=item.Cart_id %>"></a>
</td>
</tr>
</table></div>
<% } %>

