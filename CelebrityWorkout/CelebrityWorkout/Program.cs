using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CelebrityWorkout.Data;
using Microsoft.OpenApi.Models;
using CelebrityWorkout.Interfaces;
using CelebrityWorkout.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 👤 Identity setup
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 🎯 MVC & Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 🧠 Dependency Injection - Services
builder.Services.AddScoped<ICelebrityService, CelebrityService>();
builder.Services.AddScoped<IMovieRoleService, MovieRoleService>(); // FIXED name
builder.Services.AddScoped<IMovieCharacterService, MovieCharacterService>();
builder.Services.AddScoped<IWorkoutRoutineService, WorkoutRoutineService>();

// 📦 Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Celebrity Workout API",
        Version = "v1",
        Description = "Explore celebrity workouts, movie roles, and character routines."
    });
});

var app = builder.Build();

// 🌐 Middleware & Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Celebrity Workout API v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 🗺️ Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

