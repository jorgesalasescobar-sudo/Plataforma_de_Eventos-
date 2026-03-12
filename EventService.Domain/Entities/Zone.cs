using System;
using System.Collections.Generic;
using System.Text;

namespace EventService.Domain.Entities
{
    public class Zone
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }

        public Zone() { }

        public Zone(string name, decimal price, int capacity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Capacity = capacity;
        }
    }

}