using Logique.Services.Interfaces;
using Logique.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using TeleCentre.Web.Portal.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LogiqueDBContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextConnection")));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserInterface, UserRepository>();
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
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
