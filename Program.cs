using Microsoft.EntityFrameworkCore;
using TopSecret.Data;

var builder = WebApplication.CreateBuilder(args);

// Usa PostgreSQL invece di MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbPassword))
{
    Console.WriteLine(" ATTENZIONE: La variabile d'ambiente DB_PASSWORD non è stata trovata!");
}
else
{
    Console.WriteLine(" La variabile DB_PASSWORD è stata letta correttamente.");
}

// Crea manualmente la stringa di connessione per il debug
var connectionString = $"Host=dpg-cudnflq3esus73c810ug-a.frankfurt-postgres.render.com;Port=5432;Database=punteggio;Username=punteggio_user;Password={dbPassword};";

Console.WriteLine($" Stringa di connessione usata: {connectionString}");


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
