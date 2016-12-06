
Chart.defaults.global.defaultFontSize = 16;

var ctx = document.getElementById("customerChart");
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: month,
        datasets: [{
            fill: false,
            label: 'Số Lượng Khách Hàng',
            data: data,
            borderColor: 'rgb(255, 99, 132)',
            backgroundColor: 'rgb(255, 99, 132)',
            borderWidth: 2
        }]
    },
    options: {
        responsive: true,
        title: {
            display: true,
            text: "Biểu Đồ Số Lượng Khách Hàng"
        },
        legend: {
            position: 'bottom'
        },
        scales: {
            xAxes: [{
                display: true,
            }],
            yAxes: [{
                display: true,
                position: 'left'
            }]
        },
        tooltips: {
            mode: 'nearest',
            intersect: false
        }
    }
});