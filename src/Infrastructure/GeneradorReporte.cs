using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure {
    public class GeneradorReportes {
        private readonly IEnumerable<Producto> _productos;

        public GeneradorReportes(IEnumerable<Producto> productos) {
            _productos = productos;
        }

        public string GenerarResumen() {
            var sb = new StringBuilder();
            sb.AppendLine("=== Resumen del Inventario ===");
            sb.AppendLine($"Total de Productos: {_productos.Count()}");
            sb.AppendLine($"Valor Total del Inventario: ${_productos.Sum(p => p.ValorTotal):N2}");

            var ProductosPorCategoria = _productos.GroupBy(p => p.Categoria)
                .Select(g => new {
                    Categoria = g.Key,
                    Cantidad = g.Count(),
                    ValorTotal = g.Sum(p => p.ValorTotal)
                });
            
            sb.AppendLine("\nProductos por Categoría:");

            foreach (var categoria in ProductosPorCategoria) {
                sb.AppendLine($"- {categoria.Categoria}: {categoria.Cantidad} productos, Valor Total: ${categoria.ValorTotal:N2}");
            }

            return sb.ToString();
        }

        public string GenerarReporteStockBajo(int umbral = 5) {
            var sb = new StringBuilder();
            sb.AppendLine($"=== Reporte de Productos con Stock Bajo (< {umbral}) ===");

            var productosStockBajo = _productos.Where(p => p.Cantidad < umbral)
            .OrderBy(p => p.Cantidad);

            if (!productosStockBajo.Any()) {
                sb.AppendLine("No hay productos con stock bajo.");
                return sb.ToString();
            } 

            foreach (var producto in productosStockBajo) {
                sb.AppendLine($"- {producto.Id}: {producto.Nombre} (Categoría: {producto.Categoria}, Cantidad: {producto.Cantidad}, Precio: ${producto.Precio:N2})");
            }

            return sb.ToString();
        }

        public string GenerarTopProductos(int topN = 5) {
            var sb = new StringBuilder();
            sb.AppendLine($"=== Top {topN} Productos por Valor Total ===");

            var TopProductos = _productos.OrderByDescending(p => p.ValorTotal).Take(topN);

            if (!TopProductos.Any()) {
                sb.AppendLine("No hay productos en el inventario.");
                return sb.ToString();
            }

            int posicion = 1;

            foreach (var producto in TopProductos) {
                sb.AppendLine($"{posicion}. {producto.Nombre} (Categoría: {producto.Categoria}, Cantidad: {producto.Cantidad}, Precio: ${producto.Precio:N2}, Valor Total: ${producto.ValorTotal:N2})");
                posicion++;
            }

            return sb.ToString();
        }

        public string ExportarCsv() {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Nombre,Categoria,Estado,Cantidad,Precio,ValorTotal");
            foreach (var producto in _productos) {
                sb.AppendLine($"{producto.Id},{producto.Nombre},{producto.Categoria},{producto.Estado},{producto.Cantidad},{producto.Precio:N2},{producto.ValorTotal:N2}");
            }
            return sb.ToString();
        }

        public string ExportarResumenJson() {
            var resumen = new {
                TotalProductos = _productos.Count(),
                ValorTotalInventario = _productos.Sum(p => p.ValorTotal),
                ProductosPorCategoria = _productos.GroupBy(p => p.Categoria)
                    .Select(g => new {
                        Categoria = g.Key,
                        Cantidad = g.Count(),
                        ValorTotal = g.Sum(p => p.ValorTotal)
                    }),
                Top5Productos = _productos.OrderByDescending(p => p.ValorTotal).Take(5)
                    .Select(p => new {
                        p.Id,
                        p.Nombre,
                        p.Categoria,
                        p.Estado,
                        p.Cantidad,
                        p.Precio,
                        p.ValorTotal
                    })
            };
            return JsonSerializer.Serialize(resumen, new JsonSerializerOptions {
                WriteIndented = true
            });
        }
    }
}