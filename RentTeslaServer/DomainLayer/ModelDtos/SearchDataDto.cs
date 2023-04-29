namespace DomainLayer.ModelDtos
{
    public class SearchDataDto
    {
        public string NamePickUp { get; set; } = null!;
        public string NameDropOff { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
