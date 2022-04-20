using DocumentWorkflow.Core.DAL;
using DocumentWorkflow.Core.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using DbContext = DocumentWorkflow.Core.DAL.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlite("Data Source=DocumentWorkflow.db;"));

builder.Services.AddTransient<CategoriesRepository>();
builder.Services.AddTransient<TypesRepository>();
builder.Services.AddTransient<DocumentsRepository>();

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5074); // to listen for incoming http connection
//    options.ListenAnyIP(7074, configure => configure.UseHttps()); // to listen for incoming https connection
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
