using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bus_Shuttle.Models;

public class EntryModels
{
    public class EntryViewModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }

        public static EntryViewModel FromEntry(DomainModel.DomainModel.Entry entry)
        {
            return new EntryViewModel
            {
                Id = entry.Id,
                TimeStamp = entry.TimeStamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind

            };
        }
    }
    
    public class EntryEditModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }

        public static EntryEditModel FromEntry(DomainModel.DomainModel.Entry entry)
        {
            return new EntryEditModel
            {
                Id = entry.Id,
                TimeStamp = entry.TimeStamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind
            };
        }
    }
    
    public class EntryCreateModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }
    }
    
    public class AddEntryViewModel
    {
        public int LoopId { get; set; }
        public int BusId { get; set; }
        public int UserId { get; set; }
        public int StopId { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }
        public int BusNumber { get; set; }
        public String? LoopName { get; set; }
        
        public String? DriverName { get; set; }
        
        public List<SelectListItem>? Stops { get; set; }
    }
    
}