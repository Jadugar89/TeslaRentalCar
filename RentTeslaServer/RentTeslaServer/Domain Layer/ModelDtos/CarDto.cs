﻿namespace RentTeslaServer.Domain_Layer.ModelDtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public double DailyPrice { get; set; }
        public string Name { get; set; }
        public string Motor { get; set; }
        public int Range { get; set; }
        public int Seats { get; set; }

    }
}