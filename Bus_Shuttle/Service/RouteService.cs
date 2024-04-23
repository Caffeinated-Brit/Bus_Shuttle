using System;
using System.Collections.Generic;
using System.Linq;
using Bus_Shuttle.Controllers;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
using DomainModel;
namespace Bus_Shuttle.Service
{
    public class RouteService : IRouteService
    {
        private readonly BusDb _busDb;
        private readonly ILogger<HomeController> _logger;

        public RouteService(BusDb busDb, ILogger<HomeController> logger)
        {
            _busDb = busDb;
            _logger = logger;
        }
        public List<DomainModel.DomainModel.Route> GetRoutes()
        {
            var routeList = _busDb.Route.Select(r => new DomainModel.DomainModel.Route(r.Id, r.Order, r.StopId, r.LoopId)).ToList();
            return routeList;
        }

        public void UpdateRouteByID(int id, int order)
        {
            Console.Write("id: " + id);
            Console.Write("order: " + order);
            var route = _busDb.Route.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                route.Order = order;
                _busDb.SaveChanges();
                _logger.LogInformation("Route updated with ID {RouteId}, new order: {Order}", id, order);
            }
            else
            {
                _logger.LogWarning("No route found with ID {RouteId}", id);
            }
        }
        
        public void CreateRoute(int order, int stopId, int loopId)
        {
            var newRoute = new Database.Route
            {
                Order = order,
                StopId = stopId,
                LoopId = loopId,

            };
            _busDb.Route.Add(newRoute);
            _busDb.SaveChanges();
            _logger.LogInformation("New route created with order {Order}, stop ID {StopId}, and loop ID {LoopId}", order, stopId, loopId);
        }

        public DomainModel.DomainModel.Route? FindRouteByID(int id)
        {
            var route = _busDb.Route.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                return new DomainModel.DomainModel.Route(route.Id, route.Order, route.StopId, route.LoopId);
            }
            return null;
        }
        public void DeleteRoute(int id)
        {
            var route = _busDb.Route.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                _busDb.Route.Remove(route);
                _busDb.SaveChanges();
                _logger.LogInformation("Route with ID {RouteId} deleted successfully", id);
            }
            else
            {
                _logger.LogWarning("Route with ID {RouteId} not found", id);
            }
        }
    }
}