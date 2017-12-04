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
        if (status == "Thanh toán một phần") {
            inputCount.css('background-color', 'blue');
        }
        if (status == "Đã thanh toán") {
            inputCount.css('background-color', 'green');
        }
        if (status == "Chưa thanh toán") {
            inputCount.css('background-color', 'red');
        }

        if (status == "Chưa Thanh Toán") {
            inputCount.css('background-color', 'lightgreen');
        }
    })
})