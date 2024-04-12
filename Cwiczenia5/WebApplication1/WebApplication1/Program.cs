using WebApplication1;
using WebApplication1.Classes;
using WebApplication1.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IAnimalDb, AnimalDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*app.MapGet("/animals", (IAnimalDb animalDb) =>
{
    return Results.Ok(animalDb.GetAll());
});

app.MapGet("/animals/{id}", (IAnimalDb animalDb, int id) =>
{
    var animal = animalDb.GetById(id);
    if (animal is null) return Results.NotFound();
    return Results.Ok(animal);

});

app.MapGet("/animals/{id}/appointment", (IAnimalDb animalDb, int id) =>
{
    var animal = animalDb.GetById(id);
    if (animal is null) return Results.NotFound();
    return Results.Ok(animal.GetAppointments());
});

app.MapPost("/animals/{id}/appointment", (IAnimalDb animalDb, Appointment appointment,int id) =>
{
    var animal = animalDb.GetById(id);
    if (animal is null) return Results.NotFound();
    animal.AddAppointment(appointment);
    return Results.Created();
});

app.MapPost("/animals", (IAnimalDb animalDb, Animal animal) =>
{
    animalDb.AddAnimal(animal);
    return Results.Created();
});

app.MapPut("/animals/{id}", (IAnimalDb animalDb, Animal animal, int id) =>
{
    var isUpdated = animalDb.UpdateAnimal(animal, id);
    if (isUpdated) return Results.NoContent();
    return Results.NotFound();
});

app.MapDelete("/animals/{id}", (IAnimalDb animalDb, int id) =>
{
    var isRemoved = animalDb.RemoveAnimalById(id);
    if (isRemoved) return Results.NoContent();
    return Results.NotFound();
});*/
app.MapControllers();
app.Run();