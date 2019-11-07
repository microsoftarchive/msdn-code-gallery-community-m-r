$(function () {
    //$.support.cors = true;
    var vm = (function () {

        var expenseStatus = 1;
        var pageSize = 20;
        var pageCount = 0;
        var pictureType = 1;

        var expenses = ko.observableArray(),
            expensesUrl = config.getUrlBase() + 'expenses/team?expenseStatus=' + expenseStatus
                    + '&pictureType=' + pictureType
                    + '&pageSize=' + pageSize
                    + '&pageCount=' + pageCount;

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
        goToDetail = function (expense) {
            window.location.href = 'Home/Detail?id=' + expense.id;
        },
        refresh = function () {
            $.ajax({
                type: "GET",
                url: expensesUrl,
                beforeSend: function (xhr) { xhr.setRequestHeader("Authorization", Security.getSecurityHeaders()); },
                success: function (data) {
                    var mapped = data.map(function (exp) {
                        return {
                            id: exp.$id,
                            amount: exp.Amount + '$',
                            employeeFullName: exp.Employee.FirstName + ' ' + exp.Employee.LastName,
                            creationDate: moment(exp.CreationDate).format(config.dateFormat),
                            employeePicture: exp.Employee.EmployeePictures[0].Content,
                            status: getStatus(exp.Status)
                        }
                    });
                    expenses(mapped);
                }
            });
        };

        init();

        return {
            expenses: expenses,
            goToDetail: goToDetail
        };
    })();

    ko.applyBindings(vm);
});