﻿public class Course
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;

    public List<Student> Students { get; set; } = new List<Student>();
}