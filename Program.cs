using Microsoft.EntityFrameworkCore;
using TopSecret.Data;

var builder = WebApplication.CreateBuilder(args);

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbPassword))
{
    Console.WriteLine(" ERRORE: La variabile d'ambiente DB_PASSWORD non è stata trovata!");
}
else
{
    Console.WriteLine(" La variabile DB_PASSWORD è stata letta correttamente.");
}

// Costruisce la stringa di connessione con la password letta
var connectionString = $"Host=dpg-cudnflq3esus73c810ug-a.frankfurt-postgres.render.com;Port=5432;Database=punteggio;Username=punteggio_user;Password={dbPassword};";

Console.WriteLine($" Stringa di connessione usata: {connectionString}");

// Configura PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configura PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

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
