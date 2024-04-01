using DomainModel;
namespace Bus_Shuttle.Service;

public interface IStopService
{
    List<DomainModel.DomainModel.Stop> GetStops();
    void UpdateStopByID(int id, string name, double latitude, double longitude);
    void CreateStop(string name, double latitude, double longitude);
    DomainModel.DomainModel.Stop? FindStopByID(int id);
    void DeleteStop(int id);
}