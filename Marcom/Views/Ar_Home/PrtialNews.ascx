﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.News>>" %>

    <% foreach (var item in Model) { %>
    
    <div title="<%=item.News_Datetime.Value.ToShortDateString() %>" link="<%=Url.Action("NewsDetail", "Ar_News", new {id =item.News_id })%>">
    <img src="<%=Marcom.Models.clsGlobel.SrcNewsDomain+item.Image_Path %>" />
    <span title="<%=item.News_Title_Ar %>">
    <%=item.News_Desc_Ar%>
    		</span>
    		</div>
    <% } %>



