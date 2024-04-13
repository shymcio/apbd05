using API;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMockDB, MockDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/animals", (IMockDB mockDb) =>
{
    return Results.Ok(mockDb.GetAllAnimals());
});

app.MapGet("/animals/{id}", (IMockDB mockDb, int id) =>
{
    var animal = mockDb.getById(id);

    if (animal is null) return Results.NotFound();
    
    return Results.Ok(animal);
});

app.MapPost("/animals", (IMockDB mockDb, Animal animal) =>
{
    mockDb.AddAnimal(animal);
    return Results.Created();
});

app.MapPut("/animals/{id}", (IMockDB mockDb, Animal animal, int id) =>
{
    var animalToEdit = mockDb.getById(id);

    if (animalToEdit is null) return Results.NotFound();
    
    mockDb.DeleteAnimal(animalToEdit);
    mockDb.AddAnimal(animal);
    return Results.NoContent();
});

app.MapDelete("/animals/{id}", (IMockDB mockDb, int id) =>
{
    var animalToDelete = mockDb.getById(id);
    
    if (animalToDelete is null) return Results.NotFound();
    
    mockDb.DeleteAnimal(animalToDelete);
    return Results.NoContent();
});

app.MapGet("/visits", (IMockDB mockDb) =>
{
    return Results.Ok(mockDb.getAllVisits());
});

app.MapPost("/visits", (IMockDB mockDb, Visit visit) =>
{
    var animal = mockDb.getById(visit.animal.Id);

    if (animal is null) return Results.NotFound();
    
    mockDb.AddVisit(visit);
    return Results.Created();
});

app.Run();