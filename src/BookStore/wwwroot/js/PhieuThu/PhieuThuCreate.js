
$(document).ready(function () {

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
                          sotienthu: {
                              identifier: 'sotienthu',
                              rules: [
                                {
                                    type: 'empty',
                                    prompt: 'số tiền thu không hợp lệ.'
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
                          sotienthu: {
                              identifier: 'sotienthu',
                              rules: [
                                {
                                    type: 'empty',
                                    prompt: 'số tiền thu không hợp lệ.'
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
            sotienthu: {
                identifier: 'sotienthu',
                rules: [
                  {
                      type: 'empty',
                      prompt: 'số tiền thu không hợp lệ.'
                  }
                ]
            }
        }
    })
        }
    })
})
