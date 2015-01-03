<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Marcom.Models.LogOnArModel>" %>

<link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
<div id="login-box" class="login-popup">
        <a href="#" class="close">
            <img src="/Resources/Styles/Portal/images/close_pop.png"
                class="btn_close" title="Close Window" alt="Close" /></a>
                <%Html.EnableClientValidation(); %>
      <%  using (Html.BeginForm("Ar_LogOn", "Account", FormMethod.Post, new { @class="signin"}))
          {%>
        <fieldset class="textbox">
            <label class="username">
                <span>البريد الألكترونى</span>
                    <%: Html.TextBoxFor(m => m.UserName, new {@id="LoginName", @autocomplete="on" ,@placeholder="أسم الدخول"  })%>
                    <%:Html.ValidationMessageFor(model => model.UserName)%>
            </label>
            <label class="password">
                <span>كلمة المرور</span>
                    <%: Html.PasswordFor(m => m.Password, new {@id="password1",@type="password" ,@placeholder="رمز الدخول" })%>
                    <%:Html.ValidationMessageFor(model => model.Password)%>
            </label>
                 <input type="submit" value="دخول" class="submitbutton"/>
            <p>
                <a class="forgot" href="javascript:void(0);">نسيت رمز الدخول ؟</a>
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
                        jQuery("#messagewrapper .messagebox").text("الرجاء إدخال البريد الالكتروني الخاص الصحيح.");
                        displayMessages();
                        return false;
                    }
                    else {
                        jQuery.ajax({
                            url: '/clsWebService/ArForgetPassword',
                            type: 'POST',
                            //contentType: 'application/json; charset=utf-8',
                            data: 'email=' + $('#LoginName').val(),
                            success: function (result) {
                                if (result == "Please Check your mail") {
                                    jQuery("#wrapertd").html('');
                                    jQuery("#messagewrapper").html('<div class="messagebox success"></div>');
                                    jQuery("#messagewrapper .messagebox").text("يرجى التحقق من البريد الخاص بك");
                                    displayMessages();
                                }
                                else {
                                    jQuery("#messagewrapper").html('<div class="messagebox error"></div>');
                                    jQuery("#messagewrapper .messagebox").text("يرجى التحقق من البريد الخاص بك، البريد الإلكتروني غير صحيح.");
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