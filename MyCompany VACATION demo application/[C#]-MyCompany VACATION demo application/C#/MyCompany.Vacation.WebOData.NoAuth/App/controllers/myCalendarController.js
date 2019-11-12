vacationApp.controller('MyCalendarController', ['$scope', '$q', 'dataService', 'model', 'enums', 'dateUtils', 'dialogService',
    function ($scope, $q, dataService, model, enums, dateUtils, dialogService) {
        $scope.months = dateUtils.getMonths();
        $scope.weekDays = dateUtils.getWeekDays();
        $scope.year = moment().year();
        $scope.currentMonth = moment().month();
        $scope.calendar = null;
        $scope.vacations = [];
        $scope.days = [];
        $scope.userVacationDays = [];
        $scope.pendingVacationDays = [];
        $scope.isSelecting = false;

        var pendingDays = 0;

        var startDayIndex,
            endDayIndex;

        $scope.selectMonth = function (month) {
            $scope.currentMonth = month.id;
            $.each($scope.months, function (index, month) {
                month.isSelected = month.id == $scope.currentMonth;
            });
            $scope.refresh();
        }

        $scope.selectStartDay = function (dayIndex) {
            var day = $scope.days[dayIndex];
            $scope.isSelecting = true;
            if (pendingDays <= 0 || day.isHoliday) {
                $scope.isSelecting = false
                return;
            }

            resetSelection();
            day.isSelected = true;
            startDayIndex = dayIndex;
        }

        $scope.selectEndDay = function (dayIndex) {
            cleanSelection();
            endDayIndex = dayIndex;
            selectChosenPeriod();
        }

        $scope.validateSelection = function (dayIndex) {
            if (!dayIndex) {
                dayIndex = startDayIndex;
            }

            var day = $scope.days[dayIndex];
            var init = getPositiveMin(startDayIndex, dayIndex);
            var end = getPositiveMax(startDayIndex, dayIndex);

            var isDayValid = day.isSelectable(),
                isPeriodValid = true,
                isNumberOfDaysAllowed = true;

            if (!isDayValid) {
                invalidateSelection();
                return false;
            }

            for (var i = init; i <= end; i++) {
                if (!$scope.days[i].isSelectable()) {
                    isPeriodValid = false;
                }
            }

            if (!isPeriodValid) {
                invalidateSelection();
                return false;
            }

            var selectionWorkDays = getSelectionWorkDays(init, end);

            isNumberOfDaysAllowed = ((pendingDays - selectionWorkDays) >= 0);

            if (!isNumberOfDaysAllowed) {
                invalidateSelection();
                return false;
            }

            return true;
        }

        $scope.endSelection = function () {
            console.log('end selection');
            if (!$scope.isSelecting)
                return;

            $scope.isSelecting = false;

            if (!endDayIndex)
                endDayIndex = startDayIndex;

            var init = startDayIndex;
            var end = endDayIndex;
            startDayIndex = getPositiveMin(init, end);
            endDayIndex = getPositiveMax(init, end);


            var workDays = getSelectionWorkDays(startDayIndex, endDayIndex);

            if (workDays == 0) {
                cleanSelection();
                return;
            }

            var vacationRequest = {
                vacationRequestId: null,
                startDay: $scope.days[startDayIndex],
                endDay: $scope.days[endDayIndex],
                workDays: workDays,
                scope: $scope
            };

            dialogService.open('App/views/addVacationRequest.html', 'AddVacationRequestController', vacationRequest)
                .then(function (result) {
                    if (result && result.updateVacations) {
                        vacationRequest.vacationRequestId = result.vacationRequestId;
                        addSelectedVacations(vacationRequest);
                    } else {
                        cleanSelection();
                    }
                });
        }

        $scope.initialize = function () {
            $scope.$parent.showLoading();
            $q.all([
                dataService.getCalendar($scope),
                 dataService.getUserVacations({ year: $scope.year }, $scope)
            ]).then(function (results) {
                $scope.calendar = results[0]
                $scope.vacations.replaceValues(results[1]);
                setPendingDays();
                setPendingDaysCollection();
                $scope.refresh();
                $scope.$parent.hideLoading();
            });
        }

        $scope.refresh = function () {
            loadDays();
            loadDaysData($scope.calendar.calendarHolidays, $scope.vacations)
        };

        $scope.openDetail = function (day) {
            if (!day.isVacation)
                return;

            var vacationRequestDays = getVacationRequestDays(day.vacationRequestId);
            var workDays = 0;

            $.each(vacationRequestDays, function (index, day) {
                if (day.isWorkDay())
                    workDays++;
            });

            var vacationRequest = {
                vacationRequestId: day.vacationRequestId,
                startDay: vacationRequestDays[0],
                endDay: vacationRequestDays[vacationRequestDays.length - 1],
                workDays: workDays,
                scope: $scope
            };

            dialogService.open('App/views/vacationRequestDetail.html', 'VacationRequestDetailController', vacationRequest)
            .then(function (result) {
                if (result && result.updateVacations) {
                    deleteVacationRequest(result.vacationRequestId);
                }
            });
        };

        var loadDays = function () {
            var calendarDays = dateUtils.getMonthCalendarDays($scope.currentMonth).map(function (date) {
                return new model.CalendarDay(date, $scope.currentMonth);
            });
            $scope.days.replaceValues(calendarDays);
        }

        var loadDaysData = function (calendarHolidays, vacations) {
            var vacationDays = [];

            $.each(vacations, function (vacationIndex, vacationDay) {
                var currentDay = moment.utc(vacationDay.from);
                var toDay = moment.utc(vacationDay.to);

                while (currentDay.isBefore(toDay) || currentDay.isSame(toDay, 'day')) {
                    vacationDays.push({
                        vacationRequestId: vacationDay.vacationRequestId,
                        date: moment(currentDay),
                        displayDate: moment(currentDay).format('MMMM D'),
                        month: currentDay.month(),
                        day: currentDay.date(),
                        year: currentDay.year(),
                        status: vacationDay.status
                    });
                    currentDay.add('days', 1);
                }
            });

            var userVacations = [];
            $.each(vacationDays, function (dayIndex, day) {
                var dayHolidays = calendarHolidays.filter(function (data) {
                    var date = moment.utc(data.day);
                    return date.isSame(day.date, 'day');
                });

                day.isPending = day.status == enums.vacationRequestStatus.pending;

                if (!dayHolidays.length && day.status != enums.vacationRequestStatus.denied && !(day.date.day() == 0 || day.date.day() == 6)) {
                    userVacations.push(day);
                }
            });

            $scope.userVacationDays.replaceValues(userVacations);
            sortUserVacationDays();

            $.each($scope.days, function (dayIndex, day) {
                var dayHolidays = calendarHolidays.filter(function (data) {
                    var date = moment.utc(data.day);
                    return date.isSame(day.date, 'day');
                });

                if (dayHolidays.length) {
                    day.text = dayHolidays[0].name;
                    day.isHoliday = true;
                }

                if (!day.isHoliday) {
                    var dayVacations = vacationDays.filter(function (data) {
                        return data.date.isSame(day.date, 'day');
                    });

                    if (dayVacations.length > 0) {
                        day.vacationRequestId = dayVacations[0].vacationRequestId;
                        day.isVacation = true;
                        day.isApproved = dayVacations[0].status == enums.vacationRequestStatus.approved;
                        day.isDenied = dayVacations[0].status == enums.vacationRequestStatus.denied;
                        day.isPending = dayVacations[0].status == enums.vacationRequestStatus.pending;
                    }
                }
            });
        }

        function getVacationRequestDays(vacationRequestId) {
            var vacationRequestDays = $scope.days.filter(function (v) {
                return v.vacationRequestId == vacationRequestId;
            });

            return vacationRequestDays;
        }

        var resetSelection = function () {
            cleanSelection();
            startDayIndex = null;
            endDayIndex = null;
        }

        var cleanSelection = function () {
            $scope.days.forEach(function (day) {
                if (day.isSelected)
                    day.isSelected = false;
            });
        }

        var invalidateSelection = function () {
            cleanSelection();
            selectChosenPeriod();
        };

        var selectChosenPeriod = function () {
            var init = getPositiveMin(startDayIndex, endDayIndex);
            var end = getPositiveMax(startDayIndex, endDayIndex);

            for (var j = init; j <= end; j++) {
                $scope.days[j].isSelected = true;
            }
        };

        var getPositiveMax = function (val1, val2) {
            var max = Math.max(val1, val2);
            return max;
        }

        var getPositiveMin = function (val1, val2) {
            var min = Math.min(val1, val2);
            if (min < 0)
                min = Math.max(val1, val2);
            return min;
        }



        var getSelectionWorkDays = function (init, end) {
            var workDays = 0;

            for (var i = init; i <= end; i++) {
                if ($scope.days[i].isWorkDay())
                    workDays++;
            }
            return workDays;
        };

        var addSelectedVacations = function (vacationRequest) {
            var workingDays = vacationRequest.workDays;

            $scope.vacations.push({
                vacationRequestId: vacationRequest.vacationRequestId,
                from: vacationRequest.startDay.date.toDate(),
                to: vacationRequest.endDay.date.toDate(),
                numDays: vacationRequest.workDays,
                status: enums.vacationRequestStatus.pending
            });

            for (var i = startDayIndex ; i <= endDayIndex ; i++) {
                var day = $scope.days[i];

                day.vacationRequestId = vacationRequest.vacationRequestId;
                day.isSelected = false;

                if (!day.isHoliday) {
                    day.isVacation = true;
                    day.isPending = true;
                    day.isApproved = false;
                    day.isDenied = false;
                        
                    day.status = function () { return 'Pending' };

                    $scope.userVacationDays.push({
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
            pendingDays = pendingDays - workingDays;
            setPendingDaysCollection();
        }

        var deleteVacationRequest = function (vacationRequestId) {
            var requestDays = $scope.days.filter(function (element) {
                return element.vacationRequestId == vacationRequestId;
            });
            var vacationWorkingDays = 0;
            $.each(requestDays, function (index, day) {
                if (day.isWorkDay()) {
                    vacationWorkingDays++;
                }

                day.isVacation = false;
                day.isPending = false;
                day.isApproved = false;
                day.isDenied = false;
                day.status = function () { return ''; };
            });

            var vacationRequest = $scope.vacations.filter(function (element) {
                return element.vacationRequestId == vacationRequestId;
            })[0];

            $scope.vacations.splice($scope.vacations.indexOf(vacationRequest), 1);

            $scope.userVacationDays.remove(function (element) {
                return element.vacationRequestId == vacationRequestId;
            });

            if (vacationRequest.status != enums.vacationRequestStatus.denied) {
                pendingDays = pendingDays + vacationWorkingDays;
                setPendingDaysCollection();
            }
        }

        var sortUserVacationDays = function () {
            $scope.userVacationDays.sort(function (l, r) {
                return r.date.isSame(l.date) ? 0 : r.date.isBefore(l.date) ? 1 : -1;
            });
        }

        var setPendingDays = function () {
            var vacationsDays = 0;
            $.each($scope.vacations, function (index, vacationRequest) {
                if (vacationRequest.status != enums.vacationRequestStatus.denied)
                    vacationsDays += vacationRequest.numDays;
            });
            pendingDays = $scope.calendar.vacation - vacationsDays;
        }

        var setPendingDaysCollection = function () {
            var pending = [];
            for (var i = 0; i < pendingDays ; i++) {
                pending.push((i + 1));
            }
            $scope.pendingVacationDays.replaceValues(pending);
        }


        $scope.initialize();
    }]);
