namespace InventarioApp.Repositories;

using InventarioApp.Models;

/// <summary>
/// Interfaz para el repositorio de productos, define las operaciones CRUD y consultas específicas.
/// Define las operaciones que se pueden realizar sobre los productos en el inventario, como agregar, eliminar, actualizar y buscar productos.
/// </summary>

public interface IProductoRepository
{
    /// <summary>
    /// Agrega un nuevo producto al inventario.
    /// </summary>
    void Agregar(Producto producto);

    /// <summary>
    /// Obtiene un producto por su ID.
    /// Retorna NULL si no se encuentra el producto.
    /// </summary>
    Producto? ObtenerPorId(int id);

    /// <summary>
    /// Obtiene todos los productos del inventario.
    /// </summary>
    IEnumerable<Producto> ObtenerTodos();

    /// <summary>
    /// Actualiza la información de un producto existente.
    /// </summary>
    bool Actualizar(Producto producto);

    /// <summary>
    /// Elimina un producto del inventario por su ID.
    /// Retorna TRUE si la eliminación fue exitosa, FALSE si el producto no se encontró.
    /// </summary>
    bool Eliminar(int id);

    /// <summary>
    /// Cantidad total de productos en el inventario.
    /// </summary>
    int CantidadTotal { get; }
}