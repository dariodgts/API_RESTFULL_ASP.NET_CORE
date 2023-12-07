using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2))),
    new(6, "saludos najal",DateOnly.FromDateTime(DateTime.Now.AddDays(3))),
    new(7, "bienvenido perche",DateOnly.FromDateTime(DateTime.Now.AddDays(4))),
    new(8, "la gente de secreto se conoce",DateOnly.FromDateTime(DateTime.Now.AddDays(5))),
    new(9,"programando APIs",DateOnly.FromDateTime(DateTime.Now),"PROGRAMACION WEB", "pase un excelente dia", DateOnly.FromDateTime(DateTime.Now.AddDays(6)),DateOnly.FromDateTime(DateTime.Now.AddDays(7)),DateOnly.FromDateTime(DateTime.Now.AddDays(8)))
    
    
    
};

var todosApi = app.MapGroup("/todos");
todosApi.MapPost("/", () => sampleTodos);
todosApi.MapPost("/{id}", (int id)=>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id)=>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueByfecha_hoy = null, string? IsComplete=null, string? saludos=null,DateOnly? per=null,  DateOnly? SECRET=null, DateOnly? Prog=null);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    private object _requestHandler;

    
}
