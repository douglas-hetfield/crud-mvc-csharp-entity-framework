using ApiCrud.DTO;

namespace ApiCrud.DTO;

public record CourseDTO(Guid Id, string Name);

public record CourseWithStudentsDTO(Guid Id, string Name, ICollection<StudentCourseDTO> StudentCourse) : CourseDTO(Id, Name);