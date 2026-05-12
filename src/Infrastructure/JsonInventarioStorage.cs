using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Infrastructure;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure;

public class JsonInventarioStorage {
    private readonly FileManager _fileManager;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonInventarioStorage() {
        _fileManager = new FileManager();
        _jsonOptions = new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
    }

    public void Guardar(List<Producto> productos, string ruta) {
        string json = JsonSerializer.Serialize(productos, _jsonOptions);
        _fileManager.Escribir(ruta, json);
    }

    public List<Producto> Cargar(string ruta) {
        if (!_fileManager.Existe(ruta)) {
            return new List<Producto>();
        }

        string json = _fileManager.Leer(ruta);
        return JsonSerializer.Deserialize<List<Producto>>(json, _jsonOptions) ?? new List<Producto>();
    }

    public string? CrearBackup(string ruta) {
        if (!_fileManager.Existe(ruta))
            return null;
        
        string? directorio = Path.GetDirectoryName(ruta);
        string nombreSinExtension = Path.GetFileNameWithoutExtension(ruta);
        string extension = Path.GetExtension(ruta);
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        string rutaBackup = Path.Combine(
            directorio ?? "",
            $"{nombreSinExtension}_backup_{timestamp}{extension}"
        );

        File.Copy(ruta, rutaBackup);
        return rutaBackup;
    }
}