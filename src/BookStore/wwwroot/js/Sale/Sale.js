﻿$(document).ready(function () {

    //preload some products
    $.ajax({
        type: "post",
        url: urlUpdateSearchProduct,
        data: '',
        success: function (result, status, xhr) {

            if (status === 'success') {
                //todo: show status bar                   
                showProductsResult(result);
                //inactive dimmer
                inactiveProductResultsDimmer()
            }
        },
        error: function (xhr, status, error) {
            //idk what to do right here
            //inactive dimmer
            inactiveProductResultsDimmer()
        }
    });

    //numeral.locale('vn');

    class Payment {
        constructor() {
            this.id = 1;
            this.name = '';
            this.phone = '';
            this.address = '';
            this.priceType = 1;
            this.totalValue = 0;
            this.totalToPay = 0;
            this.customerPaid = 0;
            this.invoiceDataTab = '';
            this.discount = 0;
            this.discountType = 1;
        }
    }

    var saleControl = {

        invoiceTable: $('.active.tab.segment tbody'),

        customerSegment: {
            table: $("#customer-table"),
            phone: $("#customer-table").find("tr:eq(0)").find("td:eq(1)"),
            address: $("#customer-table").find("tr:eq(1)").find("td:eq(1)"),
        },

        paymentSegment: {
            table: $('#payment-table'),
            totalValue: $('#payment-table tr').eq(0).find('td').eq(1),
            customerToPay: $('#payment-table tr').eq(2).find('td').eq(1),
            customerPaid: $('input[name=CustomerPaid]'),
            discountInput: $('#discount-input input'),
            discountType: $('#discount-type')
        },
    }

    //**********************************format input**********************************
    //customer paid input

    var customerPaidInput = new Cleave('input[name=CustomerPaid]', {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand',
        numeralPositiveOnly: true
    });

    //discount input
    var discountCleaveInput = new Cleave('#discount-input input', {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand',
        numeralPositiveOnly: true
    });
    
    //*******************************global variable*********************************
    var inputEvents = 'DOMAttrModified textInput input keypress paste';

    //dont let user enter over these values
    var maxValue = 999999999999;
    var secondMaxValue = 99999999;

    $('input[name=CustomerPaid]').on(inputEvents, function () {
        
        if (numeral($(this).val()).value() > maxValue) {
            
            customerPaidInput.setRawValue(maxValue);
        }

    });    

    //api setting
    $.fn.api.settings.api = {
        'get products': urlSearchProduct,
        'pay invoice': urlPayInvoice
    }

    //add default invoice payment
    var firstPayment = new Payment();
    firstPayment.invoiceDataTab = "invoice1";
    var visibleDataTab = 'invoice1';
    invoiceObject.push(firstPayment);
    loadDataTab(firstPayment);

    var totalMoneyBeforeDiscount = 0;
    var currentInvoiceCount = 1;
    var totalTabRightNow = 1;
    var priceType = 1;
    var discountType = 1;
    var invoiceTableBody = saleControl.invoiceTable;

    var defaultImageUrl = "/images/no-image.png"

    var numberInput = '<div class="ui tiny input count-input" style="max-width: 80px">'
                       + '<input type="number" name="count" min="0">'
                       + '</div>';

    var normalInput = '<div class="ui tiny input price-input" style="max-width: 100px">'
                      + '<input type="text">'
                      + '</div>';

    var hiddenInput = '<input type="hidden" name="" value="" />';

    var trashIcon = '<i class="trash outline link icon" data-content="xóa"></i>';

    var tabMenu = '<div class="active item" data-tab="">'
                  + '<div class="ui grey right corner mini label close-tab">'
                  + '<i class="remove link icon"></i>'
                  + '</div>'
                  + '</div>';

    var tabSegment = '<div class="ui bottom attached active tab segment"'
                      + 'data-tab=""'
                      + 'style="height:330px;margin: 0;overflow-y: scroll;">'
                      + '<table class="ui very basic table">'
                      + '<thead>'
                      + '<tr>'
                      + '<th class="two wide">Mã hàng</th>'
                      + '<th class="six wide">Tên hàng</th>'
                      + '<th class="two wide">Số lượng</th>'
                      + '<th class="two wide">Đơn giá</th>'
                      + '<th class="three wide">Thành tiền</th>'
                      + '<th class="one wide"></th>'
                      + '</tr>'
                      + '</thead>'
                      + '<tbody></tbody>'
                      + '</div>';

    //define custom search template
    $.fn.search.settings.templates.message = function (message, type) {
        var
          html = '';
        if (message !== undefined && type !== undefined) {
            html += ''
              + '<div class="message ' + type + '">'
            ;
            // message type
            if (type == 'empty') {
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
        html += '<a class="action" href="" id="newCustomer">'
                + '<h4 class="ui violet action header">'
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
            var image = '';

            var formatedRetailPrice = numeral(product.retailPrice).format('0,0 $');
            console.log(product.imageUrl);
            if (product.imageUrl !== null) {
                image += '<div class="image" style="width: 6em;height: 5em;">'
                  + ' <img src="' + product.imageUrl + '">'
                  + '</div>';

            }
            else {
                image += '<div class="image" style="width: 6em;height: 5em;">'
                  + ' <img src="' + defaultImageUrl + '">'
                  + '</div>';

            }

            html += '<a class="result">';
            html += image
            html += '<div class="content" style="margin: 0 8em 0 0;">';
            html += '<div class="title">' + product.name + '</div>';
            html += '<div class="description">'
            html += '<i class="shop icon"></i>';
            html += 'Có thể bán: ' + product.available;
            html += '</div>'
            html += '<div class="ui divider" style="margin: 4px 0 4px 0;"></div>'
            html += '<div class="ui teal tag label price">' + formatedRetailPrice + '</div>'
            html += '<div class="ui floated right label">' + "Mã SP: " + product.id + '</div>'
            html += '</div>'
            html += '</a>'

        });

        return html;
    };

    //get vertification token
    var token = $('#token input').val();

    //*********************************global function*******************************

    $.fn.exists = function () {
        return this.length !== 0;
    }

    function showPopup(elem, msg) {
        var alertMsg = "<p style=\"color:black;font-weight:bold\">";

        alertMsg += msg + "</p>";

        $(elem).attr('data-html', alertMsg);
        $(elem).popup({
            transition: 'scale',
            position: 'left center',

            onHidden: function () {
                $(elem).popup('destroy');
            }
        }).popup('show');

    }

    //triggering oninput event to validate invoice
    function triggeringInputEventToValidate() {

        invoiceTableBody.find('tr').each(function (index, elem) {

            var inputCount = $(elem).find('td').eq(2).find('input');
            var inputValue = $(elem).find('td').eq(3).find('input');

            inputCount.trigger('input');
            inputValue.trigger('input');

        });
    }

    //validate current product on invoice
    function validateAvailableProduct(datatab) {

        var tab = $('.tab.segment[data-tab=' + datatab + ']');

        tab.find('tbody tr').each(function (index, elem) {

            $(elem).find('td').eq(2).find('input').trigger('input');

        });

    }

    function getCurrentPaymentObjectIndex(dataTab) {

        return invoiceObject.findIndex(function (elem) {
            return elem.invoiceDataTab == dataTab;
        })
    }

    //update customer information when using search box
    function updateCustomer(result) {
        var table = $("#customer-table");

        var phone = table.find("tr:eq(0)").find("td:eq(1)");
        var address = table.find("tr:eq(1)").find("td:eq(1)");

        //get current payment infomation

        var index = getCurrentPaymentObjectIndex(visibleDataTab);

        if (jQuery.isEmptyObject(result)) {

            phone.empty();
            address.empty();
            $('#customer-search input[name=CustomerId]').attr('value', 1);

            invoiceObject[index].name = "";
            invoiceObject[index].phone = "";
            invoiceObject[index].id = 1;
            invoiceObject[index].address = "";

            return;
        }

        saleControl.customerSegment.phone.text(result.phone);
        saleControl.customerSegment.address.text(result.address);
        $('#customer-search input[name=CustomerId]').attr('value', result.id);

        invoiceObject[index].name = result.name;
        invoiceObject[index].phone = result.phone;
        invoiceObject[index].id = result.id;
        invoiceObject[index].address = result.address;

    }

    //add product result to table when using search box
    function showProductsResult(response) {

        var table = $('#product-results');
        table.empty();

        $.each(response.results, function (index, result) {
            var data = '<tr>';

            //id            
            data += '<td>';
            data += '<div class="ui label">';
            data += result.id + '</div>';
            data += '</td>';

            //name
            data += '<td>';
            data += result.name;
            data += '</td>';

            var formatedValue;

            //retail price
            formatedValue = numeral(result.retailPrice).format('0,0 $');
            data += '<td>';
            data += '<div class="ui teal label">';
            data += formatedValue + '</div>';
            data += '</td>'

            //wholesale price
            formatedValue = numeral(result.wholeSaleprice).format('0,0 $');
            data += '<td>';
            data += '<div class="ui yellow label">';
            data += formatedValue + '</div>';
            data += '</td>'

            //available
            formatedValue = numeral(result.available).format('0,0');
            data += '<td class="center aligned">';
            data += '<div class="ui grey label">';
            data += formatedValue + '</div>';
            data += '</td>'

            data += '</tr>';
            table.append(data);
        })

        return true;
    }

    //set total money
    function calculateTotalMoney(countInput, priceInput, totalTd) {

        //get raw value
        var currentCountProducts = numeral(countInput.val()).value();
        var currentPrice = numeral(priceInput.val()).value();

        var totalMoney = currentPrice * currentCountProducts;

        var formatedTotalMoney = numeral(totalMoney).format('0,0 $');

        totalTd.text(formatedTotalMoney);

        //update payment
        updatePayment();

    }

    //update payment information
    function updatePayment() {

        var total = 0;

        invoiceTableBody.find('tr').each(function () {

            var formatedValue = $(this).find('td:eq(4)').text();
            var rawValue = numeral(formatedValue).value();

            total += rawValue;
        })

        totalMoneyBeforeDiscount = total;
        var totalMoneyAfterDiscount = 0;
        var discountValue = 0;
        //calculating for discount
        if (discountType == 1) {
            var discountValue = discountCleaveInput.getRawValue();
            totalMoneyAfterDiscount = totalMoneyBeforeDiscount - discountValue;
        }
        else if (discountType == 2) {
            var discountPercent = discountCleaveInput.getRawValue();
            var discountValue = discountPercent * totalMoneyBeforeDiscount / 100;
            console.log("percent:" + discountValue);
            totalMoneyAfterDiscount = totalMoneyBeforeDiscount - discountValue;
        }

        //also update current invoice tab
        var index = getCurrentPaymentObjectIndex(visibleDataTab);
        invoiceObject[index].totalValue = totalMoneyBeforeDiscount;
        invoiceObject[index].totalToPay = totalMoneyAfterDiscount;

        var formatedTotal = numeral(totalMoneyBeforeDiscount).format('0,0 $');
        var formatedFinalValue = numeral(totalMoneyAfterDiscount).format('0,0 $');

        saleControl.paymentSegment.totalValue.text(formatedTotal);
        //$('#payment-table tr:eq(0) td:eq(1)').text(formatedTotal).attr('value', total);
        saleControl.paymentSegment.customerToPay.text(formatedFinalValue);
        //$('#payment-table tr:eq(1) td:eq(1)').text(formatedTotal);

    }

    //update customer change
    function recalculateCustomerChange() {

        var customerPayTd = $('#paid-money input');
        var customerChangeTd = customerPayTd.closest('tr').next('tr').find('td:eq(1)');

        var customerPayValue = customerPayTd.val();
        console.log("pay: " + customerPayValue)
        var customerPayRawValue = numeral(customerPayValue).value();

        var totalMoneyToPay = numeral(saleControl.paymentSegment.customerToPay.text()).value();
        console.log("final value: " + totalMoneyToPay)
        var customerChange = customerPayRawValue - totalMoneyToPay;
        console.log("customer change: " + customerChange)
        //var customerChange = customerPay - totalMoneyBeforeDiscount;
        var formatedCustomerChange = numeral(customerChange).format('0,0 $');
        if (customerChange > 0) {
            customerChangeTd.text(formatedCustomerChange)
        }
        else {
            var formatedZeroChange = numeral(0).format('0,0 $');
            customerChangeTd.text(formatedZeroChange);
        }

    }

    //load data tab
    function loadDataTab(payment) {
        console.log(payment);

        //load customer              
        var table = $("#customer-table");
        var phone = table.find("tr:eq(0)").find("td:eq(1)");
        var address = table.find("tr:eq(1)").find("td:eq(1)");

        phone.text(payment.phone);
        address.text(payment.address);

        $('#customer-search').search('set value', payment.name);
        $('input[name=CustomerId]').attr('value', payment.id);

        //load payment       

        var formatedTotalValue = numeral(payment.totalValue).format('0,0 $');
        var formatedCustomerToPay = numeral(payment.totalToPay).format('0,0 $');

        saleControl.paymentSegment.totalValue.text(formatedTotalValue);
        customerPaidInput.setRawValue(payment.customerPaid);

        //$('#payment-table tr:eq(0) td:eq(1)').text(formatedTotalValue)
        //.attr('value', payment.totalValue);
        //$('#payment-table tr:eq(1) td:eq(1)').text(formatedTotalValue);
        //$('#payment-table tr:eq(2) td:eq(1) input').val(payment.customerPaid);

        //price type               
        $('#price-type').dropdown('set selected', payment.priceType);

        //discount type
        saleControl.paymentSegment.discountType.dropdown('set selected', payment.discountType);

        discountCleaveInput.setRawValue(payment.discount);
        saleControl.paymentSegment.customerToPay.text(formatedCustomerToPay);

        //also load customer change        
        recalculateCustomerChange();

    }


    //update price when user change price type
    function updatePriceType(productIds, priceType) {
        $.ajax({
            type: "post",
            url: urlPriceType,
            dataType: "json",
            traditional: true,
            data: {
                productIds: productIds,
                priceType: priceType,
                __RequestVerificationToken: token
            },
            success: function (result, status, xhr) {
                if (status === 'success') {

                    $.each(result, function (index, value) {

                        var productRow = invoiceTableBody.find('tr').filter(function () {
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
    $(".ui.dropdown").dropdown();

    $(".ui.input").on("click", 'input', function () {
        $(this).select();
    });

    $('.top.tabular.menu .item').tab();

    //update customer when search input was cleared
    $('#customer-search').on(inputEvents, 'input', function () {

        if (!$(this).val()) {
            var obj = {};
            updateCustomer(obj);

            //todo: update value for traveller (khách vãng like)
        }
    });

    //update when price type change
    $("#price-type").dropdown({
        onChange: function (value, text, choice) {

            //set global pricetype value            
            var index = getCurrentPaymentObjectIndex(visibleDataTab);
            invoiceObject[index].priceType = value;
            priceType = invoiceObject[index].priceType;

            var allPriceTd = invoiceTableBody.find('tr');
            var productIds = [];

            allPriceTd.each(function (index) {

                var id = $(this).find('td:eq(0)').text();
                productIds.push(id);
            })

            updatePriceType(productIds, value);
        }
    });

    //customer search box
    $('#customer-search').search({
        cache: true,
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
            //warning: this callback may cause bug
            console.log('on results: ');
            console.log(response.results);
            //updateCustomer(response.results)
            //todo: update value for traveller (khách vãng like)
        }
    });

    //product search box
    $('#product-input').search({
        apiSettings: {
            action: 'get products'
        },
        minCharacters: 0,
        maxResults: 4,
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

            //trigger click event on product results row
            var tdId = $('#product-results > tr > td').filter(function (index) {

                return $(this).eq(0).text() == result.id
            });
            tdId.closest('tr').trigger('click');
        },
    })

    //handle table row click event for adding product to invoice
    $('#products-table').on('click', 'tbody tr', function () {
        //event.preventDefault();               

        var invoice = invoiceTableBody
        var clickedRow = $(this);

        //get the data
        var id = clickedRow.find('td:eq(0)').text();
        var name = clickedRow.find('td:eq(1)').text();
        var price = priceType == 1 ? clickedRow.find('td:eq(2)').text() :
                                      clickedRow.find('td:eq(3)').text();

        var availableProduct = numeral(clickedRow.find('td:eq(4)').text()).value();

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
                    //alert("buying exceed the stock");
                    var msg = "Sản phẩm này chỉ còn lại " + availableProduct + " mặt hàng";
                    showPopup(tdInputCount, msg);
                }
                else {

                    tdInputCount.val(count);
                    tdInputCount.trigger('textInput');
                }

            }
            else {
                //it's the first added product

                var formatedPrice = numeral(price).format('0,0 $');

                var row = '<tr>';
                row += '<td>' + id + '</td>';
                row += '<td>' + name + '</td>';
                row += '<td>' + numberInput + '</td>'
                row += '<td>' + normalInput + '</td>';
                row += '<td style="font-weight:bold;">' + formatedPrice + '</td>'
                row += '<td>' + trashIcon + '</td>'
                row += '</tr>';

                invoice.append(row);

                //it's the first time added product, so the count equal to 1
                invoice.find('tr:last td:eq(2) input').val(1);
                //also set the price of product
                invoice.find('tr:last td:eq(3) input').val(price);

                var countInput = invoice.find('tr:last td:eq(2) input')
                var priceInput = invoice.find('tr:last td:eq(3) input')

                //new Cleave(countInput, {
                //    numeral: true,
                //    numeralThousandsGroupStyle: 'thousand',
                //    numeralPositiveOnly: true
                //})

                var cleavePriceInput = new Cleave(priceInput, {
                    numeral: true,
                    numeralThousandsGroupStyle: 'thousand',
                    numeralPositiveOnly: true
                })

                priceInput.on('click',function(){
                    $(this).select();
                })

                countInput.on('click', function () {
                    $(this).select();
                })


                //update payment
                updatePayment();
                recalculateCustomerChange();

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

                    //exceed the number of current product
                    if (currentCount > availableProduct) {
                        var msg = "Sản phẩm này chỉ còn lại " + availableProduct + " mặt hàng";
                        showPopup(inputTd, msg);
                        inputTd.val(availableProduct);
                    }

                    if (inputTd.val().length <= 0) {
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

                    if (priceTd.val().length <= 0) {
                        priceTd.val(0);
                    }
                    else if (numeral($(this).val()).value() > secondMaxValue) {
                        cleavePriceInput.setRawValue(secondMaxValue);
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
        var customerPaid = $(this).val();

        var customerPaidRawValue = numeral(customerPaid).value();

        //set value to attribute
        //$(this).attr('value', customerPay);
        if (customerPaid.length <= 0) {
            $(this).val(0);
        }

        //update for current tab        
        var index = getCurrentPaymentObjectIndex(visibleDataTab);
        invoiceObject[index].customerPaid = customerPaidRawValue;

        //alert(customerPay)
        var totalMoneyToPay = numeral(saleControl.paymentSegment.customerToPay.text()).value();
        var customerChange = customerPaidRawValue - totalMoneyToPay;
        var customerChangeFormated = numeral(customerChange).format('0,0 $');

        if (customerChange > 0) {
            customerChangeTd.text(customerChangeFormated);
        }
        else {
            var formatedZeroChange = numeral(0).format('0,0 $');
            customerChangeTd.text(formatedZeroChange);
        }
    })

    //bind event for create new customer action in search box product
    $('#customer-results').on('click', '#newCustomer', function (event) {

        event.preventDefault();

        var modal = $('#create-modal');

        $.ajax({
            type: "get",
            url: urlCreateCustomer,
            success: function (result, status, xhr) {

                if (status === 'success') {

                    modal.html(result);
                    modal.modal('setting', 'closable', false).modal('show');

                }
            },
            error: function (xhr, status, error) {

            }
        })

    })

    //create invoice
    $('#pay').click(function (event) {

        event.preventDefault();
        triggeringInputEventToValidate();
        var thisBtn = $(this);
        thisBtn.addClass('loading');

        var customerId = $('input[name=CustomerId]').val();
        var staff = $('input[name=StaffId]').val();
        //var totalValue = $('#payment-table tr:eq(0) td:eq(1)').text();
        var totalValue = $('#payment-table tr:eq(0) td:eq(1)').text();
        var customerPaid = $('input[name=CustomerPaid]').val();

        var rawTotalValue = numeral(totalValue).value();
        var rawCustomerPaid = numeral(customerPaid).value();
        var discount = 0;
        if (discountType == 1) {
            discount = discountCleaveInput.getRawValue();
        }
        else if (discountType == 2) {
            var percent = discountCleaveInput.getRawValue();
            discount = percent * rawTotalValue / 100;
        }

        //some client logics check before submit invoice
        if (rawTotalValue <= 0) {

            $('#zero-value-modal').modal('show');
            thisBtn.removeClass('loading');
            return;
        }
        else if (invoiceTableBody.find('tr').length === 0) {

            $('#zero-product-modal').modal('show');
            thisBtn.removeClass('loading');
            return;
        }

        var invoiceDetail = {
            CustomerId: customerId,
            Staff: staff,
            TotalValue: rawTotalValue,
            CustomerPaid: rawCustomerPaid,
            Discount: discount,
            productDetails: [],
            __RequestVerificationToken: token
        }

        //save this for later print
        __valueCustomerPay = rawCustomerPaid;


        invoiceTableBody.find('tr').each(function (index, elem) {

            var productId = $(elem).find('td:eq(0)').text();
            var count = $(elem).find('td:eq(2) input').val();
            var price = $(elem).find('td:eq(3) input').val();


            var rawCount = numeral(count).value();
            var rawPrice = numeral(price).value();

            var product = {
                ProductId: productId,
                Count: rawCount,
                Price: rawPrice
            }

            invoiceDetail.productDetails.push(product);

        })

        var modal = $('#notify-modal');
        //+"?" + token.attr('name') + "=" + token.val()
        $.ajax({
            type: "post",
            url: urlPayInvoice,
            data: invoiceDetail,
            success: function (result, status, xhr) {

                //active dimmer
                activeProductResultsDimmer()

                if (status === 'success') {
                    thisBtn.removeClass('loading');
                    modal.html(result);
                    modal.modal('setting', 'closable', false).modal('show');

                    //close this tab
                    var tab = $('.tabular.menu .item[data-tab=' + visibleDataTab + ']');
                    //tab.find('.label.close-tab .icon').trigger('click');
                    closeCurrentTab(tab);

                    //update product results when success add invoice
                    updateProductResult();

                }
            },
            error: function (xhr, status, error) {
                thisBtn.removeClass('loading');
                //inactive dimmer
                inactiveProductResultsDimmer()
            }
        });

    })

    //reload page when modal was hidden (temporarily solution)
    $('#notify-modal').modal({
        onHidden: function () {
            //location.reload();
        }
    });

    //bind click event to close tab
    $('.top.tabular.menu').on('click', '.label.close-tab .icon', function (event) {
        event.preventDefault();

        var elem = $(this);
        var thisTab = elem.closest('.item');

        var confirmModal = $('#close-invoice-modal');
        //confirmModal.modal('setting', 'closable', false);
        confirmModal.modal({
            closable: false,
            onApprove: function (element) {

                closeCurrentTab(thisTab);

            }
        }).modal('show')
    })

    //close tab
    function closeCurrentTab(tab) {

        //switching to closest tab, if thisTab is the one left then create new tab
        var closestTab = null;

        if (tab.next('.item[data-tab]').exists()) {

            closestTab = tab.next()
        }
        else if (tab.prev('.item[data-tab]').exists()) {
            closestTab = tab.prev('.item[data-tab]');
        }
        else {

            //create new one
            $('#newInvoice .button').trigger('click');
            //then close the previous
            closeDataTab(tab);
            return;
        }

        closeDataTab(tab);
        selectTab(closestTab);

        //change state of add tab button
        totalTabRightNow -= 1;
        if ($('#newInvoice .button').hasClass('disabled')) {

            if (totalTabRightNow < 7) {

                $('#newInvoice .button').removeClass('disabled');
            }
        }

    }


    //helper function to click on tab
    function selectTab(tab) {
        $(tab).click();
    }

    //helper function to close tab data
    function closeDataTab(tab) {
        var dataTab = tab.attr('data-tab');

        //remove tab
        tab.remove();

        //also remove attached table       
        var attachedTable = $('.tab.segment[data-tab=' + dataTab + ']').remove();

        //remove payment data object from global array
        invoiceObject = jQuery.grep(invoiceObject, function (value) {
            return value.invoiceDataTab != dataTab;
        })
    }

    //bind event to add new tab
    $('#newInvoice .button').on('click', function () {

        var dataTab = "invoice";
        var dataText = "Đơn hàng ";
        currentInvoiceCount += 1;

        dataTab += currentInvoiceCount;
        dataText += currentInvoiceCount;

        //set global variable
        visibleDataTab = dataTab;

        //add tab menu
        $('.tabular.menu .active.item').removeClass('active');
        $('.tabular.menu .item[data-tab]').filter(':last').after(tabMenu);
        $('.tabular.menu .active.item').attr('data-tab', dataTab).append(dataText);

        //add tab segment
        $('.active.tab.segment').removeClass('active');
        $('.tab.segment').filter(':last').after(tabSegment);

        var tabSegmentAdded = $('.tab.segment').filter(':last');
        tabSegmentAdded.attr('data-tab', dataTab);

        //hold the customer info, payment for this tabs
        var payment = new Payment();
        payment.invoiceDataTab = dataTab;

        //save payment info for this tab
        invoiceObject.push(payment);

        //reupdate global variable
        invoiceTableBody = $('.active.tab.segment tbody');
        loadDataTab(payment);
        updatePayment();

        updateTabCloseIcon();

        //todo: update ui when switching tab
        $('.top.tabular.menu .item').tab({
            onVisible: function (tabpath) {

                //reupdate global variable, payment
                visibleDataTab = tabpath;
                invoiceTableBody = $('.active.tab.segment tbody');
                updateTabCloseIcon()

                //load data               
                var paymentArr = $.grep(invoiceObject, function (e) {
                    return e.invoiceDataTab == tabpath;
                });

                loadDataTab(paymentArr[0]);
                updatePayment();

                validateAvailableProduct(tabpath);
            }
        });

        //only have seven tab at the same time
        totalTabRightNow += 1;
        console.log(totalTabRightNow);
        if (totalTabRightNow >= 7) {

            $(this).addClass('disabled');
        }

    });

    function updateTabCloseIcon() {

        $('.top.tabular.menu .item[data-tab]').not('.item[data-tab=' + visibleDataTab + ']')
            .each(function (index, elem) {

                $(elem).find('.close-tab').css('display', 'none');
            })

        $('.top.tabular.menu .item[data-tab=' + visibleDataTab + ']').
            find('.close-tab').css('display', 'inline-block');
    }

    //reload product result table 
    function updateProductResult() {
        $.ajax({
            type: "post",
            url: urlUpdateSearchProduct,
            data: '',
            success: function (result, status, xhr) {

                if (status === 'success') {

                    //todo: show status bar                   
                    showProductsResult(result);
                    //inactive dimmer
                    inactiveProductResultsDimmer()
                }
            },
            error: function (xhr, status, error) {
                //idk what to do right here
                //inactive dimmer
                inactiveProductResultsDimmer()
            }
        });
    }

    // active/remove dimmer for product results table
    function activeProductResultsDimmer() {
        $('.ui.inverted.dimmer').addClass('active');
    }
    function inactiveProductResultsDimmer() {
        $('.ui.inverted.dimmer').removeClass('active');
    }

    //handling discount input entering
    $('#discount-input').on(inputEvents, 'input', function () {

        var value = discountCleaveInput.getRawValue();

        if (value.length <= 0) {
            $(this).val(0);
        }

        if (discountType == 1) {
            if (value > totalMoneyBeforeDiscount) {
                discountCleaveInput.setRawValue(totalMoneyBeforeDiscount);
            }
        }
        else if (discountType == 2) {
            if (value > 100) {
                discountCleaveInput.setRawValue(100);
            }
        }

        var index = getCurrentPaymentObjectIndex(visibleDataTab);
        invoiceObject[index].discount = discountCleaveInput.getRawValue();

        updatePayment();
        recalculateCustomerChange();

    });

    //hanling discount type when it has changed
    $('#discount-type').dropdown({
        onChange: function (value, text, choice) {

            var index = getCurrentPaymentObjectIndex(visibleDataTab);
            invoiceObject[index].discountType = value;
            discountType = invoiceObject[index].discountType;

            if (value == 2) {
                if (discountCleaveInput.getRawValue() > 100) {
                    discountCleaveInput.setRawValue(0);

                    var index = getCurrentPaymentObjectIndex(visibleDataTab);
                    invoiceObject[index].discount = discountCleaveInput.getRawValue();

                }
            }
            updatePayment();
            recalculateCustomerChange();
        }
    });
});