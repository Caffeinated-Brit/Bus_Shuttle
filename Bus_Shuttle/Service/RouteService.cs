using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
using DomainModel;
namespace Bus_Shuttle.Service
{
    public class RouteService : IRouteService
    {
        private readonly BusDb _busDb;

        public RouteService(BusDb busDb)
        {
            _busDb = busDb;
        }
        public List<DomainModel.DomainModel.Route> GetRoutes()
        {
            var routeList = _busDb.Route.Select(r => new DomainModel.DomainModel.Route(r.Id, r.Order)).ToList();
            return routeList;
        }

        public void UpdateRouteByID(int id, int order)
        {
            var route = _busDb.Route.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                route.Order = order;
                _busDb.SaveChanges();

            }
        }
        public void CreateRoute(int order)
        {
            var newRoute = new Database.Route
            {
                Order = order
            };
            _busDb.Route.Add(newRoute);
            _busDb.SaveChanges();
        }

        public DomainModel.DomainModel.Route? FindRouteByID(int id)
        {
            var route = _busDb.Route.FirstOrDefault(r => r.Id == id);
            if (route != null)
            {
                return new DomainModel.DomainModel.Route(route.Id, route.Order);
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
            }
        }
    }
}