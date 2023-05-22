$(document).ready(() => {
    $(".apartment__content-select").change(function () {
        // Xử lý logic khi sự kiện change xảy ra
        const form = $(this).closest("form");
        const sortValue = $(this).val();
        const currentAction = form.attr("action");
        const updatedAction = currentAction + "?_sort=" + sortValue;
        form.attr("action", updatedAction);

        // Submit form
        form.submit();
    });
});
