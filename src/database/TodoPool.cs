/// Importación de modulos de EF
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Representa el contexto de base de datos para la gestión de la tabla Todo en la base de datos.
/// Hereda de DbContext, que es parte de Entity Framework Core para la administración de bases de datos.
/// </summary>
public class TodoPool : DbContext
{
    /// <summary>
    /// Constructor de la clase TodoPool que recibe opciones para la configuración del contexto de base de datos.
    /// </summary>
    /// <param name="options">Opciones para configurar el contexto de la base de datos.</param>
    public TodoPool(DbContextOptions<TodoPool> options) : base(options) { }

    /// <summary>
    /// Representa la tabla Todo en la base de datos.
    /// Permite acceder y realizar operaciones CRUD (Crear, Leer, Actualizar, Borrar) en la tabla Todo.
    /// Utiliza DbSet<T> para interactuar con la base de datos.
    /// </summary>
    public DbSet<Todo> Todos => Set<Todo>();
}
