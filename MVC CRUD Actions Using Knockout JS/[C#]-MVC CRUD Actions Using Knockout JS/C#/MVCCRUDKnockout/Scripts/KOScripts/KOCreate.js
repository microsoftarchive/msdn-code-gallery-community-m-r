$(function () {
    ko.applyBindings(modelCreate);
});
var modelCreate = {
    CourseName: ko.observable(),
    CourseDescription: ko.observable(),
    createCourse: function () {
        try {
            $.ajax({
                url: '/Home/Create',
                type: 'post',
                dataType: 'json',
                data: ko.toJSON(this), //Here the data wil be converted to JSON
                contentType: 'application/json',
                success: successCallback,
                error: errorCallback
            });
        } catch (e) {
            window.location.href = '/Home/Read/';
        }
    }
};

function successCallback(data) {
    window.location.href = '/Home/Read/';
}
function errorCallback(err) {
    window.location.href = '/Home/Read/';
}

