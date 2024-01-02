using Microsoft.EntityFrameworkCore;

class TodoPool : DbContext
{
    public TodoPool(DbContextOptions<TodoPool> options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}
