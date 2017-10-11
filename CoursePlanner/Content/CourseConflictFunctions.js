$(".clickable-row").click(function () {
    window.location.href = $(this).data("href");
});



//$("closest_parent_node_of_your_button").on("click", "your_button", function () { });
//$("th.filter").on("click", function () {
$("tr").on("click", ".filter", function(){
    var term = $(this).text();
    $("#courseconflicttable").load("Home/FilterCourses/?filter=" + term);
});


$(function () {
    $("#courseconflictoverview").DataTable({
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
