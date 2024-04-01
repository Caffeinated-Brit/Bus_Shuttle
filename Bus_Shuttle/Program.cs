using Bus_Shuttle.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bus_Shuttle.Service;
using Bus_Shuttle.Database;
using Microsoft.Extensions.Options;
using Bus_Shuttle.Middleware;

namespace Bus_Shuttle;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<BusDb>(Options => Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IBusService, BusService>();
        builder.Services.AddScoped<ILoopService, LoopService>();
        builder.Services.AddScoped<EntryServiceInterface, EntryService>();
        builder.Services.AddScoped<IRouteService, RouteService>();
        builder.Services.AddScoped<IStopService, StopService>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        
         builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Home/LoginView"; 
                options.AccessDeniedPath = "/Home/AccessDenied"; 
                options.LogoutPath = "/Home/Logout"; 
                options.ReturnUrlParameter = "returnUrl"; 
            });
        
        
        builder.Services.AddScoped<IMyAuthenticationService, SqliteAuthenticationService>(); 
        
        

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
        app.UseMiddleware<MyAuthentication>();
        app.UseAuthentication();
        app.UseAuthorization();
        
        
        //TESTING ONLY
        app.Use(async (context, next) =>
        {
            var user = context.User;
            var path = context.Request.Path;
    
            Console.WriteLine("user.Identity.IsAuthenticated: " + user.Identity.IsAuthenticated);
            
            await next();
        });
        
        
        

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}