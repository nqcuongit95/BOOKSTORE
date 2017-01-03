$(document).ready(function () {
    if ($("#doituong").val() == "Khách Hàng") {
        $("#tenkhachhang").show();
        $("#tenncc").hide();
    }
    else {
        $("#tenkhachhang").hide();
        $("#tenncc").show();
    }
})
