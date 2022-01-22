var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1",new Microsoft.OpenApi.Models.OpenApiInfo
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
app.MapGet("/", () => "Hello World!");

app.Run();
