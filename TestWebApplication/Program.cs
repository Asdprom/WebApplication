using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TestDbContext>(options => options.UseNpgsql(
                                                 builder.Configuration.GetConnectionString("Default")));
builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();
DbInitializer.InitializeDb(app);

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

app.Run();
