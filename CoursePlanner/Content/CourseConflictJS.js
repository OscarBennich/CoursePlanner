$(".clickable-row").click(function () {
    window.location.href = $(this).data("href");
});

$("tr").on("click", ".filter", function () {
    var filter = $(this).data("filter");
    $("#courseconflicttable").load("Home/FilterCourses/?filter=" + filter);
});


$(function () {
    table = $("#courseconflictoverview").DataTable({
        'retrieve': true,
        'paging': false,
        'lengthChange': false,
        'searching': true,
        'ordering': false,
        'info': false,
        'autoWidth': false,
        'columnDefs': [
            {
                'targets': '_all',
                'defaultContent': ''
            }
        ]
    });
});
