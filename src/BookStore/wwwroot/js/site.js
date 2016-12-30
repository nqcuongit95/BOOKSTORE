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

    activeMenu();

    function activeMenu() {
        $('#controller-menu a').each(function (index, elem) {
            $(elem).removeClass('active');
        })

        var url = window.location.pathname;
        console.log(url);
        var firstSlash = getPosition(url, "/", 1);
        var secondSlash = getPosition(url, "/", 2);
        var controller = url.substring(firstSlash, secondSlash);
        if (url != "/") {        
            $('#controller-menu a[href="' + controller + '"]').addClass('active');
        }
        

    }

    function getPosition(string, subString, index) {
        return string.split(subString, index).join(subString).length;
    }
});

