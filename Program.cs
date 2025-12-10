using MaestroDeCajas.Data;
using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Servicios base
builder.Services.AddRazorPages();

// Autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

// Inicializar base de datos
Database.Initialize();

// Servicios de negocio
builder.Services.AddScoped<PosServiceWeb>();
builder.Services.AddScoped<RposServiceWeb>();
builder.Services.AddScoped<AutoservicioServiceWeb>();
builder.Services.AddScoped<CelularGuardiaServiceWeb>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
