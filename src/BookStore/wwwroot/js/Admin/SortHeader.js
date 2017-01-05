$(document).ready(function () {

    updateDirection();

    //sort direction

    function updateDirection() {
        if (currentSortedHeader != undefined) {
            $('#customerTable thead th').each(function (index, elem) {

                $(elem).find('i').css('display', 'none');

            });

            $('#customerTable thead th').eq(currentSortedHeader).find('i').css('display', 'inline-block');
        }
    }



});


