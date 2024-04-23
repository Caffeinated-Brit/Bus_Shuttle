using System;
using System.Collections.Generic;
using System.Linq;
using Bus_Shuttle.Controllers;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
namespace Bus_Shuttle.Service
{
    public class UserService : IUserService
    {
        private readonly BusDb _busDb;
        private readonly ILogger<HomeController> _logger;

        public UserService(BusDb busDb, ILogger<HomeController> logger)
        {
            _busDb = busDb;
            _logger = logger;
        }
        public List<DomainModel.DomainModel.User> GetUsers()
        {
            var userList = _busDb.User.Select(u => new DomainModel.DomainModel.User(u.Id, u.FirstName, u.LastName, u.UserName, u.Password)).ToList();
            return userList;
        }
        
        public List<DomainModel.DomainModel.User> GetUnauthorizedDrivers()
        {
            var driverList = _busDb.User
                .Where(u => u.IsDriver && !u.IsAuthorizedDriver) 
                .Select(u => new DomainModel.DomainModel.User(u.Id, u.FirstName, u.LastName, u.UserName, u.Password))
                .ToList();
            return driverList;
        }
        public void SetAuthorized(int userId)
        {
            var user = _busDb.User.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsAuthorizedDriver = true;
                _busDb.SaveChanges();
                _logger.LogInformation("User with ID {UserId} set as authorized driver successfully", userId);
            }
            else
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
            }
        }
        
        public List<DomainModel.DomainModel.User> GetDrivers()
        {
            var driverList = _busDb.User
                .Where(u => u.IsDriver && u.IsAuthorizedDriver) 
                .Select(u => new DomainModel.DomainModel.User(u.Id, u.FirstName, u.LastName, u.UserName, u.Password))
                .ToList();
            return driverList;
        }

        public void CreateUser(string firstname, string lastname, string userName, string password)
        {
            var newUser = new User
            {
                FirstName = firstname,
                LastName = lastname,
                UserName = userName,
                Password = password,
                IsAuthorizedDriver = false
                
            };
            
            _busDb.User.Add(newUser);
            _busDb.SaveChanges();
            
            int newUserId = newUser.Id;
            _logger.LogInformation("New user created with ID: {UserId}", newUserId);
            
            if (newUser.Id == 1)
            {
                newUser.IsManager = true;
                newUser.IsDriver = false; 
                _logger.LogInformation("User set as Manager");
            }
            else
            {
                newUser.IsManager = false;
                newUser.IsDriver = true;
                _logger.LogInformation("User set as Driver");
            }
            _busDb.SaveChanges();
        }
        
        public int GetUserIdByUserName(string userName)
        {
            var user = _busDb.User.FirstOrDefault(u => u.UserName == userName);
            return user.Id;
        }
        public DomainModel.DomainModel.User? FindUserByID(int id)
        {
            var user = _busDb.User.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return new DomainModel.DomainModel.User(user.Id, user.FirstName, user.LastName, user.UserName, user.Password);
            }
            return null;
        }
        
        public DomainModel.DomainModel.User? FindUserByUserName(string userName)
        {
            var user = _busDb.User.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                return new DomainModel.DomainModel.User(user.Id, user.FirstName, user.LastName, user.UserName, user.Password);
            }
            return null;
        }
        
        public void UpdateUserById(int userId, string firstname, string lastname, string userName)
        {
            var user = _busDb.User.Find(userId);
            Console.WriteLine(user);
            if (user != null)
            {
                user.FirstName = firstname;
                user.LastName = lastname;
                user.UserName = userName;
                _busDb.SaveChanges();
                _logger.LogInformation("User: {FirstName} {LastName} ({UserName}) updated successfully", user.FirstName, user.LastName, user.UserName);
            }
            else
            {
                _logger.LogWarning("User with ID {UserId} not found", userId);
            }
        }
        
        public void DeleteUserById(int userId)
        {
            var user = _busDb.User.Find(userId);
            if (user != null)
            {
                _busDb.User.Remove(user);
                _busDb.SaveChanges();
                _logger.LogInformation("User with ID: {UserId} deleted successfully", userId);
            }
        }
    }
}