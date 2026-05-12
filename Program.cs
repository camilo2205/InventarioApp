using InventarioApp.Models;
using InventarioApp.Services;

var servicio = new InventarioServices();
bool continuar = true;

while (continuar) {
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "";

    switch (opcion) {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            BuscarProductoPorId();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            BuscarPorCategoria();
            break;
        case "6":
            MostrarResumenInventario();
            break;
        case "7":
            MostrarStockBajo();
            break;
        case "8":
            MostrarEstadisticas();
            break;
        case "9":
            ExportarCsv();
            break;
        case "10":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
            break;
    }
}

void MostrarMenu() {
    Console.WriteLine("\n--- Menú de Inventario ---");
    Console.WriteLine("1. Agregar Producto");
    Console.WriteLine("2. Listar Productos");
    Console.WriteLine("3. Buscar Producto por ID");
    Console.WriteLine("4. Eliminar Producto");
    Console.WriteLine("5. Buscar por Categoría");
    Console.WriteLine("6. Resumen del Inventario");
    Console.WriteLine("7. Reporte de Stock Bajo");
    Console.WriteLine("8. Estadísticas del Inventario");
    Console.WriteLine("9. Exportar a CSV");
    Console.WriteLine("10. Salir");
    Console.Write("Seleccione una opción: ");
}

void AgregarProducto() {
    Console.Write("Nombre del producto: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Precio: ");
    decimal precio = decimal.Parse(Console.ReadLine() ?? "0");

    Console.Write("Cantidad: ");
    int cantidad = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Categoría (Electrónica, Ropa, Alimentos, Hogar, Otros): ");
    string? categoriaStr = Console.ReadLine() ?? "Otros";

    CategoriaProducto categoria;
    while (!Enum.TryParse<CategoriaProducto>(categoriaStr, true, out categoria)) {
        Console.WriteLine("Categoría no válida");
        Console.WriteLine("Categoría (Electrónica, Ropa, Alimentos, Hogar, Otros): ");
        categoriaStr = Console.ReadLine();
    }

    servicio.AgregarProducto(nombre, precio, cantidad, categoria);
    Console.WriteLine("Producto agregado exitosamente.");
}

void ListarProductos() {
    var productos = servicio.ObtenerProductos();
    Console.WriteLine("\n--- Lista de Productos ---");
    foreach (var producto in productos) {
        Console.WriteLine($"{producto.Id}: {producto.Nombre} - ${producto.Precio:N2} - Cantidad: {producto.Cantidad} - Categoría: {producto.Categoria} - Estado: {producto.Estado}");
    }
}

void BuscarProductoPorId() {
    Console.Write("Ingrese el ID del producto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    var producto = servicio.ObtenerProductoPorId(id);

    if (producto != null) {
        Console.WriteLine("Producto encontrado: ");
        Console.WriteLine($"Nombre: {producto.Nombre}");
        Console.WriteLine($"Precio: ${producto.Precio:N2}");
        Console.WriteLine($"Cantidad: {producto.Cantidad}");
        Console.WriteLine($"Categoría: {producto.Categoria}");
        Console.WriteLine($"Estado: {producto.Estado}");
        Console.WriteLine($"Total en inventario: ${producto.Precio * producto.Cantidad:N2}");
    } else {
        Console.WriteLine("Producto no encontrado.");
    }
}

void EliminarProducto() {
    Console.Write("Ingrese el ID del producto a eliminar: ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    if (servicio.ObtenerProductoPorId(id) == null) {
        Console.WriteLine("Producto no encontrado.");
        return;
    } else {
        Console.Write("¿Está seguro que desea eliminar este producto? (s/n): ");
        string confirmacion = Console.ReadLine() ?? "n";
        if (confirmacion.ToLower() != "s") {
            Console.WriteLine("Eliminación cancelada.");
            return;
        }
        servicio.EliminarProducto(id);
        Console.WriteLine("Producto eliminado exitosamente.");
    }
}

void BuscarPorCategoria() {
    Console.WriteLine("Ingrese la categoría a buscar (Electrónica, Ropa, Alimentos, Hogar, Otros): ");
    string? categoriaStr = Console.ReadLine() ?? "Otros";

    CategoriaProducto categoria;
    while (!Enum.TryParse<CategoriaProducto>(categoriaStr, true, out categoria)) {
        Console.WriteLine("Categoría no válida");
        Console.WriteLine("Ingrese la categoría a buscar (Electrónica, Ropa, Alimentos, Hogar, Otros): ");
        categoriaStr = Console.ReadLine();
    }

    var productos = servicio.BuscarPorCategoria(categoria);
    if (productos.Count() > 0) {
        Console.WriteLine($"\n--- Productos en categoría {categoria} ---");
        foreach (var producto in productos) {
            Console.WriteLine($"{producto.Id}: {producto.Nombre} - ${producto.Precio:N2} - Cantidad: {producto.Cantidad} - Estado: {producto.Estado}");
        }
    } else {
        Console.WriteLine($"No se encontraron productos en la categoría {categoria}.");
    }
}

void MostrarResumenInventario() {
    var resumen = servicio.GenerarResumenInventario();
    Console.WriteLine($"\n{resumen}");
}

void MostrarStockBajo() {
    var reporte = servicio.GenerarReporteStockBajo();
    Console.WriteLine($"\n{reporte}");
}

void MostrarEstadisticas() {
    Console.WriteLine("\n--- Estadísticas del Inventario ---");
    Console.WriteLine($"Valor total del inventario: ${servicio.ObtenerValorTotalInventario():N2}");
    Console.WriteLine($"Producto más caro: {servicio.ObtenerProductoMasCaro()?.Nombre ?? "N/A"}");
    Console.WriteLine($"Precio promedio: ${servicio.ObtenerPrecioPromedio():N2}");
}

void ExportarCsv() {
    string csv = servicio.ExportarInventarioCSV();
    Console.WriteLine("\n--- CSV Exportado ---");
    Console.WriteLine(csv);
}