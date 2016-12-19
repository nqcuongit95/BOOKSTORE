$(document).ready(function () {

    //*******************************global variable*********************************
    var inputEvents = 'DOMAttrModified textInput input keypress paste';

    //api setting
    $.fn.api.settings.api = {
        'get products': urlSearchProduct,
        'pay invoice': urlPayInvoice
    }

    var totalMoneyToPay = 0;

    var priceType = 1;

    var numberInput = '<div class="ui tiny input count-input" style="max-width: 80px">'
                       + '<input type="number" name="count" min="0">'
                       + '</div>';

    var normalInput = '<div class="ui tiny input price-input" style="max-width: 100px">'
                      + '<input type="text">'
                      + '</div>';

    var hiddenInput = '<input type="hidden" name="" value="" />';

    var trashIcon = '<i class="trash outline link icon" data-content="xóa"></i>';

    //define custom search template
    $.fn.search.settings.templates.message = function(message, type) {
        var
          html = ''
        ;
        if(message !== undefined && type !== undefined) {
            html +=  ''
              + '<div class="message ' + type + '">'
            ;
            // message type
            if(type == 'empty') {
                html += ''
                  + '<div class="header">Không kết quả</div class="header">'
                  + '<div class="description">' + message + '</div class="description">'
                ;
            }
            else {
                html += ' <div class="description">' + message + '</div>';
            }
            html += '</div>';
        }
        return html;
    }

    //search customer result template
    $.fn.search.settings.templates.customerSearchTemplate = function (response) {

        var html = '';

        //add new customer action
        html +=  '<a class="action" href="" id="newCustomer">'
                +'<h4 class="ui teal action header">'
                + '<i class="add user icon"></i>'
                + '<div class="content">'
                + response.newCustomer.text
                + '</div></h4></a>';

        $.each(response.results, function (index, customer) {
            html += '<a class="result">';
            html += '<div class="content">';
            html += '<div class="title">' + customer.name + '</div>';
            if (customer.phone !== "") {
                html += '<div class="meta">'
                html += '<i class="call icon"></i>' + customer.phone
                html += '</div>'
            }
            html += '</div>'
            html += '</a>'
        })

        return html;
    };
        
    //search product result template
    $.fn.search.settings.templates.productSearchTemplate = function (response) {

        var html = '';        

        $.each(response.results, function (index, product) {
            html += '<a class="result">';            
            html += '<div class="content">';
            html += '<div class="title">' + product.name + '</div>';
            html += '<div class="description">' + "Có thể bán: " + product.available + '</div>'
            html += '<div class="ui divider" style="margin: 4px 0 4px 0;"></div>'
            html += '<div class="ui orange tag label price">' + product.retailPrice + '</div>'
            html += '<div class="ui label">' + "Mã SP: " + product.id + '</div>'            
            html += '</div>'            
            html += '</a>'
        })

        return html;
    };

    //*********************************global function*******************************

    $.fn.exists = function () {
        return this.length !== 0;
    }

    function showPopup(elem, msg) {
        var alertMsg = "<p style=\"color:red;font-weight:bold\">";

        alertMsg += msg + "</p>";

        $(elem).attr('data-html', alertMsg);
        $(elem).popup({
            transition: 'scale',
            distanceAway: -15,
            onHidden: function () {
                $(elem).popup('destroy');
            }
        }).popup('show');


    }

    //update customer information when using search box
    function updateCustomer(result) {
        var table = $("#customer-table");

        var phone = table.find("tr:eq(0)").find("td:eq(1)");
        var address = table.find("tr:eq(1)").find("td:eq(1)");

        if (jQuery.isEmptyObject(result)) {

            phone.empty();
            address.empty();
            return;
        }

        phone.text(result.phone);
        address.text(result.address);
    }

    //add product result to table when using search box
    function showProductsResult(response) {

        var table = $('#product-results');
        table.empty();

        $.each(response.results, function (index, result) {

            var data = '<tr>';

            $.each(result, function (key, value) {
                if (value !== null) {

                    //dont show the total sold data
                    if (value == result.totalSold) {
                        return;
                    }

                    data += '<td>' + value + '</td>';
                }
            })

            data += '</tr>';
            table.append(data);
        })

        return true;
    }

    //set total money
    function calculateTotalMoney(count, price, total) {

        var currentCountProducts = Number(count.val());

        var currentPrice = Number(price.val());

        var totalMoney = currentPrice * currentCountProducts;

        total.text(totalMoney);

        //update payment
        updatePayment();

    }

    //update payment information
    function updatePayment() {

        var total = 0;
        $('#invoice-table tr').each(function () {

            var value = Number($(this).find('td:eq(4)').text());

            total += value;
        })

        totalMoneyToPay = total;

        $('#payment-table tr:eq(0) td:eq(1)').text(total).attr('value', total);

        $('#payment-table tr:eq(1) td:eq(1)').text(total);

    }

    //update customer change
    function recalculateCustomerChange() {

        totalMoneyToPay

        var customerPayTd = $('#paid-money input');
        var customerPay = Number(customerPayTd.val());

        var customerChangeTd = customerPayTd.closest('tr').next('tr').find('td:eq(1)');

        var customerChange = customerPay - totalMoneyToPay;

        if (customerChange > 0) {
            customerChangeTd.text(customerChange)
        }
        else {
            customerChangeTd.text(0);
        }

    }

    //update price when user change price type
    function updatePriceType(productIds, priceType) {
        $.ajax({
            type: "POST",
            url: urlPriceType,
            dataType: "json",
            traditional: true,
            data: {
                productIds: productIds,
                priceType: priceType
            },
            success: function (result, status, xhr) {
                if (status == 'success') {

                    $.each(result, function (index, value) {

                        var productRow = $('#invoice-table').find('tr').filter(function () {
                            return $(this).find('td:eq(0)').text() == value.id;
                        })

                        var priceInput = productRow.find("td:eq(3) input");
                        priceInput.val(value.price);
                        priceInput.trigger("textInput");

                    })
                }
            }
        });
    }

    //*************************** ui controls *************************************
    $(".ui.selection.dropdown").dropdown();
    $(".ui.search").on("click", 'input', function () {
        $(this).select();
    });
    $('.top.menu .item').tab();

    //update customer when search input was cleared
    $('#customer-search').on(inputEvents, 'input', function () {

        if (!$(this).val()) {
            var obj = {};
            updateCustomer(obj);

            //todo: update value for traveller (khách vãng like)
        }
    });

    //update when price-type change
    $(".ui.selection.dropdown").dropdown({
        onChange: function (value, text, choice) {

            //set global pricetype value
            priceType = value;

            var allPriceTd = $("#invoice-table tr");
            var productIds = [];
            //console.log(value);

            allPriceTd.each(function (index) {

                var id = $(this).find('td:eq(0)').text();
                productIds.push(id);
                //console.log(id);
            })

            updatePriceType(productIds, value);
        }
    });

    //customer search box
    $('#customer-search').search({
        cache: true,
        showNoResults: true,
        type: 'customerSearchTemplate',
        apiSettings: {
            url: urlSearch + "?val={query}"
        },        
        minCharacters: 0,
        error: {
            noResults: 'Không tìm thấy khách hàng.'            
        },
        onSelect: function (result, response) {
            updateCustomer(result);

            //update value of input
            $(this).find('input[type=hidden]').attr('value', result.id);
        },
        onResults: function (response) {
            updateCustomer(response.results)
            //todo: update value for traveller (khách vãng like)
        }
    });

    //product search box
    $('#product-input').search({
        apiSettings: {
            action: 'get products'
        },
        minCharacters: 0,
        cache: false,
        type: 'productSearchTemplate',
        error: {
            noResults: 'Không tìm thấy sản phẩm.'
        },
        throttle: 200,        
        onResults: function (response) {
            showProductsResult(response);
        },
        onSelect: function (result, response) {
                                     
            var tdId = $('#product-results > tr > td').filter(function (index) {
                                
                return $(this).eq(0).text() == result.id                
            });                        
            tdId.closest('tr').trigger('click');
            $('#product-input').find('input').val('');
        },        
    })


    //handle table row click event for adding product to invoice
    $('#products-table').on('click', 'tbody tr', function () {
        //event.preventDefault();        
        
        var invoice = $('#invoice-table');
        var clickedRow = $(this);

        //get the data
        var id = clickedRow.find('td:eq(0)').text();
        var name = clickedRow.find('td:eq(1)').text();
        var price = priceType == 1 ? clickedRow.find('td:eq(2)').text() :
                                      clickedRow.find('td:eq(3)').text();

        var availableProduct = clickedRow.find('td:eq(4)').text();

        //test if available product is in stock > 0
        if (availableProduct > 0) {

            //the key is to compare id of product and added product                                 
            var alreadyAdded = invoice.find('tr td').filter(function () {
                return $(this).text() == id;
            })

            //test if product already added, then increase the numbers of it;
            if (alreadyAdded.exists()) {

                var tdInputCount = alreadyAdded.closest('tr').find('td:eq(2) input');

                var count = Number(tdInputCount.val());

                count += 1;

                //check if number of products added over the available on stock
                if (count > availableProduct) {
                    count -= 1;
                    alert("buying exceed the stock");
                }
                else {

                    tdInputCount.val(count);

                    //recalculating the total money
                    var tdTotalMoney = alreadyAdded.closest('tr').find('td:eq(4)');
                    var currentPriceTd = alreadyAdded.closest('tr').find('td:eq(3) input');
                    var currentPrice = Number(currentPriceTd.val());
                    var value = count * currentPrice;
                    tdTotalMoney.text(value);

                    //update payment
                    updatePayment();
                    recalculateCustomerChange()
                }

            }
            else {
                //it's the first added product

                var row = "<tr>";
                row += "<td>" + id + "</td>";
                row += "<td>" + name + "</td>";
                row += "<td>" + numberInput + "</td>"
                row += "<td>" + normalInput + "</td>";
                row += "<td>" + price + "</td>"
                row += "<td>" + trashIcon + "</td>"
                row += "</tr>";

                invoice.append(row);

                //it's the first time added product, so the count equal to 1
                invoice.find('tr:last td:eq(2) input').val('1');
                //also set the price of product
                invoice.find('tr:last td:eq(3) input').val(price);

                //update payment
                updatePayment();

                //bind event to remove icon
                $(".trash.outline.link.icon").popup({
                    offset: -12
                });

                $(".trash.outline.link.icon").on('click', function () {
                    $(this).closest('tr').remove();
                    updatePayment();
                    recalculateCustomerChange();
                })

                //bind input event to count td;
                invoice.find('tr:last').on(inputEvents, '.count-input input', function () {

                    var inputTd = $(this);

                    var currentCount = Number(inputTd.val());

                    if (currentCount > availableProduct) {
                        alert("number of products exceed the stock");
                        inputTd.val(availableProduct);
                    }

                    if (currentCount < 0) {

                        //show popup when user enter negative value
                        var msg = "Số lượng không được nhỏ hơn 0";

                        showPopup(inputTd, msg);

                        inputTd.val(0);

                    }

                    var countTd = $(this);
                    var currentPriceTd = countTd.closest('td').next().find('input');
                    var totalMoneyTd = currentPriceTd.closest('td').next();

                    calculateTotalMoney(countTd, currentPriceTd, totalMoneyTd);
                    recalculateCustomerChange();
                })

                //bind input event to price td;
                invoice.find('tr:last').on(inputEvents, '.price-input input', function () {

                    var priceTd = $(this);
                    var totalMoneyTd = priceTd.closest('td').next();
                    var currentCountTd = priceTd.closest('td').prev().find('input');

                    //show popup when user enter negative price value
                    if (Number(priceTd.val()) < 0) {
                        //alert("price must not be negative");

                        var msg = "Đơn giá không được nhỏ hơn 0";

                        showPopup(priceTd, msg);

                        priceTd.val(0);
                    }

                    calculateTotalMoney(currentCountTd, priceTd, totalMoneyTd);
                    recalculateCustomerChange();

                })
            }

        }
        else {
            //todo: show message indicate that no available products
        }
    })

    //update customer pay
    $('#paid-money').on(inputEvents, 'input', function () {

        var customerChangeTd = $(this).closest('tr').next().find('td:eq(1)');
        var customerPay = Number($(this).val());

        //set value to attribute
        $(this).attr('value', customerPay);

        //alert(customerPay)
        var customerChange = customerPay - totalMoneyToPay;
        //console.log(customerChange);
        if (customerChange > 0) {
            customerChangeTd.text(customerChange);
        }
        else {
            customerChangeTd.text('0');
        }
    })

    //bind event for create new customer action in search box product
    $('#customer-results').on('click','#newCustomer', function (event) {

        event.preventDefault();
        console.log('click')
        var modal = $('#create-modal');

        $.ajax({
            type: "get",
            url: urlCreateCustomer,
            success: function (result, status, xhr) {

                if (status === 'success') {
                    
                    modal.html(result);
                    modal.modal('show');

                }

            },
            error: function (xhr, status, error) {

            }
        })

    })

    //create invoice
    $('#pay').click(function (event) {

        event.preventDefault();
        var thisBtn = $(this);
        thisBtn.addClass('loading');

        var customerId = $('input[name=CustomerId]').val();
        var staff = $('input[name=StaffId]').val();
        var totalValue = $('#payment-table tr:eq(0) td:eq(1)').text();
        var customerPaid = $('input[name=CustomerPaid]').val();

        if (Number(totalValue) <= 0) {

            $('#invoice-modal').modal('show');
            thisBtn.removeClass('loading');
            return;
        }


        var invoiceDetail = {
            CustomerId: customerId,
            Staff: staff,
            TotalValue: totalValue,
            CustomerPaid: customerPaid,
            productDetails: []
        }

        $('#invoice-table tr').each(function (index, elem) {

            var productId = $(elem).find('td:eq(0)').text();
            var count = $(elem).find('td:eq(2) input').val();
            var price = $(elem).find('td:eq(3) input').val();

            var product = {
                ProductId: productId,
                Count: count,
                Price: price
            }

            invoiceDetail.productDetails.push(product);

        })

        var modal = $('#notify-modal');

        $.ajax({
            type: "post",
            url: urlPayInvoice,
            data: invoiceDetail,
            success: function (result, status, xhr) {

                if (status === 'success') {

                    thisBtn.removeClass('loading');
                    modal.html(result);
                    modal.modal('show');

                    setTimeout(function () {
                        modal.modal('hide');
                    }, 2000);
                }
            },
            error: function (xhr, status, error) {
                thisBtn.removeClass('loading');
            }
        });

    })

    //reload page when model was hidden
    $('#notify-modal').modal({
        onHidden: function () {
            location.reload();
        }
    });

});