using System.Collections.Generic;

namespace Bus_Shuttle.Models
{
    public class DriverSelectionViewModel
    {
        public List<LoopModels.LoopViewModel> Loops { get; set; }
        public List<BusModels.BusViewModel> Buses { get; set; }
    }
}