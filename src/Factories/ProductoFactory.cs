namespace InventarioApp.Factories;

using InventarioApp.Models;

public static class ProductoFactory {
    private static int _nextId = 1;

    public static Producto Crear(
        string nombre,
        decimal precio,
        int cantidad,
        CategoriaProducto categoria = CategoriaProducto.Otros
    ) {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto no puede estar vacío.");
        if (precio < 0)
            throw new ArgumentException("El precio del producto no puede ser negativo.");
        if (cantidad < 0)
            throw new ArgumentException("La cantidad del producto no puede ser negativa.");

        return new Producto
        {
            Id = _nextId++,
            Nombre = nombre.Trim(),
            Precio = precio,
            Cantidad = cantidad,
            Categoria = categoria,
            Estado = cantidad > 0 ? EstadoProducto.Disponible : EstadoProducto.Agotado
        };    
    }

    public static Producto CrearConStock(
        string nombre,
        decimal precio,
        int cantidad
    ) {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor a cero para crear un producto con stock.");
        return Crear(nombre, precio, cantidad);
    }
}