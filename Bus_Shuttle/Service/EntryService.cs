using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
using Bus_Shuttle.Models;

namespace Bus_Shuttle.Service
{
    public class EntryService : IEntryService
    {
        private readonly BusDb _busDb;

        public EntryService(BusDb busDb)
        {
            _busDb = busDb;
        }
        public List<DomainModel.DomainModel.Entry> GetEntries()
        {
            var entryList = _busDb.Entry.Select(e => new DomainModel.DomainModel.Entry(e.Id, e.TimeStamp, e.Boarded, e.LeftBehind)).ToList();
            return entryList;
        }
        
        public void AddEntry(int loopId, int busId, int userId, int stopId, int boarded, int leftBehind)
        {
            
            var entry = new Entry
            {
                Boarded = boarded,
                BusId = busId,
                LeftBehind = leftBehind,
                LoopId = loopId,
                StopId = stopId,
                TimeStamp = DateTime.Now,
                UserId = userId,
            };
            
            /*
            var entry = new Entry
            {
                Boarded = 123,
                BusId = 1,
                LeftBehind = 41,
                LoopId = 2,
                StopId = 1,
                TimeStamp = DateTime.Now,
                UserId = 1,
            };
            */
            Console.WriteLine("Timestamp: " + entry.TimeStamp);
            Console.WriteLine("LoopId: " + entry.LoopId);
            Console.WriteLine("BusId: " + entry.BusId);
            Console.WriteLine("UserId: " + entry.UserId);
            Console.WriteLine("StopId: " + entry.StopId);
            Console.WriteLine("Boarded: " + entry.Boarded);
            Console.WriteLine("LeftBehind: " + entry.LeftBehind);

            _busDb.Entry.Add(entry);
            _busDb.SaveChanges();
        }
        

        public void UpdateEntryByID(int id, DateTime timeStamp, int boarded, int leftBehind)
        {
            var entry = _busDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                entry.TimeStamp = timeStamp;
                entry.Boarded = boarded;
                entry.LeftBehind = leftBehind;
                _busDb.SaveChanges();

            }
        }
        public void CreateEntry(DateTime timeStamp, int boarded, int leftBehind)
        {
            var newEntry = new Database.Entry
            {
                TimeStamp = timeStamp,
                Boarded = boarded,
                LeftBehind = leftBehind
            };
            _busDb.Entry.Add(newEntry);
            _busDb.SaveChanges();

        }

        public DomainModel.DomainModel.Entry? FindEntryByID(int id)
        {
            var entry = _busDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                return new DomainModel.DomainModel.Entry(entry.Id, entry.TimeStamp, entry.Boarded, entry.LeftBehind);
            }
            return null;
        }
        public void DeleteEntry(int id)
        {
            var entry = _busDb.Entry.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _busDb.Entry.Remove(entry);
                _busDb.SaveChanges();
            }
        }
    }
}