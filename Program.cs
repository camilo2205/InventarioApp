using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;

Console.WriteLine("=== Prueba Integración JSON ===");

var almacenamiento = new JsonInventarioStorage();

var productos = new List<Producto> {
    new Producto { Id = 1, Nombre = "Laptop", Precio = 1500.00m, Cantidad = 10, Categoria = CategoriaProducto.Electronica, Estado = EstadoProducto.Disponible },
    new Producto { Id = 2, Nombre = "Silla de Oficina", Precio = 200.00m, Cantidad = 5, Categoria = CategoriaProducto.Muebles, Estado = EstadoProducto.Agotado },
    new Producto { Id = 3, Nombre = "Camiseta", Precio = 25.00m, Cantidad = 20, Categoria = CategoriaProducto.Ropa, Estado = EstadoProducto.Disponible }
};

string ruta = "inventario_test.json";

almacenamiento.CrearBackup(ruta);
almacenamiento.Guardar(productos, ruta);

Console.WriteLine("Inventario guardado correctamente.");

var productosCargados = almacenamiento.Cargar(ruta);

foreach (var producto in productosCargados) {
    Console.WriteLine($"Producto: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}");
}