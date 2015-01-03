
/*
* Filename:	jQuery.jGlideMenuBrand.js (formally fastFind)
* Format:	Javascript Plugin : jQuery Framework
* Author:      jmcclure@sonicradish.com
* Copyright:   2006-2009 sonicradish.com
*
* License:     You may use this code on your site and alter it as you see fit, all I ask is that you include a reference to my original version and send me any bug fixes that you find.
* 
* Revision:	0.66
* Updated:	2008-03-24 - Version 0.64
* 		2009-03-03 - Version 0.65
*		2009-04-01 - Version 0.66 - Added fix for IE7 
*		2009-04-07 - Version 0.67 - Udated to Work with Latest jQuery Library (1.3.2) and with jQuery UI (1.7.1) Support
*			                  - Added variable "demoMode" allowing whether clicked links follow through or not
*					  - Fixed CSS Bug That Prevented Right Arrows from Displaying in Chrome/Safari
*
*	Please send feedback and questions to: jmcclure@sonicradish.com
*/

/*
* To Do Items:
* 
*	- Drop Shadow Support 
*	- Toggle Menu Support (with in/out effects)	
*		- Basic Support (Fade/Slide/Animate)
*		- Enchant Library Support
*	- Launch at Mouse Position Support
*	- Easing FX Support
*	- Support for Multiple Menus on Single Page
*
*/

/*
* Known Issues
*
* 	- When slideRight == false, tile contents aren't rendered until slide completes
*	- When multiple instances on single page, all events occur in last instance
*	- DropShadow/Shadow Plugins not currently working
* 	- Easing transitions not working for horizontal scrolling
*	- Toggle Method Needs Further Development
*	- Keyboard Support Needs Work (Space Key Triggers without mouse Over)
*
* To Do
*
*	- Better Keyboard Support
* 	- shadowBox (inline effect)
*	- MouseHold Instead of/or in addition to Hover
*
*/

/*
* Testing & Support	Version 0.6.7
*
* 	+ Firefox
*		+ 2.x (XP)		Okay
*		+ 2.x (OSX)		Okay
*		+ 3.x			Okay
*	
*	+ Internet Explorer
*		+ 6.x			Okay
*		+ 7.x			Okay
*
*	+ Opera
*		+ 9.26			Okay
*		+ 9.64			Okay
*	
*	+ Safari
*		+ 3.x (XP)		Okay
*		+ 3.x (OSX)		Okay
*		+ 4.x (XP)		Okay
*
*	+ Google Chrome
*		+ 1.0.x			Okay
*/

jQuery.jGlideMenuBrand = {

    // <-- Global Declarations

    // Set By Configuration
    useDropShadow: new Boolean(),
    useDragDrop: new Boolean(),
    defaultScrollSpeed: new Number(0),
    defaultScrollBackSpeed: new Number(0),
    slideRight: new Boolean(),
    useSmoothScrolling: new Boolean(),
    easeFx: new String(''),
    closeLinkMarkUp: new String(''),
    menuShowFx: new String(''),
    menuHideFx: new String(''),
    tileWidth: new Number(0),
    tileInset: new Number(0),
    itemsToDisplay: new Number(8),
    useTileURL: new Boolean(),
    tileSource: new String(''),
    URLParams: new Object(),
    loadImage: new String(''),
    loadImageStyle: new Object(),
    initialTile: new String(''),
    alertOnError: new Boolean(),
    captureLinks: new Boolean(),
    imagePath: new String(),

    // Set By jGlideMenuBrand Script
    tileCount: new Number(0),
    animation: new Boolean(),
    helperImage: new Boolean(),
    currentElement: new Object(),
    currentElementID: new String(''),
    hasDragDropSupport: new Boolean(),
    hasShadowSupport: new Boolean(),
    displayToggle: new Boolean(),
    tileScrollPosition: new Array(),
    smoothScrollTimer: new Array(),
    mouseHover: new Boolean(),
    demoMode: new Boolean(),

    // --> Global Declarations

    // Create Plugin Instance
    initialize: function (o) {
        // Return jQuery
        return this.each(function () {

            // Init Variables	
            jQuery.jGlideMenuBrand.animation = false;
            jQuery.jGlideMenuBrand.helperImage = false;
            jQuery.jGlideMenuBrand.hasDragDropSupport = false;
            jQuery.jGlideMenuBrand.hasShadowSupport = false;
            jQuery.jGlideMenuBrand.tileCount = 0;
            jQuery.jGlideMenuBrand.displayToggle = false;
            jQuery.jGlideMenuBrand.mouseHover = false;
            jQuery.jGlideMenuBrand.demoMode = false;

            // Store Current Element	
            jQuery.jGlideMenuBrand.currentElement = jQuery(this);
            if (this.id) jQuery.jGlideMenuBrand.currentElementID = this.id;

            // Default Values
            var s = {
                itemsToDisplay: 12,
                tileInset: -2,
                tileWidth: 300,
                useDropShadow: false,
                slideRight: true,
                useDragDrop: true,
                useSmoothScrolling: true,
                useTileURL: false,
                defaultScrollSpeed: 750,
                defaultScrollBackSpeed: 800,
                tileSource: 'myTiles',
                URLParams: {},
                closeLinkMarkUp: 'Close',
                menuShowFx: 'fadeIn',
                menuHideFx: 'fadeOut',
                easeFx: 'linear',
                loadImage: '../../Resources/JQuery/jGlideMenu/img/ajax.gif',
                initialTile: 'tile_011',
                alertOnError: false,
                captureLinks: true,
                loadImageStyle: { 'position': 'absolute', 'bottom': '10px', 'left': '10px', 'z-index': '999' },
                imagePath: '../../Resources/JQuery/jGlideMenu/img/',
                demoMode: false
            };

            // Merge Submitted Settings
            if (o) jQuery.extend(s, o);

            // Check Library Support for FX
            jQuery.jGlideMenuBrand.checkFeatures();

            // Ensure Values (Basic)
            if (s.closeLinkMarkUp.length < 1) s.closeLinkMarkUp = 'x Close';
            if (parseInt(s.itemsToDisplay) < 1) s.itemsToDisplay = 1;
            if (s.initialTile.length < 0) jQuery.jGlideMenuBrand.errorTrap('Invalid Configuration');

            // Set Global Values 
            for (i in s) jQuery.jGlideMenuBrand[i] = s[i];

            // <-- Remove Any Place Holder Content From Element and Hide Tiles
            // Hide Tiles In View (if DOM Mode)
            if (jQuery.jGlideMenuBrand.useTileURL == false && jQuery.jGlideMenuBrand.tileSource.length > 0) {
                jQuery(jQuery.jGlideMenuBrand.tileSource).css('display', 'none');
                // If Tiles are Inside of Element, Remove Everything Else
                /*
                // Old Method Used with jQuery 1.2.x
                var x = jQuery(jQuery.jGlideMenuBrand.currentElement).children();
                jQuery(x+':not('+jQuery.jGlideMenuBrand.tileSource+')').remove();
                */
                jQuery(jQuery.jGlideMenuBrand.currentElement).children().not(jQuery.jGlideMenuBrand.tileSource).remove();
            }
            else jQuery(jQuery.jGlideMenuBrand.currentElement).html('');
            // --> Remove Any Place Holder Content From Element and Hide Tiles

            // <-- Create Menu Structure
            jQuery(jQuery.jGlideMenuBrand.currentElement).append('<div class="jGMBrand_wrapper" id="jGMBrand_wrapper_' + this.id + '"></div>');
            // --> Create Menu Structure

            // <-- Create Animation/Load Image
            var img = document.createElement('img');
            img.src = jQuery.jGlideMenuBrand.loadImage;
            img.style.display = 'none';
            img.id = 'jGMBrand_helper' + jQuery.jGlideMenuBrand.currentElementID;
            jQuery(jQuery.jGlideMenuBrand.currentElement).append(img);
            jQuery('img#' + img.id).css(jQuery.jGlideMenuBrand.loadImageStyle);
            jQuery.jGlideMenuBrand.helperImage = true;
            // --> Create Animation/Load Image

            // Add Drag Drop Support
            if (jQuery.jGlideMenuBrand.hasDragDropSupport == true && jQuery.jGlideMenuBrand.useDragDrop == true) {
                if (jQuery.isFunction(jQuery('body').Draggable))
                    jQuery(this).Draggable({ handle: '.jGMBrand_header' });
                else
                    jQuery(this).draggable({ handle: '.jGMBrand_header' });
            }

            // Add Drop Shaddow Support
            if (jQuery.jGlideMenuBrand.hasShadowSupport == true && jQuery.jGlideMenuBrand.useDropShadow == true) {
                if (jQuery.isFunction(jQuery('body').dropShadow))
                    jQuery(this).dropShadow();
                else
                    jQuery(this).shadow({ color: '#cccccc' });
            }

            // Triggle Close Button
            jQuery(jQuery.jGlideMenuBrand.currentElement).find('div.jGMBrand_header a').bind('click', function () {
                if (jQuery.jGlideMenuBrand.displayToggle == true) return false;
                jQuery.jGlideMenuBrand.toggleDisplay(true);
                return false;
            });

            // Check for Mouse Over Menu
            jQuery(this).hover(function () { jQuery.jGlideMenuBrand.mouseHover = true; }, function () { jQuery.jGlideMenuBrand.mouseHover = false; });

            // Bind Keyboard Events (Top Level)
            jQuery(document).keydown(function (e) {
                // Disabled in this version:
                return true;

                var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;

                // Toggle Display when Space is Pressed
                if (key == 32 && jQuery.jGlideMenuBrand.mouseHover == false) { jQuery.jGlideMenuBrand.toggleDisplay(); return; }

                if (jQuery.jGlideMenuBrand.mouseHover == false) return false;
                switch (key) {
                    // Left      
                    case 37:
                        break;
                    // Up      
                    case 38:
                        break;
                    // Right      
                    case 39:
                        break;
                    // Down      
                    case 40:
                        break;
                    // return      
                    case 13:
                        break;
                    // space      
                    case 32:
                        jQuery.jGlideMenuBrand.toggleDisplay(); return;
                        break;
                }
            });

            // Load Initial Tile 
            jQuery.jGlideMenuBrand.loadTile(jQuery.jGlideMenuBrand.initialTile, jQuery.jGlideMenuBrand.URLParams);


        });
    },

    // Toggle Display of Menu
    toggleDisplay: function (r) {
        jQuery.jGlideMenuBrand.displayToggle = true;
        jQuery.jGlideMenuBrand.mouseHover == false;
        if (jQuery(jQuery.jGlideMenuBrand.currentElement).css('display') == 'block')
            var toggle_value = 0;
        else
            var toggle_value = 1;
        jQuery(jQuery.jGlideMenuBrand.currentElement).animate(
                        {
                            opacity: toggle_value
                        }, 'slow', function () {
                            // Check for Reset Flag
                            if (r == true) {
                                jQuery.jGlideMenuBrand.scrollToTile(0, jQuery.jGlideMenuBrand.defaultScrollBackSpeed);
                                //jQuery.jGlideMenuBrand.tileScrollPosition = [];
                                jQuery.jGlideMenuBrand.tileScrollPosition[0] = 0;
                            }
                            if (toggle_value > 0)
                                jQuery(this).css('display', 'block');
                            else
                                jQuery(this).css('display', 'none');
                            jQuery.jGlideMenuBrand.displayToggle = false;
                        }
                );
    },
    // Check Feature Availability
    checkFeatures: function () {
        // Check Drap Drop Support (jQuery Interface)
        jQuery.jGlideMenuBrand.hasDragDropSupport = jQuery.isFunction(jQuery('body').Draggable);
        // Check Drag Drop Support (jQuery UI)
        if (jQuery.jGlideMenuBrand.hasDragDropSupport == false)
            jQuery.jGlideMenuBrand.hasDragDropSupport = jQuery.isFunction(jQuery('body').draggable);
        // <-- Not Supported Currently
        // Check Shadow Support (DropShadow Plugin)
        jQuery.jGlideMenuBrand.hasShadowSupport = jQuery.isFunction(jQuery('body').dropShadow);
        // Check Shadow Support (jQuery UI)
        if (jQuery.jGlideMenuBrand.hasShadowSupport == false)
            jQuery.jGlideMenuBrand.hasShadowSupport = jQuery.isFunction(jQuery('body').shadow);
        // --> Not Supported Currently
        return;
    },
    parseURL: function (u) {
        // MSIE 6 (maybe 7) Returns #tile_001 as http://...#tile_001
        // ## jQuery.browser is Depreciated !!! ##
        if (!jQuery.browser.msie) {
            return u;
        }
        if (u.indexOf('#tile_') < 0) {
            // Regular Link
            return u;
        }
        // Navigation Link
        var bits = u.split('#');
        return '#' + bits[(bits.length - 1)];
    },
    // Return Number of Active Tiles
    countTiles: function () { jQuery.jGlideMenuBrand.tileCount = parseInt(jQuery('div.jGMBrand_tile').size()); },
    // Create and Load Tile
    loadTile: function (u, p) {
        // Create New Tile Wrapper
        var ptr = document.createElement('div');
        // New Tile Position
        var ctr = jQuery.jGlideMenuBrand.tileCount + 1;
        ptr.id = 'jGMBrand_tile_' + jQuery.jGlideMenuBrand.currentElementID + '_' + ctr;
        // Position of Tile
        if (jQuery.jGlideMenuBrand.slideRight == true)
            var off = jQuery.jGlideMenuBrand.tileWidth * jQuery.jGlideMenuBrand.tileCount + jQuery.jGlideMenuBrand.tileInset;
        else
            var off = jQuery.jGlideMenuBrand.tileWidth * jQuery.jGlideMenuBrand.tileCount * -1 + jQuery.jGlideMenuBrand.tileInset;
        // Add to DOM
        jQuery('#jGMBrand_wrapper_' + jQuery.jGlideMenuBrand.currentElementID).append(ptr);
        // Apply Class & Style to Tile
        jQuery('#' + ptr.id).addClass('jGMBrand_tile').css({
            top: 0,
            right: off + 'px',
            width: '300px',
            position: 'absolute',
            overflow: 'hidden',
            margin: 0,
            padding: 0,
            border: 0,
            display: 'block'
        });
        // Default Tile Content
        var tmpl = '<div style="height:100%;margin:0;border:0;width:100%;padding:0;text-align:center;">'
                                    + '<h3>Missing Tile</h3><p>Unable to locate the requested Tile</p></div>';
        // Load Content Into Tile
        if (jQuery.jGlideMenuBrand.useTileURL == false) {
            // DOM
            if (jQuery('ul#' + u).size() < 1) {
                jQuery.jGlideMenuBrand.errorTrap('Invalid Tile Request');
                return false;
            }
            var title = jQuery('ul#' + u).attr('title');
            var desc = jQuery('ul#' + u).attr('alt');
            var items = jQuery('ul#' + u + ' li').size();
            var links = [];
            jQuery('ul#' + u + ' li').each(function () {
                if (jQuery('a', this).size() > 0)
                    links[links.length] = [jQuery('a', this).attr('href'), jQuery('a', this).text(), 1];
                else
                    links[links.length] = [jQuery(this).attr('rel'), jQuery(this).text(), 0];
            });
            var tmpl = jQuery.jGlideMenuBrand.buildTile(title, desc, links);
        }
        else {
            // AJAX
            if (jQuery.jGlideMenuBrand.tileSource.length < 1) {
                jQuery.jGlideMenuBrand.errorTrap('Invalid AJAX Request');
                return false;
            }
            // Bind AJAX Events
            var mon = 'img#jGMBrand_helper' + jQuery.jGlideMenuBrand.currentElementID;
            jQuery(mon).ajaxStart(function () { jQuery(this).animate({ opacity: 'show' }, 'fast'); })
                                   .ajaxStop(function () { jQuery(this).animate({ opacity: 'hide' }, 'slow'); });
            p.tile = u;
            jQuery.ajax({
                type: "POST",
                url: jQuery.jGlideMenuBrand.tileSource,
                data: p,
                async: false,
                success: function (xhtml) {
                    // Make it Usable --> "var dom = jQuery(xhtml)" is crashing FF2
                    jQuery('body').append('<div id="jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + '" style="display:none;">' + xhtml + '</div>');
                    if (jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + ' ul#' + u).size() < 1) {
                        jQuery.jGlideMenuBrand.errorTrap('AJAX: Invalid Tile Request');
                        return false;
                    }
                    var title = jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + ' ul#' + u).attr('title');
                    var desc = jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + ' ul#' + u).attr('alt');
                    var items = jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + ' ul#' + u + ' li').size();
                    var links = [];
                    jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile + ' ul#' + u + ' li').each(function () {
                        if (jQuery('a', this).size() > 0)
                            links[links.length] = [jQuery('a', this).attr('href'), jQuery('a', this).text(), 1];
                        else
                            links[links.length] = [jQuery(this).attr('rel'), jQuery(this).text(), 0];
                    });
                    // Remove Temporary Tile
                    jQuery('#jGMBrand_temp_' + jQuery.jGlideMenuBrand.currentElementID + p.tile).remove();
                    tmpl = jQuery.jGlideMenuBrand.buildTile(title, desc, links);
                    // Remove AJAX Event Triggers					
                    jQuery(mon).ajaxStart(function () { }).ajaxStop(function () { });
                },
                error: function (rslt) {
                    jQuery.jGlideMenuBrand.errorTrap('Invalid AJAX Tile Request');
                    // Remove AJAX Event Triggers
                    jQuery(mon).ajaxStart(function () { }).ajaxStop(function () { });
                }
            });
        }
        // Add to DOM
        jQuery('#' + ptr.id).html(tmpl);
        // Catch Click Event for List Items	
        jQuery('#' + ptr.id + ' div.jGMBrand_content a').bind('click', function () {
            var target = '';
            target = jQuery.jGlideMenuBrand.parseURL(jQuery(this).attr('href'));
            if (target.length < 1) { return false; }
            if (target.substr(0, 1) == '#') {
                if (jQuery.jGlideMenuBrand.animation == true) return false;
                var dest = target.substr(1, target.length - 1);
                //alert('Pager Scroll Request: '+target);
                jQuery.jGlideMenuBrand.loadTile(dest, jQuery.jGlideMenuBrand.URLParams);
                return false;
            }
            else {
                if (jQuery.jGlideMenuBrand.demoMode) {
                    alert('Navigation Requestion: ' + target);
                }
                else {
                    window.location.href = target;
                }
                // Prevent Default Action As Needed
                if (jQuery.jGlideMenuBrand.captureLinks == true) return false;
            }
            return true;
        });
        // Update Tile Count
        jQuery.jGlideMenuBrand.countTiles();
        // Add Back/Reset Buttons (as Needed)
        if (jQuery.jGlideMenuBrand.tileCount > 1) {
            // Insert Back Button
            jQuery('#' + ptr.id).append('<div class="jGMBrand_back"><a href="#">رجوع</a></div>');
            jQuery('#' + ptr.id + ' div.jGMBrand_back').bind('click', function () {
                if (jQuery.jGlideMenuBrand.animation == true) return false;
                jQuery.jGlideMenuBrand.scrollToTile((ctr - 1), jQuery.jGlideMenuBrand.defaultScrollBackSpeed);
                return false;
            });
            if (jQuery.jGlideMenuBrand.tileCount > 2) {
                // Insert Reset Button
                jQuery('#' + ptr.id).append('<div class="jGMBrand_reset"><a href="#">الرئيسية</a></div>');
                jQuery('#' + ptr.id + ' div.jGMBrand_reset').bind('click', function () {
                    if (jQuery.jGlideMenuBrand.animation == true) return false;
                    jQuery.jGlideMenuBrand.scrollToTile(1, jQuery.jGlideMenuBrand.defaultScrollBackSpeed);
                    return false;
                });
            }
        }
        // Set Tile Scroll Position
        jQuery.jGlideMenuBrand.tileScrollPosition[ctr] = 0;
        // Set Pager (Init)
        jQuery.jGlideMenuBrand.drawPagers(ptr.id, jQuery('#' + ptr.id + ' .jGMBrand_content a').size());
        // Catch Pager Scroll
        if (jQuery.jGlideMenuBrand.useSmoothScrolling == false) {
            jQuery('#' + ptr.id + ' .jGMBrand_pager a').click(function () {
                var dir = 1;
                if (jQuery(this).attr('rel') == 'Up') dir = 0;
                jQuery.jGlideMenuBrand.scrollItems(dir);
            });
        }
        else {
            jQuery('#' + ptr.id + ' .jGMBrand_pager a').hover(function () {
                var dir = 1;
                if (jQuery(this).attr('rel') == 'Up')
                    dir = 0;
                jQuery.jGlideMenuBrand.smoothScrollTimer[jQuery.jGlideMenuBrand.tileCount]
					= window.setInterval('jQuery.jGlideMenuBrand.scrollItems(' + dir + ')', 250);
            }, function () {
                window.clearInterval(jQuery.jGlideMenuBrand.smoothScrollTimer[jQuery.jGlideMenuBrand.tileCount]);
            });
        }
        // Scroll
        jQuery.jGlideMenuBrand.scrollToTile(ctr, jQuery.jGlideMenuBrand.defaultScrollSpeed);
    },
    // Scroll Items
    scrollItems: function (d) {
        var x = '#jGMBrand_tile_' + jQuery.jGlideMenuBrand.currentElementID + '_' + jQuery.jGlideMenuBrand.tileCount;
        var s = jQuery(x + ' .jGMBrand_content a');
        var c = jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount];
        // Enforce Bounds
        if (c <= 0 && d == 0) return;
        if (c + jQuery.jGlideMenuBrand.itemsToDisplay -2 >= jQuery(s).size() && d == 1) return;
        // Handle Scroll
        if (d == 0)
            jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount]--;
        else
            jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount]++;
        jQuery(s).show();
        jQuery(x + ' .jGMBrand_content').children('a:lt(' + jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount] + ')').hide();
        jQuery.jGlideMenuBrand.drawPagers(x.substr(1, x.length), jQuery(s).size());
    },
    // Draw Pager Controls (Toggle Visibility)
    drawPagers: function (p, c) {
        jQuery('#' + p + ' .jGMBrand_pager').find('a').each(function () {
            if (jQuery(this).attr('rel') == 'Up') {
                if (jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount] > 0)
                    jQuery(this).css('display', 'block');
                else
                    jQuery(this).css('display', 'none');
            }
            else {
                if (jQuery.jGlideMenuBrand.tileScrollPosition[jQuery.jGlideMenuBrand.tileCount] + jQuery.jGlideMenuBrand.itemsToDisplay - 2 <
						jQuery('#' + p + ' .jGMBrand_content a').size())
                    jQuery(this).css('display', 'block');
                else
                    jQuery(this).css('display', 'none');
            }
        });
    },
    // Remove Tiles
    cleanTiles: function (n) {
        var start = n + 1;
        var stop = jQuery.jGlideMenuBrand.tileCount;
        if (n >= stop) return false;
        for (var i = start; i <= stop; i++) {
            jQuery('#jGMBrand_tile_' + jQuery.jGlideMenuBrand.currentElementID + '_' + i).remove();
            jQuery.jGlideMenuBrand.tileScrollPosition[i] = 0;
        }
        jQuery.jGlideMenuBrand.countTiles();
        return;
    },

    // Handle Horizontal Scroll
    scrollToTile: function (n, s) {
        // Get Tile Count
        jQuery.jGlideMenuBrand.countTiles();
        var t = jQuery.jGlideMenuBrand.tileCount;
        // Enforce Bounds	
        if (n > t) n = t;
        if (n < 1) n = 1;
        // Set Speed	
        if (!s) s = jQuery.jGlideMenuBrand.defaultScrollSpeed;
        var b = (jQuery.jGlideMenuBrand.tileWidth * n) - jQuery.jGlideMenuBrand.tileWidth;
        var a = (jQuery.jGlideMenuBrand.slideRight == true) ? b * -1 : b;
        // Animate
        jQuery.jGlideMenuBrand.animation = true;
        jQuery('div#jGMBrand_wrapper_' + jQuery.jGlideMenuBrand.currentElementID).animate({ 'right': a }, s, jQuery.jGlideMenuBrand.easeFx, function () {
            // Remove Extra Tiles
            if (n < t)
                jQuery.jGlideMenuBrand.cleanTiles(n);
            if (a != 0) a += 'px';
            jQuery(this).css({ 'right': a });
            jQuery.jGlideMenuBrand.animation = false;
        });
    },

    // Return Template
    buildTile: function (t, d, l) {
        if (jQuery.jGlideMenuBrand.imagePath.length > 1 && jQuery.jGlideMenuBrand.imagePath.substr(-1, 1) != '/')
            jQuery.jGlideMenuBrand.imagePath += '/';
        var template = new String('');
        // Header Layout
        template = '<div class="jGMBrand_cats"><img src=' + t + '><p class="jGMBrand_desc">' + d + '</p></div>';
        // Scroll Up
        template += '<div class="jGMBrand_pager"><a href="#" rel="Up" title="Scroll Up" style="display:none"><span>المزيد</span></a></div>';
        // Items
        template += '<div class="jGMBrand_content">';
        for (var i = 0; i < l.length; i++) {
            var hash = (l[i][2] == 1) ? '' : '#';
            var type = (l[i][2] == 1) ? '' : ' class="jGMBrand_more"';
            template += '<a href="' + hash + l[i][0] + '"' + type + '>' + l[i][1] + '</a>';
            //<a href="fetch://{'current':5,'previous':1,'apiKey':'bd51b0648d268122996b9e68cfd86175','client':'ActiveSpotLight'}"  class="jGMBrand_more">More Options..</a>
        }
        template += '</div>';
        // Scroll Down
        template += '<div class="jGMBrand_pager"><a href="#" rel="Down" title="Scroll Down" style="display:none"><span>المزيد</span></a></div>';
        return template;
    },
    // Error Function
    errorTrap: function (m) {
        if (jQuery.jGlideMenuBrand.alertOnError == true)
            alert(m);
        return;
    }
}

// Extend Global jQuery Functions
jQuery.fn.jGlideMenuBrand = jQuery.jGlideMenuBrand.initialize;
jQuery.fn.jGlideMenuBrandToggle = jQuery.jGlideMenuBrand.toggleDisplay;
jQuery.fn.reverseBrand = function () { return this.pushStack(this.get().reverse(), arguments); };