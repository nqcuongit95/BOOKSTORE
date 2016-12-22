
Chart.defaults.global.defaultFontSize = 16;

var ctx = document.getElementById("bestSellingGoods");

var myChart = new Chart(ctx, {
    type: 'bar',
    data: {        
        labels: products,
        datasets: [
        {
            label: "Mặt Hàng",
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
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