using Microsoft.EntityFrameworkCore;
using PizzaStoreMiniAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString= builder.Configuration.GetConnectionString("Pizzas")??
    "Data Source=Pizza.db";
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDbContext<PizzaDb>(OptionsBuilderConfigurationExtensions=>OptionsBuilderConfigurationExtensions.UseInMemoryDatabase("items"));
builder.Services.AddSqlite<PizzaDb>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title ="PizzaStore Mini API",
        Description ="Its a mini api in .net core",
        Version ="v1"
    });

});
var app = builder.Build();
if(app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","PizzaStore Mini API V1");
});

app.MapGet("/pizzas/{id}",async (PizzaDb db,int id)=>await db.Pizzas.FindAsync(id));
app.MapGet("/pizzas",async (PizzaDb db)=>await db.Pizzas.ToListAsync());
app.MapPost("/pizzas",async (PizzaDb db,Pizza pizza) =>{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}",pizza);

});
app.MapPut("/pizzas",async (PizzaDb db,Pizza updatePizaa,int id) =>
{
    var pizza=await db.Pizzas.FindAsync(id);
    if(pizza is null) return Results.NotFound();
    pizza.Name=updatePizaa.Name;
    pizza.Description=updatePizaa.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/pizzas/{id}",async (PizzaDb db,int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if(pizza is null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
