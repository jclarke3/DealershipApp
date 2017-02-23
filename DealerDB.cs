using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipApp.DataAccess
{
    public class DealerDB
    {
        DealerDBEntities db = new DealerDBEntities();
        public IEnumerable<VehicleFull> getVehicles()
        {
            try
            {
                return db.VehicleFulls.ToList();
            }
            catch (Exception)
            {
                return new List<VehicleFull>();
            }
            
        }

        public int Register(Dealer dealer)
        {
            //if(db.Accounts.Find())
            try
            {
                db.Dealerships.Add(dealer.dealership);
                db.SaveChanges();
                int dealerId = db.Dealerships.FirstOrDefault(d => d.Name.Equals(dealer.dealership.Name)).DealershipId;
                dealer.account.DealershipId = dealerId;
                db.Accounts.Add(dealer.account);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public int Login(Dealer dealer)
        {
            string un="";
            try
            {
                un = db.Accounts.FirstOrDefault(d => d.Username.Equals(dealer.account.Username)).Username;

            }
            catch (Exception)
            {
                return -1;
            }
            if (un == null)
            {
                return -1;
            }
            var pw = db.Accounts.FirstOrDefault(p => p.Password.Equals(dealer.account.Password));
            if (pw == null)
            {
                return -1;
            }else
            {
                return db.Accounts.FirstOrDefault(d => d.Username.Equals(dealer.account.Username)).DealershipId;
            }
        }

        public bool SellVehicle(Dealer dealer)
        {
            try
            {
                db.Vehicles.Add(dealer.vehicle);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
            
        }

        public IEnumerable<Vehicle> LotInfo(Dealer dealer)
        {
            List<Vehicle> list = new List<Vehicle>();
            foreach(var item in db.Vehicles)
            {
                if (item.DealershipId == dealer.account.DealershipId)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public Dealership AccountInfo(Dealer dealer)
        {
            return db.Dealerships.FirstOrDefault(a => a.DealershipId == dealer.account.DealershipId);
        }

        public bool Remove(int id)
        {
            var stud = (from v in db.Vehicles
                        where v.VehicleId == id
                        select v).FirstOrDefault();
            db.Vehicles.Remove(stud);
            db.SaveChanges();
            return true;
        }
    }
}
