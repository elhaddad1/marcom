function validateentities() {

    var emptyfields = $("input[value=]");
    if (emptyfields.size() > 0) {
        emptyfields.each(function () {
            $(this).stop()
.animate({ left: "-10px" }, 100).animate({ left: "10px" }, 100)
.animate({ left: "-10px" }, 100).animate({ left: "10px" }, 100)
.animate({ left: "0px" }, 100)
.addClass("required");
        });

    }

}



