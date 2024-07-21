using ApiCrud.Records;
using ApiCrud.Services;

namespace ApiCrud.Routes;

public static class CourseRoutes
{
    public static void AddRoutes(WebApplication app)
    {
        var groupCourseRoutes = app.MapGroup("Course");

        groupCourseRoutes
            .MapGet("", async (CourseService courseService, CancellationToken ct) =>  
            await courseService.All(ct));

        groupCourseRoutes
            .MapPost("", async (AddCourseRequest request, CourseService courseService, CancellationToken ct) =>
            await courseService.Save(request, ct));

        groupCourseRoutes.MapPut("{id:Guid}", async (Guid id, UpdateCourseRequest request, CancellationToken ct, CourseService courseService) => 
            await courseService.Update(id, request, ct));

        groupCourseRoutes.MapDelete("{id:Guid}", async (Guid id, CourseService courseService, CancellationToken ct) =>
            await courseService.Delete(id, ct));

        groupCourseRoutes.MapPost("addStudent", async (AddStudentOnCourseRequest request, CancellationToken ct, CourseService courseService) =>
            await courseService.AddStudent(request, ct));
    }
}