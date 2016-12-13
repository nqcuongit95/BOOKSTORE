$(document).ready(function () {

    //define some global variable
    var inputEvents = 'DOMAttrModified textInput input keypress paste';

    var recentlyAddedRow;

    var totalMoneyToPay = 0;
    
    var numberInput = "<div class=\"ui tiny input count-input\" style=\"max-width: 80px\">"
                       + "<input type=\"number\" name='count'>"
                        + "</div>"

    var normalInput = "<div class=\"ui tiny input price-input\" style=\"max-width: 100px\">"
                      + "<input type=\"text\">"
                       + "</div>"


    $.fn.exists = function () {
        return this.length !== 0;
    }

    var g_productTable = $('.ui.bottom.attached.segment');

    $('.top.menu .item').tab();

    $('.ui.fluid.search').search({
        apiSettings: {
            url: urlSearch + "?val={query}"
        },
        fields: {
            title: 'name',
            description: 'phone'
        },
        minCharacters: 3,
        onSelect: function (result, response) {
            updateCustomer(result);
        }
    });

    function updateCustomer(result) {
        var table = $("#customer-table");

        var phone = table.find("tr:eq(0)").find("td:eq(1)");
        var address = table.find("tr:eq(1)").find("td:eq(1)");

        phone.text(result.phone);
        address.text(result.address);
    }

    //search products
    $.fn.api.settings.api = {
        'get products': urlSearchProduct
    }

    $('#product-input').api({
        action: 'get products',
        throttle: 200,
        onSuccess: function (response) {
            showProductsResult(response);
        },
        onRequest: function () {
            $('.transparent.icon.input').addClass('loading');
        },
        onComplete: function () {
            $('.transparent.icon.input').removeClass('loading');
        }
    })

    function showProductsResult(response) {

        var table = $('#product-results');

        table.empty();

        $.each(response, function (index, value) {

            var data = '<tr>';

            $.each(value, function (key, value) {
                if (value != null) {
                    data += '<td>' + value + '</td>';
                }
            })

            data += '</tr>';
            table.append(data);
        })

    }

    //handle table row click event
    $('#products-table').on('click', 'tbody tr', function () {
        //event.preventDefault();        

        var invoice = $('#invoice-table');
        var clickedRow = $(this);

        //get the data
        var id = clickedRow.find('td:eq(0)').text();
        var name = clickedRow.find('td:eq(1)').text();
        var price = clickedRow.find('td:eq(2)').text();
        var availableProduct = clickedRow.find('td:eq(3)').text();

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
                row += "<td>Xóa</td>"
                row += "</tr>";

                invoice.append(row);

                //it's the first time added product, so the count equal to 1
                invoice.find('tr:last td:eq(2) input').val('1');
                //also set the price of product
                invoice.find('tr:last td:eq(3) input').val(price);

                //update payment
                updatePayment();

                //bind input event to count td;
                invoice.find('tr:last').on(inputEvents, '.count-input input', function () {

                    var inputTd = $(this);

                    var currentCount = Number(inputTd.val());

                    if (currentCount > availableProduct) {
                        alert("number of products exceed the stock");
                        inputTd.val(availableProduct);
                    }

                    var countTd = $(this);
                    var currentPriceTd = countTd.closest('td').next().find('input');
                    var totalMoneyTd = currentPriceTd.closest('td').next();

                    calculateTotalMoney(countTd, currentPriceTd, totalMoneyTd);

                })

                //bind input event to price td;
                invoice.find('tr:last').on(inputEvents, '.price-input input', function () {

                    var priceTd = $(this);
                    var totalMoneyTd = priceTd.closest('td').next();
                    var currentCountTd = priceTd.closest('td').prev().find('input');

                    calculateTotalMoney(currentCountTd, priceTd, totalMoneyTd);

                })
            }

        }
        else {


        }
    })

    //set total money
    function calculateTotalMoney(count, price, total) {

        var currentCountProducts = Number(count.val());

        var currentPrice = Number(price.val());

        var totalMoney = currentPrice * currentCountProducts;

        total.text(totalMoney);

        //update payment
        updatePayment();
                
    }

    function updatePayment() {

        var total = 0;
        $('#invoice-table tr').each(function () {
            
            var value = Number($(this).find('td:eq(4)').text());
            
            total += value;            
        })

        totalMoneyToPay = total;

        $('#payment-table tr:eq(0) td:eq(1)').text(total);

        $('#payment-table tr:eq(1) td:eq(1)').text(total);
               
    }

    //handle customer pay
    $('#paid-money').on(inputEvents, 'input', function () {
                                
            var customerChangeTd = $(this).closest('tr').next().find('td:eq(1)');
            var customerPay = Number($(this).val());
            //alert(customerPay)
            var customerChange = customerPay - totalMoneyToPay;
            console.log(customerChange);
            if (customerChange > 0) {
                customerChangeTd.text(customerChange);
            }
            else {
                customerChangeTd.text('0');
            }                  
    })

});