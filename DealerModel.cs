using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealershipApp.Web.Models
{
    public class DealerModel
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}