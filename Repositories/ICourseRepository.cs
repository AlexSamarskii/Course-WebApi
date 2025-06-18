public interface ICourseRepository : IDisposable
{
    Task<List<Course>> GetCoursesAsync();
    Task<Course?> GetCourseAsync(Guid courseId);
    Task InsertCourseAsync(Course course);
    Task SaveAsync();
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Guid courseId);

    Task<Student> AddStudentAsync(Guid courseId, Student student);
}