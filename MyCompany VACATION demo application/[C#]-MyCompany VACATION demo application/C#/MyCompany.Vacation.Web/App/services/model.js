define(['config'], function (config) {

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

        teamVacation.vacationRequestId = entity && entity.VacationRequestId;
        teamVacation.from = entity && entity.From;
        teamVacation.to = entity && entity.To;
        teamVacation.numDays = entity && entity.NumDays;
        teamVacation.status = entity && entity.Status;
        teamVacation.comments = entity && entity.Comments;
        teamVacation.createdDateTime = entity && entity.CreationDate;
        teamVacation.lastModifiedDateTime = entity && entity.LastModifiedDate;
        teamVacation.employeeId = entity && entity.EmployeeId;
        teamVacation.employee = new Employee(entity && entity.Employee);
        
        teamVacation = ko.mapping.fromJS(teamVacation);
        teamVacation = addTeamVacationComputeds(teamVacation);
      
        return teamVacation;
    };

   

    var Employee = function (entity) {
        var employee = this;

        employee.employeeId = entity && entity.EmployeeId;
        employee.firstName = entity && entity.FirstName;
        employee.lastName = entity && entity.LastName;
        employee.jobTitle = entity && entity.JobTitle;
        employee.pictureContent = entity && entity.EmployeePictures && entity.EmployeePictures.length > 0 ? entity.EmployeePictures[0].Content : null;
        employee.isManager = entity && entity.IsManager;
        employee.vacationRequests = [];

        if (entity && entity.VacationRequests) {
            $.each(entity.VacationRequests, function (index, vacation) {
                employee.vacationRequests.push(new TeamVacation(vacation));
            });
        }

        return addEmployeeComputeds(ko.mapping.fromJS(employee));
    };

    var Calendar = function (entity) {
        var calendar = this;

        calendar.calendarId = entity && entity.CalendarId;
        calendar.vacation = entity && entity.Vacation;
        calendar.calendarHolidays = [];

        if (entity && entity.CalendarHolidays) {
            $.each(entity.CalendarHolidays, function (index, holiday) {
                calendar.calendarHolidays.push(new CalendarHoliday(holiday));
            });
        }

        return addCalendarComputeds(ko.mapping.fromJS(calendar));
    };

    var CalendarHoliday = function (entity) {
        var calendarHoliday = this;

        calendarHoliday.calendarHolidaysId = entity && entity.CalendarHolidaysId;
        calendarHoliday.name = entity && entity.Name;
        calendarHoliday.day = entity && entity.Day;

        return addCalendarHolidayComputeds(ko.mapping.fromJS(calendarHoliday));
    };

    var CalendarDay = function (date, month) {
        var calendarDay = this;

        calendarDay.vacationRequestId = ko.observable();
        calendarDay.date = moment(date);
        calendarDay.month = date.month();
        calendarDay.day = date.format('DD');
        calendarDay.year = date.year();
        calendarDay.text = ko.observable();
        calendarDay.isSelected = ko.observable(false);
        calendarDay.isOutOfMonth = month != date.month();
        calendarDay.isToday = ko.observable(moment().isSame(date, 'day'));
        calendarDay.isHoliday = ko.observable(date.day() == 0 || date.day() == 6);
        calendarDay.isVacation = ko.observable(false);
        calendarDay.isApproved = ko.observable(false);
        calendarDay.isPending = ko.observable(false);
        calendarDay.isDenied = ko.observable(false);
        
        calendarDay.isWorkDay = function () {
            return !calendarDay.isHoliday();
        };
        
        calendarDay.isSelectable = ko.computed(function () {
            return calendarDay.date.isAfter(moment().subtract('days', 1)) && !calendarDay.isVacation() && (calendarDay.date.year() == moment().year());
        });
        
        calendarDay.status = function () {
            if (calendarDay.isHoliday())
                return calendarDay.text();
            if (calendarDay.isApproved())
                return 'Approved';
            if (calendarDay.isDenied())
                return 'Denied';
            if (calendarDay.isPending())
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
        calendarDay.isHoliday = ko.observable(date.day() == 0 || date.day() == 6);
        calendarDay.isVacation = ko.observable(false);
        calendarDay.isApproved = ko.observable(false);
        calendarDay.isPending = ko.observable(false);
        calendarDay.isDenied = ko.observable(false);

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

    function addEmployeeComputeds(entity) {
        entity.fullName = ko.computed(function () {
            return (entity.firstName() || '') + ' ' + (entity.lastName() || '');
        });
        entity.picture = ko.computed(function () {
            var content = entity.pictureContent();
            return content ? 'data:image/jpeg;base64,' + content : '/Content/Images/no_pending.jpg';
        });
        return entity;
    }


    function addTeamVacationComputeds(entity) {
        entity.friendlyDatesInterval = ko.computed(function () {
            var momentTo = moment(entity.to());
            var momentFrom = moment(entity.from());

            return momentFrom.format("MMM D") + " - " + momentTo.format("MMM D");
        });

        return entity;
    }

    function addCalendarHolidayComputeds(entity) {
        return entity;
    }

    function addCalendarComputeds(entity) {
        return entity;
    }
});