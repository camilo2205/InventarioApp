namespace InventarioApp.Repositories;

using InventarioApp.Models;

public class InMemoryProductoRepository : IProductoRepository {
    private readonly List<Producto> _productos = new();

    private int _proximoId = 1;

    public void Agregar(Producto producto) {
        producto.Id = _proximoId++;
        _productos.Add(producto);
    }

    public Producto? ObtenerPorId(int id) {
        return _productos.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Producto> ObtenerTodos() {
        return _productos.AsReadOnly();
    }

    public bool Actualizar(Producto producto) {
        var existente = ObtenerPorId(producto.Id);
        if (existente == null) return false;

        existente.Nombre = producto.Nombre;
        existente.Precio = producto.Precio;
        existente.Cantidad = producto.Cantidad;
        existente.Categoria = producto.Categoria;
        existente.Estado = producto.Estado;

        return true;
    }

    public bool Eliminar(int id) {
        var producto = ObtenerPorId(id);
        if (producto == null) return false;
        return _productos.Remove(producto);
    }

    public int CantidadTotal => _productos.Count;

    // Busquedas con Where LINQ
    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria) {
        return _productos.Where(p => p.Categoria == categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre) {
        return _productos.Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Producto> BuscarPorRangoPrecio(decimal precioMin, decimal precioMax) {
        return _productos.Where(p => p.Precio >= precioMin && p.Precio <= precioMax);
    }

    // Select y any
    public IEnumerable<string> ObtenerNombresProductos() {
        return _productos.Select(p => p.Nombre);
    }

    public bool HayStockBajo(int umbral = 5) {
        return _productos.Any(p => p.Cantidad < umbral);
    }

    public IEnumerable<Producto> ObtenerOrdenadosPorPrecio() {
        return _productos.OrderBy(p => p.Precio);
    }

    public IEnumerable<Producto> ObtenerTopPorPrecio(int top) {
        return _productos.OrderByDescending(p => p.Precio).Take(top);
    }

    public IEnumerable<IGrouping<CategoriaProducto, Producto>> AgruparPorCategoria() {
        return _productos.GroupBy(p => p.Categoria);
    }

    public Dictionary<CategoriaProducto, int> ContarPorCategoria() {
        return _productos.GroupBy(p => p.Categoria).
            ToDictionary(g => g.Key, g => g.Count());
    }

    public decimal ValorTotalInventario() {
        return _productos.Sum(p => p.ValorTotal);
    }

    public decimal PrecioPromedio() {
        if (_productos.Count == 0) return 0;
        return _productos.Average(p => p.Precio);
    }

    public Producto? ObtenerProductoMasCaro() {
        return _productos.MaxBy(p => p.Precio);
    }

    public Dictionary<CategoriaProducto, decimal> ValorTotalPorCategoria() {
        return _productos.GroupBy(p => p.Categoria).
            ToDictionary(g => g.Key, g => g.Sum(p => p.ValorTotal));
    }

    public IEnumerable<Producto> ObtenerStockBajo(int umbral = 5) {
        return _productos.Where(p => p.Cantidad < umbral);
    }
}