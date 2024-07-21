using ApiCrud.DTO;
using ApiCrud.Models;
using ApiCrud.Records;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.DAO;

public class CourseDAO(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<CourseWithStudentsDTO>> All(CancellationToken ct)
    {
        try {
            return await _context.Course
                .Where(course => course.Status)
                .Include(course => course.StudentCourses)
                    .ThenInclude(studentCourse => studentCourse.Student)
                .Select(course => new CourseWithStudentsDTO(
                    course.Id,
                    course.Name,
                    course.StudentCourses.Select(sc => new StudentCourseDTO(
                        sc.Student.Id,
                        sc.Student.Name
                    )).ToList()
                ))
                .ToListAsync(ct);

        }catch(Exception ex){
            Console.WriteLine($"Erro ao obter lista de turmas: {ex.Message}");
            throw;
        }
    }

    public async Task<Course?> FindById(Guid Id, CancellationToken ct)
    {
        try{
            return await _context.Course
                .FirstOrDefaultAsync(course => course.Id == Id, ct);

        }catch(Exception ex){
            Console.WriteLine($"Erro ao buscar Turma: {ex.Message}");
            throw;
        }
    }

    public async Task<Course> Save(Course course, CancellationToken ct)
    {
        try{
            await _context.Course
                .AddAsync(course, ct);
            return course;

        }catch(DbUpdateException ex){
            Console.WriteLine($"Erro ao tentar adicionar turma: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> HasCourse(string name, CancellationToken ct)
    {
        try{
            return await _context.Course
                .AnyAsync(course => course.Name == name, ct);

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
                
        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }
}