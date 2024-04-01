using DomainModel;
namespace Bus_Shuttle.Service;

public interface IRouteService
{
    List<DomainModel.DomainModel.Route> GetRoutes();
    void UpdateRouteByID(int id, int order);
    void CreateRoute(int order);
    DomainModel.DomainModel.Route? FindRouteByID(int id);
    void DeleteRoute(int id);
}