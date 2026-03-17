using System.Data.Common;
using Todo.BAL.Repository;
using Todo.BAL.Interface;
using Todo.DAL;
using Todo.DAL.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DbConnectionOptions
{
    ConnectionString = connection
});
builder.Services.AddScoped<Todo.DAL.Interface.IAuthentication, Todo.DAL.Repository.UserRepository>();
builder.Services.AddScoped<Todo.BAL.Interface.IUserInterface, Todo.BAL.Repository.IUserRepository>();
builder.Services.AddScoped<Todo.BAL.Interface.IManageNoteInterface, Todo.BAL.Repository.IManageNoteRepository>();
builder.Services.AddScoped<Todo.DAL.Interface.IManageNoteInterface, Todo.DAL.Repository.IManageNoteRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
