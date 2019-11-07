
vacationApp.directive('calendar', function () {
    var timeOut;
    return {
        restrict: 'A',
        link: function (scope, elem, attr, ctrl) {
            var deattachEvents = function () {
                $(document).off('mouseup', stopSelection);
                $(document).off('mouseover', selectDay);
            }

            var selectDay = function (e) {
                e.preventDefault();

                if (!scope.isSelecting)
                    return;

                var $this = $(this);
                var dayIndex = parseInt($this.attr('id'));

                elem.removeClass('invalid');
                scope.$apply(function () {
                    var selectionIsValid = scope.validateSelection(dayIndex);
                    if (selectionIsValid) {
                        scope.selectEndDay(dayIndex);
                    } else {
                        elem.addClass('invalid');
                    }
                });
            }

            var stopSelection = function () {
                elem.removeClass('invalid');
                deattachEvents();

                if (!scope.isSelecting)
                    return;

                scope.$apply(function () {
                    scope.endSelection();
                });
            }

            elem.on('mousedown', '.day', function (e) {
                var attachEvents = function () {
                    $(document).on('mouseup', stopSelection);
                    elem.on('mouseover', '.day', selectDay);
                };

                e.preventDefault();
                var dayIndex = parseInt($(this).attr('id'));
                if (!scope.days[dayIndex].isSelectable())
                    return;
                scope.$apply(function () {
                    scope.selectStartDay(dayIndex);
                });
                attachEvents();
            });
        }
    };
});