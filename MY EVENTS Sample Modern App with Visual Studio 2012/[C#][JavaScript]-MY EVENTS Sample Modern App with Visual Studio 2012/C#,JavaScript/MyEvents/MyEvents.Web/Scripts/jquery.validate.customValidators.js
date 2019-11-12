(function ($) {
    $.validator.addMethod("dategreaterthan", function (value, element, params) {
        return Date.parse(value) > Date.parse($(params).val());
    });
    
    //$.validator.addMethod("istrue", function (value, element, params) {
    //    return value == true;
    //});


    $.validator.unobtrusive.adapters.add("dategreaterthan", ["pastdate"], function (options) {
        options.rules["dategreaterthan"] = "#" + options.params.pastdate;
        options.messages["dategreaterthan"] = options.message;
    });
    
    //$.validator.unobtrusive.adapters.add("istrue", {}, function (options) {
    //    options.messages["istrue"] = options.message;
    //});
    
} (jQuery));