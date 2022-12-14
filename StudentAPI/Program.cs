using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using StudentAPI.BLL;
using StudentAPI.Models;
using StudentAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//http://localhost:4200/
builder.Services.AddCors(option =>
    option.AddPolicy("DefaultPolicy", policy =>
        policy.AllowAnyHeader().
            AllowAnyMethod().
            AllowAnyOrigin()));

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDegreeRepository, DegreeRepository>();
builder.Services.AddScoped<StudentBLL>();



builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<UniversityContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")
    )
   );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("DefaultPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

