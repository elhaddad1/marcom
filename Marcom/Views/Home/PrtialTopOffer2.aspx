<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Marcom.Models.UsersData>>" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>PrtialTopOffer2</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th>
            User_name
        </th>
        <th>
            User_Email
        </th>
        <th>
            User_Address
        </th>
        <th>
            User_Mobile
        </th>
        <th>
            User_Password
        </th>
        <th>
            User_IsActive
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_name) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_Email) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_Address) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_Mobile) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_Password) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.User_IsActive) %>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.User_id }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.User_id }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.User_id }) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
