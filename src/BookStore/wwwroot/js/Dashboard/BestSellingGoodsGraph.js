
Chart.defaults.global.defaultFontSize = 14;

var ctx = document.getElementById("bestSellingGoods");

var formatedProducts = [];
var maxWidth = 20;

$.each(products, function (index, value) {

    var str = formatLabel(value, 36);
    formatedProducts.push(str);

})

var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: formatedProducts,
        datasets: [
        {
            label: "Đã bán",
            backgroundColor: [
                'rgba(255, 99, 132, 0.9)',
                'rgba(54, 162, 235, 0.9)',
                'rgba(255, 206, 86, 0.9)',
                'rgba(75, 192, 192, 0.9)',
                'rgba(153, 102, 255, 0.9)',
                'rgba(255, 159, 64, 0.9)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1,
            data: solds,
        }
        ]
    },
    options: {
        responsive: true,
        title: {
            display: true,
            text: "Mặt Hàng Bán Chạy"
        },
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                display: false,
            }],
            yAxes: [{
                display: true,
                position: 'left',
                ticks: {                    
                    userCallback: function (label, index, labels) {
                        if (Math.floor(label) === label) {
                            return label;
                        }

                    }
                }
            }]
        },
        tooltips: {
            mode: 'nearest',
            intersect: true
        }
    }
});