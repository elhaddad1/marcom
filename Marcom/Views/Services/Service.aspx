<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Marcom.Models.Service>>" %>

<%@ Import Namespace="Marcom.Helpers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="MasterNewsTable" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td class="SiteMap">
                <a href="<%=Url.Action("Home", "Home")%>" class="SiteMap_link">
                    <h1>Home ></h1>
                </a><a href="#" class="SiteMap_link">
                    <h1>Services </h1>
                </a>
            </td>
        </tr>
        <tr>
            <td class="News_td">
                <ul class="portfolio">
                    <% 
                        int x = 2;
                        foreach (var item in Model)
                        {%>
                    <li>
                        <%
                            if (x % 2 == 0)
                            { %>
                        <div class="NewsTableTable_td_div1">
                            <%
                            }
                            else
                            {%>
                            <div class="NewsTableTable_td_div2">
                                <%} x++; %>
                                <table class="OneNewsTable" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td class="OneNewsTable_td1">
                                            <img class="Img" src="<%=Marcom.Models.clsGlobel.SrcServiceDomain+item.Image_Path %>" alt=" <%=item.Service_Title_Eng %>" />
                                        </td>
                                        <td class="OneNewsTable_td2">
                                            <a class="Header_Link" href="<%=Url.Action("ServiceDetail", "Services", new {id =item.Service_id })%>">
                                                <span class="News_Header">
                                                    <%=item.Service_Title_Eng %></span></a>
                                            <br />
                                            <span class="date">
                                                <%=item.Service_Datetime.Value.ToShortDateString() %>
                                            </span><a class="link" href="<%=Url.Action("ServiceDetail", "Services", new {id =item.Service_id })%>">
                                                <p class="News_p">
                                                    <%=item.Service_Desc_Eng %>
                                                </p>
                                            </a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                    </li>
                    <% } %>
                </ul>
            </td>
        </tr>
        <tr>
            <td class="Pager_td">
                <%: Html.Pager(Request.QueryString["Page"], 4, Marcom.Models.clsGlobel.getTotalServiceRecords(), Url.Action("Services"))%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" Marcom Trade Your Partner & Technical Advisor For safe Navigation at Sea Egypt, Africa, and Middle East"/>
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />

    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/News/News_Style.css")%>"
        rel="stylesheet" type="text/css" />
</asp:Content>
