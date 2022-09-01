using Notes.Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
using(var serviceProvider = services.BuildServiceProvider())
{
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception ex)
    {

    }
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
