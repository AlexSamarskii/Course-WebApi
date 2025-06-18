public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _context;

    public CourseRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<Student> AddStudentAsync(Guid courseId, Student student)
    {
        var course = await _context.Courses.FindAsync(courseId);
        if (course == null)
            throw new KeyNotFoundException("Course not found");

        student.CourseId = courseId;
        _context.Students.Add(student);
        await _context.SaveChangesAsync(); 
        return student;
    }

    public async Task DeleteCourseAsync(Guid courseId)
    {
        var courseFromDb = await _context.Courses.FindAsync(courseId);
        if (courseFromDb == null) return;
        _context.Courses.Remove(courseFromDb);
    }

    public async Task<Course?> GetCourseAsync(Guid courseId) =>
        await _context.Courses.Include(c => c.Students)
                              .FirstOrDefaultAsync(c => c.Id == courseId);

    public async Task<List<Course>> GetCoursesAsync() =>
        await _context.Courses.Include(c => c.Students).ToListAsync();

    public async Task InsertCourseAsync(Course course) =>
        await _context.Courses.AddAsync(course);

    public Task SaveAsync() => _context.SaveChangesAsync();

    public async Task UpdateCourseAsync(Course course)
    {
        var courseFromDb = await _context.Courses.FindAsync(course.Id);
        if (courseFromDb == null) return;

        courseFromDb.Name = course.Name;
    }

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
