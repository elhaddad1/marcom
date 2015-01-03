<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Marcom.Models.RegisterArModel>" %>
<link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
<div id="Register-box" class="Regiter-popup">
    <a href="#" class="close">
        <img src="/Resources/Styles/Portal/images/close_pop.png" class="btn_close" title="Close Window"
            alt="Close" /></a>
    <%Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Ar_Register", "Account", FormMethod.Post, new { @class = "Register" }))
       {
    %>
    <%:Html.ValidationSummary(true) %>
    <input type="hidden" id="RegUser_id" name="RegUser_id" value="0" />
    <fieldset class="textbox">
        <table>
            <tr>
                <td>
                    <label class="Email">
                        <span>البريد الألكترونى</span>
                        <%: Html.TextBoxFor(m => m.Email, new { @id = "RegEmail", @placeholder = "البريد الاألكترونى" })%>
                        <%:Html.ValidationMessageFor(model => model.Email,"")%>
                    </label>
                </td>
                <td>
                    <label class="textbox">
                        <%--<span><%: Html.LabelFor(m => m.Gender)%></span>--%>
                        <%=Html.DropDownListFor(model => model.Gender, (SelectList)ViewBag.genderlist, new { @id = "RegGender" })%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="LoginName">
                        <span>الأسم الأول</span>
                        <%: Html.TextBoxFor(m => m.UserName, new {@id = "RegLoginName", @autocomplete="on" ,@placeholder="الأسم الاول"  })%>
                        <%:Html.ValidationMessageFor(model => model.UserName, "")%>
                    </label>
                </td>
                <td>
                    <label class="LoginName">
                        <span>الأسم الأخير</span>
                        <%: Html.TextBoxFor(m => m.UserName, new { @id = "RegSecondName", @autocomplete = "on", @placeholder = "الأسم الاخير" })%>
                        <%:Html.ValidationMessageFor(model => model.UserName, "")%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <span>الشركة</span>
                        <%: Html.TextBoxFor(m => m.User_Company, new { @id = "RegUser_Company", @placeholder = "الشركة" })%>
                    </label>
                </td>
                <td>
                    <label class="ConfirmPassword">
                        <span>الهاتف</span>
                        <%: Html.TextBoxFor(m => m.User_Mobile, new { @id = "RegUser_Mobile", @placeholder = "الهاتف" })%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <span>العنوان</span>
                        <%: Html.TextBoxFor(m => m.User_Address, new { @id = "RegUser_Address", @placeholder = "العنوان" })%>
                    </label>
                </td>
                <td>
                    <label class="ConfirmPassword">
                        <span>تأكيدكلمة المرور</span>
                        <%: Html.PasswordFor(m => m.ConfirmPassword, new {@id = "RegConfirmPassword",@type="password" ,@placeholder="تأكيد رمز الدخول" })%>
                        <%:Html.ValidationMessageFor(model => model.ConfirmPassword, "")%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="password">
                        <span>كلمة المرور</span>
                        <%: Html.PasswordFor(m => m.Password, new {@id = "Regpassword1",@type="password" ,@placeholder="رمز الدخول" })%>
                        <%:Html.ValidationMessageFor(model => model.Password, "")%>
                    </label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <%-- <label class="ConfirmPassword">
                        <span>مهتم ب</span>
                        <%=Html.DropDownListFor(model => model.User_InteristIn, (SelectList)ViewBag.Categorylist, "-------- إختار تصنيف --------", new { @id = "RegUser_InteristIn" })%>
                    </label>--%>
                    <label class="ConfirmPassword">
                        <%: Html.CheckBoxFor(m => m.User_Subscripe, new { @id = "RegUser_Subscripe" })%>
                        <span>متابعة الموقع</span>
                    </label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" width="632px">
                    <div class="Groupbox" id="Groupbox">
                    <%
           List<int> LstInt = (List<int>)ViewBag.UserCategoryInt;
                        foreach (var item in (List<Marcom.Models.Categories>)ViewBag.Categorylist)
                      {%>
                        <label class="check">
                        <%if (LstInt.Contains(item.Category_id))
                          { %>
                            <input type="checkbox" value="<%=item.Category_id %>" checked="checked" name="ctgcheckbox" style="width: 20px ! important; float: right;" />
                            <%}
                          else
                          { %>
                          <input type="checkbox" value="<%=item.Category_id %>"  name="ctgcheckbox" style="width: 20px ! important; float: right;" />
                            <%} %>
                            <span><%=item.Category_Name_Ar %></span>
                        </label>
                    <%} %>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="submit" value="حفظ" class="submitbutton" />
                </td>
            </tr>
        </table>
    </fieldset>
    <%} %>
</div>
<script type="text/javascript">
    jQuery.noConflict();
    jQuery(document).ready(function () {
        //  jQuery('#Groupbox').hide();
        jQuery("#RegUser_Subscripe").change(function () {
            if (jQuery('#RegUser_Subscripe')[0].checked == true) {
                //  jQuery('#Groupbox').removeAttr("disabled");
                jQuery("#Groupbox").css({ "display": "block" });
            }
            else {
                //  jQuery('#Groupbox').attr("disabled", "disabled");
                jQuery("#Groupbox").css({ "display": "none" });
            }



            //       jQuery("#Terms_Link").click(function () {
            //           jQuery('#Terms_div').slideToggle(1500);
        });
    });
</script>
