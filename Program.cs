using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var cnStr = builder.Configuration.GetConnectionString("Default");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/test", () =>
{
    var client = new MongoClient(cnStr);
    var db = client.GetDatabase("mydb");
    var cols = db.ListCollectionNames();
    if (!cols.Any())
    {
        db.CreateCollection("col1");
        db.CreateCollection("col2");
    }

    return db.ListCollectionNames().ToEnumerable();
})
.WithName("test");

app.Run();
