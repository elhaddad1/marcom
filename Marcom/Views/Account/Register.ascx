<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Marcom.Models.RegisterModel>" %>
<link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
<div id="Register-box" class="Regiter-popup">
    <a href="#" class="close">
        <img src="/Resources/Styles/Portal/images/close_pop.png" class="btn_close" title="Close Window"
            alt="Close" /></a>
    <%Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Register", "Account", FormMethod.Post, new { @class = "Register" }))
       {
    %>
    <%:Html.ValidationSummary(true) %>
    <input type="hidden" id="RegUser_id" name="RegUser_id" value="0" />
    <fieldset class="textbox">
        <table>
            <tr>
                <td>
                    <label class="Email">
                        <span>Email</span>
                        <%: Html.TextBoxFor(m => m.Email, new { @id = "RegEmail", @placeholder = "Email" })%>
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
                        <span>First Name</span>
                        <%: Html.TextBoxFor(m => m.UserName, new { @id = "RegLoginName", @autocomplete = "on", @placeholder = "First Name" })%>
                        <%:Html.ValidationMessageFor(model => model.UserName, "")%>
                    </label>
                </td>
                <td>
                    <label class="LoginName">
                        <span>Last Name</span>
                        <%: Html.TextBoxFor(m => m.LastName, new {@id = "RegSecondName", @autocomplete="on" ,@placeholder="Last Name"  })%>
                        <%:Html.ValidationMessageFor(model => model.UserName, "")%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <span>Company</span>
                        <%: Html.TextBoxFor(m => m.User_Company, new { @id = "RegUser_Company", @placeholder = "Company" })%>
                    </label>
                </td>
                <td>
                    <label class="ConfirmPassword">
                        <span>Mobile</span>
                        <%: Html.TextBoxFor(m => m.User_Mobile, new { @id = "RegUser_Mobile", @placeholder = "Mobile" })%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <span>Address</span>
                        <%: Html.TextBoxFor(m => m.User_Address, new { @id = "RegUser_Address", @placeholder = "Address" })%>
                    </label>
                </td>
                <td>
                    <label class="password">
                        <span>Password</span>
                        <%: Html.PasswordFor(m => m.Password, new {@id = "Regpassword1",@type="password" ,@placeholder="Password" })%>
                        <%:Html.ValidationMessageFor(model => model.Password, "")%>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <span>Confirm Password</span>
                        <%: Html.PasswordFor(m => m.ConfirmPassword, new {@id = "RegConfirmPassword",@type="password" ,@placeholder="Confirm Password" })%>
                        <%:Html.ValidationMessageFor(model => model.ConfirmPassword, "")%>
                    </label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="ConfirmPassword">
                        <%: Html.CheckBoxFor(m => m.User_Subscripe, new { @id = "RegUser_Subscripe" })%>
                        <span>
                            <%: Html.LabelFor(m => m.User_Subscripe)%></span>
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
                            <input type="checkbox" value="<%=item.Category_id %>" checked="checked" name="ctgcheckbox" style="width: 20px ! important; float: left;" />
                            <%}
                          else
                          { %>
                          <input type="checkbox" value="<%=item.Category_id %>"  name="ctgcheckbox" style="width: 20px ! important; float: left;" />
                            <%} %>
                            <span><%=item.Category_Name_Eng %></span>
                        </label>
                    <%} %>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="submit" value="Save" class="submitbutton" />
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
