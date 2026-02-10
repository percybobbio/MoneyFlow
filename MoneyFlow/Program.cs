using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoneyFlow.Context;
using MoneyFlow.Managers;
using System.Runtime.ConstrainedExecution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Agregar el contexto de la base de datos para poder usarlo en los managers y controladores, esto es necesario para poder acceder a la base de datos y realizar operaciones CRUD, además de configurar la cadena de conexión a la base de datos que se encuentra en el archivo appsettings.json, en este caso se usa Npgsql para conectarse a una base de datos PostgreSQL, pero se puede cambiar a otro proveedor de base de datos si es necesario.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Cadena local de conexion a la base de datos, se puede cambiar por una cadena de conexión a una base de datos en la nube o en otro servidor, esto es solo un ejemplo para desarrollo local.
    //options.UseNpgsql(builder.Configuration.GetConnectionString("sqlstring"));

    options.UseNpgsql(builder.Configuration.GetConnectionString("AzurePostgresConnection"));

    // Pega la cadena de conexión directamente aquí(Solo para hacer el Update-Database)
    //options.UseNpgsql("Host=db-moneyflow-percy.postgres.database.azure.com; Database=moneyFlowDB; Username=dbadmin; Password=Dreamtheater/2402; Port=5432; Ssl Mode=Require;");
});

//Agregar inyección de dependencias para los managers caso contrario no se puede usar el contructor para inyectar los managers en los controladores y ver los nav del proyecto
builder.Services.AddScoped<ServiceManager>();
builder.Services.AddScoped<TransactionManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
