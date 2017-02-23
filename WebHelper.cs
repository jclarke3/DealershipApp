using DealershipApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealershipApp.Domain;
using System.Diagnostics;

namespace DealershipApp.Web.Domain
{
    public class WebHelper
    {
        DomainHelper dh = new DomainHelper();
        public IEnumerable<VehicleModel> GetVehicles(VehicleModel vehicle, double lat, double lon)
        {
            VehicleDTO v = WebMapper.WebToDomain(vehicle);
            List<VehicleModel> ret = new List<VehicleModel>();
            var list = dh.GetVehicles(v, vehicle.distance, lat, lon);
            foreach(var item in list)
            {
                ret.Add(WebMapper.DomainToWeb(item));
            }
            return ret;
        }

        public int Register(DealerModel dealer)
        {
            DealershipDTO d = new DealershipDTO()
            {
                Address = dealer.Address,
                City = dealer.City,
                State = dealer.State,
                Zip = dealer.Zip,
                Name = dealer.Name,
                Phone = dealer.Phone,
                Latitude = dealer.Latitude,
                Longitude = dealer.Longitude
            };
            AccountDTO a = new AccountDTO()
            {
                Username = dealer.Username,
                Password = dealer.Password,
                Email = dealer.Email
            };
            Debug.WriteLine("un: " + d.Latitude + d.Longitude);
            return dh.Register(a, d);
        }

        public int Login(DealerModel dealer)
        {
            AccountDTO a = new AccountDTO()
            {
                Username = dealer.Username,
                Password = dealer.Password
            };
            return dh.Login(a);
        }

        public bool SellVehicle(VehicleModel vehicle)
        {
            VehicleDTO v = new VehicleDTO()
            {
                Color = vehicle.Color,
                Condition = vehicle.Condition,
                DealershipId = vehicle.DealershipId,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Price = vehicle.Price,
                Mileage = vehicle.Mileage,
                Type = vehicle.Type
            };
            return dh.SellVehicle(v);
        }

        public IEnumerable<VehicleModel> LotInfo(int i)
        {
            var list = dh.LotInfo(i);
            List<VehicleModel> ret = new List<VehicleModel>();
            foreach (var item in list)
            {
                ret.Add(WebMapper.DomainToWeb(item));
            }
            return ret;
        }

        public DealerModel AccountInfo(int i)
        {
            var acc = dh.AccountInfo(i);
            DealerModel d = new DealerModel()
            {
                Address = acc.Address,
                City = acc.City,
                State = acc.State,
                Zip = acc.Zip,
                Name = acc.Name,
                Phone = acc.Phone
            };
            return d;
        }

        public bool Remove(int id)
        {
            return dh.Remove(id);
        }
    }
}