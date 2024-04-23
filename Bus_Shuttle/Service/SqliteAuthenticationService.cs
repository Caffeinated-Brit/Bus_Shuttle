using System.Security.Claims;
using System.Text;
using Bus_Shuttle.Controllers;
using Bus_Shuttle.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bus_Shuttle.Database;


namespace Bus_Shuttle.Service;

public class SqliteAuthenticationService : IMyAuthenticationService
{
    private readonly BusDb _dbContext;
    private readonly ILogger<HomeController> _logger;

    public SqliteAuthenticationService(BusDb dbContext, ILogger<HomeController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<bool> Authenticate(User user)
    {
        // this runs without being given a user every time a page is accesed, so no logging is done here becase its only
        // really matters on the login so its logged there
        
        //_logger.LogInformation("Authenticating user {UserName}", user.UserName);
        
        var dbUser = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == user.UserName);
        
        if (dbUser == null || !VerifyPassword(user.Password, dbUser.Password))
        {
            //_logger.LogWarning("Authentication failed for user {UserName}", user.UserName);
            return false;
        }
        //_logger.LogInformation("User {UserName} authenticated successfully", user.UserName);
        return true;
    }

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        return enteredPassword == storedPassword;
    }
    
    
}