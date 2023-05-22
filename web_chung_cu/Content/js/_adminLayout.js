$(document).ready(() => {
    // filter
    const selectInputEl = $(".select__input");
    const selectFormEl = $(".select__form");

    selectInputEl.change((event) => {
        selectFormEl.submit();
    })

    $(".input__password").keyup((event) => {
        $(".input__password-confirm").val(event.target.value);
    })

    // toggle sidebar
    const toggleIconEl = $('.toggle');
    const sidebarEl = $('.sidebar');

    toggleIconEl.click(() => {
        sidebarEl.toggleClass("small");

        if (toggleIconEl.hasClass("bx bx-chevron-left")) {
            toggleIconEl.removeClass("bx bx-chevron-left");
            toggleIconEl.addClass("bx bx-chevron-right");
        } else {
            toggleIconEl.removeClass("bx bx-chevron-right");
            toggleIconEl.addClass("bx bx-chevron-left");
        }
    });

    // active navigate sidebar
    const navLinkEls = $('.menu__item-link');
    const path = window.location.pathname.split("/quan-tri")[1];

    navLinkEls.each((index, navLinkEl) => {
        const navActiveEl = $(navLinkEl).parent();
        const navLinkHref = $(navLinkEl).attr("href").split("/quan-tri")[1];

        if ((index == 0 && !path) || (navLinkHref && path.includes(navLinkHref))) {
            navActiveEl.addClass("active");
        } else {
            navActiveEl.removeClass("active");
        }
    })

    // convert to slug
    const inputConvertToSlugEl = $(".input__convert-to-slug");
    inputConvertToSlugEl.keyup((event) => {
        const value = event.target.value;

        //Đổi chữ hoa thành chữ thường
        let slug = value.toLowerCase();

        //Đổi ký tự có dấu thành không dấu
        slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
        slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
        slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
        slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
        slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
        slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
        slug = slug.replace(/đ/gi, 'd');
        //Xóa các ký tự đặt biệt
        slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
        //Đổi khoảng trắng thành ký tự gạch ngang
        slug = slug.replace(/ /gi, "-");
        //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
        //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
        slug = slug.replace(/\-\-\-\-\-/gi, '-');
        slug = slug.replace(/\-\-\-\-/gi, '-');
        slug = slug.replace(/\-\-\-/gi, '-');
        slug = slug.replace(/\-\-/gi, '-');
        //Xóa các ký tự gạch ngang ở đầu và cuối
        slug = '@' + slug + '@';
        slug = slug.replace(/\@\-|\-\@|\@/gi, '');

        const inputSlugEl = inputConvertToSlugEl.parent().parent().find(".input__slug");
        inputSlugEl.val(slug);
    });

    $(".table__header-search").change(function () {
        // Xử lý logic khi sự kiện change xảy ra
        const form = $(this).closest("form");
        const searchTerm = $(this).val();
        const currentAction = form.attr("action");
        const updatedAction = currentAction + "?searchTerm=" + searchTerm;
        form.attr("action", updatedAction);

        // Submit form
        form.submit();
    });
});