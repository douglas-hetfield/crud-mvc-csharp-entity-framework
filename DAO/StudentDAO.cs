using ApiCrud.DTO;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.DAO;

public class StudentDAO(AppDbContext context) {
    private readonly AppDbContext _context = context;

    public async Task<bool> HasStudent(string Name, CancellationToken ct)
    {
        return await _context.Student
            .AnyAsync(student => student.Name == Name, ct);
    }

    public async Task<Student> Save(Student student, CancellationToken ct)
    {
        try{
            await _context.Student.AddAsync(student, ct);
            return student;

        }catch(DbUpdateException ex){
            Console.WriteLine($"Erro ao salvar estudante no banco de dados: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<List<StudentDTO>> All(CancellationToken ct)
    {
        try{
            return await _context
                .Student
                .Where(student => student.Status)
                .Select(student => new StudentDTO(student.Id, student.Name))
                .ToListAsync(ct);

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<Student?> FindById(Guid Id, CancellationToken ct)
    {
        try{
            return await _context
                .Student
                .FirstOrDefaultAsync(student => student.Id == Id, ct);

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
            Console.WriteLine($"Erro ao salvar estudante no banco de dados: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }
}