
 $(document).ready(function () {
     $(".clickable-row").click(function () {
         //var url = '@Url.Action("Details", "Course", new { id = "__id__" })';
         window.location.href = $(this).data("href");
         //window.location = 'Details/' + $(this).data("href");
     });

 });
