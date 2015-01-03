<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.LinkTable>>" %>



 <td>

<% foreach (var item in Model) { %>
                                        <a href="<%=item.Link_Src %>" style="text-decoration:none !important;" target="_blank">
                                            <img src="<%=Marcom.Models.clsGlobel.SrcLiksDomain + item.Image_Path%>" alt=""></img>
                                        </a>
                                   
<% } %>

 </td>