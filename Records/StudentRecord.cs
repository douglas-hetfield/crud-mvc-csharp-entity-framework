using System.ComponentModel.DataAnnotations;

namespace ApiCrud.Records;

public record AddStudentRequest(
    [property: Required(ErrorMessage = "Campo Name é requerido.")]
    [property: StringLength(100, MinimumLength = 3, ErrorMessage = "O Campo Name deve ter de 3 a 100 caractéres.")]
    string Name
);

public record UpdateStudentRequest(string Name);