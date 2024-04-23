using System;
using System.Collections.Generic;
using System.Linq;
using Bus_Shuttle.Controllers;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
namespace Bus_Shuttle.Service
{
    public class BusService : IBusService
    {
        private readonly BusDb _busDb;
        private readonly ILogger<HomeController> _logger;

        public BusService(BusDb busDb, ILogger<HomeController> logger)
        {
            _busDb = busDb;
            _logger = logger;
        }
        public List<DomainModel.DomainModel.Bus> GetBuses()
        {
            var busList = _busDb.Bus.Select(b => new DomainModel.DomainModel.Bus(b.Id, b.BusNumber)).ToList();
            return busList;
            
        }

        public void UpdateBusByID(int id, int busNumber)
        {
            var bus = _busDb.Bus.FirstOrDefault(b => b.Id == id);
            _logger.LogInformation("Bus id: " + id + " updated to busNumber: " + busNumber);
            if (bus != null)
            {
                bus.BusNumber = busNumber;
                _busDb.SaveChanges();

            }
        }

        public void CreateBus(int busNumber)
        {
            var newBus = new Bus
            {
                BusNumber = busNumber
            };
            _busDb.Bus.Add(newBus);
            _busDb.SaveChanges();
            _logger.LogInformation("New bus created successfully: Bus Number {BusNumber}", busNumber);
        }

        public DomainModel.DomainModel.Bus? FindBusByID(int id)
        {
            var bus = _busDb.Bus.FirstOrDefault(b => b.Id == id);
            if (bus != null)
            {
                return new DomainModel.DomainModel.Bus(bus.Id, bus.BusNumber);
            }
            return null;
        }
        public void DeleteBus(int id)
        {
            var bus = _busDb.Bus.FirstOrDefault(b => b.Id == id);
            _logger.LogInformation("Bus with id " + id + " deleted");
            if (bus != null)
            {
                _busDb.Bus.Remove(bus);
                _busDb.SaveChanges();
            }
        }
    }





}