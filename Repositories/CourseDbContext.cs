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

        modelBuilder.Entity<Course>()
    .HasMany(c => c.Students)
    .WithOne(s => s.Course)  
    .HasForeignKey(s => s.CourseId) 
    .OnDelete(DeleteBehavior.Cascade);
    }
}