jQuery.noConflict();
jQuery(document).ready(function () {

    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetWebMV",
        data: "{'id': '" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jQuery('.HomeTable_td1_div').css('background-image', 'url(' + data + ')');
        }
    });

    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetWebPrm",
        data: "{'id': '" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jQuery('.HomeTable_td2_div').css('background-image', 'url(' + data + ')');
        }
    });

    jQuery.ajax({
        type: "POST",
        url: "/clsWebService/GetShowPrm",
        data: "{'id': '" + 0 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == false) {
                jQuery('.HomeHeaders_div').css('background-image', 'url()');
            }
        }
    });

    jQuery('#PhotoGallary').rhinoslider({
        autoPlay: true,
        pauseOnHover: false,
        controlsPrevNext: false,
        controlsPlayPause: false,
        showTime: 5000,
        effectTime: 500

    });
    jQuery('#TopProducts').rhinoslider({
        effect: 'fade',
        easing: 'easeInQuad',
        controlsKeyboard: false,
        controlsPlayPause: false,
        autoPlay: true,
        showBullets: 'never',
        showTime: 5000,
        controlsPrevNext: false,
        effectTime: 3000
    });

    jQuery('#fancyNews').fancyNews({ maxWords: 70, slideTime: 6000, width: 305, height: 262 });

    //jQuery('#horiz_container_outer').horizontalScroll();

});

