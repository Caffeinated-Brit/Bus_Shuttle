using System;
using System.Collections.Generic;
using System.Linq;
using Bus_Shuttle.Controllers;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
using Bus_Shuttle.Models;

namespace Bus_Shuttle.Service
{
    public class EntryService : IEntryService
    {
        private readonly BusDb _busDb;
        private readonly ILogger<HomeController> _logger;

        public EntryService(BusDb busDb, ILogger<HomeController> logger)
        {
            _busDb = busDb;
            _logger = logger;
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
            _busDb.Entry.Add(entry);
            _busDb.SaveChanges();
            
            _logger.LogInformation("Entry added successfully: LoopId {LoopId}, BusId {BusId}, StopId {StopId}, Boarded {Boarded}, LeftBehind {LeftBehind}, UserId {UserId}", 
                loopId, busId, stopId, boarded, leftBehind, userId);
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
                
                _logger.LogInformation("Entry updated successfully: Id {Id}, TimeStamp {TimeStamp}, Boarded {Boarded}, LeftBehind {LeftBehind}", 
                    id, timeStamp, boarded, leftBehind);
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
            
            _logger.LogInformation("New entry created successfully: TimeStamp {TimeStamp}, Boarded {Boarded}, LeftBehind {LeftBehind}", 
                timeStamp, boarded, leftBehind);
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
                _logger.LogInformation("Entry deleted successfully: ID {EntryId}", id);
            }
            else
            {
                _logger.LogWarning("Entry with ID {EntryId} not found. No entry was deleted.", id);
            }
        }
    }
}