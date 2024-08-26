using Pendientes.Controllers;

var builder = WebApplication.CreateBuilder(args);


// Añadir HttpClient
builder.Services.AddHttpClient<AccountController>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/AuthPendientes/token");
});

builder.Services.AddHttpClient<PendientesController>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/api/Pendientes");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
