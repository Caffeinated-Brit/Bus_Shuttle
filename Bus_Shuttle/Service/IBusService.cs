using DomainModel;
namespace Bus_Shuttle.Service;

public interface IBusService
{
    List<DomainModel.DomainModel.Bus> GetBusses();
    void UpdateBusByID(int id, int busNumber);
    void CreateBus(int busNumber);

    DomainModel.DomainModel.Bus? FindBusByID(int id);
    void DeleteBus(int id);
}