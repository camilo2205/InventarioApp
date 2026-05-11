using System.IO;

namespace InventarioApp.Infrastructure;

public class FileManager {
    
    public void Escribir(string ruta, string contenido) {
        File.WriteAllText(ruta, contenido);
    }

    public string Leer(string ruta) {
        return File.ReadAllText(ruta);
    }

    public void Agregar(string ruta, string contenido) {
        File.AppendAllText(ruta, contenido);
    }

    public bool Existe(string ruta) {
        return File.Exists(ruta);
    }

    public void Eliminar(string ruta) {
        if (File.Exists(ruta)) {
            File.Delete(ruta);
        }
    }

    public string[] LeerLineas(string ruta) {
        return File.ReadAllLines(ruta);
    }

    public void EscribirLineas(string ruta, string[] lineas) {
        File.WriteAllLines(ruta, lineas);
    }

    public void CrearDirectorio(string ruta) {
        Directory.CreateDirectory(ruta);
    }

    public string[] ListarArchivos(string ruta, string patron = "*.*") {
        return Directory.GetFiles(ruta, patron);
    }
}