using Microsoft.AspNetCore.Builder;
using TimeExpired.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDateFileClient, DateFileClient>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:44448"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
