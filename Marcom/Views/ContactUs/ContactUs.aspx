<%@ Page Title="Marcom Trade"  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.CompanyContactUs>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ContactUs_div">
        <table class="ContactUsTable" cellspacing="0">
            <tr>
                <td class="ContactUsTable_td1">
                    <div class="ContactUsTable_td1_div">
                        <%if (Model != null)
                          { %>
                        <%=Model.ContactUs_Eng%>
                        <%} %>
                    </div>
                </td>
                <td class="GoogleMap_td" rowspan="2">
                    <iframe width="450" height="481" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" src="https://maps.google.com/maps?f=q&amp;source=embed&amp;hl=ar&amp;geocode=&amp;q=Marcom+Trade+for+Marine+%26+Communication+Systems,+Ezbet+Saad,+Qism+Sidi+Gabir,+Alexandria,+Egypt&amp;aq=&amp;sll=30.334954,31.552734&amp;sspn=10.741356,21.643066&amp;ie=UTF8&amp;hq=Marcom+Trade+for+Marine+%26+Communication+Systems,&amp;hnear=%D8%B9%D8%B2%D8%A8%D8%A9+%D8%B3%D8%B9%D8%AF%D8%8C+%D9%82%D8%B3%D9%85+%D8%B3%D9%8A%D8%AF%D9%89+%D8%AC%D8%A7%D8%A8%D8%B1%D8%8C+%D8%A7%D9%84%D8%A5%D8%B3%D9%83%D9%86%D8%AF%D8%B1%D9%8A%D8%A9%D8%8C+%D9%85%D8%B5%D8%B1&amp;t=m&amp;cid=12279899479564245553&amp;ll=31.222858,29.945555&amp;spn=0.033029,0.041113&amp;z=14&amp;iwloc=A&amp;output=embed">
                    </iframe>
                    <br />
                    <small><a href="https://maps.google.com/maps?f=q&amp;source=embed&amp;hl=ar&amp;geocode=&amp;q=Marcom+Trade+for+Marine+%26+Communication+Systems,+Ezbet+Saad,+Qism+Sidi+Gabir,+Alexandria,+Egypt&amp;aq=&amp;sll=30.334954,31.552734&amp;sspn=10.741356,21.643066&amp;ie=UTF8&amp;hq=Marcom+Trade+for+Marine+%26+Communication+Systems,&amp;hnear=%D8%B9%D8%B2%D8%A8%D8%A9+%D8%B3%D8%B9%D8%AF%D8%8C+%D9%82%D8%B3%D9%85+%D8%B3%D9%8A%D8%AF%D9%89+%D8%AC%D8%A7%D8%A8%D8%B1%D8%8C+%D8%A7%D9%84%D8%A5%D8%B3%D9%83%D9%86%D8%AF%D8%B1%D9%8A%D8%A9%D8%8C+%D9%85%D8%B5%D8%B1&amp;t=m&amp;cid=12279899479564245553&amp;ll=31.222858,29.945555&amp;spn=0.033029,0.041113&amp;z=14&amp;iwloc=A"
                        style="color: #0000FF; text-align: left">Click to enlarge Google map.</a></small>
                </td>
            </tr>
            <tr>
                <td class="ContactUsTable_td2">
                    <%  using (Html.BeginForm("ContactUs", "ContactUs", FormMethod.Post, new { @class = "frmContuctUs" }))
                        {%>
                    <table class="ContactInfo_table" width="100%" height="100%" border="0" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td class="ContactInfo_table_tdlable">
                                <label>
                                    Name:</label>
                            </td>
                            <td class="ContactInfo_table_tdtext">
                                <input type="text" class="text" name="name" id="cntname" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ContactInfo_table_tdlable">
                                <label>
                                    Company:</label>
                            </td>
                            <td class="ContactInfo_table_tdtext">
                                <input type="text" class="text" name="company" id="cntcompany" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ContactInfo_table_tdlable">
                                <label>
                                    Your e-mail:</label>
                            </td>
                            <td class="ContactInfo_table_tdtext">
                                <input type="text" class="text" name="email" id="cntemail" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ContactInfo_table_tdlable">
                                <label>
                                    Contact number:</label>
                            </td>
                            <td class="ContactInfo_table_tdtext">
                                <input type="text" class="text" name="phone" id="cntphone" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ContactInfo_table_tdlable">
                                <label>
                                    Message:</label>
                            </td>
                            <td class="ContactInfo_table_tdtext">
                                <textarea class="textarea" name="message" id="cntmessage"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="ContactInfo_table_tdbutton">
                                <input type="submit" class="btn" value="Submit" />
                            </td>
                        </tr>
                    </table>
                    <%} %>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content=" Marcom Trade Your Partner & Technical Advisor For safe Navigation at Sea Egypt, Africa, and Middle East">
    <meta name="keywords" content="amp,marine,simrad,koden,radar" />
    <link href="<%=Page.ResolveClientUrl("~/Resources/Styles/ContactUs/ContactUs_Style.css")%>"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        jQuery(function () // execute once the DOM has loaded
        {
            jQuery.ajax({
                type: "POST",
                url: "/clsWebService/GetUserData",
                dataType: "json",
                success: function (data) {
                    if (data.User_id > 0) {
                        jQuery('#cntname').val(data.User_name != null ? data.User_name : "");
                        jQuery('#cntcompany').val(data.User_Company != null ? data.User_Company : "");
                        jQuery('#cntemail').val(data.User_Email != null ? data.User_Email : "");
                        jQuery('#cntphone').val(data.User_Mobile != null ? data.User_Mobile : "");
                    }
                }
            });
            jQuery(".frmContuctUs").submit(function () {
                var bolFrm = true;
                if (!validateEmail(jQuery('#cntemail').val())) {
                    jQuery("#messagewrapper").html('<div class="messagebox error"></div>');
                    jQuery("#messagewrapper .messagebox").text("Please Insert Correct Email.");
                    displayMessages();
                    return false;
                }
                else if (
                    jQuery('#cntname').val() != null && jQuery('#cntname').val() != "" &&
                    jQuery('#cntcompany').val() != null && jQuery('#cntcompany').val() != "" &&
                    jQuery('#cntemail').val() != null && jQuery('#cntemail').val() != "" &&
                    jQuery('#cntphone').val() != null && jQuery('#cntphone').val() != "" &&
                    jQuery('#cntmessage').val() != null && jQuery('#cntmessage').val() != ""
                    ) {
                    return true;
                }
                else {
                    jQuery("#messagewrapper").html('<div class="messagebox error"></div>');
                    jQuery("#messagewrapper .messagebox").text("Please Insert Correct Data.");
                    displayMessages();
                    return false;
                }
            });
        });
        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)jQuery/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+jQuery/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
