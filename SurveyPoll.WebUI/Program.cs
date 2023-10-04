using Data.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using SurveryPoll.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
// CORS politikalarýný ekleyin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
//servisleri container'a bu 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<QuestionOptionRepository>();
builder.Services.AddScoped<SurveyRepository>();
builder.Services.AddScoped<CorrectAnswerRepository>();
builder.Services.AddScoped<SurveyQuestionRepository>();
builder.Services.AddScoped<SurveyResponseRepository>();
builder.Services.AddScoped<AnswerRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(
    options =>
    {
        // Baþarýsýz giriþ denemeleri sayýsý
        options.Lockout.MaxFailedAccessAttempts = 4;

        // Hesabýn kilitlendiði süre (dakika cinsinden)
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    }
    ).AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders(); ;
builder.Services.AddMvc().AddFluentValidation();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAuthentication();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

        // Admin rolünü oluþtur
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            adminRole = new AppRole
            {
                Name = "Admin",
            };
            await roleManager.CreateAsync(adminRole);
        }

        // Admin kullanýcýsýný oluþtur
        var adminUser = await userManager.FindByNameAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                Name ="Admin",
                Surname="Admin",
                UserName = "admin@example.com",
                Status=1,
                Email = "admin@example.com"
                
            };

            await userManager.CreateAsync(adminUser, "12345678Eb*"); // Güçlü ve güvenli bir þifre kullanýn

            // Admin kullanýcýsýný Admin rolüne ekle
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
app.Run();
