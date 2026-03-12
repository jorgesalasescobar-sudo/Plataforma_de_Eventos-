using System;
using System.Collections.Generic;
using System.Text;

namespace EventService.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get;  set; }
        public string Location { get; set; }
        public List<Zone> Zones { get; set; } = new();

        // Constructor vacío para EF Core y para inicialización manual
        public Event() { }

        public Event(string name, DateTime date, string location, List<Zone> zones)
        {
            Id = Guid.NewGuid();
            Name = name;
            Date = date;
            Location = location;
            Zones = zones;
        }
    }

}
