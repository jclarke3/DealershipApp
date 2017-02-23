using DealershipApp.Domain;
using DealershipApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealershipApp.Web.Domain
{
    public class WebMapper
    {
        public static VehicleDTO WebToDomain(VehicleModel vehicle)
        {
            VehicleDTO ret = new VehicleDTO()
            {
                VehicleId = vehicle.VehicleId,
                DealershipId = vehicle.DealershipId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Condition = vehicle.Condition,
                Type = vehicle.Type,
                Price = vehicle.Price,
                Mileage = vehicle.Mileage,
                Latitude = vehicle.Latitude,
                Longitude = vehicle.Longitude,
                Address = vehicle.Address,
                City = vehicle.City,
                State = vehicle.State,
                Zip = vehicle.Zip,
                Phone = vehicle.Phone,
                Name = vehicle.Name,
                PriceMin = vehicle.PriceMin,
                PriceMax = vehicle.PriceMax
            };
            return ret;
        }

        public static VehicleModel DomainToWeb(VehicleDTO vehicle)
        {
            VehicleModel ret = new VehicleModel()
            {
                VehicleId = vehicle.VehicleId,
                DealershipId = vehicle.DealershipId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Condition = vehicle.Condition,
                Type = vehicle.Type,
                Price = vehicle.Price,
                Mileage = vehicle.Mileage,
                Latitude = vehicle.Latitude,
                Longitude = vehicle.Longitude,
                Address = vehicle.Address,
                City = vehicle.City,
                State = vehicle.State,
                Zip = vehicle.Zip,
                Phone = vehicle.Phone,
                Name = vehicle.Name,
                PriceMin = vehicle.PriceMin,
                PriceMax = vehicle.PriceMax
            };
            return ret;
        }
    }
}