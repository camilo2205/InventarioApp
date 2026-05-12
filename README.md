# Sistema de Gestión de Inventario

Proyecto de consola desarrollado como parte de mi aprendizaje en el curso de **Fundamentos de .NET** de **Platzi**.

La idea del proyecto es practicar los conceptos base de .NET y C# construyendo una aplicación sencilla para administrar productos de inventario desde la terminal.

## Objetivo

Este repositorio refleja mi progreso estudiando fundamentos de .NET. El proyecto me sirve para practicar:

- Estructura básica de una aplicación de consola en .NET.
- Uso de clases, enums, métodos y namespaces en C#.
- Separación de responsabilidades por carpetas.
- Manejo de colecciones con LINQ.
- Persistencia de datos en archivos JSON.
- Generación de reportes simples.
- Lectura de datos desde consola y flujo de menús.

## Funcionalidades

- Agregar productos al inventario.
- Listar productos guardados.
- Buscar productos por ID.
- Buscar productos por categoría.
- Eliminar productos.
- Ver resumen del inventario.
- Ver reporte de productos con stock bajo.
- Consultar estadísticas básicas.
- Exportar inventario en formato CSV.
- Guardar y cargar datos desde `inventario.json`.

## Tecnologías

- C#
- .NET
- LINQ
- JSON
- Aplicación de consola

## Estructura del proyecto

```text
InventarioApp/
├── Program.cs
├── InventarioApp.csproj
├── inventario.json
└── src/
    ├── Factories/
    ├── Infrastructure/
    ├── Models/
    ├── Repositories/
    └── Services/
```

## Cómo ejecutar

Desde la raíz del proyecto:

```bash
dotnet run
```

Para compilar:

```bash
dotnet build
```

## Estado del aprendizaje

Este proyecto fue desarrollado mientras avanzaba en el curso. La intención no es crear un sistema de inventario completo para producción, sino reforzar fundamentos de programación, organización de código y buenas prácticas iniciales en .NET.

## Autor

Cristian Camilo Ledesma López
