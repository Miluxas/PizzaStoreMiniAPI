using PizzaStoreMiniAPI.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
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

app.MapGet("/pizzas/{id}",(int id)=>PizzaStoreMiniAPI.DB.PizzaDB.GetPizza(id));
app.MapGet("/pizzas",()=>PizzaStoreMiniAPI.DB.PizzaDB.GetPizzas);
app.MapPost("/pizzas",(Pizza pizaa)=>PizzaStoreMiniAPI.DB.PizzaDB.CreatePizza(pizaa));
app.MapPut("/pizzas",(Pizza pizaa)=>PizzaStoreMiniAPI.DB.PizzaDB.UpdatePizza(pizaa));
app.MapDelete("/pizzas/{id}",(int id)=>PizzaStoreMiniAPI.DB.PizzaDB.RemovePizza(id));

app.Run();
