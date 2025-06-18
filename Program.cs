using Microsoft.AspNetCore.Mvc;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/courses", async (ICourseRepository repo) =>
    Results.Ok(await repo.GetCoursesAsync()));

app.MapPost("/courses", async ([FromBody] Course course, ICourseRepository repo) =>
{
    await repo.InsertCourseAsync(course);
    return Results.Ok(new { course.Id });
});

app.MapPost("/courses/{id:guid}/students", async (
    Guid id,
    [FromBody] Student student,
    ICourseRepository repo) =>
    await repo.AddStudentAsync(id, student) is Student stud
    ? Results.Ok(stud)
    : Results.NotFound());


app.MapDelete("/courses/{id:guid}", async (Guid id, ICourseRepository repo) =>
{
    await repo.DeleteCourseAsync(id);
    await repo.SaveAsync();
    return Results.NoContent();
});

app.UseHttpsRedirection();
app.Run();
