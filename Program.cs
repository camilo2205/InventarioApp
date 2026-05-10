// ===============================================================
//  InventarioApp - Programa principal
//  Autor: Cristian Camilo Ledesma López
//  Estado: Estructura básica
// ===============================================================


// Variables
int cantidadProductos = 0;
decimal valorTotalInventario = 0.0m;
bool sistemaActivo = true;

MostrarBanner();
bool continuar = true;

while (continuar)
{
    MostrarMenu();
    string comando = LeerComando("Ingrese un comando: ");
    continuar = ProcesarComando(comando);
}

bool ProcesarComando(string comando)
{
    switch (comando.ToLower())
    {
        case "agregar":
            AgregarProducto();
            break;
        case "listar":
            ListarProductos();
            break;
        case "buscar":
            BuscarProducto();
            break;
        case "salir":
            Console.WriteLine("Saliendo del sistema...");
            return false;
        default:
            Console.WriteLine("Comando no reconocido. Escribe 'ayuda' para ver las opciones.");
            break;
    }
    return true;
}

void ListarProductos()
{
    Console.WriteLine($"Total: {cantidadProductos} productos en el inventario.");
    Console.WriteLine($"Valor: ${valorTotalInventario:F2}");
    // Aquí iría la lógica para mostrar los productos
}

void AgregarProducto()
{
    Console.WriteLine("Agregando un nuevo producto (proximamente)...");
    // Aquí iría la lógica para agregar un producto al inventario
}

void BuscarProducto()
{
    Console.WriteLine("Buscando un producto (proximamente)...");
    // Aquí iría la lógica para buscar un producto en el inventario
}

string LeerComando(string? mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine() ?? string.Empty;
}

void MostrarMenu()
{
    Console.WriteLine();
    Console.WriteLine("Comandos disponibles:");
    Console.WriteLine(" - agregar: Agregar un nuevo producto");
    Console.WriteLine(" - listar: Listar todos los productos");
    Console.WriteLine(" - buscar: Buscar un producto por nombre");
    Console.WriteLine(" - salir: Salir del sistema");
    Console.WriteLine();
}

// ===== Funciones =====
void MostrarBanner()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine();
}

void MostrarAyuda() {
    Console.WriteLine("Uso: InventarioApp [opción]");
    Console.WriteLine();
    Console.WriteLine("Opciones:");
    Console.WriteLine("--  --help,-- -h       Muestra esta ayuda");
    Console.WriteLine("--  --version,-- -v    Muestra la versión de la aplicación");
}