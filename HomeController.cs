using DealershipApp.Web.Domain;
using DealershipApp.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DealershipApp.Web.Controllers
{
    public class HomeController : Controller
    {
        WebHelper wh = new WebHelper();
        public ViewResult Index()
        {
            return View(new VehicleModel());
        }

        public ActionResult SearchVehicles(VehicleModel input)
        {
            //Debug.WriteLine("price" + input.Price + input.PriceMin + " " + input.PriceMax);
            if (input.Zip != null)
            {
                Debug.WriteLine("zip: " + input.Zip);
                var address = input.Zip.ToString();
                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());

                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");
                input.Latitude = Convert.ToDouble(lat.Value);
                input.Longitude = Convert.ToDouble(lng.Value);

                Debug.WriteLine(lat.Value + " " + lng.Value);
            }           

            var res = wh.GetVehicles(input, input.Latitude, input.Longitude);
            
            return View("SearchVehicles", new VehicleList(res));
        }

        public ViewResult Login()
        {
            return View();
        }
        /*public ActionResult Login2(string input)
        {
            var arr = input.Split(',');
            var un = arr[0];
            var pw = arr[1];
            DealerModel d = new DealerModel()
            {
                Username = un,
                Password = pw
            };
            var res = wh.Login(d);
            if (res == -1)
            {
                ViewBag.Message = "Login failed, username/password mismatch";
                return View("Login");
            }
            else
            {
                ViewBag.Message = "";
                Session["id"] = res;
                return Info();
            }
        }*/

        public string Login2(DealerModel input)
        {
            var res = wh.Login(input);
            if (res == -1)
            {
                ViewBag.Message = "Login failed, username/password mismatch";
                input.AccountId = -1;
                return JsonConvert.SerializeObject(input);
            }
            else
            {
                ViewBag.Message = "";
                Session["id"] = res;
                input.AccountId = 1;
                return JsonConvert.SerializeObject(input);
            }
        }

        public ViewResult Register()
        {
            return View("Register", new DealerModel());
        }

        public ActionResult NewDealership(DealerModel input)
        {
            //Debug.WriteLine(input.Name);
            //Debug.WriteLine("LL: " + input.Longitude + input.Latitude);
            var address = input.Zip.ToString();
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat");
            var lng = locationElement.Element("lng");
            input.Latitude = Convert.ToDouble(lat.Value);
            input.Longitude = Convert.ToDouble(lng.Value);
            var res = wh.Register(input);
            return View("Index", new VehicleModel());

        }



        public ActionResult LoginUser(DealerModel input)
        {
            var res = wh.Login(input);
            if (res == -1)
            {
                ViewBag.Message = "Login failed, username/password mismatch";
                return View("Login");
            }
            else
            {
                ViewBag.Message = "";
                Session["id"] = res;
                return Info();
            }
        }

        public ActionResult SellVehicle()
        {
            if (Session["id"] == null)
            {
                Session["id"] = 0;
                return View("Index", new VehicleModel());
            }
            if ((int)Session["id"] > 0)
            {
                return View("SellVehicle");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Sell(VehicleModel input)
        {
            if (Session["id"] != null)
            {
                input.DealershipId = (int)Session["id"];
                wh.SellVehicle(input);
                return Info();
            }
            else
            {
                return View("Login");
            }
            
        }

        public ActionResult LogOff()
        {
            Session["id"] = 0;
            return View("Index");
        }

        public ViewResult Info()
        {
            if (Session["id"] != null )
            {
                var list = wh.LotInfo((int)Session["id"]);
                VehicleList ret = new VehicleList(list);
                var acc = wh.AccountInfo((int)Session["id"]);
                ret.Address = acc.Address;
                ret.City = acc.City;
                ret.State = acc.State;
                ret.Zip = acc.Zip;
                ret.Phone = acc.Phone;
                ret.Name = acc.Name;
                return View("Info", ret);
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Remove(VehicleModel input)
        {
            wh.Remove(input.VehicleId);
            var list = wh.LotInfo((int)Session["id"]);
            VehicleList ret = new VehicleList(list);
            var acc = wh.AccountInfo((int)Session["id"]);
            ret.Address = acc.Address;
            ret.City = acc.City;
            ret.State = acc.State;
            ret.Zip = acc.Zip;
            ret.Phone = acc.Phone;
            ret.Name = acc.Name;
            return View("Info", ret);

        }

    }
}