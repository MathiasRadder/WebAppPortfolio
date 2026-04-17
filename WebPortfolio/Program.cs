using DataPortfolio.Models;
using DataPortfolio.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ServicesPortfolio.Services;
using WebPortfolio.Services;

var builder = WebApplication.CreateBuilder(args);

//Setting up the connection to the database
builder.Services.AddDbContext<ProjectsWebContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("PortfolioWebConnection"),
x => x.MigrationsAssembly("DataPortfolio")));


// Add services to the container.
builder.Services.AddControllersWithViews();

//Services
builder.Services.AddTransient<ProjectPageService>();
builder.Services.AddTransient<IProjectPageRepository, SQLProjectPageRepository>();

builder.Services.AddTransient<SectionTypeService>();
builder.Services.AddTransient<ISectionTypeRepository, SQLSectionTypeRepository>();

builder.Services.AddSingleton<HelperService>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ProjectCardsRepositoryCache>();
builder.Services.AddSingleton<CreateProjectCardsVMService>();

builder.Services.AddScoped<PageSectionsRepositoryCache>();
builder.Services.AddSingleton<CreatePageSectionsVMService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(1);
    });
    app.UseHsts();

}

app.UseStatusCodePagesWithReExecute("/Home/Error");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();