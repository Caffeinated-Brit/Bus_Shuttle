using DomainModel;
namespace Bus_Shuttle.Service;

public interface IUserService 
{
    List<DomainModel.DomainModel.User> GetUsers();
    DomainModel.DomainModel.User? FindUserByID(int id);
    void CreateUser(string firstname, string lastname, string username, string password);
    public List<DomainModel.DomainModel.User> GetDrivers();
    
    public List<DomainModel.DomainModel.User> GetUnauthorizedDrivers();
    void SetAuthorized(int userId);
    
}