using ApiCrud.Models;
using ApiCrud.DAO;
using ApiCrud.Records;
using ApiCrud.DTO;

namespace ApiCrud.Services;

public class CourseService(CourseDAO courseDAO, StudentDAO studentDAO, StudentCourseDAO studentCourseDAO)
{
    private readonly CourseDAO _courseDAO = courseDAO;
    private readonly StudentDAO _studentDAO = studentDAO;
    private readonly StudentCourseDAO _studentCourseDAO = studentCourseDAO;
    public async Task<IResult> All(CancellationToken ct)
    {
        try{
            var coursesList = await _courseDAO.All(ct);
            return Results.Ok(coursesList);
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Save(AddCourseRequest request ,CancellationToken ct)
    {
        try{
            var hasCourse = await _courseDAO.HasCourse(request.Name, ct);

            if(hasCourse) return Results.Conflict("Turma já foi registrada anteriormente!");

            var course = await _courseDAO.Save(new Course(request.Name), ct);
            await _courseDAO.SaveChangesAsync(ct);

            return Results.Ok(new CourseDTO(course.Id, course.Name));

        }catch(Exception ex){
            return Results.Problem(ex.Message);
        }
    }

    public async Task<IResult> Update(Guid Id, UpdateCourseRequest request, CancellationToken ct)
    {
        try{
            var course = await _courseDAO.FindById(Id, ct);
            if(course == null) return Results.NotFound();

            course.SetName(request.Name);
            await _courseDAO.SaveChangesAsync(ct);
            return Results.Ok(new StudentDTO(course.Id, course.Name));

        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Delete(Guid id, CancellationToken ct)
    {
        try{
            var course = await _courseDAO.FindById(id, ct);
            if(course == null) return Results.NotFound();

            course.Deactive();
            await _courseDAO.SaveChangesAsync(ct);

            return Results.Ok();

        }catch(Exception ex){
            return Results.Problem($"Erro ao deletar turma: {ex.Message}");
        }
    }

    public async Task<IResult> AddStudent(AddStudentOnCourseRequest request, CancellationToken ct)
    {
        try{
            var student = await _studentDAO.FindById(request.StudentId, ct);
            var course = await _courseDAO.FindById(request.CourseId, ct);

            if(student == null || course == null) return Results.NotFound("Estudante ou Turma não encontrados!");

            await _studentCourseDAO.AddStudent(new StudentCourse(student.Id, course.Id), ct);
            await _studentCourseDAO.SaveChangesAsync(ct);
            return Results.Ok();

        }catch(Exception ex){
            return Results.Problem($"Erro ao tentar adicionar estudante na turma: {ex.Message}");
        }
    }
}