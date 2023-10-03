using Data.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
    ).AddEntityFrameworkStores<Context>();
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
app.Run();
