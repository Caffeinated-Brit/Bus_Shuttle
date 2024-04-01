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
            
            if (context.Request.HasFormContentType)
            {
                username = context.Request.Form["username"];
                password = context.Request.Form["password"];
            }
            
            
            var isAuthenticated = await authenticationService.Authenticate(new User { UserName = username, Password = password });
            
            
            if (isAuthenticated)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                };

                var identity = new ClaimsIdentity(claims, "CustomAuthentication");

                
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await context.SignInAsync("Cookies", new ClaimsPrincipal(identity), authProperties);
                context.Response.Redirect("/Home/Index");
                return;
            }
            

            
            await _next(context);
        }
    }
}