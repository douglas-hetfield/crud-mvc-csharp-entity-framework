using ApiCrud.DAO;
using ApiCrud.DTO;
using ApiCrud.Models;
using ApiCrud.Records;

namespace ApiCrud.Services;

public class StudentService(StudentDAO studentDAO)
{
    private readonly StudentDAO _studentDao = studentDAO;

    public async Task<IResult> Save(AddStudentRequest request, CancellationToken ct)
    {
        try{
            var hasStudent = await _studentDao.HasStudent(request.Name, ct);
            if(hasStudent) return Results.Conflict("Estudante Já existe!");
            
            var newStudent = await _studentDao.Save(new Student(request.Name), ct);
            await _studentDao.SaveChangesAsync(ct);

            return Results.Ok(new StudentDTO(newStudent.Id, newStudent.Name));
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> All(CancellationToken ct)
    {
        try{
            var studentsList = await _studentDao.All(ct);
            return Results.Ok(studentsList);

        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Update(Guid Id, UpdateStudentRequest request, CancellationToken ct)
    {
        try{
            var student = await _studentDao.FindById(Id, ct);
            if(student == null) return Results.NotFound();

            student.SetName(request.Name);
            await _studentDao.SaveChangesAsync(ct);
            
            return Results.Ok(new StudentDTO(student.Id, student.Name));
            
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Delete(Guid Id, CancellationToken ct)
    {
        try{
            var student = await _studentDao.FindById(Id, ct);
            if(student == null) return Results.NotFound();

            student.Deactive();
            await _studentDao.SaveChangesAsync(ct);

            return Results.Ok();
            
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }
}