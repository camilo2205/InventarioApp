using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;
using InventarioApp.Factories;

namespace InventarioApp.Services {
    public class InventarioServices {
        private readonly InMemoryProductoRepository _repository;
        private readonly JsonInventarioStorage _storage;
        private readonly string _storageFilePath;

        public InventarioServices(string storageFilePath = "inventario.json") {
            _repository = new InMemoryProductoRepository();
            _storage = new JsonInventarioStorage();
            _storageFilePath = storageFilePath;

            CargarInventario();
        }

        private void CargarInventario() {
            var productos = _storage.Cargar(_storageFilePath);
            foreach (var producto in productos) {
                _repository.Agregar(producto);
            }
        }

        public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria) {
            var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
            _repository.Agregar(producto);
            _Persistir();
        }

        public IEnumerable<Producto> ObtenerProductos() {
            return _repository.ObtenerTodos();
        }

        public Producto? ObtenerProductoPorId(int id) {
            return _repository.ObtenerPorId(id);
        }

        public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria) {
            var producto = _repository.ObtenerPorId(id);
            
            if (producto != null) {
                producto.Nombre = nombre;
                producto.Precio = precio;
                producto.Cantidad = cantidad;
                producto.Categoria = categoria;

                _repository.Actualizar(producto);
                _Persistir();
            }
        } 

        public void EliminarProducto(int id) {
            _repository.Eliminar(id);
            _Persistir();
        }

        public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria) {
            return _repository.BuscarPorCategoria(categoria);
        }

        public IEnumerable<Producto> BuscarPorNombre(string nombre) {
            return _repository.BuscarPorNombre(nombre);
        }

        public IEnumerable<Producto> ObtenerProductosBajoStock(int umbral = 5) {
            return _repository.ObtenerStockBajo(umbral);
        }

        public decimal ObtenerValorTotalInventario() {
            return _repository.ValorTotalInventario();
        }

        public decimal ObtenerPrecioPromedio() {
            return _repository.PrecioPromedio();
        }

        public Producto? ObtenerProductoMasCaro() {
            return _repository.ObtenerProductoMasCaro();
        }

        // Métodos de Reporte
        public string GenerarResumenInventario() {
            var productos = _repository.ObtenerTodos();
            var generador = new GeneradorReportes(productos);
            return generador.GenerarResumen();
        }

        public string GenerarReporteStockBajo(int umbral = 5) {
            var productos = _repository.ObtenerTodos();
            var generador = new GeneradorReportes(productos);
            return generador.GenerarReporteStockBajo(umbral);
        }

        public string GenerarTopProductos(int topN = 5) {
            var productos = _repository.ObtenerTodos();
            var generador = new GeneradorReportes(productos);
            return generador.GenerarTopProductos(topN);
        }

        public string ExportarInventarioCSV() {
            var productos = _repository.ObtenerTodos();
            var generador = new GeneradorReportes(productos);
            return generador.ExportarCsv();
        }

        private void _Persistir() {
            _storage.CrearBackup(_storageFilePath);
            var productos = _repository.ObtenerTodos().ToList();
            _storage.Guardar(productos, _storageFilePath);
        }
    }
}