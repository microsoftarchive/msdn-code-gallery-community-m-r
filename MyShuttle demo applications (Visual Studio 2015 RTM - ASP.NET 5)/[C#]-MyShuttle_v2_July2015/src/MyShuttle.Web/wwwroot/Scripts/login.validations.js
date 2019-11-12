
$(function () {

    $('form').submit(function () {
        $(this).find('div.form-control-div').each(function () {
            if ($(this).find('span.field-validation-error').length == 0) {
                $(this).removeClass('has-error');
            }
        });

        if (!$(this).valid()) {
            $(this).find('div.form-control-div').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('has-error');
                }
            });
        }
    });

    $('form').each(function () {
        $(this).find('div.form-control-div').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
});

var page = function () {
    //Update that validator
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest(".form-control-div").addClass("has-error");
        },
        unhighlight: function (element) {
            $(element).closest(".form-control-div").removeClass("has-error");
        }
    });
} ();


