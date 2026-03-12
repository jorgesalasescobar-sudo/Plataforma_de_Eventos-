namespace EventService.Application.DTOs
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<CreateZoneDto> Zones { get; set; }
    }

    public class CreateZoneDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}