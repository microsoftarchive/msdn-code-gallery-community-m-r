$(function () {
    //$.support.cors = true;
    var vm = (function () {
        var id = $('#Id').val(),
        expense = ko.observable();

        var bigPicture = 2;
        expenseUrl = config.getUrlBase() + 'expenses/' + id + '/' + bigPicture;

        var init = function(){
            refresh();
        },
        getStatus = function (status) {
            switch (status) {
                case 1:
                    return "pending";
                case 2:
                    return "approved";
                case 3:
                    return "denied";
                default:
                    return "unknown";
            }
        },
        refresh = function () {
            $.ajax({
                type: "GET",
                url: expenseUrl,
                beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", Security.getSecurityHeaders()); },
                success: function (data) {
                    expense({
                        id: data.$id,
                        amount: data.Amount + '$',
                        employeeFullName: data.Employee.FirstName + ' ' + data.Employee.LastName,
                        name: data.Name,
                        description: data.Description,
                        creationDate: moment(data.CreationDate).format(config.dateFormat),
                        picture: data.Picture,
                        employeePicture: data.Employee.EmployeePictures[0].Content,
                        status: getStatus(data.Status)
                    })
                }
            });
        };

        init();

        return {
            expense: expense
        };
    })();

    ko.applyBindings(vm);
});