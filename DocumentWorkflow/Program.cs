using DocumentWorkflow.Core.DAL;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Services;
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
builder.Services.AddTransient<StudentsRepository>();

builder.Services.AddSingleton<OrgSettings>();

builder.Services.AddTransient<TemplateParser>();
builder.Services.AddTransient<DocumentCreator>();
builder.Services.AddTransient<ExcelParser>();
builder.Services.AddTransient<StudentsImporter>();

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

    var orgSettings = services.GetRequiredService<OrgSettings>();
    if (string.IsNullOrWhiteSpace(orgSettings.FullName))
    {
        orgSettings.FullName = "Муниципальное казённое общеобразовательное учреждение Таежниниская школа № 20";
        orgSettings.FullNameGenitiveCase = "Муниципальным казённым общеобразовательным учреждением Таежниниская школа № 20";
        orgSettings.Address = "663467, Красноярский край, Богучанский район, п. Таежный, ул. Новая, 15";
        orgSettings.Phone = "8 (39162) 26-606";
        orgSettings.Email = "tsosh20@mail.ru";
        orgSettings.INN = "2407063008";
        orgSettings.KPP = "240701001";
    }

    var studentsImporter = services.GetRequiredService<StudentsImporter>();
    studentsImporter.Import(Path.Combine(AppContext.BaseDirectory, "Import", "Students.csv"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
