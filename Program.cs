using InventarioApp.Models;
using InventarioApp.Repositories;

Console.WriteLine("=== InventarioApp ===");

var repo = new InMemoryProductoRepository();

var laptop = new Producto {
    Nombre = "Laptop",
    Precio = 1500.00m,
    Cantidad = 10,
    Categoria = CategoriaProducto.Electronica,
    Estado = EstadoProducto.Disponible
};

var mouse = new Producto {
    Nombre = "Mouse",
    Precio = 25.00m,
    Cantidad = 50,
    Categoria = CategoriaProducto.Electronica,
    Estado = EstadoProducto.Disponible
};

var teclado = new Producto {
    Nombre = "Teclado",
    Precio = 45.00m,
    Cantidad = 30,
    Categoria = CategoriaProducto.Electronica,
    Estado = EstadoProducto.Disponible
};

var silla = new Producto {
    Nombre = "Silla de Oficina",
    Precio = 120.00m,
    Cantidad = 20,
    Categoria = CategoriaProducto.Muebles,
    Estado = EstadoProducto.Disponible
};

var escritorio = new Producto {
    Nombre = "Escritorio",
    Precio = 250.00m,
    Cantidad = 15,
    Categoria = CategoriaProducto.Muebles,
    Estado = EstadoProducto.Disponible
};

repo.Agregar(laptop);
repo.Agregar(mouse);
repo.Agregar(teclado);
repo.Agregar(silla);
repo.Agregar(escritorio);

Console.WriteLine($"Total productos en inventario: {repo.CantidadTotal}");

// Consultas con LINQ
var electronicos = repo.BuscarPorCategoria(CategoriaProducto.Electronica);
Console.WriteLine("\nProductos en categoría Electrónica:");
foreach (var producto in electronicos)
{
    Console.WriteLine($" - {producto.Nombre}: ${producto.Precio:N2}");
}

var conMouse = repo.BuscarPorNombre("ouse");
Console.WriteLine("\nProductos que contienen 'ouse' en el nombre:");
foreach (var producto in conMouse)
{
    Console.WriteLine($" - {producto.Nombre}: ${producto.Precio:N2}");
}

var nombres = repo.ObtenerNombresProductos();
Console.WriteLine($"\nNombres de todos los productos: {string.Join(", ", nombres)}");

var hayStockBajo = repo.HayStockBajo(25);
Console.WriteLine($"\n¿Hay productos con stock bajo? {(hayStockBajo ? "Sí" : "No")}");