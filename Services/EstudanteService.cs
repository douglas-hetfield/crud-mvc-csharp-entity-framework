using ApiCrud.DAO;
using ApiCrud.DTO;
using ApiCrud.Models;
using ApiCrud.Records;

namespace ApiCrud.Services;

public class EstudanteService(EstudanteDAO estudanteDAO)
{
    private readonly EstudanteDAO _estudanteDao = estudanteDAO;

    public async Task<IResult> Save(AddEstudanteRequest request, CancellationToken ct)
    {
        try{
            var hasEstudante = await _estudanteDao.HasEstudante(request.Name, ct);
            if(hasEstudante) return Results.Conflict("Estudante Já existe!");
            
            var newEstudante = await _estudanteDao.Save(new Estudante(request.Name), ct);
            await _estudanteDao.SaveChangesAsync(ct);

            return Results.Ok(new EstudanteDTO(newEstudante.Id, newEstudante.Name));
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> All(CancellationToken ct)
    {
        try{
            var estudantesList = await _estudanteDao.All(ct);
            return Results.Ok(estudantesList);

        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Update(Guid Id, UpdateEstudanteRequest request, CancellationToken ct)
    {
        try{
            var estudante = await _estudanteDao.FindById(Id, ct);
            if(estudante == null) return Results.NotFound();

            estudante.SetName(request.Name);
            await _estudanteDao.SaveChangesAsync(ct);
            
            return Results.Ok(new EstudanteDTO(estudante.Id, estudante.Name));
            
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }

    public async Task<IResult> Delete(Guid Id, CancellationToken ct)
    {
        try{
            var estudante = await _estudanteDao.FindById(Id, ct);
            if(estudante == null) return Results.NotFound();

            estudante.Deactive();
            await _estudanteDao.SaveChangesAsync(ct);

            return Results.Ok();
            
        }catch(Exception ex){
            return Results.Problem($"Erro inesperado: {ex.Message}");
        }
    }
}