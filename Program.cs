using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Courses API",
        Version = "v1",
        Description = "API для управления курсами и студентами"
    });
});

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
    db.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courses API V1");
    });
}

app.MapGet("/courses", async (ICourseRepository repo) =>
    Results.Ok(await repo.GetCoursesAsync()))
    .WithName("GetCourses")
    .WithTags("Courses");

app.MapPost("/courses", async ([FromBody] Course course, ICourseRepository repo) =>
{
    await repo.InsertCourseAsync(course);
    await repo.SaveAsync();
    return Results.Ok(new { course.Id });
})
.WithName("CreateCourse")
.WithTags("Courses");

app.MapPost("/courses/{id:guid}/students", async (
    Guid id,
    [FromBody] Student student,
    ICourseRepository repo) =>
    await repo.AddStudentAsync(id, student) is Student stud
    ? Results.Ok(stud)
    : Results.NotFound())
.WithName("AddStudentToCourse")
.WithTags("Students");

app.MapDelete("/courses/{id:guid}", async (Guid id, ICourseRepository repo) =>
{
    await repo.DeleteCourseAsync(id);
    await repo.SaveAsync();
    return Results.NoContent();
})
.WithName("DeleteCourse")
.WithTags("Courses");

app.UseHttpsRedirection();

app.Run();
