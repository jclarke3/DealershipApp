using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealershipApp.Web.Models
{
    public class VehicleList
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public IEnumerable<VehicleModel> list { get; set; }
        //public GeoVehicles geo;
        public HtmlString gs { get; set; }
        public VehicleList(IEnumerable<VehicleModel> vehicles)
        {
            list = vehicles;

            //geo = new GeoVehicles(list);
            var model = new FeatureCollection();
            int count = 1;
            foreach(var item in list)
            {
                item.index = count;
                var geom = new Point(new GeographicPosition(item.Latitude, item.Longitude));
                var props = new Dictionary<string, object>
                {
                    { "Make", item.Make},
                    { "Model", item.Model},
                    { "Year", item.Year },
                    { "Name", item.Name },
                    {"Address", item.Address },
                    {"City", item.City },
                    {"State", item.State },
                    {"Zip", item.Zip },
                    {"Phone", item.Phone },
                    {"Index", count},
                    {"Latitude", item.Latitude },
                    {"Longitude", item.Longitude }

                };
                var feature = new GeoJSON.Net.Feature.Feature(geom, props, count.ToString());
                model.Features.Add(feature);
                gs = new HtmlString(JsonConvert.SerializeObject(model));
                count++;
            }
        }
    }
}