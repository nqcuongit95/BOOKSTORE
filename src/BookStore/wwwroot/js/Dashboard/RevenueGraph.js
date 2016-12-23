
var ctx = document.getElementById("revenueChart");
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: timelines,
        datasets: [{
            fill: false,            
            label: 'Doanh thu',
            data: values,
            borderColor: 'rgb(66, 134, 244)',
            backgroundColor: 'rgba(66, 134, 244, 0.4)',
            borderWidth: 2
        }]
    },
    options: {
        responsive: true,
        title: {
            display: true,
            text: "Doanh thu bán hàng"
        },
        legend: {
            position: 'bottom'
        },
        scales: {
            xAxes: [{
                display: true                
            }],
            yAxes: [{
                display: true,
                position: 'left',
                ticks: {
                    beginAtZero: true,
                    callback: function (label) {
                        return numeral(label).format('0a');
                    }
                }
            }]
            
        },
        tooltips: {
            mode: 'nearest',
            intersect: false,
            callbacks: {
                label: function (tooltipItem, data) {
                    return numeral(tooltipItem.yLabel).format('0,0$');
                }
            }
        }        
    }
});
