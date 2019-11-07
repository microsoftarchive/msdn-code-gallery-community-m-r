$(function () {
    ko.applyBindings(modelView);
    modelView.viewCourses();
});

var modelView = {
    Courses: ko.observableArray([]),
    viewCourses: function () {
        var thisObj = this;
        try {
            $.ajax({
                url: '/Home/ListCourses',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    thisObj.Courses(data); //Here we are assigning values to KO Observable array
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }
            });
        } catch (e) {
            window.location.href = '/Home/Read/';
        }
    }
};

