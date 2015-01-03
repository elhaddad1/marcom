jQuery.noConflict();
var PublicBarnd;
var PublicDept;
jQuery(document).ready(function () {
    var overlay = jQuery('<div id="overlay"> </div>');
    overlay.appendTo(document.body);
    jQuery(".Menu-SearchTable").hide();
    jQuery(".Logo_img").attr('src', '');
    jQuery('.bg').css('background-image', 'url()');

    jQuery('body').append('<div id="Cart" class="Cart_div"><table class="Cart_Wraper" border="0" cellpadding="0" cellspacing="0">' +
            '<tr><td class="Cart_Wraper_td1"><div class="cart_header"> <span id="Itemcount" class="Itemcount">&nbsp 0</span> &nbsp مشتريات.</div>' +
                '</td>  </tr> <tr> <td class="Cart_Wraper_td2"><div id="wrapertd" class="items_container"> </div></td></tr>' +
              ' <tr> <td class="Cart_Wraper_td3"> <div class="cart_Close"> <a id="Close_Cart">غلق</a></div> <div class="cart_Checkout"> <a id="CheckOut">طلب شراء</a></div>' +
                '</td></tr> </table></div>');
    jQuery('body').append('<div id="Cartcomment" class="Comment_div"> <table class="Comment_Wraper" border="0" cellpadding="0" cellspacing="0">' +
           ' <tr> <td class="Comment_Wraper_td1"> <div id="CommentContainer" class="CommentContainer_div"> ' +
            ' <textarea  id="CommentText"class="CommentTextarea" name="comment" placeholder="        أترك رسالتك هنا...."></textarea></div>' +
               ' </td></tr> <tr> <td class="Comment_Wraper_td2"><div class="Comment_Close"> <a id="BacktoCart">رجوع</a> </div>' +
                    '<div class="Confirm"><a id="ConfirmCart">تاكيد الطلب</a></div></td> </tr></table> </div>');
    jQuery('#Cart').hide();
    jQuery('#Cartcomment').hide();
    jQuery.get("/Ar_menu/Brand", function (result) {
        // TODO: process the results of the server side call
        jQuery('.MUBrand').append("<a href=\"javascript:void(0);\" class=\"navlink\">الماركات</a>");
        jQuery('.MUjGlide').append("<a href=\"javascript:void(0);\" class=\"navlink\">تصنيف بالسفينة</a>");
        jQuery('.MUDepts').append("<a href=\"javascript:void(0);\" class=\"navlink\">المنتجات</a>");
        PublicBarnd = result;
        jQuery('#Brands-jGlide').hide();

        jQuery.get("/Ar_menu/Product", function (result) {
            // TODO: process the results of the server side call
            PublicDept = result;

            jQuery('#Depts-jGlide').hide();
            jQuery.get("/Ar_menu/Category", function (result) {
                // TODO: process the results of the server side call
                jQuery('#jGlide_050').hide();
                jQuery('.MUjGlide').append(result);
                jQuery('.MUDepts').append(PublicDept);
                jQuery('.MUBrand').append(PublicBarnd);

                jQuery('#navigation_horiz').naviDropDown({
                    dropDownWidth: '300px'
                });


                jQuery('#jGlide_050').jGlideMenu({ tileSource: '.jGlide_11050_tiles',
                    demoMode: false,
                    mouseHover: true,
                    itemsToDisplay: 14,
                    initialTile: 'tile_11050'
                }).show();
                jQuery('#Depts-jGlide').jGlideMenuDept({ tileSource: '.jGlide_111_tiles',
                    demoMode: false,
                    mouseHover: true,
                    itemsToDisplay: 14,
                    initialTile: 'tile_111'
                }).show();
                jQuery('#Brands-jGlide').jGlideMenuBrand({ tileSource: '.jGlide_1011_tiles',
                    demoMode: false,
                    mouseHover: true,
                    itemsToDisplay: 14,
                    initialTile: 'tile_1011'
                }).show();

                jQuery('#Brands-jGlide').show();
                jQuery('#Depts-jGlide').show();
                jQuery(".Menu-SearchTable").fadeIn();
                jQuery('#overlay').remove();
            });

        });
    });

    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetWebLogo",
        data: "{'id': '" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jQuery(".Logo_img").attr('src', data);
        }
    });

    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetWebBG",
        data: "{'id': '" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jQuery('.bg').css('background-image', 'url(' + data + ')');
        }
    });
    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetUserData",
        dataType: "json",
        success: function (data) {
            if (data.User_id > 0) {
                jQuery('#RegUser_id').val(data.User_id);
                jQuery('#RegEmail').val(data.User_Email != null ? data.User_Email : "");
                jQuery('#RegGender').val(data.Gender != null ? data.Gender : "");
                jQuery('#RegLoginName').val(data.User_name != null ? data.User_name : "");
                jQuery('#RegSecondName').val(data.User_LastName != null ? data.User_LastName : "");
                jQuery('#Regpassword1').val(data.User_Password != null ? data.User_Password : "");
                jQuery('#RegConfirmPassword').val(data.User_Password != null ? data.User_Password : "");
                jQuery('#RegUser_Company').val(data.User_Company != null ? data.User_Company : "");
                jQuery('#RegUser_Mobile').val(data.User_Mobile != null ? data.User_Mobile : "");
                jQuery('#RegUser_Address').val(data.User_Address != null ? data.User_Address : "");
                jQuery('#RegUser_InteristIn').val(data.User_InteristIn != null ? data.User_InteristIn : "");
                if (data.User_Subscripe == true) {
                    jQuery('#RegUser_Subscripe').attr('checked', 'checked');
                }
                else {
                    jQuery('#RegUser_Subscripe').removeAttr('checked');
                }
            }
        }
    });


    jQuery('a.login-window').click(function () {


        // Getting the variable's value from a link 
        var loginBox = jQuery(this).attr('href');

        //Fade in the Popup and add close button
        jQuery(loginBox).fadeIn(300);

        //Set the center alignment padding + border
        var popMargTop = (jQuery(loginBox).height() + 24) / 2;
        var popMargLeft = (jQuery(loginBox).width() + 24) / 2;

        jQuery(loginBox).css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });

        // Add the mask to body
        jQuery('body').append('<div id="mask"></div>');
        jQuery('#mask').fadeIn(300);

        return false;
    });

    // When clicking on the button close or the mask layer the popup closed
    jQuery('a.close, #mask').live('click', function () {
        jQuery('#mask , .login-popup').fadeOut(300, function () {

            //            jQuery(document.getElementById("Register-span")).text("Welcome mohamed");

            //            jQuery(document.getElementById("Login-span")).text("Logout");
            jQuery('#mask').remove();
        });
        return false;
    });



    jQuery('a.Register-window').click(function () {

        // Getting the variable's value from a link 
        var RegisterBox = jQuery(this).attr('href');

        //Fade in the Popup and add close button
        jQuery(RegisterBox).fadeIn(300);

        //Set the center alignment padding + border
        var popMargTop = (jQuery(RegisterBox).height() + 24) / 2;
        var popMargLeft = (jQuery(RegisterBox).width() + 24) / 2;

        jQuery(RegisterBox).css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });

        // Add the mask to body
        jQuery('body').append('<div id="mask"></div>');
        jQuery('#mask').fadeIn(300);

        return false;
    });

    // When clicking on the button close or the mask layer the popup closed
    jQuery('a.close, #mask').live('click', function () {
        jQuery('#mask , .Regiter-popup').fadeOut(300, function () {
            jQuery('#mask').remove();
        });
        return false;
    });


    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }
    jQuery("#searchField")
    // don't navigate away from the field on tab when selecting an item
			.bind("keydown", function (event) {
			    if (event.keyCode === jQuery.ui.keyCode.TAB &&
						jQuery(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        jQuery.getJSON("/Ar_SearchAutoCmplt/Searchresult", {
			            featureClass: "P",
			            style: "full",
			            maxRows: 12,
			            name_startsWith: request.term
			        }, response);
			    },
			    search: function () {
			        // custom minLength
			        var term = extractLast(this.value);
			        if (term.length < 1) {
			            return false;
			        }
			    },
			    focus: function () {
			        // prevent value inserted on focus
			        return false;
			    },
			    select: function (event, ui) {
			        location.href = '/Ar_SearchAutoCmplt/Index?Str=' + ui.item.value + '';
			    }
			});
});
