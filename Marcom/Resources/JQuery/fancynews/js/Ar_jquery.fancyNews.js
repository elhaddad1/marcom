/*!
* Fancy News v1.3
*
* Copyright 2011, Rafael Dery
*
* Only for sale at the envato marketplaces
*/

(function (e) {
    function z(l, m) {
        var k = l.split(" "), s = "";
        if (k.length > m) {
            for (var n = 0; n < m; ++n)
                s += k[n] + " ";
            return s + "<span style='font-size: 12px;color: #97B250;'> للمزيد أضغط هنا</span>"
        }
        else
            return l
    }
    function t(l, m, k) {
        return String(l + "-" + k + " of " + k + " News")
    }
    jQuery.fn.fancyNews = function (l) {
        function m() {
            var d = [];
            e.jGFeed(a.feed, function (f) {
                if (!f) {
                    b.find("#fn-preloader").remove();
                    b.append("<span id='fc-error'>No feeds found! Please check your RSS feed!</span>");
                    return false
                }
                for (var c = 0; c < f.entries.length; c++) {
                    var j = f.entries[c];
                    o[c] = j.title;
                    p[c] = String(j.publishedDate).substr(0, 16);
                    u[c] = z(j.contentSnippet.slice(0, j.contentSnippet.length - 3), a.maxWords) ;
                    x[c] = j.content;
                    A[c] = j.link;
                    d[c] = "<div class='fn-newsPreview' style='width:" + a.width + "px; height:" + q + "px; background:" + a.backgroundColor + ";'><span class='fn-newsPreviewText' ><h3>" + o[c] + "</h3><h4>" + p[c] + "</h4>" + u[c] + "</span></div>"
                } k(d)
            }, a.numOfEntries)
        } function k(d) {
            b.find("#fn-preloader").remove(); for (var f = 0; f < d.length; ++f) b.append(d[f]).find(".fn-newsPreview:last").css({ top: f * (q + a.previewOffset) }); h = e.makeArray(b.find(".fn-newsPreview"));
            b.append("<div id='fn-newsView' style='width:100%;height:" + b.height() + "px;'><h3 id='fn-newsViewTitle'></h3><div id='fn-newsViewHtml' style='width:" + (a.width - 30) + "px;height:" + (b.height() - 50) + "px;'></div></div>"); b.find("#fn-newsViewHtml").jScrollPane(); d = b.find("#fn-newsViewHtml"); d.jScrollPane(); v = d.data("jsp");
            b.after("<div id='fn-newsFooterBar' style='width:" + a.width + "px;background:" + a.backgroundColor + "'><span id='newsFooterText'>" + "" + /*+ t(g + 1, g + a.previewsPerPage, h.length) + */"</span><span id='fn-previousButton'></span><span id='fn-nextButton'></span><div id='fn-newsViewClose'></div></div>");
            b.next("#fn-newsFooterBar").hide();
            b.next("#fn-newsFooterBar").find("#fn-newsViewClose").hide().click(function () {
                B();
                b.next("#fn-newsFooterBar").find("#newsFooterText").text("");

                //  b.next("#fn-newsFooterBar").find("#newsFooterText").text(t(g + 1, g + a.previewsPerPage, h.length));
                b.find("#fn-newsView").stop().fadeTo(340, 0, function () { e(this).css("visibility", "hidden") })
            });
            for (f = 0; f < a.previewsPerPage; ++f)
                e(h[f]).hide().delay(340 + f * 340).fadeIn(800);
            setTimeout(function () {
                b.next("#fn-newsFooterBar").fadeIn(800, s)
            }, 340 + a.previewsPerPage * 340)
        }
        function s() {
            i = false; b.find(".fn-newsPreview").hover(function () {
                if (!i) {
                    i =
true;
                    e(this).stop().animate(
{
    backgroundColor: a.backgroundOverColor
}
, 800)
                }
            }, function () {
                if (!r) {
                    i = false;
                    e(this).stop().animate({ backgroundColor: a.backgroundColor }, 340)
                }
            }).click(function () {
                if (!r) {
                    var d = b.find(".fn-newsPreview").index(this); if (a.useLinks && a.feed) window.open(A[d], a.targetWindow);
                    else {
                        var f = x[d] + "</br><a href='" + link[d] + "'><span style='font-size: 12px;color: #97B250;cursor: pointer;'> أقرأ التفاصيل كاملة.</span></a>";
                         B(); b.find("#fn-newsView").css({ visibility: "visible", opacity: 0 }).stop().fadeTo(340, 1);
                        b.find("#fn-newsViewTitle").text(o[d]); b.next("#fn-newsFooterBar").find("#newsFooterText").text(p[d]); v.getContentPane().html(f);
                        v.reinitialise({ verticalGutter: 15 }); v.scrollToPercentY(0); e("#fn-newsView img").jScale({ ls: a.longestImageSide })
                    }
                }
            });
            b.next("#fn-newsFooterBar").find("#fn-previousButton").click(function () { g != 0 && !i && C() });
            b.next("#fn-newsFooterBar").find("#fn-nextButton").click(function () { g != h.length - a.previewsPerPage && !i && n() });
            if (a.slideTime) y = setInterval(D, a.slideTime)
        } function n() {
            E("-"); ++g; b.next("#fn-newsFooterBar").find("#newsFooterText").text("");
            e(h[g + a.previewsPerPage - 1]).css("opacity",
0).animate({ opacity: 1 }, 340, function () { i = r = false })
        } function C() {
            --g; E("+");
            b.next("#fn-newsFooterBar").find("#newsFooterText").text("");
            e(h[g]).css("opacity", 0).animate({ opacity: 1 }, function () { i = r = false })
        } function E(d) {
            r = i = true;
            for (var f = q + 2, c = g; c < h.length; ++c) e(h[c]).animate({ top: d + "=" + f + "px" }, 340)
        }
        function B() {
            if (b.next().find("#fn-previousButton").is(":hidden")) {
                b.next().find("#fn-newsViewClose").hide();
                b.next().find("#fn-previousButton, #fn-nextButton").show(); if (a.slideTime) y =
setInterval(D, a.slideTime)
            } else {
                b.next().find("#fn-newsViewClose").show(); b.next().find("#fn-previousButton, #fn-nextButton").hide();
                a.slideTime && clearInterval(y)
            }
        } function D() {
            if (!i) {
                if (g != 0 && !i && w == "previous") C(); else w = "next";
                if (g != h.length - a.previewsPerPage && !i && w == "next") n(); else w = "previous"
            }
        }
        var a = e.extend({}, e.fn.fancyNews.defaults, l), b, q, v, y, o = [], p = [], u = [], x = [], A = [], h = [], link = [], g = 0, i = true, r = false, w = "next";
        return this.each(function () {
            b = e(this); var d = e.makeArray(b.find("div").hide()); b.css({ overflow: "hidden",
                position: "relative", width: a.width, height: a.height + a.previewOffset * a.previewsPerPage
            }); b.append("<div id='fn-preloader'></div>");
            b.find("#fn-preloader").css({ top: a.height * 0.5 - b.find("#fn-preloader").height() * 0.5, left: a.width * 0.5 - b.find("#fn-preloader").width() * 0.5 });
            q = a.height / a.previewsPerPage;
            if (a.feed) m();
            else {
                for (var f = [], c = 0; c < d.length; ++c) {
                    p[c] = e(d[c]).attr("title") ? e(d[c]).attr("title") : "";
                    o[c] = e(d[c]).children("span").attr("title").length > 0 ? e(d[c]).find("span").attr("title") : "";
                    link[c] = e(d[c]).attr("link") ? e(d[c]).attr("link") : "";
                    u[c] = z(e(d[c]).children("span").text(), a.maxWords);
                    x[c] = e(d[c]).children("span").html(); var j = e(d[c]).children("img").attr("src");
                    f[c] = "<div class='fn-newsPreview' style='width:" + a.width + "px; height:" + q + "px; background:" + a.backgroundColor + ";'>" + (j ? '<img class="fn-newsPreviewThumb" src="' + j + '" />' : "") + "<span class='fn-newsPreviewText' ><h3>" + o[c] + "</h3><h4>" + p[c] + "</h4>" + u[c] + "</span></div>"
                } k(f)
            }
        })
    };
    e.fn.fancyNews.defaults = { width: 340, height: 200, previewOffset: 2, maxWords: 60, previewsPerPage: 1, numOfEntries: 10, slideTime: 0, longestImageSide: 100,
        feed: "", backgroundColor: "#005984", backgroundOverColor: "#0077B1", useLinks: false, targetWindow: "_blank"
    }
})(jQuery);


