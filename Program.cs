using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services MVC
builder.Services.AddControllersWithViews();

// Ajouter le contexte de base de données
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();