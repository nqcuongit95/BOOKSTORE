$('.search-input').keypress(function (event) {

    if (event.which == 13) {

        var keyword = $(this).val();
        var url = '@Url.Action("Index","DonHang")';

        window.location = url + "?searchString=" + keyword;
    }
});

$(document).ready(function () {
    $('tbody tr').each(function () {
        var thisRow = $(this);
        var inputCount = $(this).find('td:eq(2) a');
        var inputth = $(this).find('td:eq(2) input');
        var status = inputth.val();
        if (status == "Thanh Toán Một Phần") {
            inputCount.css('background-color', 'green');
        }
        if (status == "Đã Thanh Toán") {
            inputCount.css('background-color', 'red');
        }
        if (status == "Đang giao dịch") {
            inputCount.css('background-color', 'lightgreen');
        }
    })
})