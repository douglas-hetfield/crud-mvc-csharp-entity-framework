using ApiCrud.Records;
using ApiCrud.Services;

namespace ApiCrud.Routes;

public static class EstudantesRoutes
{
    public static void addRoutesEstudantes(WebApplication app)
    {
        var groupRoutesEstudante = app.MapGroup("estudantes");

        groupRoutesEstudante.MapPost("", async (AddEstudanteRequest request, EstudanteService estudanteService, CancellationToken ct) => {
            return await estudanteService.Save(request, ct);
        });

        groupRoutesEstudante.MapGet("", async (EstudanteService estudanteService, CancellationToken ct) => {
            return await estudanteService.All(ct);
        });

        groupRoutesEstudante.MapPut("{id:guid}", async (Guid id, UpdateEstudanteRequest request, EstudanteService estudanteService, CancellationToken ct) => {
            return await estudanteService.Update(id, request, ct);
        });

        groupRoutesEstudante.MapDelete("{id:guid}", async (Guid Id, EstudanteService estudanteService, CancellationToken ct) => {
            return await estudanteService.Delete(Id, ct);
        });
    }
} 