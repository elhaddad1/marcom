<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Marcom.Models.LogOnModel>" %>

<link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
<div id="login-box" class="login-popup">
        <a href="#" class="close">
            <img src="/Resources/Styles/Portal/images/close_pop.png"
                class="btn_close" title="Close Window" alt="Close" /></a>
                <%Html.EnableClientValidation(); %>
      <%  using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { @class="signin"}))
          {%>
        <fieldset class="textbox">
            <label class="username">
                <span>Email</span>
                    <%: Html.TextBoxFor(m => m.UserName, new {@id="LoginName", @autocomplete="on" ,@placeholder="User Name"  })%>
                    <%:Html.ValidationMessageFor(model => model.UserName, "")%>
            </label>
            <label class="password">
                <span>Password</span>
                    <%: Html.PasswordFor(m => m.Password, new {@id="password1",@type="password" ,@placeholder="Password" })%>
                    <%:Html.ValidationMessageFor(model => model.Password, "")%>
            </label>
                 <input type="submit" value="Log In" class="submitbutton"/>
            <p>
                <a class="forgot" href="javascript:void(0);">Forgot your password?</a>
            </p>
        </fieldset>
       <%} %>
    </div>
    
        <script type="text/javascript">
            $(function () // execute once the DOM has loaded
            {
                $(".forgot").click(function () {
                    var bolFrm = true;
                    if (!validateEmail($('#LoginName').val())) {
                        jQuery("#messagewrapper").html('<div class="messagebox error"></div>');
                        jQuery("#messagewrapper .messagebox").text("Please Insert Correct Email.");
                        displayMessages();
                        return false;
                    }
                    else {
                        jQuery.ajax({
                            url: '/clsWebService/ForgetPassword',
                            type: 'POST',
                            //contentType: 'application/json; charset=utf-8',
                            data: 'email='+$('#LoginName').val() ,
                            success: function (result) {
                                if (result == "Please Check your mail") {
                                    jQuery("#wrapertd").html('');
                                    jQuery("#messagewrapper").html('<div class="messagebox success"></div>');
                                    jQuery("#messagewrapper .messagebox").text("Please Check your mail for the password");
                                    displayMessages();
                                }
                                else {
                                    jQuery("#messagewrapper").html('<div class="messagebox error"></div>');
                                    jQuery("#messagewrapper .messagebox").text("Invalid Email");
                                    displayMessages();
                                }
                                $('#LoginName').val('');
                            }
                        });
                        return true;
                    }
                });
            });
            function validateEmail(sEmail) {
                var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                if (filter.test(sEmail)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            
        </script>