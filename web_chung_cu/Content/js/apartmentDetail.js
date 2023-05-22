$(document).ready(() => {
    $(".carousel-control-prev").click(function () {
        $(".apartmentDetail__image-item").removeClass("active");

        let numverVal = parseInt($(".apartmentDetail__carousel-number").text());
        if (numverVal > 1) {
            numverVal--;
        } else {
            numverVal = $(".apartmentDetail__image-item").length;
        }
        $(".apartmentDetail__carousel-number").text(numverVal);


        $(".apartmentDetail__image-item").each((index, imageItem) => {
            if (index == numverVal - 1) {
                $(imageItem).addClass("active");
            }
        });
    });

    $(".carousel-control-next").click(function () {
        $(".apartmentDetail__image-item").removeClass("active");

        let numverVal = parseInt($(".apartmentDetail__carousel-number").text());
        if (numverVal == $(".apartmentDetail__image-item").length) {
            numverVal = 1;
        } else {
            numverVal++;
        }
        $(".apartmentDetail__carousel-number").text(numverVal);


        $(".apartmentDetail__image-item").each((index, imageItem) => {
            if (index == numverVal - 1) {
                $(imageItem).addClass("active");
            }
        });
    });

    $(".apartmentDetail__image-item").click(function () {
        const clickedIndex = $(".apartmentDetail__image-item").index(this);
        $(".apartmentDetail__carousel-number").text(clickedIndex + 1);

        $(".apartmentDetail__image-item").removeClass("active");
        $(this).addClass("active");

        $(".apartmentDetail__carousel-item").removeClass("active");
        $(".apartmentDetail__carousel-item").eq(clickedIndex).addClass("active");
    });
});
