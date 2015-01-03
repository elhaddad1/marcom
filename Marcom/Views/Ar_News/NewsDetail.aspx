<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.News>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NewsDetails_div">
        <table class="NewsMsterTable" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="SiteMap">
                    <a href="<%=Url.Action("Home", "Ar_Home")%>" class="SiteMap_link">
                        <h1>الرئيسية ></h1>
                    </a><a href="<%=Url.Action("News", "Ar_News")%>" class="SiteMap_link">
                        <h1>الاخبار ></h1>
                    </a><a href="#" class="SiteMap_link">
                        <h1>
                            <%=Model.News_Title_Ar %>
                        </h1>
                    </a>
                </td>
            </tr>
            <tr>
                <td class="Title_td">
                    <div class="Title">
                        <%=Model.News_Title_Ar%>
                    </div>
                    <div class="Date">
                        <%=Model.News_Datetime.Value.ToShortDateString()%>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="NewsMsterTable_td1">
                    <img class="News_img" src="<%=Marcom.Models.clsGlobel.SrcNewsDomain+Model.Image_Path %>" alt="<%=Model.News_Title_Ar%>" />
                </td>
            </tr>
            <tr>
                <td class="NewsMsterTable_td2" align="center">
                    <p class="NewsDscription">
                        <%=Model.News_Desc_Ar %>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="<%=Model.News_Title_Ar%>">
    <meta name="keywords" content="<%=Model.News_Title_Ar%>">
    <meta name="Title" content="<%=Model.News_Title_Ar%>">

    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/News/Ar_NewsDeyail_Style.css")%>"
        rel="stylesheet" type="text/css" />
</asp:Content>
