// Write your Javascript code.
$(document).ready(function () {
    
    var customerSegment = $('#customer-action');

    //details 
    var detailLink = $('#customer-details');
    var detailUrl = detailLink.attr('href');

    detailLink.trigger('click');

    detailLink.on('click', function (event) {
        event.preventDefault();
        activeDimmer();
        removeActive($(this));
        $(this).addClass('active');
        $.ajax({
            type: "post",
            url: detailUrl,
            data: '',
            //dataType: 'json',
            success: function (result, status, xhr) {
                if (status === 'success') {
                    deactiveDimmer()
                    customerSegment.html(result);
                }
            },
            error: function (xhr, status, error) {
                //go the the fucking hell
                customerRegisterChartSegment.removeClass('loading');
            }
        })
    })

    detailLink.trigger('click');

    //edit 
    var editLink = $('#customer-edit');
    var editUrl = editLink.attr('href');

    editLink.on('click', function (event) {
        event.preventDefault();
        activeDimmer();
        removeActive($(this));
        $(this).addClass('active');
        $.ajax({
            type: "post",
            url: editUrl,
            data: '',
            //dataType: 'json',
            success: function (result, status, xhr) {
                if (status === 'success') {
                    deactiveDimmer()
                    customerSegment.html(result);
                }
            },
            error: function (xhr, status, error) {
                //go the the fucking hell
                customerRegisterChartSegment.removeClass('loading');
            }
        })


    })


    var transactionLink = $("#customer-transaction");
    var transactionUrl = transactionLink.attr('href');

    transactionLink.on('click', function (event) {        
        event.preventDefault();
        activeDimmer();
        removeActive($(this));
        $(this).addClass('active');

        $.ajax({
            type: "post",
            url: transactionUrl,
            data: '',
            //dataType: 'json',
            success: function (result, status, xhr) {
                if (status === 'success') {
                    deactiveDimmer()
                    customerSegment.html(result);
                }
            },
            error: function (xhr, status, error) {
                //go the the fucking hell
                customerRegisterChartSegment.removeClass('loading');
            }
        })


    })

    //liabilities

    var liabilitesLink = $('#customer-liabilities');
    var liabilitesUrl = liabilitesLink.attr('href');

    liabilitesLink.on('click', function () {
        event.preventDefault();
        activeDimmer()
        liabilitesLink($(this));
        $(this).addClass('active');

        $.ajax({
            type: "post",
            url: liabilitesUrl,
            data: '',
            //dataType: 'json',
            success: function (result, status, xhr) {
                if (status === 'success') {
                    deactiveDimmer()
                    customerSegment.html(result);
                }
            },
            error: function (xhr, status, error) {
                //go the the fucking hell
                customerRegisterChartSegment.removeClass('loading');
            }
        })

    })

    function removeActive(elem)
    {
        elem.siblings().removeClass('active');
    }

    function activeDimmer()
    {
        $('#dimmer').addClass('active');
        
    }

    function deactiveDimmer()
    {
        $('#dimmer').removeClass('active');
        
    }

});