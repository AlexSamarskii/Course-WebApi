public class Student
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FullName { get; set; } = string.Empty;

    [ForeignKey("Course")]
    public Guid CourseId { get; set; }

    public Course Course { get; set; } = null!;
}