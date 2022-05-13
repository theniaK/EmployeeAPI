using EmployeeAPI.Processors;
using EmployeeAPI.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmployeesProcessor, EmployeesProcessor>();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
    return new MongoClient(uri);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(
    options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
);

//app.UseAuthorization();

app.MapControllers();

app.Run();
