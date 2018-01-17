using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class CourierRepository : ICourierRepository
    {
        private DMSEntities _entities;
        public CourierRepository()
        {
            this._entities = new DMSEntities();
        }


        public List<courier> GetAllCouriers()
        {
            var courier = _entities.couriers.OrderBy(c => c.courier_name).ToList();
            return courier;
        }

        public bool CheckDuplicateCouriers(string courier_name)
        {
            var checkDuplicatecouriers = _entities.couriers.FirstOrDefault(c => c.courier_name == courier_name);
            bool return_type = checkDuplicatecouriers == null ? false : true;
            return return_type;
        }

        public long AddCourier(courier courier)
        {
            try
            {
                courier insert_courier = new courier
                {
                    courier_name = courier.courier_name,
                    cell = courier.cell

                };

                _entities.couriers.Add(insert_courier);
                _entities.SaveChanges();
                long last_insert_id = insert_courier.courier_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public courier GetcourierById(long courier_id)
        {
            var courier = _entities.couriers.Find(courier_id);
            return courier;
        }

        public bool Editcourier(courier courier)
        {
            try
            {

                courier cr = _entities.couriers.Find(courier.courier_id);
                cr.courier_name = courier.courier_name;
                cr.cell = courier.cell;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Deletecourier(long courier_id)
        {
            try
            {
                courier oCourier = _entities.couriers.FirstOrDefault(c => c.courier_id == courier_id);
                _entities.couriers.Attach(oCourier);
                _entities.couriers.Remove(oCourier);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool CheckDuplicateCouriers(long id, string name)
        {
            try
            {
                var ttt = _entities.couriers.Where(a => a.courier_id != id && a.courier_name == name).ToList();
                if (ttt.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}