namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class CarType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Motor { get; set; }
        public int Range { get; set; }
        public int Seats { get; set; }

        public virtual List<Car>? Cars { get;}

    }
}
