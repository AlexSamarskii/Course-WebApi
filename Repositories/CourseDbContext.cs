using System.Collections.Generic;
using System.Reflection.Emit;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация связи Course — Students (один ко многим)
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // Дополнительные настройки можно добавить тут, если нужно
    }
}