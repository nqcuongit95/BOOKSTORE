$('.search-input').keypress(function (event) {

    if (event.which == 13) {

        var keyword = $(this).val();
        var url = '@Url.Action("Index","PhieuThu")';

        window.location = url + "?searchString=" + keyword;
    }
});