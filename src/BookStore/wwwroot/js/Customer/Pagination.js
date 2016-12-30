$(document).ready(function () {

    var customerSegment = $('#customer-action');

    $('#pagination a').on('click', function (event) {
        
        event.preventDefault();
        var url = $(this).attr('href');

        $.ajax({
            type: "post",
            url: url,
            data: '',
            //dataType: 'json',
            success: function (result, status, xhr) {
                if (status === 'success') {

                    customerSegment.html(result);
                }
            },
            error: function (xhr, status, error) {
                //go the the fucking hell
                customerRegisterChartSegment.removeClass('loading');
            }
        })

    })


});