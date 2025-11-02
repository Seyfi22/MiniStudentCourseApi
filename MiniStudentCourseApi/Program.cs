using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs;
using MiniStudentCourseApi.Mappings;
using MiniStudentCourseApi.Services;
using MiniStudentCourseApi.Services.Implementations;
using MiniStudentCourseApi.Services.Interfaces;
using MiniStudentCourseApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// FluentValidation configuration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<StudentDtoValidator>();

// AutoMapper configuration
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// DbContext configuration
builder.Services.AddDbContext<StudentCourseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Register Generic Services
builder.Services.AddScoped<IGenericService<StudentDto>, StudentService>();
builder.Services.AddScoped<IGenericService<CourseDto>, CourseService>();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
