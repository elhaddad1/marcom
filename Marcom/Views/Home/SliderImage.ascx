<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.ImagesGallery>>" %>

<% foreach (var item in Model) { 
       %>
   <li><img src="<%= Marcom.Models.clsGlobel.SrcimgDomain+item.Image_Path %>" /></li>
<% } %>

