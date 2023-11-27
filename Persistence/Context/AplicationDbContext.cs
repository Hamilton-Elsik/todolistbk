using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Persistence.Context;

public partial class AplicationDbContext: DbContext
{
    public AplicationDbContext()
    {
    }
    public DbSet<UserModel> User { get; set; }
    public DbSet<TaskModel> Task { get; set; }
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*       modelBuilder
                   .UseCollation("utf8mb4_0900_ai_ci")
                   .HasCharSet("utf8mb4");*/

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
