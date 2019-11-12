define(['services/dataservice', 'services/model', 'services/enums', 'services/dateUtils', 'durandal/app'], function (dataservice, model, enums, dateUtils, app) {
    var months = dateUtils.getMonths(),
        year = moment().year(),
        currentMont = ko.observable(moment().month()),
        calendar,
        vacations,
        weekDays = dateUtils.getWeekDays(),
        days = ko.observableArray(),
        isSelecting = ko.observable(false),
        startDay = ko.observable(),
        endDay = ko.observable(),
        pendingDays = ko.observable(0),
        userVacationDays = ko.observableArray(),
        pendingVacationDays = ko.observableArray();

    var vm = {
        activate: activate,
        refresh: refresh,
        selectMonth: selectMonth,
        title: 'Team vacations overlaps title',
        year: year,
        months: months,
        weekDays: weekDays,
        days: days,
        pendingDays: pendingDays,
        userVacationDays: userVacationDays,
        pendingVacationDays: pendingVacationDays,
        isSelecting: isSelecting,
        selectStartDay: selectStartDay,
        selectEndDay: selectEndDay,
        resetSelection: resetSelection,
        validateSelection: validateSelection,
        endSelection: endSelection,
        openDetail: openDetail
    };

    return vm;

    function validateSelection(dayIndex) {
        console.log('INDEX:' + dayIndex);
        if (!endDay())
            endDay(startDay());

        var allDays = days(),
            day = allDays[dayIndex],
            isDayValid = day.isSelectable(),
            isPeriodValid = true,
            isNumberOfDaysAllowed = true,
            init = Math.min(startDay(), dayIndex),
            end = Math.max(startDay(), dayIndex);

        if (!isDayValid) {
            invalidateSelection();
            return false;
        }

        for (var i = init; i <= end; i++) {
            if (!allDays[i].isSelectable()) {
                isPeriodValid = false;
            }
        }

        if (!isPeriodValid) {
            console.log('INVALID PERIOD');
            invalidateSelection();
            return false;
        }

        var selectionWorkDays = _getSelectionWorkDays(init, end);
        isNumberOfDaysAllowed = ((pendingDays() - selectionWorkDays) >= 0);

        if (!isNumberOfDaysAllowed) {
            invalidateSelection();
            return false;
        }

        return true;
    }

    function invalidateSelection() {
        cleanSelection();
        selectSelectedPeriod();
    }

    function selectStartDay(dayIndex) {
        if (pendingDays() <= 0 || days()[dayIndex].isHoliday()) {
            isSelecting(false);
            return;
        }
        resetSelection();
        days()[dayIndex].isSelected(true);
        startDay(dayIndex);
    }

    function selectEndDay(dayIndex) {
        cleanSelection();
        endDay(dayIndex);
        selectSelectedPeriod();
    }

    function selectSelectedPeriod() {
        var init = Math.min(startDay(), endDay());
        var end = Math.max(startDay(), endDay());

        var allDays = days();
        for (var j = init; j <= end; j++) {
            allDays[j].isSelected(true);
        }
    }

    function cleanSelection() {
        var selectedDays = days().filter(function (day) {
            return day.isSelected();
        });

        for (var i = 0; i < selectedDays.length; i++) {
            console.log('deselected');
            selectedDays[i].isSelected(false);
        }
    }

    function resetSelection() {
        cleanSelection();
        startDay(null);
        endDay(null);
    }

    function endSelection() {
        if (!isSelecting())
            return;

        if (!endDay())
            endDay(startDay());

        if (endDay() < startDay()) {
            var end = endDay();
            var start = startDay();
            endDay(start);
            startDay(end);
        }

        var workDays = _getSelectionWorkDays(startDay(), endDay());

        if (workDays == 0) {
            cleanSelection();
            return;
        }

        var vacationRequest = {
            vacationRequestId: null,
            startDay: days()[startDay()],
            endDay: days()[endDay()],
            workDays: workDays
        };
        app.showModal('viewmodels/addVacationRequest', vacationRequest)
            .then(function (response) {
                if (response && response.updateVacations) {
                    vacationRequest.vacationRequestId = response.vacationRequestId;
                    addSelectedVacations(vacationRequest);
                } else {
                    cleanSelection();
                }
            });
    }

    function addSelectedVacations(vacationRequest) {
        var allDays = days(),
            workingDays = vacationRequest.workDays;
        
        vacations.push({
            vacationRequestId: ko.observable(vacationRequest.vacationRequestId),
            from: ko.observable(vacationRequest.startDay.date.toDate()),
            to: ko.observable(vacationRequest.endDay.date.toDate()),
            numDays: ko.observable(vacationRequest.workDays),
            status: ko.observable(enums.vacationRequestStatus.pending)
        });

        for (var i = startDay() ; i <= endDay() ; i++) {
            var day = allDays[i];
            day.vacationRequestId(vacationRequest.vacationRequestId);
            day.isSelected(false);

            if (!day.isHoliday()) {
                day.vacationRequestId(vacationRequest.vacationRequestId);
                day.isVacation(true);
                day.isPending(true);
                day.isApproved(false);
                day.isDenied(false);
                day.status('Pending');

                userVacationDays.push({
                    vacationRequestId: vacationRequest.vacationRequestId,
                    date: moment(day.date),
                    displayDate: moment(day.date).format('MMMM D'),
                    month: moment(day.date).month,
                    day: moment(day.date).date,
                    year: moment(day.date).year,
                    status: enums.vacationRequestStatus.pending,
                    isPending: true
                });
            }
        }

        sortUserVacationDays();
        pendingDays(pendingDays() - workingDays);
        _setPendingDaysCollection();
    }

    function openDetail(calendarDay) {
        if (!calendarDay.isVacation())
            return;


        var vacationRequestDays = getVacationRequestDays(calendarDay.vacationRequestId());
        var workDays = 0;

        $.each(vacationRequestDays, function (index, day) {
            if (day.isWorkDay())
                workDays++;
        });

        var vacationRequest = {
            vacationRequestId: calendarDay.vacationRequestId(),
            startDay: vacationRequestDays[0],
            endDay: vacationRequestDays[vacationRequestDays.length - 1],
            workDays: workDays
        };

        app.showModal('viewmodels/vacationRequestDetail', vacationRequest)
            .then(function (response) {
                if(response && response.updateVacations) {
                    deleteVacationRequest(response.vacationRequestId);
                }
            });
    }

    function getVacationRequestDays(vacationRequestId) {
        var vacationRequestDays = days().filter(function (v) {
            return v.vacationRequestId() == vacationRequestId;
        });

        return vacationRequestDays;
    }

    function deleteVacationRequest(vacationRequestId) {
        var requestDays = days().filter(function (element) {
            return element.vacationRequestId() == vacationRequestId;
        });
        var vacationWorkingDays = 0;
        $.each(requestDays, function (index, day) {
            if (day.isWorkDay()) {
                vacationWorkingDays++;
            }

            day.isVacation(false);
            day.isPending(false);
            day.isApproved(false);
            day.isDenied(false);
        });

        var vacationRequest = vacations.filter(function(element) {
            return element.vacationRequestId() == vacationRequestId;
        })[0];

        vacations.splice(vacations.indexOf(vacationRequest), 1);

        userVacationDays.remove(function (element) {
            return element.vacationRequestId == vacationRequestId;
        });
        
        if (vacationRequest.status() != enums.vacationRequestStatus.denied) {
            pendingDays(pendingDays() + vacationWorkingDays);
            _setPendingDaysCollection();
        }
    }

    function activate() {
        return $.when(dataservice.getCalendar(),
                     dataservice.getUserVacations({ year: moment().year() }))
                .done(function (cal, vac) {
                    calendar = cal;
                    vacations = vac;
                    _setPendingDays();
                    _setPendingDaysCollection();
                    refresh();
                });
    }

    function selectMonth(selectedMonth) {
        currentMont(selectedMonth.id);
        $.each(months, function (index, month) {
            month.isSelected(month.id == selectedMonth.id);
        });
        refresh();
    }

    function sortUserVacationDays() {
        userVacationDays.sort(function (l, r) {
            return r.date.isSame(l.date) ? 0 : r.date.isBefore(l.date) ? 1 : -1;
        });
    }

    function refreshDays() {
        var calendarDays = dateUtils.getMonthCalendarDays(currentMont()).map(function (date) {
            return new model.CalendarDay(date, currentMont());
        });
        days(calendarDays);
    }

    function refresh() {
        refreshDays();
        _setCalendarData(calendar.calendarHolidays(), vacations);
    }

    function _setCalendarData(calendarHolidays, vacationList) {
        var vacationDays = [];
        $.each(vacationList, function (vacationIndex, vacationDay) {
            var currentDay = moment.utc(vacationDay.from());
            var toDay = moment.utc(vacationDay.to());

            while (currentDay.isBefore(toDay) || currentDay.isSame(toDay, 'day')) {
                vacationDays.push({
                    vacationRequestId: vacationDay.vacationRequestId(),
                    date: moment(currentDay),
                    displayDate: moment(currentDay).format('MMMM D'),
                    month: currentDay.month(),
                    day: currentDay.date(),
                    year: currentDay.year(),
                    status: vacationDay.status()
                });
                currentDay.add('days', 1);
            }
        });

        var userVacations = [];
        $.each(vacationDays, function (dayIndex, day) {
            var dayHolidays = calendarHolidays.filter(function (data) {
                var date = moment.utc(data.day());
                return date.isSame(day.date, 'day');
            });

            day.isPending = day.status == enums.vacationRequestStatus.pending;

            if (!dayHolidays.length && day.status != enums.vacationRequestStatus.denied && !(day.date.day() == 0 || day.date.day() == 6)) {
                userVacations.push(day);
            }
        });

        userVacationDays(userVacations);
        sortUserVacationDays();

        $.each(days(), function (dayIndex, day) {
            var dayHolidays = calendarHolidays.filter(function (data) {
                var date = moment.utc(data.day());
                return date.isSame(day.date, 'day');
            });

            if (dayHolidays.length) {
                day.text(dayHolidays[0].name());
                day.isHoliday(true);
            }

            if (!day.isHoliday()) {
                var dayVacations = vacationDays.filter(function (data) {
                    return data.date.isSame(day.date, 'day');
                });

                if (dayVacations.length > 0) {
                    day.vacationRequestId(dayVacations[0].vacationRequestId);
                    day.isVacation(true);
                    day.isApproved(dayVacations[0].status == enums.vacationRequestStatus.approved);
                    day.isDenied(dayVacations[0].status == enums.vacationRequestStatus.denied);
                    day.isPending(dayVacations[0].status == enums.vacationRequestStatus.pending);
                }
            }
        });


    }

    function _getSelectionWorkDays(init, end) {
        var workDays = 0,
            allDays = days();

        for (var i = init; i <= end; i++) {
            if (allDays[i].isWorkDay())
                workDays++;
        }
        return workDays;
    }

    function _setPendingDays() {
        var vacationsDays = 0;
        $.each(vacations, function (index, vacationRequest) {
            if (vacationRequest.status() != enums.vacationRequestStatus.denied)
                vacationsDays += vacationRequest.numDays();
        });
        pendingDays(calendar.vacation() - vacationsDays);
    }

    function _setPendingDaysCollection() {
        var pending = [];
        for (var i = 0; i < pendingDays() ; i++) {
            pending.push((i + 1));
        }
        pendingVacationDays(pending);
    }
});

