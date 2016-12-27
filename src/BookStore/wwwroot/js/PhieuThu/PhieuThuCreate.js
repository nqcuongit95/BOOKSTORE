$('.ui.dropdown').dropdown();
$(document).ready(function () {
    $("#customerFields").show();
    $("#providerFields").hide();
    $("#btnXoa").click(function () {
        $("#customerValue").val("");
        $("#KhachHangId").val("");
        $("#providerValue").val("");
        $("#ncc").val("");
        $("#tien").val("");
    })
    $("#customer-search").search({
        apiSettings: {
            url: urlSearch
        },
        fields: {
            title: 'name',
            description: 'phone'
        },
        minCharacters: 1,
        maxResults: 4,
        onSelect: function (result, response) {
            $(this).find('input[type=hidden]').attr('value', result.id);
        },
    });
    $("#provider-search").search({
        apiSettings: {
            url: urlSearchProvider
        },
        fields: {
            title: 'name',
        },
        minCharacters: 0,
        maxResults: 4,
        onSelect: function (result, response) {
            $(this).find('input[type=hidden]').attr('value', result.id);
        }
    });


    $("#drop").change(function () {
        var doituong = this.value;
        console.log(this.value)
        if (doituong == "1") {

            $("#customerFields").show();
            $("#providerFields").hide();
            $("#providerValue").val("");
            $("#ncc").val("");
        }
        if (doituong == "2") {

            $("#customerFields").hide();
            $("#providerFields").show();
            $("#customerValue").val("");
            $("#khachhang").val("");
        }
    })
})
