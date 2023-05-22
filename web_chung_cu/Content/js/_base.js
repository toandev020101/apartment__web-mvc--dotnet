$(document).ready(() => {
    // show hide password
    const inputIconPasswordEls = $('.input__icon-password');

    inputIconPasswordEls.each((index, inputIconPasswordEl) => {
        const inputPasswordEl = $(inputIconPasswordEl).parent().find('.input__password');

        $(inputIconPasswordEl).click(() => {
            const passwordType = inputPasswordEl.attr('type') === 'text' ? 'password' : 'text';
            inputPasswordEl.attr('type', passwordType);

            let iconClass = passwordType === 'text' ? 'bx bx-hide' : 'bx bx-show-alt';
            iconClass += " input__icon input__icon-password";
            $(inputIconPasswordEl).attr('class', iconClass);
        });
    });

    // fix icon show hide
    const inputInValidEl = $('.input__password.form-control.is-invalid');
    if (inputInValidEl) {
        const inputIconPasswordEl = $(inputInValidEl).parent().find('.input__icon-password');
        inputIconPasswordEl.css('top', '15%');
        inputIconPasswordEl.css('transform', 'translateY(0)');
    }

    // show tooltip
    const tooltipTriggerList = $('[data-bs-toggle="tooltip"]')
    tooltipTriggerList.each((index, tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl))
});