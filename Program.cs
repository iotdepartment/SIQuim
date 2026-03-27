using Microsoft.EntityFrameworkCore;
using SIQuim.Models; // Ajusta al namespace donde está AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inventario}/{action=Index}/{id?}");

app.Run();