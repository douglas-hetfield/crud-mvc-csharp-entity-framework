using Microsoft.EntityFrameworkCore;
using ApiCrud.Models;

namespace ApiCrud.DAO;

public class StudentCourseDAO(AppDbContext context)
{
    private readonly AppDbContext _context = context;
    public async Task<StudentCourse> AddStudent(StudentCourse studentCourse, CancellationToken ct)
    {
        try{
            await _context.StudentCourse
                .AddAsync(studentCourse, ct);

            return studentCourse;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        try{
            await _context
                .SaveChangesAsync(ct);

        }catch(DbUpdateException ex){
            Console.WriteLine($"Erro ao tentar salvar informações no banco de dados: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
        
    }
}