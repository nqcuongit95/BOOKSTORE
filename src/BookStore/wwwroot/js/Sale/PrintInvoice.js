$(document).ready(function () {

    $('#print-btn').on('click', function () {

        $(this).addClass('loading');
        var billId = $('input[name=BillID]').val();

        var data = {
            id: billId,
            pay: __valueCustomerPay
        }

        var modal = $('#print-preview-modal');

        $.ajax({
            type: "post",
            url: urlPrint,
            data: data,
            success: function (result, status, xhr) {

                if (status === 'success') {
                    $(this).removeClass('loading');

                    modal.html(result);

                    modal.modal({
                        onVisible: function () {
                            window.print();
                        }
                    }).modal('show');

                }
            },
            error: function (xhr, status, error) {
                $(this).removeClass('loading');

            }
        });

    });

});