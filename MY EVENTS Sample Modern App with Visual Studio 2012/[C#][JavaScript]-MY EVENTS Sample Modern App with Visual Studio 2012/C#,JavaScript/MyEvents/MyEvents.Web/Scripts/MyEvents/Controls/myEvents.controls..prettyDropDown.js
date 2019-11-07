(function ($) {
    $.fn.prettyDropDown = function () {
        var self = $(this);
        var SELECT_CLASS = 'custom-select';
        self.wrapAll("<div class='" + SELECT_CLASS + "' />");
        return this;
    };
})(jQuery);
