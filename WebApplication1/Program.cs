var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); 
app.UseRouting(); 

app.MapControllerRoute(
    name: "machines",
    pattern: "machines/",
    defaults: new { controller = "Machine", action = "Index" });

app.MapControllerRoute(
    name: "ajout",
    pattern: "ajout/",
    defaults: new { controller = "Machine", action = "Ajouter" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Machine}/{action=Index}/{id?}");

app.Run();


app.MapControllerRoute(
    name: "machines",
    pattern: "machines/",
    defaults: new { controller = "Machine", action = "Index" });

app.MapControllerRoute(
    name: "ajout",
    pattern: "ajout/",
    defaults: new { controller = "Machine", action = "Ajouter" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Machine}/{action=Index}/{id?}");
