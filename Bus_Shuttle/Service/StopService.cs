using System;
using System.Collections.Generic;
using System.Linq;
using Bus_Shuttle.Controllers;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
namespace Bus_Shuttle.Service
{
    public class StopService : IStopService
    {
        private readonly BusDb _busDb;
        private readonly ILogger<HomeController> _logger;

        public StopService(BusDb busDb, ILogger<HomeController> logger)
        {
            _busDb = busDb;
            _logger = logger;
        }
        public List<DomainModel.DomainModel.Stop> GetStops()
        {
            var stopList = _busDb.Stop.Select(s => new DomainModel.DomainModel.Stop(s.Id, s.Name, s.Latitude, s.Longitude)).ToList();
            return stopList;
        }

        public void UpdateStopByID(int id, string name, double latitude, double longitude)
        {
            var stop = _busDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                stop.Name = name;
                stop.Latitude = latitude;
                stop.Longitude = longitude;
                _busDb.SaveChanges();
                _logger.LogInformation("Stop with ID {StopId} updated successfully", id);
            }
            else
            {
                _logger.LogWarning("Stop with ID {StopId} not found for updating", id);
            }
        }

        public void CreateStop(string name, double latitude, double longitude)
        {
            var newStop = new Database.Stop
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };
            _busDb.Stop.Add(newStop);
            _busDb.SaveChanges();
            _logger.LogInformation("Stop created successfully with ID {StopId}", newStop.Id);
        }

        public DomainModel.DomainModel.Stop? FindStopByID(int id)
        {
            var stop = _busDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                return new DomainModel.DomainModel.Stop(stop.Id, stop.Name, stop.Latitude, stop.Longitude);
            }
            return null;
        }
        public void DeleteStop(int id)
        {
            var stop = _busDb.Stop.FirstOrDefault(s => s.Id == id);
            if (stop != null)
            {
                _busDb.Stop.Remove(stop);
                _busDb.SaveChanges();
                _logger.LogInformation("Stop with ID {StopId} deleted successfully", id);
            }
            else
            {
                _logger.LogWarning("Stop with ID {StopId} not found", id);
            }
        }
    }





}