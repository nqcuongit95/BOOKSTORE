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
            customerRegisterChartSegment.removeClass('loading');
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
            bestSellingGoodsChartSegment.removeClass('loading');
        }
    })

    //get latest activity
    var latestActivity = $('#latest-activity');
    $.ajax({
        type: "post",
        url: urlLastedActivity,
        data: '',
        //dataType: 'json',
        success: function (result, status, xhr) {
            if (status === 'success') {

                latestActivity.removeClass('loading');
                latestActivity.html(result);
            }
        },
        error: function (xhr, status, error) {
            //go the the fucking hell
            latestActivity.removeClass('loading');
        }
    })

    //render revenue chart
    var revenueChart = $('#revenue-statistic-chart');
    $.ajax({
        type: "post",
        url: urlRevenueChart,
        data: '',
        //dataType: 'json',
        success: function (result, status, xhr) {
            if (status === 'success') {

                revenueChart.removeClass('loading');
                revenueChart.html(result);
            }
        },
        error: function (xhr, status, error) {
            //go the the fucking hell
            revenueChart.removeClass('loading');
        }
    })

    //handle view by week, month
    //customer chart
    $('#best-selling-item a').on('click', function (event) {

        event.preventDefault();

        menuItem = $(this);        
        menuItem.siblings().removeClass('active');
        menuItem.addClass('active');

        bestSellingGoodsChartSegment.empty();
        bestSellingGoodsChartSegment.addClass('loading');

        var url = $(this).attr('href');

        $.ajax({
            type: "post",
            url: url,
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
                bestSellingGoodsChartSegment.removeClass('loading');
            }
        })
    })

});