

public class CourseRepository : ICourseRepository
{
    public Task<Student> AddStudentAsync(Guid courseId, Student student)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCourseAsync(Guid courseId)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<Course?> GetCourseAsync(Guid courseId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Course>> GetCoursesAsync()
    {
        throw new NotImplementedException();
    }

    public Task InsertCourseAsync(Course course)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateCourseAsync(Course course)
    {
        throw new NotImplementedException();
    }
}
