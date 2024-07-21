using ApiCrud.Records;
using ApiCrud.Services;

namespace ApiCrud.Routes;

public static class StudentRoutes
{
    public static void AddRoutes(WebApplication app)
    {
        var groupStudentRoutes = app.MapGroup("students");

        groupStudentRoutes
            .MapPost("", async (AddStudentRequest request, StudentService studentService, CancellationToken ct) => 
            await studentService.Save(request, ct));

        groupStudentRoutes
            .MapGet("", async (StudentService studentService, CancellationToken ct) =>
            await studentService.All(ct));

        groupStudentRoutes
            .MapPut("{id:guid}", async (Guid id, UpdateStudentRequest request, StudentService studentService, CancellationToken ct) =>
            await studentService.Update(id, request, ct));

        groupStudentRoutes
            .MapDelete("{id:guid}", async (Guid Id, StudentService studentService, CancellationToken ct) =>
            await studentService.Delete(Id, ct));
    }
} 