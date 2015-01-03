window.addEvent('domready', function () {

    var newprods = $$('a.newproduct');
    newprods.each(function (el) {
        el.adopt(new Element('span'));
    });

    var list = $$('#newproducts div.float');
    list.each(function (element) {

        var childP = element.getElement('p');
        var childA = childP.getElement('a');

        element.addEvent('mouseenter', function () {

            element.setStyle('background-position', 'center bottom');
            childP.setStyle('border-color', '#ba0117');
            childA.setStyle('color', '#000');

        });

        element.addEvent('mouseleave', function () {

            element.setStyle('background-position', 'center top');
            childP.setStyle('border-color', '#d9dde3');
            childA.setStyle('color', '#999');

        });

    });


    if (($('product-more')) && ($('product-body'))) {

        var mySlide = new Fx.Slide('product-body');
        mySlide.hide();

        $('product-more').addEvent('click', function (e) {
            e = new Event(e);
            if (this.innerHTML == 'More....') {
                this.setHTML('Less....');
                mySlide.slideIn();
                e.stop();
            } else {
                this.setHTML('More....');
                mySlide.slideOut();
                e.stop();
            }
        });
    }

    if ($('product-slide')) {

        // Product Slider
        var sliderX = 0;
        var sliderDisplay = 4;
        var sliderMax = $$('#slider div.section').length - sliderDisplay;
        var sliderWidth = 140;
        var sliderHeight = $('slider-container').getCoordinates().height;

        var slider = new Fx.Scroll('slider-container', {
            wait: false,
            duration: 500,
            transition: Fx.Transitions.Quad.easeInOut
        });

        $('slider-left').addEvent('click', function (event) {
            event = new Event(event).stop();
            sliderX = sliderX - sliderWidth;
            if (sliderX < sliderWidth) sliderX = 0;
            slider.scrollTo(sliderX, 0);
        });
        $('slider-right').addEvent('click', function (event) {
            event = new Event(event).stop();
            sliderX = sliderX + sliderWidth;
            if (sliderX > (sliderMax * sliderWidth)) sliderX = sliderMax * sliderWidth;
            slider.scrollTo(sliderX, 0);
        });

        sliderX = slideactive * sliderWidth;
        if (sliderX > (sliderMax * sliderWidth)) sliderX = sliderMax * sliderWidth;
        slider.scrollTo(sliderX, 0);

    }

    if ($('product-image')) {

        //		$('product-img-display').addEvent('click', function(e) {
        //			e = new Event(e).stop();
        //			Slimbox.open($('product-img-display').getProperty('src'), $('product-details-title').innerHTML);
        //		});

        // Gallery Images
        var ImageLink = $('Image-Link')
        var galleryBig = $('product-img-display');
        var galleryThumbs = $$('#product-imageicons img');

        galleryThumbs.each(function (el, i) {
            el.addEvent('click', function (event) {
                event = new Event(event).stop();
                ns = this.getProperty('src');
                //				$('product-img-display').addEvent('click', function(e) {
                //					e = new Event(e).stop();
                //					Slimbox.open(ns, $('product-details-title').innerHTML);
                //				});
                var galleryTrans = galleryBig.effects({ duration: 200, transition: Fx.Transitions.Quart.easeOut });
                galleryTrans.start({
                    'opacity': 0
                }).chain(function () {
                    galleryBig.setProperty('src', ns);
                    ImageLink.setProperty('href', ns)
                    galleryTrans.start.delay(50, this, {
                        'opacity': 1
                    });
                });
            });
        });
    }


    // Specs / Features Switcher
    if ($('specs-click')) {
        $('specs-click').addEvent('click', function (event) {

            event = new Event(event).stop();
            $('product-features').setStyle('display', 'none');
            $('product-specs').setStyle('display', 'block');

        });
        $('features-click').addEvent('click', function (event) {

            event = new Event(event).stop();
            $('product-specs').setStyle('display', 'none');
            $('product-features').setStyle('display', 'block');

        });
    }
    // InTheBox / Optional Switcher
    if ($('optional-click')) {
        $('optional-click').addEvent('click', function (event) {

            event = new Event(event).stop();
            $('product-inthebox').setStyle('display', 'none');
            $('product-optional').setStyle('display', 'block');

        });
        $('supplied-click').addEvent('click', function (event) {

            event = new Event(event).stop();
            $('product-optional').setStyle('display', 'none');
            $('product-inthebox').setStyle('display', 'block');

        });
    }

});
