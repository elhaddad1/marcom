<%@ Page Title="Marcom Trade" Language="C#" MasterPageFile="~/Views/Shared/Ar_Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.CompanyAboutUs>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="About_div">
        <table class="AboutUsTable" cellspacing="0">
            <tr>
                <%--     <td class="AboutUsTable_td1">
               <%if (Model != null)
                 { %>
               <%=Model.HomeTxt_Ar%>
               <%} %>
            </td>--%>
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
                    <%=Model.AboutUs_txt_Ar %>
                    <%} %>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="مــــــــــاركــوم تـريــــــد شــــــريــكك ومستشــــــــــارك الفنــــــــى للمـــــلاحــة الامنـــــــــة فى مصــــر، افرقيــــا، والشــــرق الاوســــــط " />
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />

    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/AboutUs/Ar_AboutUs_Style.css")%>"
        rel="stylesheet" type="text/css" />

</asp:Content>
