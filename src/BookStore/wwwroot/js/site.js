// Write your Javascript code.
$(document).ready(function () {
    var currentDate = new Date();

    //responsive sidebar menu
    $("a.sidebar-toggle").click(function () {
        $('.ui.sidebar').sidebar('toggle');
    });

    $('.disabled').click(function (e) {
        e.preventDefault();
    })
});

