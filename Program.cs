using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

if (args.Length > 0) {
    switch (args[0].ToLower()) {
        case "--help":
        case "-h":
            MostrarAyuda();
            Environment.Exit(0);
            break;
        case "--version":
        case "-v":
            Console.WriteLine($"InventarioApp versión {version}");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine($"Opción desconocida: {args[0]}");
            Console.WriteLine("Usa --help para ver las opciones disponibles.");
            Environment.Exit(2);
            break;
    }
}

int cantidadProductos = 0;
// decimal valorTotalDelInventario = 0.0m;
bool sistemaActivo = true;
// string nombreSistema = "Sistema de Gestión de Inventario";

Console.WriteLine("Estado del Sistema:");
// Console.WriteLine($"Nombre del sistema: {nombreSistema}");
Console.WriteLine($"Cantidad de productos: {cantidadProductos}");
// Console.WriteLine($"Valor total del inventario: {valorTotalDelInventario:N2}");
Console.WriteLine($"Sistema activo: {(sistemaActivo ? "Sí" : "No")}");

Console.WriteLine("Comandos: listar, agregar, buscar, salir");
Console.Write("Inventario: ");
string? entrada = Console.ReadLine();

while (sistemaActivo) {
    switch (entrada?.ToLower()) {
        case "listar":
            Console.WriteLine("Listando productos...");
            Console.WriteLine($"Cantidad de productos: {cantidadProductos}");
            break;
        case "agregar":
            Console.WriteLine("Agregando producto...");
            break;
        case "buscar":
            Console.WriteLine("Buscando producto...");
            break;
        case "salir":
            Console.WriteLine("Saliendo del sistema...");
            sistemaActivo = false;
            break;
        default:
            Console.WriteLine("Comando no reconocido. Intente nuevamente.");
            break;
    }

    if (sistemaActivo) {
        Console.WriteLine("Comandos: listar, agregar, buscar, salir");
        entrada = Console.ReadLine();
    }
}

// ===== Funciones =====
void MostrarBanner()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine($"Versión: {version}");
    Console.WriteLine($".NET: {Environment.Version}");
    Console.WriteLine($"Sistema: {Environment.OSVersion.Platform}");
    Console.WriteLine();
}

void MostrarAyuda() {
    Console.WriteLine("Uso: InventarioApp [opción]");
    Console.WriteLine();
    Console.WriteLine("Opciones:");
    Console.WriteLine("--  --help,-- -h       Muestra esta ayuda");
    Console.WriteLine("--  --version,-- -v    Muestra la versión de la aplicación");
}