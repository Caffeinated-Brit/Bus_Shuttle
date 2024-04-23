
using Route = Microsoft.AspNetCore.Routing.Route;

namespace Bus_Shuttle.Models;

public class ReportsModel
{
    public List<DomainModel.DomainModel.Route>? Routes { get; set; }
    public List<DomainModel.DomainModel.Bus>? Buses { get; set; }
    public List<DomainModel.DomainModel.Stop>? Stops { get; set; }
    public List<DomainModel.DomainModel.Entry>? EntryReport { get; set; }

    
}


