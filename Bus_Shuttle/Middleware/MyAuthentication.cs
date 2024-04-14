using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Bus_Shuttle.Database;
using Bus_Shuttle.Service;


namespace Bus_Shuttle.Middleware
{
    public class MyAuthentication
    {
        
        private readonly RequestDelegate _next;

        public MyAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMyAuthenticationService authenticationService)
        {
            string username = "";
            string password = "";
            var dbContext = context.RequestServices.GetRequiredService<BusDb>();
            
            if (context.Request.HasFormContentType)
            {
                username = context.Request.Form["username"];
                password = context.Request.Form["password"];
            }
            
            
            var isAuthenticated = await authenticationService.Authenticate(new User { UserName = username, Password = password });


            if (isAuthenticated)
            {
                Console.WriteLine("User Authenticated");
                var user = dbContext.User.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    if (user.IsManager)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, "Manager")
                        };
                        
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        await context.SignInAsync("Cookies", new ClaimsPrincipal(identity), authProperties);
                        context.Response.Redirect("/Home/Index");
                        return;
                    }
                    
                    if (user.IsDriver && user.IsAuthorizedDriver)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, "Driver")
                        };
                        
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        await context.SignInAsync("Cookies", new ClaimsPrincipal(identity), authProperties);
                        //context.Response.Redirect("/Home/DriverTempPage");
                        context.Response.Redirect("/Home/DriverSelection");
                        
                        
                        //logging
                        foreach (var claim in identity.Claims)
                        {
                            Console.WriteLine($"Claim: {claim.Type}, Value: {claim.Value}");
                        }
                        return;
                    }

                    if (user is { IsDriver: true, IsAuthorizedDriver: false })
                    {
                        context.Response.Redirect("/Home/DriverNotAuthorized");
                        Console.WriteLine("Is driver but not authorized");
                    }
                    
                }

                if (user == null)
                {
                    context.Response.Redirect("/Home/LoginView");
                }

            }
            await _next(context); 
            
            } 
        }
}