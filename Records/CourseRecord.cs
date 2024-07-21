namespace ApiCrud.Records;

public record AddCourseRequest(string Name);

public record UpdateCourseRequest(string Name);

public record AddStudentOnCourseRequest(Guid StudentId, Guid CourseId);