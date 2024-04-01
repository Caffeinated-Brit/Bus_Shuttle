using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
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