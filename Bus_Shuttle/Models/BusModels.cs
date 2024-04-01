using System.ComponentModel.DataAnnotations;

namespace Bus_Shuttle.Models;

public class BusModels
{
    public class BusCreateModel
    {
        public int Id { get; set; }

        public int BusNumber { get; set; }
    }
    
    public class BusViewModel
    {
        public int Id { get; set; }
        public int BusNumber { get; set; }
        [Display(Name = "Bus Number")]

        public static BusViewModel FromBus(DomainModel.DomainModel.Bus bus)
        {
            return new BusViewModel
            {
                Id = bus.Id,
                BusNumber = bus.BusNumber
            };
        }
    }
    
    public class BusEditModel
    {
        public int Id { get; set; }
        public int BusNumber { get; set; }


        public static BusEditModel FromBus(DomainModel.DomainModel.Bus bus)
        {
            return new BusEditModel
            {
                Id = bus.Id,
                BusNumber = bus.BusNumber,
            };
        }
    }
    
}