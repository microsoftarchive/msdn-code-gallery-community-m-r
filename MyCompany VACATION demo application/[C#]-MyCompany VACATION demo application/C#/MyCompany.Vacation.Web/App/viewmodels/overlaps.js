define(['services/dataservice', 'services/enums', 'services/dateUtils', 'services/model', 'durandal/app', 'viewmodels/base'],
    function (dataservice, enums, dateUtils, model, app, base) {
    var months = dateUtils.getMonths(),
        currentMonth = ko.observable(moment().month()),
        currentMonthDays = ko.observableArray(),
        calendar,
        weekDays = dateUtils.getWeekDays(),
        teamVacations = ko.observableArray();

    var vm = {
        activate: activate,
        refresh: refresh,
        selectMonth: selectMonth,
        currentMonthDays: currentMonthDays,
        title: 'Team vacations title',
        months: months,
        weekDays: weekDays,
        teamVacations: teamVacations,
        openDetail: openDetail
    };

    return vm;

    function activate() {
        return dataservice.getCalendar().then(function (cal) {
            calendar = cal;
            calendar.holidayHash = {};
            $.each(cal.calendarHolidays(), function (index, holiday) {
                calendar.holidayHash[moment.utc(holiday.day()).date()] = true;
            });

            return refresh();
        });
    }

    function selectMonth(selectedMonth) {
        currentMonth(selectedMonth.id);
        refresh();
    }

    function _setCalendarData(days, vacationList) {
        var vacationHash = {};

        $.each(vacationList, function (vacationIndex, vacationDay) {
            if (vacationDay.status() == enums.vacationRequestStatus.denied)
                return;

            var currentDay = moment.utc(vacationDay.from());
            var toDay = moment.utc(vacationDay.to());

            while (currentDay.isBefore(toDay) || currentDay.isSame(toDay, 'day')) {
                var day = currentDay.date();

                if (!vacationHash[day]) {
                    vacationHash[day] = {
                        status: vacationDay.status(),
                        vacationRequestId: vacationDay.vacationRequestId()
                    };
                }
                currentDay.add('days', 1);
            }
        });

        $.each(days, function (dayIndex, day) {
            if (calendar.holidayHash[day.date.date()]) {
                day.isHoliday(true);
            }

            if (!day.isHoliday()) {
                if (vacationHash[day.date.date()]) {
                    var vacationDay = vacationHash[day.date.date()];

                    day.vacationRequestId = vacationDay.vacationRequestId;
                    day.isVacation(true);
                    day.isApproved(vacationDay.status == enums.vacationRequestStatus.approved);
                    day.isDenied(vacationDay.status == enums.vacationRequestStatus.denied);
                    day.isPending(vacationDay.status == enums.vacationRequestStatus.pending);
                }
            }
        });
    }

    function refresh() {
        setTimeout(function() {
        base.showLoading();
        
        $.each(months, function (index, month) {
            month.isSelected(month.id == currentMonth());
        });

        var parameters = {
            month: currentMonth() + 1,
            pictureType: enums.pictureType.small,
            year: 2013,
            status: 3
        };

        var calendarDates = dateUtils.getMonthDays(currentMonth());
        var results = [];
        dataservice.getAllTeamVacations(parameters).then(function (employees) {
            if (employees && employees.length > 0) {
                $.each(employees, function (index, employee) {
                    var employeeVacationDays = {
                        days: ko.observableArray(),
                        employee: {
                            employeeId: employee.employeeId(),
                            picture: employee.picture,
                            vacationRequests: employee.vacationRequests()
                        }
                    };

                    var calendarDays = calendarDates.map(function (date) {
                        return new model.OverlapDay(date, employee.employeeId(), currentMonth());
                    });

                    employeeVacationDays.days(calendarDays);
                    _setCalendarData(employeeVacationDays.days(), employee.vacationRequests());

                    results.push(employeeVacationDays);
                });
            }
            teamVacations(results);
        }).then(function () {
            currentMonthDays(calendarDates.map(function (date) {
                return date.date();
            }));
        }).done(function () {
            base.hideLoading();
        });
        },300);

    }
    
    function openDetail(calendarDay) {
        if (!calendarDay.isVacation() || calendarDay.isDenied())
            return;
        
        var teamVacation = teamVacations().filter(function(element) {
            return element.employee.employeeId == calendarDay.employeeId;
        })[0];
        
        var vacationRequest = teamVacation.employee.vacationRequests.filter(function (element) {
            return element.vacationRequestId() == calendarDay.vacationRequestId;
        })[0];

        app.showModal('viewmodels/manageVacationRequest', vacationRequest)
            .then(function (response) {
                if (response) {
                    var vacationRequestDays = teamVacation.days().filter(function (element) {
                        return element.vacationRequestId == calendarDay.vacationRequestId;
                    });
                    if(response.vacationAccepted) {
                        $.each(vacationRequestDays, function(index, day) {
                            day.isApproved(true);
                            day.isPending(false);
                            day.isDenied(false);

                        });
                    }
                    if (response.vacationRejected) {
                        $.each(vacationRequestDays, function (index, day) {
                            day.isVacation(false);
                            day.isApproved(false);
                            day.isPending(false);
                            day.isDenied(false);
                        });
                    }
                }
            });
    }
});