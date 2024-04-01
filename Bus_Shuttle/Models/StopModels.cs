namespace Bus_Shuttle.Models;

public class StopModels
{
    public class StopEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static StopEditModel FromStop(DomainModel.DomainModel.Stop stop)
        {
            return new StopEditModel
            {
                Id = stop.Id,
                Name = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            };
        }
    }
    
    public class StopCreateModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    
    public class StopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static StopViewModel FromStop(DomainModel.DomainModel.Stop stop)
        {
            return new StopViewModel
            {
                Id = stop.Id,
                Name = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            };
        }
    }
    
    
}