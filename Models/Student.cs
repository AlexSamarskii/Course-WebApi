public class Student
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FullName { get; set; } = string.Empty;

    public Guid CourseId { get; set; }

    public Course Course { get; set; } = null!;
}