using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealershipApp.Web.Models
{
    public class GeoVehicles
    {
        public string type = "FeatureCollection";
        public List<Feature> features;
        public class Feature
        {
            public string type = "Feature";
            internal Geometry geometry;
            public Feature(VehicleModel v)
            {
                geometry = new Geometry(v);
            }
            internal class Geometry
            {
                public string type = "Point";
                public double[] coordinates;
                public List<Property> properties;

                public Geometry(VehicleModel vehicle)
                {
                    coordinates = new double[2] { vehicle.Latitude, vehicle.Longitude};
                    properties = new List<Property>();
                    properties.Add(new Property() { name = "Make", value = vehicle.Make });
                    properties.Add(new Property() { name = "Model", value = vehicle.Model });
                    properties.Add(new Property() { name = "Year", value = vehicle.Year });
                }
                internal class Property
                {
                    public string name { get; set; }
                    public string value { get; set; }
                }
            }
        }

        public  GeoVehicles(IEnumerable<VehicleModel> vList)
        {
            features = new List<Feature>();
            foreach(var item in vList)
            {
                features.Add(new Feature(item));            
            }
        }
    }    
}