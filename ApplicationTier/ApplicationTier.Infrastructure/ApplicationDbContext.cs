using ApplicationTier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTier.Infrastructure;

public partial class ApplicationDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ListTask> ListTasks { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }

}