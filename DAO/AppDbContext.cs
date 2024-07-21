using Microsoft.EntityFrameworkCore;
using ApiCrud.Models;

namespace ApiCrud.DAO;

public class AppDbContext : DbContext
{
    public DbSet<Student> Student { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<StudentCourse> StudentCourse { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //configure mysql access
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); // logs information on terminal 
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>()
            .HasKey(et => new { et.StudentId, et.CourseId });

        modelBuilder.Entity<StudentCourse>()
            .HasOne(et => et.Student)
            .WithMany(e => e.StudentCourses)
            .HasForeignKey(et => et.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(et => et.Course)
            .WithMany(e => e.StudentCourses)
            .HasForeignKey(et => et.CourseId);

        base.OnModelCreating(modelBuilder);
    }
}