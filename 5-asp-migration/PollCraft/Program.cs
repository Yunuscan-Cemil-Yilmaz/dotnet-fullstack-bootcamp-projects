using Microsoft.EntityFrameworkCore;
using PollCraft.Infrastructure.Data;
using PollCraft.Mappings;
using PollCraft.Repositories;
using PollCraft.Services;
using PollCraft.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api/Auth/register"),
    subApp => subApp.UseMiddleware<RegisterMiddleware>()
);

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api/Auth/login"),
    subApp => subApp.UseMiddleware<LoginMiddleware>()
);

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/application"),
    subApp => subApp.UseMiddleware<ApplicationViewMiddleware>()
);

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api/Auth/logout"),
    subApp => subApp.UseMiddleware<LogoutMiddleware>()
);


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
