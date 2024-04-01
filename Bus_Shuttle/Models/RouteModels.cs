namespace Bus_Shuttle.Models;

public class RouteModels
{
    public class RouteEditModel
    {
        public int Id { get; set; }
        public int Order { get; set; }


        public static RouteEditModel FromRoute(DomainModel.DomainModel.Route route)
        {
            return new RouteEditModel
            {
                Id = route.Id,
                Order = route.Order
            };
        }
    }
    
    public class RouteCreateModel
    {
        public int Id { get; set; }

        public int Order { get; set; }
    }
    
    public class RouteViewModel
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public static RouteViewModel FromRoute(DomainModel.DomainModel.Route route)
        {
            return new RouteViewModel
            {
                Id = route.Id,
                Order = route.Order
            };
        }
    }
}