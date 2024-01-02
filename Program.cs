using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de la base de datos en memoria para la gestión de la lista de tareas.
builder.Services.AddDbContext<TodoPool>(opt => opt.UseInMemoryDatabase("TodoList"));

// Configuración para mostrar información detallada en caso de excepciones relacionadas con la base de datos en las páginas de desarrollador.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Endpoint para obtener todos los elementos de la lista de tareas.
app.MapGet("/todoitems", async (TodoPool db) =>
    await db.Todos.ToListAsync());

// Endpoint para obtener los elementos completados de la lista de tareas.
app.MapGet("/todoitems/complete", async (TodoPool db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

// Endpoint para obtener un elemento específico de la lista de tareas por su ID.
app.MapGet("/todoitems/{id}", async (int id, TodoPool db) =>
    await db.Todos.FindAsync(id)
        is Todo todo ? Results.Ok(todo) : Results.NotFound());

// Endpoint para crear un nuevo elemento en la lista de tareas.
app.MapPost("/todoitems", async (Todo todo, TodoPool db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

// Endpoint para actualizar un elemento existente en la lista de tareas por su ID.
app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoPool db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint para eliminar un elemento de la lista de tareas por su ID.
app.MapDelete("/todoitems/{id}", async (int id, TodoPool db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
