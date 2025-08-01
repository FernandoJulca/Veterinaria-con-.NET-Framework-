﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionProductos
    {
        private readonly ProductoDTO _producto;
        private readonly CategoriaDTO _categoria;
        private readonly EstadoDTO _estado;

        public GestionProductos()
        {
            _producto = new ProductoDTO();
            _categoria = new CategoriaDTO();
            _estado = new EstadoDTO();
        }

        public async Task<List<ListadoProductos>> ObtenerListadoProductos()
        {
            var productos = await _producto.Listar();
            var categorias = await _categoria.Listar();
            var estados = await _estado.Listar();

            return productos.Select(p => new ListadoProductos
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                ImagenBase64 = p.ImagenBase64,
                IdCategoria = p.IdCategoria,
                Categoria = categorias.FirstOrDefault(c => c.IdCategoria == p.IdCategoria)?.NombreCategoria ?? "Sin categoria",
                Precio = p.Precio,
                Stock = p.Stock,
                IdEstado = p.IdEstado,
                NombreEstado = estados.FirstOrDefault(e => e.IdEstado == p.IdEstado)?.Descripcion ?? "Sin estado",
                flgEliminado = p.flgEliminado
            }).ToList();
        }

        public async Task<List<ListadoProductos>> ListarProductosPorCategoria(int id)
        {
            var productos = await _producto.ListarPorCategoria(id);
            var categorias = await _categoria.Listar();
            var estados = await _estado.Listar();
            return productos.Select(p => new ListadoProductos
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                ImagenBase64 = p.ImagenBase64,
                IdCategoria = p.IdCategoria,
                Categoria = categorias.FirstOrDefault(c => c.IdCategoria == p.IdCategoria)?.NombreCategoria ?? "Sin categoria",
                Precio = p.Precio,
                Stock = p.Stock,
                IdEstado = p.IdEstado,
                NombreEstado = estados.FirstOrDefault(e => e.IdEstado == p.IdEstado)?.Descripcion ?? "Sin estado",
                flgEliminado = p.flgEliminado
            }).ToList();
        }

        public async Task<List<ListadoProductos>> ListarProductosPorNombre(string nombre)
        {
            var productos = await _producto.ListarPorNombre(nombre);
            var categorias = await _categoria.Listar();
            var estados = await _estado.Listar();
            return productos.Select(p => new ListadoProductos
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                ImagenBase64 = p.ImagenBase64,
                IdCategoria = p.IdCategoria,
                Categoria = categorias.FirstOrDefault(c => c.IdCategoria == p.IdCategoria)?.NombreCategoria ?? "Sin categoria",
                Precio = p.Precio,
                Stock = p.Stock,
                IdEstado = p.IdEstado,
                NombreEstado = estados.FirstOrDefault(e => e.IdEstado == p.IdEstado)?.Descripcion ?? "Sin estado",
                flgEliminado = p.flgEliminado
            }).ToList();
        }

        public async Task<List<ListadoProductos>> ListarProductosEntrePrecios(decimal preciomin,decimal preciomax)
        {
            var productos = await _producto.ListarEntrePrecios(preciomin, preciomax);
            var categorias = await _categoria.Listar();
            var estados = await _estado.Listar();
            return productos.Select(p => new ListadoProductos
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                ImagenBase64 = p.ImagenBase64,
                IdCategoria = p.IdCategoria,
                Categoria = categorias.FirstOrDefault(c => c.IdCategoria == p.IdCategoria)?.NombreCategoria ?? "Sin categoria",
                Precio = p.Precio,
                Stock = p.Stock,
                IdEstado = p.IdEstado,
                NombreEstado = estados.FirstOrDefault(e => e.IdEstado == p.IdEstado)?.Descripcion ?? "Sin estado",
                flgEliminado = p.flgEliminado
            }).ToList();
        }

        public async Task<string> AgregarProducto(Producto p)
        {
            return await _producto.Agregar(p);
        }

        public async Task<string> ActualizaProducto(Producto p)
        {
            return await _producto.Actualizar(p);
        }

        public async Task<Producto> BuscarProducto(int id)
        {
            return await _producto.Buscar(id);
        }

        public async Task<string> EliminarProducto(int p)
        {
            return await _producto.Eliminar(p);
        }
    }
}
