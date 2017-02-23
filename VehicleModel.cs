using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealershipApp.Web.Models
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }
        public int DealershipId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public int Mileage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public float PriceMin { get; set; }
        public float PriceMax { get; set; }
        public int distance { get; set; }
        public int index { get; set; }

    }
}