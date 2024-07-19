using ApiCrud.DTO;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.DAO;

public class EstudanteDAO(AppDbContext context) {
    private readonly AppDbContext _context = context;

    public async Task<bool> HasEstudante(string Name, CancellationToken ct)
    {
        return await _context.Estudante.AnyAsync(estudante => estudante.Name == Name, ct);
    }

    public async Task<Estudante> Save(Estudante estudante, CancellationToken ct)
    {
        try{
            await _context.Estudante.AddAsync(estudante, ct);
            return estudante;

        }catch(DbUpdateException ex){
            Console.WriteLine($"Erro ao salvar estudante no banco de dados: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<List<EstudanteDTO>> All(CancellationToken ct)
    {
        try{
            return await _context
                .Estudante
                .Where(estudante => estudante.Status)
                .Select(estudante => new EstudanteDTO(estudante.Id, estudante.Name))
                .ToListAsync(ct);

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task<Estudante?> FindById(Guid Id, CancellationToken ct)
    {
        try{
            return await _context
                .Estudante
                .FirstOrDefaultAsync(estudante => estudante.Id == Id, ct);

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        try{
            await _context.SaveChangesAsync(ct);

        }catch(DbUpdateException ex){
            Console.WriteLine($"Erro ao salvar estudante no banco de dados: {ex.Message}");
            throw;

        }catch(Exception ex){
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            throw;
        }
    }
    
}