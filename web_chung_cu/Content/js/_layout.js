$(document).ready(() => {
    // active navigate menu
    const navLinkEls = $('.menu__item-link');
    const path = window.location.pathname.split("/")[1];

    navLinkEls.each((index, navLinkEl) => {
        const navActiveEl = $(navLinkEl).parent();
        const navLinkHref = $(navLinkEl).attr("href").split("/")[1];

        if ((index == 0 && !path) || (navLinkHref && path.includes(navLinkHref))) {
            navActiveEl.addClass("active");
        } else {
            navActiveEl.removeClass("active");
        }
    })

    // back top
    $(window).scroll(function () {
        if ($(this).scrollTop()) {
            $("#back-top").fadeIn();
            $("#back-top").css("display", "flex");
        } else {
            $("#back-top").fadeOut();
        }
    });

    $("#back-top").click(function () {
        $('html, body').animate({ scrollTop: 0 }, 500);
    });

    // carousel
    const wrappers = document.querySelectorAll(".carousel-multiple-wrapper");
    wrappers.forEach(wrapper => {
        const carousel = wrapper.querySelector(".carousel-multiple");
        const firstCardWidth = carousel.querySelector(".carousel-multiple-item").offsetWidth;
        const arrowBtns = wrapper.querySelectorAll(".carousel-multiple-control");
        const carouselChildrens = [...carousel.children];
        let isAutoPlay = true, timeoutId;

        if (carousel.getAttribute("data-slide-number")) {
            carousel.style.gridAutoColumns = `calc((100% / ${carousel.getAttribute("data-slide-number")}) - 12px)`;
        }

        // Get the number of cards that can fit in the carousel at once
        let cardPerView = Math.round(carousel.offsetWidth / firstCardWidth);
        // Insert copies of the last few cards to beginning of carousel for infinite scrolling
        carouselChildrens.slice(-cardPerView).reverse().forEach(card => {
            carousel.insertAdjacentHTML("afterbegin", card.outerHTML);
        });
        // Insert copies of the first few cards to end of carousel for infinite scrolling
        carouselChildrens.slice(0, cardPerView).forEach(card => {
            carousel.insertAdjacentHTML("beforeend", card.outerHTML);
        });
        // Scroll the carousel at appropriate postition to hide first few duplicate cards on Firefox
        carousel.classList.add("no-transition");
        carousel.scrollLeft = carousel.offsetWidth;
        carousel.classList.remove("no-transition");
        // Add event listeners for the arrow buttons to scroll the carousel left and right
        arrowBtns.forEach(btn => {
            btn.addEventListener("click", () => {
                carousel.scrollLeft += btn.id == "carousel-multiple-control-prev" ? -firstCardWidth : firstCardWidth;
            });
        });

        const infiniteScroll = () => {
            // If the carousel is at the beginning, scroll to the end
            if (carousel.scrollLeft === 0) {
                carousel.classList.add("no-transition");
                carousel.scrollLeft = carousel.scrollWidth - (2 * carousel.offsetWidth);
                carousel.classList.remove("no-transition");
            }
            // If the carousel is at the end, scroll to the beginning
            else if (Math.ceil(carousel.scrollLeft) === carousel.scrollWidth - carousel.offsetWidth) {
                carousel.classList.add("no-transition");
                carousel.scrollLeft = carousel.offsetWidth;
                carousel.classList.remove("no-transition");
            }
            // Clear existing timeout & start autoplay if mouse is not hovering over carousel
            clearTimeout(timeoutId);
            if (!wrapper.matches(":hover")) autoPlay();
        }
        const autoPlay = () => {
            if (window.innerWidth < 800 || !isAutoPlay) return; // Return if window is smaller than 800 or isAutoPlay is false
            // Autoplay the carous
            timeoutId = setTimeout(() => carousel.scrollLeft += firstCardWidth, 2500);
        }
        autoPlay();

        carousel.addEventListener("scroll", infiniteScroll);
        wrapper.addEventListener("mouseenter", () => clearTimeout(timeoutId));
        wrapper.addEventListener("mouseleave", autoPlay);
    });
});