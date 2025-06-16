$(document).ready(function () {
    var categoriaSeleccionada = 0;

    function cargarProductos(categoriaId, pagina = 1) {
        categoriaSeleccionada = categoriaId;

        $.ajax({
            url: 'https://localhost:44333/Producto/ListarProductosPorCategoria',
            data: { id: categoriaId, page: pagina },
            type: 'GET',
            success: function (response) {
                var productos = response.productos;
                var html = '';

                if (productos.length === 0) {
                    html = '<div class="col-12"><p class="text-center">No se encontraron productos.</p></div>';
                } else {
                    productos.forEach(function (producto) {
                        const src = producto.ImagenBase64
                            ? `data:image/png;base64,${producto.ImagenBase64}`
                            : `/imagenes/Productos/P${producto.IdProducto}.jpg`;
                        html += `
                            <div class="col-12 col-sm-6 col-md-4 col-lg-3 producto-item">
    <div class="card h-100 shadow-sm border-0 rounded-4">
<img src="${src}" class="card-img-top rounded-top-4" alt="${producto.NombreProducto}"
                     style="object-fit: cover; height: 200px;"
                     onerror="this.onerror=null; this.src='/imagenes/no-imagen.png';">
        <div class="card-body d-flex flex-column px-3">
            <h5 class="card-title text-center text-secondary fw-bold" style="font-family: 'Comic Sans MS', cursive;">
                🐾 ${producto.NombreProducto}
            </h5>

            <p class="card-text text-center text-muted small mb-2" style="font-style: italic;">
                ${producto.Categoria}
            </p>

            <div class="d-flex justify-content-between align-items-center mt-auto pt-2 border-top">
                <span class="fw-bold text-success fs-6">S/. ${producto.Precio.toFixed(2)}</span>
                <a href="/Carro/AgregarAlCarrito/${producto.IdProducto}" class="btn btn-sm btn-outline-primary rounded-pill">
    Comprar
</a>

            </div>
        </div>
    </div>
</div>
`;
                    });
                }

                $('#productos-listado').html(html);

                var paginacionHtml = '';
                for (var i = 1; i <= response.totalPaginas; i++) {
                    paginacionHtml += `<button 
    class="btn btn-sm mx-1 btn-pagina ${i === response.paginaActual ? 'pagina-activa' : 'pagina-normal'}" 
    data-pagina="${i}">
    ${i}
  </button>`;
                }

                $('#paginacion').html(paginacionHtml);
            },
            error: function () {
                alert('Error al cargar los productos.');
            }
        });
    }

    $('.button-categorias').on('click', function () {
        var categoriaId = $(this).data('id');

        $('.button-categorias').removeClass('active');
        $(this).addClass('active');

        cargarProductos(categoriaId, 1);
    });

    $(document).on('click', '.btn-pagina', function () {
        var pagina = $(this).data('pagina');
        cargarProductos(categoriaSeleccionada, pagina);
    });

    cargarProductos(0, 1);
});
