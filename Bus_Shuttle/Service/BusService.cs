using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
namespace Bus_Shuttle.Service
{
    public class BusService : IBusService
    {
        private readonly BusDb _busDb;

        public BusService(BusDb busDb)
        {
            _busDb = busDb;
        }
        public List<DomainModel.DomainModel.Bus> GetBuses()
        {
            var busList = _busDb.Bus.Select(b => new DomainModel.DomainModel.Bus(b.Id, b.BusNumber)).ToList();
            return busList;
            
        }

        public void UpdateBusByID(int id, int busNumber)
        {
            var bus = _busDb.Bus.FirstOrDefault(b => b.Id == id);
            if (bus != null)
            {
                bus.BusNumber = busNumber;
                _busDb.SaveChanges();

            }
        }

        public void CreateBus(int busNumber)
        {
            var newBus = new Database.Bus
            {
                BusNumber = busNumber
            };
            _busDb.Bus.Add(newBus);
            _busDb.SaveChanges();

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
            if (bus != null)
            {
                _busDb.Bus.Remove(bus);
                _busDb.SaveChanges();
            }
        }
    }





}