using DomainModel;
namespace Bus_Shuttle.Service;

public interface ILoopService
{
    List<DomainModel.DomainModel.Loop> GetLoops();
    void UpdateLoopByID(int id, string name);
    void CreateLoop(string name);
    DomainModel.DomainModel.Loop? FindLoopByID(int id);
    void DeleteLoop(int id);
}