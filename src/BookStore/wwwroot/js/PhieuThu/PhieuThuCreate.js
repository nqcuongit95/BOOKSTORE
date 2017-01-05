$(document).ready(function () {
    var inputEvents = 'DOMAttrModified textInput input keypress paste';
    updatepay();
    $('#tien').on(inputEvents, function () {
        var tiendaThu = $("#tien").val();
        var tiendaThuValue = numeral(tiendaThu).value();
        $('#tongtien').val(tiendaThuValue);
    });
    function updatepay() {
        var tiendaThu = $("#tien").val();
        var tiendaThuValue = numeral(tiendaThu).value();
        $('#tongtien').val(tiendaThuValue);
    };
    var TongTientemp = new Cleave('input[name=TongTientemp]', {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand',
        numeralPositiveOnly: true
    });
    $('#notify-modal').modal({
        onHidden: function () {
            location.reload();
        }
    });
    $('.ui.dropdown').dropdown();
    $("#customerFields").show();
    $("#providerFields").hide();
    $("#btnXoa").click(function () {
        $("#customerValue").val("");
        $("#KhachHangId").val("");
        $("#providerValue").val("");
        $("#ncc").val("");
        $("#tien").val("");
    })
    $('.ui.form')
                  .form({
                      inline: true,
                      fields: {
                          tenkhachhang: {
                              identifier: 'tenkhachhang',
                              rules: [
                                {
                                    type: 'empty',
                                    prompt: 'tên khách hàng không hợp lệ.'
                                }
                              ]
                          },
                          TongTien: {
                              identifier: 'TongTien',
                              rules: [
                                {
                                    type: 'integer[1000..]',
                                    prompt: 'số tiền thu tối thiểu là 1000đ'
                                }
                              ]
                          }
                      }
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
            $("#NCCId").val("");
            $('.ui.form')
                  .form({
                      inline: true,
                      fields: {
                          tenkhachhang: {
                              identifier: 'tenkhachhang',
                              rules: [
                                {
                                    type: 'empty',
                                    prompt: 'tên khách hàng không hợp lệ.'
                                }
                              ]
                          },
                          TongTien: {
                              identifier: 'TongTien',
                              rules: [
                                {
                                    type: 'integer[1000..]',
                                    prompt: 'số tiền thu tối thiểu là 1000đ'
                                }
                              ]
                          }
                      }
                  })
        }
        if (doituong == "2") {
            $("#customerFields").hide();
            $("#providerFields").show();
            $("#customerValue").val("");
            $("#khachhang").val("");
            $('.ui.form')
    .form({
        inline: true,
        fields: {
            tenncc: {
                identifier: 'tenncc',
                rules: [
                  {
                      type: 'empty',
                      prompt: 'tên nhà cung cấp không hợp lệ.'
                  }
                ]
            },
            TongTien: {
                identifier: 'TongTien',
                rules: [
                  {
                      type: 'integer[1000..]',
                      prompt: 'số tiền thu tối thiểu là 1000đ'
                  }
                ]
            },
            NCCId: {
                identifier: 'NCCId',
                rules: [
                  {
                      type: 'empty',
                      prompt: 'không tìm thấy Nhà cung cấp.'
                  }
                ]
            }
        }
    })
        }
    })
})
