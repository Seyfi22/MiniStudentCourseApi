using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs.Course;
using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Mappings;
using MiniStudentCourseApi.Services;
using MiniStudentCourseApi.Services.Implementations;
using MiniStudentCourseApi.Services.Interfaces;
using MiniStudentCourseApi.Validators.Student;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// FluentValidation configuration

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateStudentDtoValidator>();
        fv.ImplicitlyValidateChildProperties = true;
    });



// AutoMapper configuration
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// DbContext configuration
builder.Services.AddDbContext<StudentCourseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Register Generic Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

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
