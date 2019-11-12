vacationApp.factory('model', ['config', 'enums',
    function (config, enums) {
        var EmployeeVacationRequest = function (entity) {
            var employeeVacationRequest = this;

            employeeVacationRequest.Employee = new Employee(entity && entity.Employee);
            employeeVacationRequest.VacationRequests = [];

            if (entity && entity.VacationRequests) {
                $.each(entity.VacationRequests, function (index, vacationRequest) {
                    employeeVacationRequest.VacationRequests.push(new TeamVacation(vacationRequest));
                });
            }

            return employeeVacationRequest;
        };

        var TeamVacation = function (entity) {
            var teamVacation = this;

            entity = entity || {};

            teamVacation.vacationRequestId = entity.VacationRequestId;
            teamVacation.from = entity.From;
            teamVacation.to = entity.To;
            teamVacation.numDays = entity.NumDays;
            teamVacation.status = entity.Status && enums.getEnumValue(enums.vacationRequestStatus, entity.Status);
            teamVacation.comments = entity.Comments;
            teamVacation.createdDateTime = entity.CreationDate;
            teamVacation.lastModifiedDateTime = entity.LastModifiedDate;
            teamVacation.employeeId = entity.EmployeeId;
            teamVacation.employee = new Employee(entity.Employee);


            var momentTo = moment(teamVacation.to);
            var momentFrom = moment(teamVacation.from);
            teamVacation.friendlyDatesInterval = momentFrom.format("MMM D") + " - " + momentTo.format("MMM D");

            return teamVacation;
        };


        var Employee = function (entity) {
            var employee = this;

            entity = entity || {};

            employee.employeeId = entity.EmployeeId;
            employee.email = entity.Email;
            employee.firstName = entity.FirstName;
            employee.lastName = entity.LastName;
            employee.jobTitle = entity.JobTitle;
            employee.isManager = entity.IsManager;
            employee.officeId = entity.OfficeId;
            employee.teamId = entity.TeamId;
            employee.vacationRequests = [];

            //employee pictures
            if (entity.EmployeePictures) {
                var employeePictures = entity.EmployeePictures.results || entity.EmployeePictures;

                employee.pictureContent = employeePictures.length > 0 ? employeePictures[0].Content : null;
            }

            //vacation requests
            if (entity.VacationRequests) {
                $.each(entity.VacationRequests, function (index, vacation) {
                    employee.vacationRequests.push(new TeamVacation(vacation));
                });
            }

            employee.fullName = (employee.firstName || '') + ' ' + (employee.lastName || '');
            employee.picture = employee.pictureContent ? 'data:image/jpeg;base64,' + employee.pictureContent : '/Content/Images/no_pending.jpg'

            return employee;
        };

        var Calendar = function (entity) {
            var calendar = this;

            entity = entity || {};

            calendar.calendarId = entity.CalendarId;
            calendar.vacation = entity.Vacation;
            calendar.calendarHolidays = [];

            if (entity.CalendarHolidays && entity.CalendarHolidays.results) {
                $.each(entity.CalendarHolidays.results, function (index, holiday) {
                    calendar.calendarHolidays.push(new CalendarHoliday(holiday));
                });
            }

            return calendar;
        };

        var CalendarHoliday = function (entity) {
            var calendarHoliday = this;

            entity = entity || {};

            calendarHoliday.calendarHolidaysId = entity.CalendarHolidaysId;
            calendarHoliday.name = entity.Name;
            calendarHoliday.day = entity.Day;

            return calendarHoliday;
        };

        var CalendarDay = function (date, month) {
            var calendarDay = this;

            calendarDay.vacationRequestId = null;
            calendarDay.date = moment(date);
            calendarDay.month = date.month();
            calendarDay.day = date.format('DD');
            calendarDay.year = date.year();
            calendarDay.text = '';
            calendarDay.isSelected = false;
            calendarDay.isOutOfMonth = month != date.month();
            calendarDay.isToday = moment().isSame(date, 'day');
            calendarDay.isHoliday = date.day() == 0 || date.day() == 6;
            calendarDay.isVacation = false;
            calendarDay.isApproved = false;
            calendarDay.isPending = false;
            calendarDay.isDenied = false;

            calendarDay.isWorkDay = function () {
                return !calendarDay.isHoliday;
            };

            var now = moment();
            var isAfterToday = calendarDay.date.isAfter(now.subtract('days', 1));
            var isCurrentYear = calendarDay.date.year() == now.year();

            calendarDay.isSelectable = function () {
                return !calendarDay.isVacation && isAfterToday && isCurrentYear;
            };

            calendarDay.status = function () {
                if (calendarDay.isHoliday)
                    return calendarDay.text;
                if (calendarDay.isApproved)
                    return 'Approved';
                if (calendarDay.isDenied)
                    return 'Denied';
                if (calendarDay.isPending)
                    return 'Pending';

                return '';
            };

            return calendarDay;
        };

        var OverlapDay = function (date, employeeId) {
            var calendarDay = this;

            calendarDay.employeeId = employeeId;
            calendarDay.date = moment(date);
            calendarDay.month = date.month();
            calendarDay.isHoliday = date.day() == 0 || date.day() == 6;
            calendarDay.isVacation = false;
            calendarDay.isApproved = false;
            calendarDay.isPending = false;
            calendarDay.isDenied = false;

            calendarDay.status = function () {
                if (calendarDay.isHoliday)
                    return calendarDay.text();
                if (calendarDay.isApproved)
                    return 'Approved';
                if (calendarDay.isDenied)
                    return 'Denied';
                if (calendarDay.isPending)
                    return 'Pending';

                return '';
            };

            return calendarDay;
        };

        var model = {
            TeamVacation: TeamVacation,
            Employee: Employee,
            CalendarHoliday: CalendarHoliday,
            Calendar: Calendar,
            CalendarDay: CalendarDay,
            OverlapDay: OverlapDay,
            EmployeeVacationRequest: EmployeeVacationRequest
        };

        return model;
    }]);