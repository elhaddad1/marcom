<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.CompanyAboutUs>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" Marcom Trade Your Partner & Technical Advisor For safe Navigation at Sea Egypt, Africa, and Middle East">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />
    <link href="../../Resources/Styles/AboutUs/AboutUs_Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="About_div">
        <table class="AboutUsTable" cellspacing="0">
            <tr>
                <td class="AboutUsTable_td2" align="center">
                    <%if (Model != null)
                      { %>
                    <img src="<%=Marcom.Models.clsGlobel.SrcAboutUsDomain+Model.Image_Path %>" style="max-width: 600px;" alt="Marcom Trade" />
                    <%} %>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="AboutUsTable_td3">
                    <%if (Model != null)
                      { %>
                    <%=Model.AboutUs_txt_Eng %>
                    <%} %>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

