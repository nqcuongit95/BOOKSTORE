$(document).ready(function () {
    updatePayment();
    updatepay();
    function updatepay() {
        var tiendaThu = $("#tienthu").text();
        var tiendaThuValue = numeral(tiendaThu).value();
        var formatedtienthu = numeral(tiendaThuValue).format('0,0 $');
        $('#tienphaitra').val(tiendaThuValue);
    }
    var inputEvents = 'DOMAttrModified textInput input keypress paste';
    $('#tienphaitra').on(inputEvents, function () {
        var tiendaThu = $("#tienthu").text();
        var tiendaThuValue = numeral(tiendaThu).value();
        var formatedtienthu = numeral(tiendaThuValue).format('0,0 $');
        var tientra = numeral($('#tienphaitra').val()).value();
        if (tientra > tiendaThuValue) {
            $('#tienphaitra').val(tiendaThuValue);
        }
    })
    $(".ui.input").on("click", 'input', function () {
        $(this).select();
    });
    $('#return').on("click", function () {
        var url = $("#returnForm").attr("action");
        var paid = numeral($('input[name=TienThu]').val()).value();
        var id = $('input[name=donhangID]').val();
        var token = $('#returnForm input').val();
        var count = 0;
        $('#detailbdy tr').each(function () {
            if ($(this).find('td:eq(0) input').prop('checked')) {
                count++;
            }
        })
        //var tiendaThu = $("#tienthu").text();
        //var tiendaThuValue = numeral(tiendaThu).value();
        //if (paid < tiendaThuValue) {
        //    $('#error-modal').modal('show');
        //    return;
        //}
        if (count == 0) {
            $('#error-2-modal').modal('show');
            return;
        }
        var data = {
            TienThu: paid,
            ID: id,
            returnDetails: [],
            __RequestVerificationToken: token
        }
        $('#detailbdy tr').each(function () {
            var ctID = $(this).find('td:eq(1) input');
            var inputCount = $(this).find('td:eq(3) input');
            var inputPrice = $(this).find('td:eq(4) input');
            var checkbox = $(this).find('td:eq(0) input');
            if($(this).find('td:eq(0) input').prop('checked')) {                    
                    var gt = numeral(inputPrice.val()).value();
                    var chitietID = ctID.val();
                    var sl = numeral(inputCount.val()).value();
                    var detail = {
                        ChiTietDonHangId: chitietID,
                        Soluong: sl,
                        GiaTra: gt,
                    }
                    data.returnDetails.push(detail);
                }
        });
        var modal = $('#notify-modal');
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
    })
    $('#notify-modal').modal({
        onHidden: function () {
            window.history.back();
        }
    });
    function updatePayment() {
        var total = 0;
        $('#detailbdy tr').each(function () {
            var formatedValue = $(this).find('td:eq(5)').text();
            var rawValue = numeral(formatedValue).value();
            total += rawValue;
        })
        var formatedTotal = numeral(total).format('0,0 $');
        $('#tongtientra').text(formatedTotal);
    }
    $('tbody tr').each(function () {
        var thisRow = $(this);
        var inputCount = $(this).find('td:eq(3) input');
        var inputPrice = $(this).find('td:eq(4) input');
        var totalValueTd = $(this).find('td:eq(5)');
        var checkbox = $(this).find('td:eq(0) input')
        var maxcount = $(this).find('td:eq(2) input');
        var maxprice = $(this).find('td:eq(5) input');
        checkbox.on('change', function () {
            if (this.checked) // if changed state is "CHECKED"
            {
                inputCount.val(1);
                var count = inputCount.val();
                var price = numeral(inputPrice.val()).value();
                var total = count * price;
                var formatedTotal = numeral(total).format('0,0 $');
                totalValueTd.text(formatedTotal);
                updatePayment();
            }
            else {
                totalValueTd.text(0);
                inputCount.val(0);
                updatePayment();
            }
        })
        inputCount.on(inputEvents, function () {
            if ($(this).val() > maxcount.val()) {
                $(this).val(maxcount.val());
            }
            if ($(this).val() < 1) {
                $(this).val(1);
            }
            var count = $(this).val();
            var price = numeral(inputPrice.val()).value();
            var total = count * price;
            var formatedTotal = numeral(total).format('0,0 $');
            totalValueTd.text(formatedTotal);
            updatePayment();
        });
        inputPrice.on(inputEvents, function () {
            if (numeral($(this).val()).value() > numeral(maxprice.val()).value()) {
                $(this).val(maxprice.val());
            }
            if (numeral($(this).val()).value() < 1) {
                $(this).val(1);
            }
            var count = inputCount.val();
            var price = numeral($(this).val()).value();
            var total = count * price;
            var formatedTotal = numeral(total).format('0,0 $');
            totalValueTd.text(formatedTotal);
            updatePayment();
        });
    });
    var TienThu = new Cleave('input[name=TienThu]', {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand',
        numeralPositiveOnly: true
    });
    //$('tbody tr').each(function (event) {
    //    var inputPrice = $(this).find('td:eq(4) input');
    //    new Cleave(inputPrice, {
    //        numeral: true,
    //        numeralThousandsGroupStyle: 'thousand',
    //        numeralPositiveOnly: true
    //    });
    //})
    $('.input-price').toArray().forEach(function (field) {
        //var inputPrice = $(this).find('td:eq(4) input');
        new Cleave(field, {
            numeral: true,
            numeralThousandsGroupStyle: 'thousand',
            numeralPositiveOnly: true
        });
    })
})
