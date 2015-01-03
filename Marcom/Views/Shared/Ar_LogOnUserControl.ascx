<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Session["UserId"] != null)
    {
%>
        <a href="#Register-box" class="Register-window" style="text-decoration: none; color: #000000;">
                                            <span id="Register-span" class="R-L_Span"><%=Marcom.Models.clsGlobel.GetUserName((int)Session["UserId"])%></span></a>|
        <a href="<%= Url.Action("Ar_LogOff", "Account")%>" class="Register-window" style="text-decoration: none; color: #000000;">
                                            <span id="Register-span" class="R-L_Span">خروج</span></a> 
<%
    }
    else {
%> 
         <a href="#Register-box" class="Register-window" style="text-decoration: none; color: #000000;">
                                            <span id="Register-span" class="R-L_Span">تسجيل</span></a> | <a href="#login-box"
                                                class="login-window" style="text-decoration: none; color: #000000;"><span id="Login-span"
                                                    class="R-L_Span">دخول</span></a>
<%
    }
%>
