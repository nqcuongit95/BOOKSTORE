$(document).ready(function () {

    class Payment {
        constructor() {
            this.id = 1;
            this.name = "";
            this.phone = "";
            this.address = "";
            this.priceType = 1;
            this.totalValue = 0;
            this.totalToPay = 0;
            this.customerPaid = 0;
            this.invoiceDataTab = "";
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
            customerToPlay: $('#payment-table tr').eq(1).find('td').eq(1),
            customerPaid: $('input[name=CustomerPaid]'),
        },
    }


    //*******************************global variable*********************************
    var inputEvents = 'DOMAttrModified textInput input keypress paste';

    //api setting
    $.fn.api.settings.api = {
        'get products': urlSearchProduct,
        'pay invoice': urlPayInvoice
    }

    var invoiceObject = [];

    //add default invoice payment
    var firstPayment = new Payment();
    firstPayment.invoiceDataTab = "invoice1";
    var visibleDataTab = 'invoice1';
    invoiceObject.push(firstPayment);
    loadDataTab(firstPayment);

    var totalMoneyToPay = 0;
    var currentInvoiceCount = 1;
    var priceType = 1;
    var invoiceTableBody = saleControl.invoiceTable;
    

    var numberInput = '<div class="ui tiny input count-input" style="max-width: 80px">'
                       + '<input type="number" name="count" min="0">'
                       + '</div>';

    var normalInput = '<div class="ui tiny input price-input" style="max-width: 100px">'
                      + '<input type="text">'
                      + '</div>';

    var hiddenInput = '<input type="hidden" name="" value="" />';

    var trashIcon = '<i class="trash outline link icon" data-content="xóa"></i>';

    var tabMenu = '<div class="active item" data-tab="">'
                  + '<a class="ui grey right corner mini label close-tab">'
                  + '<i class="remove icon"></i>'
                  + '</a>'
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
                + '<h4 class="ui blue action header">'
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

        invoiceTableBody.find('tr').each(function () {

            var value = Number($(this).find('td:eq(4)').text());

            total += value;
        })

        totalMoneyToPay = total;

        //also update current invoice tab
        var index = getCurrentPaymentObjectIndex(visibleDataTab);
        invoiceObject[index].totalValue = totalMoneyToPay;
        invoiceObject[index].totalToPay = totalMoneyToPay;

        $('#payment-table tr:eq(0) td:eq(1)').text(total).attr('value', total);

        $('#payment-table tr:eq(1) td:eq(1)').text(total);

    }

    //update customer change
    function recalculateCustomerChange() {

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
        $('#payment-table tr:eq(0) td:eq(1)').text(payment.totalValue)
                                             .attr('value', payment.totalValue);
        $('#payment-table tr:eq(1) td:eq(1)').text(payment.totalValue);
        $('#payment-table tr:eq(2) td:eq(1) input').val(payment.customerPaid);

        //price type               
        $('#price-type').dropdown('set selected', payment.priceType);
        
        //also load customer change
        recalculateCustomerChange()

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
                priceType: priceType
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
    $(".ui.selection.dropdown").dropdown();
    
    $(".ui.input").on("click",'input', function () {
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

        var invoice = invoiceTableBody
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

        //update for current tab        
        var index = getCurrentPaymentObjectIndex(visibleDataTab);        
        invoiceObject[index].customerPaid = customerPay;

        //alert(customerPay)
        var customerChange = customerPay - totalMoneyToPay;
        
        if (customerChange > 0) {
            customerChangeTd.text(customerChange);
        }
        else {
            customerChangeTd.text('0');
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

        invoiceTableBody.find('tr').each(function (index, elem) {

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

    //reload page when model was hidden (temporarily solution)
    $('#notify-modal').modal({
        onHidden: function () {
            //location.reload();
        }
    });

    //bind click event to close tab
    $('.top.tabular.menu').on('click', '.label.close-tab', function (event) {
        event.preventDefault();
        //alert('click')

        var thisTab = $(this).closest('.item');

        //switching to closest tab, if thisTab is the one left then create new tab
        var closestTab = null;

        if (thisTab.next('.item[data-tab]').exists()) {

            closestTab = thisTab.next()
        }
        else if (thisTab.prev('.item[data-tab]').exists()) {
            closestTab = thisTab.prev('.item[data-tab]');
        }
        else {
            
            //create new one
            $('#newInvoice').trigger('click');
            //then close the previous
            closeTab(thisTab);
            return;
        }
        closeTab(thisTab);
        selectTab(closestTab);
    })

    //helpter function to click on tab
    function selectTab(tab) {
        $(tab).click();
    }

    //helper function to close tab
    function closeTab(tab) {
        var dataTab = tab.attr('data-tab');

        //remove tab
        tab.remove();

        //also remove attached table       
        var attachedTable = $('.tab.segment[data-tab=' + dataTab + ']').remove();
    }

    //bind event to add new tab
    $('#newInvoice').on('click', function () {

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
        updatePayment();
        loadDataTab(payment);

        //todo: update ui when switching tab
        $('.top.tabular.menu .item').tab({
            onVisible: function (tabpath) {                
                //reupdate global variable, payment
                visibleDataTab = tabpath;
                invoiceTableBody = $('.active.tab.segment tbody');
                
                updatePayment();

                //load data               
                var paymentArr = $.grep(invoiceObject, function (e) {
                    return e.invoiceDataTab == tabpath;
                });
                //var test = $(this).tab('get path');
                
                loadDataTab(paymentArr[0])
            }
        });
        
    })

});