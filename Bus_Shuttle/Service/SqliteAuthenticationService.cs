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
        
        // Log all usernames and passwords for debugging purposes
        /*
        var allUsers = await _dbContext.User.ToListAsync();
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("All usernames and passwords:");
        foreach (var u in allUsers)
        {
            sb.AppendLine($"Username: {u.UserName}, Password: {u.Password}");
        }
        Console.WriteLine(sb.ToString());
        */

        // Find the user by username
        var dbUser = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == user.UserName);

        // If user not found, or passwords don't match, return false
        if (dbUser == null || !VerifyPassword(user.Password, dbUser.Password))
        {
            return false;
        }

        // User found and passwords match
        return true;
    }

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        // Implement your password verification logic here
        // For demonstration purposes, let's assume plain text comparison
        return enteredPassword == storedPassword;
    }
    
    
}