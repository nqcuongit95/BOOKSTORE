$(document).ready(function () {
    $("#customer-type").dropdown();

    $('form#create-customer')
       .form({
           on: 'blur',
           fields: {
               name: {
                   identifier: 'valid-name',
                   rules: [{
                       type: 'empty',
                       prompt: 'Tên không được để trống.'
                   }]
               },
               phone: {
                   identifier: 'valid-phone',
                   rules: [
                       {
                           type: 'empty',
                           prompt: 'Số điện thoại không được để trống.'
                       },
                       {
                           type: 'minLength[10]',
                           prompt: "Vui lòng nhập đúng số điện thoại"
                       }
                   ]
               },                              
           }
       });

    $('#submit-btn').on('click', function () {
        
        $(this).addClass('loading');

        var form = $('.ui.form');
        var modal = $('#notify-modal');
        form.form('validate form');

        var data = form.serialize();

        if (form.form('is valid')) {
            
            $.ajax({
                type: "post",
                url: urlCreateCustomer,
                data: data,
                dataType: 'json',                
                success: function (result, status, xhr) {

                    if (status === 'success') {
                        console.log(result.id);
                        $(this).removeClass('loading');
                        modal.html(result.modal);
                        modal.modal('show');
                        
                        var customerId = { id: result.id };

                        //update last created customer
                        $.ajax({
                            type: "post",                            
                            url: urlCustomerCreatedInfo,
                            data: customerId,
                            dataType: 'json',
                            success: function (result, status, xhr) {
                                if (status === 'success') {                                                                      
                                    updateCustomer(result);
                                }
                            },
                            error: function (xhr, status, error) {
                                //go the the fucking hell
                            }
                        })

                    }

                },
                error: function (xhr, status, error) {
                    $(this).removeClass('loading');
                }
            })
        }
        else {
            $(this).removeClass('loading');
        }
    })

    function updateLastCreatedCustomer(result)
    {
        //update last created customer
        $('#customer-search').search('set value', result.name);
        $('#customer-table tr').eq(0).find('td').eq(1).text(result.phone);
        $('#customer-table tr').eq(1).find('td').eq(1).text(result.address);
        $('#customer-search input[name=CustomerId]').attr('value', result.id);

    }

    function updateCustomer(result) {

        var table = $("#customer-table");
        var phone = table.find("tr:eq(0)").find("td:eq(1)");
        var address = table.find("tr:eq(1)").find("td:eq(1)");

        $('#customer-search').search('set value', result.name);        

        var dataTab = $('.tabular.menu .active.item').attr('data-tab');
        console.log(dataTab);

        var index = invoiceObject.findIndex(function (elem) {
            return elem.invoiceDataTab == dataTab;
        });
             
        phone.text(result.phone);
        address.text(result.address);
        $('#customer-search input[name=CustomerId]').attr('value', result.id);

        invoiceObject[index].name = result.name;
        invoiceObject[index].phone = result.phone;
        invoiceObject[index].id = result.id;
        invoiceObject[index].address = result.address;

    }
});