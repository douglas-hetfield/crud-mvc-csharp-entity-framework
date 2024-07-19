using Microsoft.EntityFrameworkCore;
using ApiCrud.Models;

namespace ApiCrud.DAO;

public class AppDbContext : DbContext
{
    public DbSet<Estudante> Estudante { get; set; }

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
}