ko.bindingHandlers.multiline = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // First get the latest data that we're bound to
        var value = valueAccessor(), allBindings = allBindingsAccessor();

        // Next, whether or not the supplied model property is observable, get its current value
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        $(element).html(valueUnwrapped);
        //$(element).hide();

        $(element).dotdotdot({
            after: "a.read-more"
        });
    }
};


ko.bindingHandlers.calendarSelection = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        initializeCalendar(element, valueAccessor, viewModel);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

    }
};

function initializeCalendar(element, valueAccessor, viewModel) {
    var options = {
        selectStartDay: 'selectStartDay',
        selectEndDay: 'selectEndDay'
    },
        $calendar = $(element),
        isSelecting = valueAccessor(),
        selectStartDay = viewModel[options.selectStartDay],
        selectEndDay = viewModel[options.selectEndDay];

    $calendar.on('mousedown', '.day', function (e) {
        e.preventDefault();
        var $this = $(this);
        if (!ko.dataFor($this[0]).isSelectable())
            return;

        isSelecting(true);
        selectStartDay(parseInt($this.attr('id')));

        attachEvents();
    });

    function attachEvents() {
        $(document).on('mouseup', stopSelection);
        $calendar.on('mouseover', '.day', selectDay);
    }

    function deattachEvents() {
        $(document).off('mouseup', stopSelection);
        $(document).off('mouseover', selectDay);
    }

    function selectDay(e) {
        e.preventDefault();
        if (!isSelecting())
            return;

        var $this = $(this);

        var dayIndex = parseInt($this.attr('id'));

        $calendar.removeClass('invalid');
        var isValidSelection = viewModel.validateSelection(dayIndex);
        if (isValidSelection) {
            selectEndDay(dayIndex);
        } else {
            $calendar.addClass('invalid');
        }
    }

    function stopSelection() {
        $calendar.removeClass('invalid');
        deattachEvents();
        if (!isSelecting())
            return;
        viewModel.endSelection();
        isSelecting(false);
    }
}