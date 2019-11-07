ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker('hide');

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "changeDate", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                value(event.date);
            }
        });
    },
    update: function (element, valueAccessor) {
        var widget = $(element).data("datepicker");
        //when the view model is updated, update the widget
        if (widget) {
            widget.date = moment(ko.utils.unwrapObservable(valueAccessor()));
            if (widget.date) {
                widget.setValue(widget.date);
            }
        }
    }
};

ko.bindingHandlers.dateString = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor(),
            allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        var pattern = allBindings.datePattern || 'MM/DD/YYYY';
        $(element).text(moment.utc(valueUnwrapped).local().format(pattern));
    }
};

ko.bindingHandlers.uploadFile = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var binding = valueAccessor();

        element.addEventListener("change", updateFile, false);
        updateFile();

        function updateFile() {
            var file = element.files[0];
            if (file) {
                //fileReader.readAsBinaryFile(file);
                binding.file(file);
            } else {
                binding.file(null);
            }
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    }
};

ko.bindingHandlers['class'] = {
    'update': function (element, valueAccessor) {
        if (element['__ko__previousClassValue__']) {
            ko.utils.toggleDomNodeCssClass(element, element['__ko__previousClassValue__'], false);
        }
        var value = ko.utils.unwrapObservable(valueAccessor());
        ko.utils.toggleDomNodeCssClass(element, value, true);
        element['__ko__previousClassValue__'] = value;
    }
};
