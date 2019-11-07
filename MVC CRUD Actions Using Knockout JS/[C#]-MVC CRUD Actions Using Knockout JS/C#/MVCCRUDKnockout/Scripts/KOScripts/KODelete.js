var parsedSelectedCourse = $.parseJSON(selectedCourse);
$(function () {
    ko.applyBindings(modelDelete);
});

var modelDelete = {
    CourseID: ko.observable(parsedSelectedCourse.CourseID),
    CourseName: ko.observable(parsedSelectedCourse.CourseName),
    CourseDescription: ko.observable(parsedSelectedCourse.CourseDescription),
    deleteCourse: function () {
        try {
            $.ajax({
                url: '/Home/Delete',
                type: 'POST',
                dataType: 'json',
                data: ko.toJSON(this),
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