using Bus_Shuttle.Models;
using DomainModel;
namespace Bus_Shuttle.Service;

public interface IEntryService
{
    List<DomainModel.DomainModel.Entry> GetEntries();
    void UpdateEntryByID(int id, DateTime timeStamp, int boarded, int leftBehind);
    void CreateEntry(DateTime timeStamp, int boarded, int leftBehind);
    DomainModel.DomainModel.Entry? FindEntryByID(int id);
    void DeleteEntry(int id);
    void AddEntry(int loopId, int busId, int userId, int stopId, int boarded, int leftBehind);
}