<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.Services>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NewsDetails_div">
        <table class="NewsMsterTable" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="SiteMap">
                    <a href="<%=Url.Action("Home", "Home")%>" class="SiteMap_link">
                        <h1>Home ></h1>
                    </a><a href="<%=Url.Action("Service", "Services")%>" class="SiteMap_link">
                        <h1>Service ></h1>
                    </a><a href="#" class="SiteMap_link">
                        <h1>
                            <%=Model.Service_Title_Eng %>
                        </h1>
                    </a>
                </td>
            </tr>
            <tr>
                <td class="Title_td">
                    <div class="Title">
                        <%=Model.Service_Title_Eng%>
                    </div>
                    <div class="Date">
                        <%=Model.Service_Datetime.Value.ToShortDateString()%>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="NewsMsterTable_td1">
                    <img class="News_img" src="<%=Marcom.Models.clsGlobel.SrcServiceDomain+Model.Image_Path %>" alt="<%=Model.Service_Title_Eng%>" />
                </td>
            </tr>
            <tr>
                <td class="NewsMsterTable_td2" align="center">
                    <p class="NewsDscription">
                        <%=Model.Service_Desc_Eng %>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" <%=Model.Service_Title_Eng %>">
    <meta name="keywords" content=" <%=Model.Service_Title_Eng %> ,amp,marine,simrad,koden,radar">
    <meta name="Title" content=" <%=Model.Service_Title_Eng %>">
    <link href="../../Resources/Styles/News/NewsDeyail_Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
