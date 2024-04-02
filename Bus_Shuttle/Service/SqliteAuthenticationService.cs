using System.Security.Claims;
using System.Text;
using Bus_Shuttle.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bus_Shuttle.Database;


namespace Bus_Shuttle.Service;

public class SqliteAuthenticationService : IMyAuthenticationService
{
    private readonly BusDb _dbContext;

    public SqliteAuthenticationService(BusDb dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> Authenticate(User user)
    {
        
        Console.WriteLine("Name: " + user.UserName);
        Console.WriteLine("Password: " + user.Password);
        
        
        var dbUser = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == user.UserName);
        
        if (dbUser == null || !VerifyPassword(user.Password, dbUser.Password))
        {
            return false;
        }
        
        return true;
    }

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        return enteredPassword == storedPassword;
    }
    
    
}