namespace Bus_Shuttle.Models;

public class RouteModels
{
    public class RouteEditModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int StopId { get; set; }
        public StopModels.StopEditModel Stop { get; set; }
        public int LoopId { get; set; }
        public LoopModels.LoopEditModel Loop { get; set; }


        public static RouteEditModel FromRoute(DomainModel.DomainModel.Route route)
        {
            return new RouteEditModel
            {
                Id = route.Id,
                Order = route.Order,
                StopId = route.StopId,
                LoopId = route.LoopId,
            };
        }
    }
    
    public class RouteCreateModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int StopId { get; set; }
        public int LoopId { get; set; }
        
        public List<LoopModels.LoopViewModel> AvailableLoops { get; set; } = new List<LoopModels.LoopViewModel>();
        public List<StopModels.StopViewModel> AvailableStops { get; set; } = new List<StopModels.StopViewModel>();
    }
    
    public class RouteViewModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int StopId { get; set; }
        public int LoopId { get; set; }
        public string LoopName { get; set; }
        public string StopName { get; set; }

        public static RouteViewModel FromRoute(DomainModel.DomainModel.Route route)
        {
            return new RouteViewModel
            {
                Id = route.Id,
                Order = route.Order,
                StopId = route.StopId,
                LoopId = route.LoopId
            };
        }
    }
}