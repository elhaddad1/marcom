<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Marcom.Models.CertificateTable>>" %>

 <td class="CertficatesTable_td1">
<% foreach (var item in Model) { %>

                                            <img src="<%=Marcom.Models.clsGlobel.SrcCertificateDomain + item.Image_Path%>" />
                                        
                              
<% } %>
      </td>