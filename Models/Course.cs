namespace ApiCrud.Models;

public class Course(string name)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public bool Status { get; private set; } = true;

    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public void SetName(string name)
    {
        Name = name;
    }

    public void Deactive()
    {
        Status = false;
    }
}