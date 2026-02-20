using System.Data.Common;
using Todo.DAL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DbConnectionOptions
{
    ConnectionString = connection
});
builder.Services.AddScoped<Todo.DAL.Interface.IAuthentication, Todo.DAL.Repository.UserRepository>();
builder.Services.AddScoped<Todo.BAL.Interface.IUserInterface, Todo.BAL.Repository.IUserRepository>();
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
