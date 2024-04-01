using DomainModel;
namespace Bus_Shuttle.Service;

public interface IUserService 
{
    List<DomainModel.DomainModel.User> GetUsers();
    DomainModel.DomainModel.User? FindUserByID(int id);
    DomainModel.DomainModel.User? FindUserByUserName(string userName);
    void CreateUser(string firstname, string lastname, string username, string password);
    public List<DomainModel.DomainModel.User> GetDrivers();
    
    public List<DomainModel.DomainModel.User> GetUnauthorizedDrivers();
    void SetAuthorized(int userId);

    void UpdateUserById(int userId, string firstname, string lastname, string userName);

    void DeleteUserById(int userId);



}