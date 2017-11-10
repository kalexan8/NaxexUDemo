// Write your JavaScript code.
function EnrollInCourse(course, userId) {
    var courseId = course.getAttribute("data-courseId");
    var data = { courseId: courseId, userId: userId };
    course = $(course);
    $.ajax({
        url: "api/Enrollment/EnrollInCourse",
        type: "POST",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            course.css("visibility", 'hidden');
            var enrolled = course.parent().siblings(".numberEnrolled").html();
            course.parent().siblings(".numberEnrolled").html(Number(enrolled) + 1);
            course.siblings(".dropCourse").css("visibility", "visible");
            RefreshMyEnrollments();
        }

    });
}
function DropCourse(course, userId) {
    var courseId = course.getAttribute("data-courseId");
    var data = { courseId: courseId, userId: userId };
    course = $(course);
    $.ajax({
        url: "api/Enrollment/DropCourse",
        type: "POST",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            course.css("visibility", 'hidden');
            var enrolled = course.parent().siblings(".numberEnrolled").html();
            course.parent().siblings(".numberEnrolled").html(Number(enrolled) - 1);
            course.siblings(".enrollInCourse").css("visibility", "visible");
            RefreshMyEnrollments();
        }

    });
}
