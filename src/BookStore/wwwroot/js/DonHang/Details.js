$(document).ready(function () {
    updatepay();
    var inputEvents = 'DOMAttrModified textInput input keypress paste';
    $('#customerPaid').on(inputEvents, function () {
        var tiendaThu = $("#tiendathu").text();
        var tongTien = $("#tongtien").text();
        var tiendaThuValue = numeral(tiendaThu).value();
        var tongTienValue = numeral(tongTien).value();
        var tiencon = (tongTienValue - tiendaThuValue);
        var tienthu = numeral($('#customerPaid').val()).value();
        var formatedtienthu = numeral(tiencon).format('0,0 $');
        if (tienthu > tiencon) {
            $('#customerPaid').val(tiencon);
        }
    })
    function updatepay() {
        var tiendaThu = $("#tiendathu").text();
        var tongTien = $("#tongtien").text();
        var tiendaThuValue =numeral(tiendaThu).value();
        var tongTienValue =numeral(tongTien).value();
        var tienthu =(tongTienValue - tiendaThuValue);
        var formatedtienthu = numeral(tienthu).format('0,0 $');
        $('#customerPaid').val(formatedtienthu);
    }
    var phieutraid = $('#phieutraID').val();
    //alert(phieutraid)
    //stuff
    update();
    if (phieutraid != '') {
        $("#payment-table").hide();
        $('#hoantra').hide();
        $('#danhan').show();
    }
    if (phieutraid == '') {
        $('#hoantra').show();
        $('#danhan').hide();
    }
    

    function update() {
        var tienThu = $("#tiendathu").text();
        var tongTien = $("#tongtien").text();
        var tienThuValue = numeral(tienThu).value();
        var tongTienValue = numeral(tongTien).value();
        
        if (tienThuValue >= tongTienValue) {
            $("#payment-table").hide();
        }
        else {
            $("#payment-table").show();
        }
    }
    $("#btnPay").on("click", function (event) {
        event.preventDefault

        var paid = numeral($('input[name=TienThu]').val()).value();
        var id = $('input[name=ID]').val();
        var token = $('#submitHD input').val();
        if (paid <= 0) {
            $('#error-modal').modal('show');
            return;
        }
        var data = {
            TienThu: paid,
            ID: id,
            __RequestVerificationToken: token
        }

        //modal
        var modal = $('#notify-modal');

        var url = $("#submitHD").attr("action");
        $.ajax({
            type: "post",
            url: url,
            data: data,
            success: function (result, status, xhr) {

                if (status === 'success') {

                    //window.location.reload();
                    //alert(result.str);
                    modal.html(result);
                    modal.modal('show');

                    setTimeout(function () {
                        modal.modal('hide');
                    }, 1000);
                }
            },
            error: function (xhr, status, error) {
                //idk what to do right here
                //inactive dimmer

            }
        });
    });

    //reload page when modal was hidden (temporarily solution)
    $('#notify-modal').modal({
        onHidden: function () {
            location.reload();
        }
    });

    //fortmat input
    var customerPaidInput = new Cleave('input[name=TienThu]', {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand',
        numeralPositiveOnly: true
    });
})