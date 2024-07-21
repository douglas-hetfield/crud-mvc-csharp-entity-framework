namespace ApiCrud.Models;

public class StudentCourse(Guid StudentId, Guid CourseId)
{
    public Guid StudentId { get; set; } = StudentId;
    public Student Student { get; set; }

    public Guid CourseId { get; set; } = CourseId;
    public Course Course { get; set; }

}