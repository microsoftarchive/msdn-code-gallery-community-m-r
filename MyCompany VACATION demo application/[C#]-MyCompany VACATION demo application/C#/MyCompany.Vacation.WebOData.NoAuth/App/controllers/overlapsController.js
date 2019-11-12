vacationApp.controller('OverlapsController', ['$scope', '$q', 'dataService', 'dateUtils', 'enums', 'model', 'dialogService',
    function ($scope, $q, dataService, dateUtils, enums, model, dialogService) {
        $scope.months = dateUtils.getMonths();
        $scope.weekDays = dateUtils.getWeekDays();
        $scope.year = moment().year();
        $scope.currentMonth = moment().month();
        var calendar;

        $scope.selectMonth = function (month) {
            $scope.currentMonth = month.id;
            $.each($scope.months, function (index, month) {
                month.isSelected = month.id == $scope.currentMonth;
            });

            $scope.refresh();
        };

        function setCalendarData(days, vacationList) {
            var vacationHash = {};

            $.each(vacationList, function (vacationIndex, vacationDay) {
                if (vacationDay.status == enums.vacationRequestStatus.denied)
                    return;

                var currentDay = moment.utc(vacationDay.from);
                var toDay = moment.utc(vacationDay.to);

                while (currentDay.isBefore(toDay) || currentDay.isSame(toDay, 'day')) {
                    var day = currentDay.date();

                    if (!vacationHash[day]) {
                        vacationHash[day] = {
                            status: vacationDay.status,
                            vacationRequestId: vacationDay.vacationRequestId
                        };
                    }
                    currentDay.add('days', 1);
                }
            });

            $.each(days, function (dayIndex, day) {
                if (calendar.holidayHash[day.date.month()][day.date.date()]) {
                    day.isHoliday = true;
                }

                if (!day.isHoliday) {
                    if (vacationHash[day.date.date()]) {
                        var vacationDay = vacationHash[day.date.date()];

                        day.vacationRequestId = vacationDay.vacationRequestId;
                        day.isVacation = true;
                        day.isApproved = vacationDay.status == enums.vacationRequestStatus.approved;
                        day.isDenied = vacationDay.status == enums.vacationRequestStatus.denied;
                        day.isPending = vacationDay.status == enums.vacationRequestStatus.pending;
                    }
                }
            });
        };

        $scope.openDetail = function (calendarDay) {
            if (!calendarDay.isVacation || calendarDay.isDenied)
                return;

            var teamVacation = $scope.teamVacations.filter(function (element) {
                return element.employee.employeeId == calendarDay.employeeId;
            })[0];

            var vacationRequest = teamVacation.employee.vacationRequests.filter(function (element) {
                return element.vacationRequestId == calendarDay.vacationRequestId;
            })[0];

            vacationRequest.scope = $scope;

            dialogService.open('App/views/manageVacationRequest.html', 'ManageVacationController', vacationRequest)
                .then(function (response) {
                    if (response) {
                        var vacationRequestDays = teamVacation.days.filter(function (element) {
                            return element.vacationRequestId == calendarDay.vacationRequestId;
                        });
                        if (response.vacationAccepted) {
                            $.each(vacationRequestDays, function (index, day) {
                                day.isApproved = true;
                                day.isPending = false;
                                day.isDenied = false;
                            });
                        }

                        if (response.vacationRejected) {
                            $.each(vacationRequestDays, function (index, day) {
                                day.isVacation = false;
                                day.isApproved = false;
                                day.isPending = false;
                                day.isDenied = false;
                            });
                        }
                    }
                });
        };

        $scope.refresh = function () {
            $scope.$parent.showLoading();

            var parameters = {
                month: $scope.currentMonth + 1,
                pictureType: enums.pictureType.small,
                year: 2013,
                status: 3
            };

            var days = dateUtils.getMonthDays($scope.currentMonth);
            $scope.currentMonthDays = days.map(function (date) {
                return date.date();
            });

            var results = [];
            dataService.getAllTeamVacations(parameters, $scope).then(function (employees) {
                if (employees && employees.length > 0) {
                    $.each(employees, function (index, employee) {
                        var employeeVacationDays = {
                            days: [],
                            employee: {
                                employeeId: employee.employeeId,
                                picture: employee.picture,
                                vacationRequests: employee.vacationRequests
                            }
                        };

                        var calendarDays = days.map(function (date) {
                            return new model.OverlapDay(date, employee.employeeId, $scope.currentMonth);
                        });

                        employeeVacationDays.days = calendarDays;
                        setCalendarData(employeeVacationDays.days, employee.vacationRequests);

                        results.push(employeeVacationDays);
                    });
                }

                $scope.teamVacations = results;
                $scope.$parent.hideLoading();
            });
        };

        dataService.getCalendar($scope).then(function (cal) {
            calendar = cal;
            calendar.holidayHash = {};
            $.each(cal.calendarHolidays, function (index, holiday) {
                var holidayDay = moment.utc(holiday.day);

                if (!calendar.holidayHash[holidayDay.month()])
                    calendar.holidayHash[holidayDay.month()] = {};

                calendar.holidayHash[holidayDay.month()][holidayDay.date()] = true;
            });

            $scope.refresh();
        });
    }]);