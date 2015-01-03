jQuery.noConflict();
jQuery(document).ready(function () {
    /////Declarations
    jQuery('#Cart').hide();
    jQuery('#Cartcomment').hide();
    jQuery("#Items").click(function () {
        //        jQuery('#Cart').removeClass('Cart_div').addClass('Cart_div1');
        jQuery('#Cart').slideToggle(1500);
    });

    jQuery("#Close_Cart").click(function () {
        jQuery('#Cart').slideToggle(1500);
    });


    jQuery(".cart").click(function () {

        var itemid = this.getAttribute("ItemID");
        var itemname = this.getAttribute("ItemName");
        //        var id = jQuery(document.getElementById("Text_" + itemid + ""));
        //        if (id.length == 0) {
        AddNewItem(itemid, itemname);
        //        }
        //        else { }

    });

    jQuery('#CheckOut').bind('click', function () {
        var count = jQuery("#Itemcount").text();
        count = parseInt(count);
        if (count == 0) {
            jQuery("#messagewrapper").html('<div class="messagebox success"></div>');
            jQuery("#messagewrapper .messagebox").text('أضف منتجات للقائمةأولا');
            displayMessages();
        }
        else {
            jQuery('#Cart').hide();
            jQuery('#Cartcomment').show();
            jQuery("#CommentText").val('');
        }
    });

    jQuery('#BacktoCart').bind('click', function () {

        jQuery('#Cart').show();
        jQuery('#Cartcomment').hide();
    });
    jQuery('#ConfirmCart').bind('click', function () {
        CheckOutUserItems();
        jQuery('#Cartcomment').hide();
        jQuery('#Cart').show();

    });
    //// Get User Items from database
    GetUserItems();

});

function GetUserItems() {

    jQuery.ajax({
        cache: false,
        type: "GET",
        async: false,
        url: '/clsWebService/AddtocardList',
        dataType: "text",
        success: function (data) {
            LoadUserItems(data);
            //GetItemsCount();
            CalculateAmount();
            jQuery('.Remove').bind('click', function () {
                var itemid = this.getAttribute("ItemID");
                jQuery.getJSON('/clsWebService/AddtocardDelete?ProductId=' + itemid, function (data) {
                    GetUserItems();
                    //GetItemsCount();
                })
.fail(function () { console.log("error"); });
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}
function GetItemsCount() {
    jQuery.getJSON('/clsWebService/AddtocardAmount', function (data) {
        jQuery("#Itemcount").text("" + data + "")
        jQuery("#itemcount1").text("" + data + "");
    })
.fail(function () { console.log("error"); });
}
function CalculateAmountEvent(cntrl) {
    var RwId = jQuery(cntrl).parent().parent().parent().parent().parent().parent().attr("id");
    var RwAmount = jQuery(cntrl).val();
    jQuery.getJSON('/clsWebService/ChangeProductAmount?CrdId=' + RwId + '&amount=' + RwAmount + '', function (data) {
        if (data != "error") {
            var total = 0;
            if (jQuery('.Quantity') != null) {
                jQuery('.Quantity').each(function () {
                    total = parseFloat(total) + parseFloat(jQuery(this).val());
                });
            }
            jQuery("#Itemcount").text("" + total + "")
            jQuery("#itemcount1").text("" + total + "");

            //GetItemsCount();
        }
    })
.fail(function () { console.log("error"); });

}
function CalculateAmount() {
    var total = 0;
    if (jQuery('.Quantity') != null) {
        jQuery('.Quantity').each(function () {
            total = parseFloat(total) + parseFloat(jQuery(this).val());
        });
    }
    jQuery("#Itemcount").text("" + total + "")
    jQuery("#itemcount1").text("" + total + "");

}
function LoadUserItems(data) {
    jQuery("#wrapertd").html(data);
}

function AddNewItem(ItemID, ItemName) {
    jQuery.getJSON('/clsWebService/AddtocardAddProduct?ProductId=' + ItemID + '', function (data) {
        if (data != "error") {
            GetUserItems();
            //GetItemsCount();
        }
    })
.fail(function () { console.log("error"); });
}

function DrowItem(ID, Name) {
    var id = jQuery(document.getElementById("" + ID + ""));
    if (id.length == 0) {
        var cartitem = "<div id='" + ID + "' class='item_wraper'>" +
                           " <table class='one_item_table' border='0' cellpadding='0' cellspacing='0'>" +
                                "<tr>" +
                                    "<td>" +
                                        "<div class='ItemName_div'> " + Name +
                                         "</div>" +
                                    "</td>" +
                                    "<td>" +
                                       "<div class='Item_count_div'>" +
                                       "<input id='Text_" + ID + "' value='1' class='Quantity' />" +
                                        "</div>" +
                                    "</td>" +
                                    "<td>" +
                                    "<a class='Remove' id='Remove" + ID + "' ItemID='" + ID + "'></a>" +
                                    "</td>" +
                                     "</tr>" +
                            "</table></div>"
        jQuery("#wrapertd").prepend(cartitem);
    }
    else {

        var q = jQuery(document.getElementById("Text_" + ID + "")).val();
        q++;
        jQuery(document.getElementById("Text_" + ID + "")).val("" + q + "");
    }



    jQuery('.Remove').unbind('click');

    jQuery('.Remove').bind('click', function () {
        var itemid = this.getAttribute("ItemID");
        jQuery.getJSON('/clsWebService/AddtocardDelete?ProductId=' + itemid, function (data) {
            GetUserItems();
            //GetItemsCount();
            //            var ItemQ = jQuery("#Text_" + itemid + "").val();
            //            jQuery("#" + itemid + "").remove();
        })
.fail(function () { console.log("error"); });
    });
}

function CheckOutUserItems() {


    var arrayId = [];
    var arrayQuamtity = [];
    jQuery("#wrapertd").children("div").each(function (index) {
        var ItemDivID = jQuery(this).attr("id");
        var ItemQuantity = jQuery("#Text_" + ItemDivID + "");
        arrayId.push(ItemDivID);
        arrayQuamtity.push(ItemQuantity.val());
    });
    var CommentText = jQuery("#CommentText").val()

    if (arrayId.length > 0) {
        jQuery.ajax({
            url: '/clsWebService/AddtocardArCheckout',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ arrayId: arrayId, arrayQuamtity: arrayQuamtity, CommentText: CommentText }),
            success: function (result) {
                jQuery("#wrapertd").html('');
                jQuery("#messagewrapper").html('<div class="messagebox success"></div>');
                jQuery("#messagewrapper .messagebox").text("طلب الشراء تم بنجاح ، برجاء مراجعة بريدك الالكترونى");
                displayMessages();
                CalculateAmount();
            }
        });
    }
    else {
        jQuery("#messagewrapper").html('<div class="messagebox warning"></div>');
        jQuery("#messagewrapper .messagebox").text("برجاء أضافة منتجات للقائمة أولا");
        displayMessages();
        CalculateAmount();
    }
}