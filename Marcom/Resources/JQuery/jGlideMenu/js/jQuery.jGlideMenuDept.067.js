
/*
* Filename:	jQuery.jGlideMenuDept.js (formally fastFind)
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

jQuery.jGlideMenuDept = {

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

    // Set By jGlideMenuDept Script
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
            jQuery.jGlideMenuDept.animation = false;
            jQuery.jGlideMenuDept.helperImage = false;
            jQuery.jGlideMenuDept.hasDragDropSupport = false;
            jQuery.jGlideMenuDept.hasShadowSupport = false;
            jQuery.jGlideMenuDept.tileCount = 0;
            jQuery.jGlideMenuDept.displayToggle = false;
            jQuery.jGlideMenuDept.mouseHover = false;
            jQuery.jGlideMenuDept.demoMode = false;

            // Store Current Element	
            jQuery.jGlideMenuDept.currentElement = jQuery(this);
            if (this.id) jQuery.jGlideMenuDept.currentElementID = this.id;

            // Default Values
            var s = {
                itemsToDisplay: 12,
                tileInset: 7,
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
            jQuery.jGlideMenuDept.checkFeatures();

            // Ensure Values (Basic)
            if (s.closeLinkMarkUp.length < 1) s.closeLinkMarkUp = 'x Close';
            if (parseInt(s.itemsToDisplay) < 1) s.itemsToDisplay = 1;
            if (s.initialTile.length < 0) jQuery.jGlideMenuDept.errorTrap('Invalid Configuration');

            // Set Global Values 
            for (i in s) jQuery.jGlideMenuDept[i] = s[i];

            // <-- Remove Any Place Holder Content From Element and Hide Tiles
            // Hide Tiles In View (if DOM Mode)
            if (jQuery.jGlideMenuDept.useTileURL == false && jQuery.jGlideMenuDept.tileSource.length > 0) {
                jQuery(jQuery.jGlideMenuDept.tileSource).css('display', 'none');
                // If Tiles are Inside of Element, Remove Everything Else
                /*
                // Old Method Used with jQuery 1.2.x
                var x = jQuery(jQuery.jGlideMenuDept.currentElement).children();
                jQuery(x+':not('+jQuery.jGlideMenuDept.tileSource+')').remove();
                */
                jQuery(jQuery.jGlideMenuDept.currentElement).children().not(jQuery.jGlideMenuDept.tileSource).remove();
            }
            else jQuery(jQuery.jGlideMenuDept.currentElement).html('');
            // --> Remove Any Place Holder Content From Element and Hide Tiles

            // <-- Create Menu Structure
            jQuery(jQuery.jGlideMenuDept.currentElement).append('<div class="jGMDept_wrapper" id="jGMDept_wrapper_' + this.id + '"></div>');
            // --> Create Menu Structure

            // <-- Create Animation/Load Image
            var img = document.createElement('img');
            img.src = jQuery.jGlideMenuDept.loadImage;
            img.style.display = 'none';
            img.id = 'jGMDept_helper' + jQuery.jGlideMenuDept.currentElementID;
            jQuery(jQuery.jGlideMenuDept.currentElement).append(img);
            jQuery('img#' + img.id).css(jQuery.jGlideMenuDept.loadImageStyle);
            jQuery.jGlideMenuDept.helperImage = true;
            // --> Create Animation/Load Image

            // Add Drag Drop Support
            if (jQuery.jGlideMenuDept.hasDragDropSupport == true && jQuery.jGlideMenuDept.useDragDrop == true) {
                if (jQuery.isFunction(jQuery('body').Draggable))
                    jQuery(this).Draggable({ handle: '.jGMDept_header' });
                else
                    jQuery(this).draggable({ handle: '.jGMDept_header' });
            }

            // Add Drop Shaddow Support
            if (jQuery.jGlideMenuDept.hasShadowSupport == true && jQuery.jGlideMenuDept.useDropShadow == true) {
                if (jQuery.isFunction(jQuery('body').dropShadow))
                    jQuery(this).dropShadow();
                else
                    jQuery(this).shadow({ color: '#cccccc' });
            }

            // Triggle Close Button
            jQuery(jQuery.jGlideMenuDept.currentElement).find('div.jGMDept_header a').bind('click', function () {
                if (jQuery.jGlideMenuDept.displayToggle == true) return false;
                jQuery.jGlideMenuDept.toggleDisplay(true);
                return false;
            });

            // Check for Mouse Over Menu
            jQuery(this).hover(function () { jQuery.jGlideMenuDept.mouseHover = true; }, function () { jQuery.jGlideMenuDept.mouseHover = false; });

            // Bind Keyboard Events (Top Level)
            jQuery(document).keydown(function (e) {
                // Disabled in this version:
                return true;

                var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;

                // Toggle Display when Space is Pressed
                if (key == 32 && jQuery.jGlideMenuDept.mouseHover == false) { jQuery.jGlideMenuDept.toggleDisplay(); return; }

                if (jQuery.jGlideMenuDept.mouseHover == false) return false;
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
                        jQuery.jGlideMenuDept.toggleDisplay(); return;
                        break;
                }
            });

            // Load Initial Tile 
            jQuery.jGlideMenuDept.loadTile(jQuery.jGlideMenuDept.initialTile, jQuery.jGlideMenuDept.URLParams);


        });
    },

    // Toggle Display of Menu
    toggleDisplay: function (r) {
        jQuery.jGlideMenuDept.displayToggle = true;
        jQuery.jGlideMenuDept.mouseHover == false;
        if (jQuery(jQuery.jGlideMenuDept.currentElement).css('display') == 'block')
            var toggle_value = 0;
        else
            var toggle_value = 1;
        jQuery(jQuery.jGlideMenuDept.currentElement).animate(
                        {
                            opacity: toggle_value
                        }, 'slow', function () {
                            // Check for Reset Flag
                            if (r == true) {
                                jQuery.jGlideMenuDept.scrollToTile(0, jQuery.jGlideMenuDept.defaultScrollBackSpeed);
                                //jQuery.jGlideMenuDept.tileScrollPosition = [];
                                jQuery.jGlideMenuDept.tileScrollPosition[0] = 0;
                            }
                            if (toggle_value > 0)
                                jQuery(this).css('display', 'block');
                            else
                                jQuery(this).css('display', 'none');
                            jQuery.jGlideMenuDept.displayToggle = false;
                        }
                );
    },

    // Check Feature Availability
    checkFeatures: function () {
        // Check Drap Drop Support (jQuery Interface)
        jQuery.jGlideMenuDept.hasDragDropSupport = jQuery.isFunction(jQuery('body').Draggable);
        // Check Drag Drop Support (jQuery UI)
        if (jQuery.jGlideMenuDept.hasDragDropSupport == false)
            jQuery.jGlideMenuDept.hasDragDropSupport = jQuery.isFunction(jQuery('body').draggable);

        // <-- Not Supported Currently
        // Check Shadow Support (DropShadow Plugin)
        jQuery.jGlideMenuDept.hasShadowSupport = jQuery.isFunction(jQuery('body').dropShadow);
        // Check Shadow Support (jQuery UI)
        if (jQuery.jGlideMenuDept.hasShadowSupport == false)
            jQuery.jGlideMenuDept.hasShadowSupport = jQuery.isFunction(jQuery('body').shadow);
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
    countTiles: function () { jQuery.jGlideMenuDept.tileCount = parseInt(jQuery('div.jGMDept_tile').size()); },

    // Create and Load Tile
    loadTile: function (u, p) {
        // Create New Tile Wrapper
        var ptr = document.createElement('div');
        // New Tile Position
        var ctr = jQuery.jGlideMenuDept.tileCount + 1;
        ptr.id = 'jGMDept_tile_' + jQuery.jGlideMenuDept.currentElementID + '_' + ctr;
        // Position of Tile
        if (jQuery.jGlideMenuDept.slideRight == true)
            var off = jQuery.jGlideMenuDept.tileWidth * jQuery.jGlideMenuDept.tileCount + jQuery.jGlideMenuDept.tileInset;
        else
            var off = jQuery.jGlideMenuDept.tileWidth * jQuery.jGlideMenuDept.tileCount * -1 + jQuery.jGlideMenuDept.tileInset;
        // Add to DOM
        jQuery('#jGMDept_wrapper_' + jQuery.jGlideMenuDept.currentElementID).append(ptr);
        // Apply Class & Style to Tile
        jQuery('#' + ptr.id).addClass('jGMDept_tile').css({
            top: 0,
            left: off + 'px',
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
        if (jQuery.jGlideMenuDept.useTileURL == false) {
            // DOM
            if (jQuery('ul#' + u).size() < 1) {
                jQuery.jGlideMenuDept.errorTrap('Invalid Tile Request');
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
            var tmpl = jQuery.jGlideMenuDept.buildTile(title, desc, links);
        }
        else {
            // AJAX
            if (jQuery.jGlideMenuDept.tileSource.length < 1) {
                jQuery.jGlideMenuDept.errorTrap('Invalid AJAX Request');
                return false;
            }

            // Bind AJAX Events
            var mon = 'img#jGMDept_helper' + jQuery.jGlideMenuDept.currentElementID;
            jQuery(mon).ajaxStart(function () { jQuery(this).animate({ opacity: 'show' }, 'fast'); })
                                   .ajaxStop(function () { jQuery(this).animate({ opacity: 'hide' }, 'slow'); });
            p.tile = u;
            jQuery.ajax({
                type: "POST",
                url: jQuery.jGlideMenuDept.tileSource,
                data: p,
                async: false,
                success: function (xhtml) {
                    // Make it Usable --> "var dom = jQuery(xhtml)" is crashing FF2
                    jQuery('body').append('<div id="jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + '" style="display:none;">' + xhtml + '</div>');
                    if (jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + ' ul#' + u).size() < 1) {
                        jQuery.jGlideMenuDept.errorTrap('AJAX: Invalid Tile Request');
                        return false;
                    }
                    var title = jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + ' ul#' + u).attr('title');
                    var desc = jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + ' ul#' + u).attr('alt');
                    var items = jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + ' ul#' + u + ' li').size();
                    var links = [];
                    jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile + ' ul#' + u + ' li').each(function () {
                        if (jQuery('a', this).size() > 0)
                            links[links.length] = [jQuery('a', this).attr('href'), jQuery('a', this).text(), 1];
                        else
                            links[links.length] = [jQuery(this).attr('rel'), jQuery(this).text(), 0];
                    });
                    // Remove Temporary Tile
                    jQuery('#jGMDept_temp_' + jQuery.jGlideMenuDept.currentElementID + p.tile).remove();
                    tmpl = jQuery.jGlideMenuDept.buildTile(title, desc, links);
                    // Remove AJAX Event Triggers					
                    jQuery(mon).ajaxStart(function () { }).ajaxStop(function () { });
                },
                error: function (rslt) {
                    jQuery.jGlideMenuDept.errorTrap('Invalid AJAX Tile Request');
                    // Remove AJAX Event Triggers
                    jQuery(mon).ajaxStart(function () { }).ajaxStop(function () { });
                }
            });
        }

        // Add to DOM
        jQuery('#' + ptr.id).html(tmpl);

        // Catch Click Event for List Items	
        jQuery('#' + ptr.id + ' div.jGMDept_content a').bind('click', function () {
            var target = '';
            target = jQuery.jGlideMenuDept.parseURL(jQuery(this).attr('href'));

            if (target.length < 1) { return false; }

            if (target.substr(0, 1) == '#') {
                if (jQuery.jGlideMenuDept.animation == true) return false;
                var dest = target.substr(1, target.length - 1);
                //alert('Pager Scroll Request: '+target);
                jQuery.jGlideMenuDept.loadTile(dest, jQuery.jGlideMenuDept.URLParams);
                return false;
            }
            else {
                if (jQuery.jGlideMenuDept.demoMode) {
                    alert('Navigation Requestion: ' + target);
                }
                else {
                    window.location.href = target;
                }

                // Prevent Default Action As Needed
                if (jQuery.jGlideMenuDept.captureLinks == true) return false;
            }
            return true;
        });

        // Update Tile Count
        jQuery.jGlideMenuDept.countTiles();

        // Add Back/Reset Buttons (as Needed)
        if (jQuery.jGlideMenuDept.tileCount > 1) {
            // Insert Back Button
            jQuery('#' + ptr.id).append('<div class="jGMDept_back"><a href="#">&laquo; Back</a></div>');
            jQuery('#' + ptr.id + ' div.jGMDept_back').bind('click', function () {
                if (jQuery.jGlideMenuDept.animation == true) return false;
                jQuery.jGlideMenuDept.scrollToTile((ctr - 1), jQuery.jGlideMenuDept.defaultScrollBackSpeed);
                return false;
            });

            if (jQuery.jGlideMenuDept.tileCount > 2) {
                // Insert Reset Button
                jQuery('#' + ptr.id).append('<div class="jGMDept_reset"><a href="#">&laquo; Home</a></div>');
                jQuery('#' + ptr.id + ' div.jGMDept_reset').bind('click', function () {
                    if (jQuery.jGlideMenuDept.animation == true) return false;
                    jQuery.jGlideMenuDept.scrollToTile(1, jQuery.jGlideMenuDept.defaultScrollBackSpeed);
                    return false;
                });
            }
        }

        // Set Tile Scroll Position
        jQuery.jGlideMenuDept.tileScrollPosition[ctr] = 0;

        // Set Pager (Init)
        jQuery.jGlideMenuDept.drawPagers(ptr.id, jQuery('#' + ptr.id + ' .jGMDept_content a').size());

        // Catch Pager Scroll
        if (jQuery.jGlideMenuDept.useSmoothScrolling == false) {
            jQuery('#' + ptr.id + ' .jGMDept_pager a').click(function () {
                var dir = 1;
                if (jQuery(this).attr('rel') == 'Up') dir = 0;
                jQuery.jGlideMenuDept.scrollItems(dir);
            });
        }
        else {
            jQuery('#' + ptr.id + ' .jGMDept_pager a').hover(function () {
                var dir = 1;
                if (jQuery(this).attr('rel') == 'Up')
                    dir = 0;
                jQuery.jGlideMenuDept.smoothScrollTimer[jQuery.jGlideMenuDept.tileCount]
					= window.setInterval('jQuery.jGlideMenuDept.scrollItems(' + dir + ')', 250);
            }, function () {
                window.clearInterval(jQuery.jGlideMenuDept.smoothScrollTimer[jQuery.jGlideMenuDept.tileCount]);
            });
        }

        // Scroll
        jQuery.jGlideMenuDept.scrollToTile(ctr, jQuery.jGlideMenuDept.defaultScrollSpeed);

    },

    // Scroll Items
    scrollItems: function (d) {
        var x = '#jGMDept_tile_' + jQuery.jGlideMenuDept.currentElementID + '_' + jQuery.jGlideMenuDept.tileCount;
        var s = jQuery(x + ' .jGMDept_content a');
        var c = jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount];
        // Enforce Bounds
        if (c <= 0 && d == 0) return;
        if (c + jQuery.jGlideMenuDept.itemsToDisplay - 2 >= jQuery(s).size() && d == 1) return;
        // Handle Scroll
        if (d == 0)
            jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount]--;
        else
            jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount]++;
        jQuery(s).show();
        jQuery(x + ' .jGMDept_content').children('a:lt(' + jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount] + ')').hide();
        jQuery.jGlideMenuDept.drawPagers(x.substr(1, x.length), jQuery(s).size());
    },

    // Draw Pager Controls (Toggle Visibility)
    drawPagers: function (p, c) {
        jQuery('#' + p + ' .jGMDept_pager').find('a').each(function () {
            if (jQuery(this).attr('rel') == 'Up') {
                if (jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount] > 0)
                    jQuery(this).css('display', 'block');
                else
                    jQuery(this).css('display', 'none');
            }
            else {
                if (jQuery.jGlideMenuDept.tileScrollPosition[jQuery.jGlideMenuDept.tileCount] + jQuery.jGlideMenuDept.itemsToDisplay - 2 <
						jQuery('#' + p + ' .jGMDept_content a').size())
                    jQuery(this).css('display', 'block');
                else
                    jQuery(this).css('display', 'none');
            }
        });
    },

    // Remove Tiles
    cleanTiles: function (n) {
        var start = n + 1;
        var stop = jQuery.jGlideMenuDept.tileCount;

        if (n >= stop) return false;

        for (var i = start; i <= stop; i++) {
            jQuery('#jGMDept_tile_' + jQuery.jGlideMenuDept.currentElementID + '_' + i).remove();
            jQuery.jGlideMenuDept.tileScrollPosition[i] = 0;
        }

        jQuery.jGlideMenuDept.countTiles();

        return;
    },

    // Handle Horizontal Scroll
    scrollToTile: function (n, s) {

        // Get Tile Count
        jQuery.jGlideMenuDept.countTiles();
        var t = jQuery.jGlideMenuDept.tileCount;

        // Enforce Bounds	
        if (n > t) n = t;
        if (n < 1) n = 1;

        // Set Speed	
        if (!s) s = jQuery.jGlideMenuDept.defaultScrollSpeed;

        var b = (jQuery.jGlideMenuDept.tileWidth * n) - jQuery.jGlideMenuDept.tileWidth;
        var a = (jQuery.jGlideMenuDept.slideRight == true) ? b * -1 : b;
        // Animate
        jQuery.jGlideMenuDept.animation = true;
        jQuery('div#jGMDept_wrapper_' + jQuery.jGlideMenuDept.currentElementID).animate({ 'left': a }, s, jQuery.jGlideMenuDept.easeFx, function () {
            // Remove Extra Tiles
            if (n < t)
                jQuery.jGlideMenuDept.cleanTiles(n);
            if (a != 0) a += 'px';
            jQuery(this).css({ 'left': a });

            jQuery.jGlideMenuDept.animation = false;
        });
    },

    // Return Template
    buildTile: function (t, d, l) {
        if (jQuery.jGlideMenuDept.imagePath.length > 1 && jQuery.jGlideMenuDept.imagePath.substr(-1, 1) != '/')
            jQuery.jGlideMenuDept.imagePath += '/';

        var template = new String('');
        // Header Layout
        template = '<div class="jGMDept_cats"><img src=' + t + '><p class="jGMDept_desc">' + d + '</p></div>';
        // Scroll Up
        template += '<div class="jGMDept_pager"><a href="#" rel="Up" title="Scroll Up" style="display:none"><span>More</span></a></div>';
        // Items
        template += '<div class="jGMDept_content">';
        for (var i = 0; i < l.length; i++) {
            var hash = (l[i][2] == 1) ? '' : '#';
            var type = (l[i][2] == 1) ? '' : ' class="jGMDept_more"';
            template += '<a href="' + hash + l[i][0] + '"' + type + '>' + l[i][1] + '</a>';
            //<a href="fetch://{'current':5,'previous':1,'apiKey':'bd51b0648d268122996b9e68cfd86175','client':'ActiveSpotLight'}"  class="jGMDept_more">More Options..</a>
        }
        template += '</div>';
        // Scroll Down
        template += '<div class="jGMDept_pager"><a href="#" rel="Down" title="Scroll Down" style="display:none"><span>More</span></a></div>';
        return template;
    },

    // Error Function
    errorTrap: function (m) {
        if (jQuery.jGlideMenuDept.alertOnError == true)
            alert(m);
        return;
    }
}

// Extend Global jQuery Functions
jQuery.fn.jGlideMenuDept = jQuery.jGlideMenuDept.initialize;
jQuery.fn.jGlideMenuDeptToggle = jQuery.jGlideMenuDept.toggleDisplay;
jQuery.fn.reverseDept = function () { return this.pushStack(this.get().reverse(), arguments); };