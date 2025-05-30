document.addEventListener("DOMContentLoaded", function () {
    const ctx = document.getElementById('ventasChart').getContext('2d');

    const rawLabels = JSON.parse(document.getElementById('ventasChart').dataset.labels);
    const data = JSON.parse(document.getElementById('ventasChart').dataset.valores);

    function formatLabels(labels) {
        const meses = [
            "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
            "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
        ];
        return labels.map(label => {
            const [year, month] = label.split('-');
            const mesNombre = meses[parseInt(month, 10) - 1];
            return `${year} - ${mesNombre}`;
        });
    }

    const formattedLabels = formatLabels(rawLabels);

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: formattedLabels,
            datasets: [{
                label: 'Total de Ventas por Mes (S/.)',
                data: data,
                backgroundColor: 'rgba(54, 162, 235, 0.7)',
                borderColor: 'rgba(54, 162, 235, 1)',        
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false, 
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});
