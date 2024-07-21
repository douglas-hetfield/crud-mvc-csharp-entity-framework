using ApiCrud.Routes;
using ApiCrud.DAO;
using ApiCrud.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<StudentDAO>();

builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseDAO>();

builder.Services.AddScoped<StudentCourseDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
StudentRoutes.AddRoutes(app);
CourseRoutes.AddRoutes(app);

app.Run();