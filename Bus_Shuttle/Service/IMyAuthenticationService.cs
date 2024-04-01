using Bus_Shuttle.Database;

namespace Bus_Shuttle.Service;

public interface IMyAuthenticationService
{
    Task<bool> Authenticate(User user);
}