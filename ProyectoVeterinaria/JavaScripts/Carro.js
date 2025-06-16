$('#carritoModal').on('show.bs.modal', function () {
    console.log("El modal se está abriendo...");

    $.ajax({
        url: '/Carro/ObtenerCarrito',
        type: 'GET',
        success: function (data) {
            console.log("Datos recibidos:", data);
            const list = $('#cart-items-list');
            list.empty();

            if (data.length === 0) {
                list.append('<li class="list-group-item">No hay productos en el carrito.</li>');
            } else {
                let total = 0;

                data.forEach(function (producto) {
                    const imagenHtml = producto.ImagenBase64
                        ? `<img src="${producto.ImagenBase64}" alt="Imagen" class="img-thumbnail me-2" style="width: 50px; height: 50px;" />`
                        : '';

                    const subtotal = producto.SubTotal.toFixed(2);
                    total += producto.SubTotal;

                    list.append(`
                        <li class="list-group-item d-flex align-items-center justify-content-between">
                            <div class="d-flex align-items-center">
                                ${imagenHtml}
                                <div>
                                    <strong>${producto.Nombre}</strong><br />
                                    Cantidad: ${producto.Cantidad} - Precio: $${producto.Precio} <br />
                                    Subtotal: $${subtotal}
                                </div>
                            </div>
                        </li>
                    `);
                });

                list.append(`
                    <li class="list-group-item text-end">
                        <strong>Total: $${total.toFixed(2)}</strong>
                    </li>
                `);
            }
        },
        error: function () {
            alert('Error al cargar el carrito.');
        }
    });
});



function actualizarContadorCarrito() {
    console.log("Llamando al contador de carrito...");
    $.ajax({
        url: '/Carro/ContarItemsCarrito', 
        type: 'GET',
        success: function (cantidad) {
            $('#count-items').text(cantidad);
        },
        error: function () {
            console.error('No se pudo obtener el contador del carrito.');
        }
    });
}

$(document).ready(function () {
    actualizarContadorCarrito();
});
