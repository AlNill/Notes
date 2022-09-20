using Microsoft.AspNetCore.Authentication.JwtBearer;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebApi.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var builderServices = builder.Services;
builderServices.AddApplication();
IConfiguration configuration = builderServices.BuildServiceProvider().GetRequiredService<IConfiguration>();
builderServices.AddPersistence(configuration);
builderServices.AddControllers();

builderServices.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

builderServices.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builderServices.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7129/";
        options.Audience = "NotesWebAPI";
        options.RequireHttpsMetadata = false;
    });

builderServices.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
var services = app.Services;

using (var scope = services.CreateScope())
{
    try
    {
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<NotesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {

    }
}

app.UseSwagger();
app.UseSwaggerUI(config => {
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
});

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
});

app.Run();
