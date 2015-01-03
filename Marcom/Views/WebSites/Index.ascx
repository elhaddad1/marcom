<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.WebSiteTable>>" %>



 <td class="CertficatesTable_td3">

<% foreach (var item in Model) { %>

                                        <a href="<%=item.WebSite_Src %>" style="text-decoration:none !important;" target="_blank">
                                            <img src="<%=Marcom.Models.clsGlobel.SrcWebSitesDomain + item.Image_Path%>" alt="<%=item.WebSite_Src %>"></img>
                                        </a>
                                   
<% } %>

 </td>