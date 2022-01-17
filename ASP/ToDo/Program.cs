using Todo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//app.MapGet("/", () => "Hello Marcelo");

app.MapControllers();


app.Run();