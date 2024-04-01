using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Microsoft.VisualBasic;
using Bus_Shuttle.Database;
namespace Bus_Shuttle.Service
{
    public class LoopService : ILoopService
    {
        private readonly BusDb _busDb;

        public LoopService(BusDb busDb)
        {
            _busDb = busDb;
        }
        public List<DomainModel.DomainModel.Loop> GetLoops()
        {
            var loopList = _busDb.Loop.Select(l => new DomainModel.DomainModel.Loop(l.Id, l.Name)).ToList();
            return loopList;
        }

        public void UpdateLoopByID(int id, string name)
        {
            var loop = _busDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                loop.Name = name;
                _busDb.SaveChanges();

            }
        }
        public void CreateLoop(string name)
        {
            var newLoop = new Database.Loop
            {
                Name = name
            };
            _busDb.Loop.Add(newLoop);
            _busDb.SaveChanges();
        }

        public DomainModel.DomainModel.Loop? FindLoopByID(int id)
        {
            var loop = _busDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                return new DomainModel.DomainModel.Loop(loop.Id, loop.Name);
            }
            return null;
        }
        public void DeleteLoop(int id)
        {
            var loop = _busDb.Loop.FirstOrDefault(l => l.Id == id);
            if (loop != null)
            {
                _busDb.Loop.Remove(loop);
                _busDb.SaveChanges();
            }
        }
    }
}