$(document).ready(function () {
    
    //global variable
    var customerRegisterChartSegment = $('#customer-register-chart');

    //render customer chart
    $.ajax({
        type: "post",
        url: urlCustomerRegisterChart,
        data: '',
        //dataType: 'json',
        success: function (result, status, xhr) {
            if (status === 'success') {

                customerRegisterChartSegment.removeClass('loading');
                customerRegisterChartSegment.html(result);
            }
        },
        error: function (xhr, status, error) {
            //go the the fucking hell
        }
    })
    
    //render product chart
    var bestSellingGoodsChartSegment = $('#best-selling-chart');

    //render customer chart
    $.ajax({
        type: "post",
        url: urlBestSellingGoodsChart,
        data: '',
        //dataType: 'json',
        success: function (result, status, xhr) {
            if (status === 'success') {

                bestSellingGoodsChartSegment.removeClass('loading');
                bestSellingGoodsChartSegment.html(result);
            }
        },
        error: function (xhr, status, error) {
            //go the the fucking hell
        }
    })
});