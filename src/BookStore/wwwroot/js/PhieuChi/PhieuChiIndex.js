﻿$('.search-input').keypress(function (event) {

    if (event.which == 13) {

        var keyword = $(this).val();
        var url = '@Url.Action("Index","PhieuChi")';

        window.location = url + "?searchString=" + keyword;
    }
});