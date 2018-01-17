using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMSApi.Models.IRepository;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace DMSApi.Models.Repository
{
    public class WarehouseRepository:IWarehouseRepository
    {
         private DMSEntities _entities;

        public WarehouseRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<warehouse> GetAllWarehouse()
        {
            var warehouse = _entities.warehouses.OrderBy(w => w.warehouse_name).Where(w => w.warehouse_name != "[RESERVED]").ToList();
            return warehouse;
        }

        public object GetAdaWarehouse()
        {
            var warehouse = _entities.warehouses.OrderByDescending(w => w.warehouse_id).Where(w => w.party_type_id == 1 && w.warehouse_type == "Physical").ToList();
            return warehouse;
        }

        public object GetWeWarehouse()
        {
            var warehouse = _entities.warehouses.OrderBy(w => w.warehouse_name).Where(w => w.party_type_id == 1 && w.warehouse_name != "[RESERVED]").ToList();
            return warehouse;
        }

        public object GetWarehouseByPartyId(long party_id)
        {
            var warehouse = _entities.warehouses.Where(w => w.party_id == party_id && w.warehouse_type == "Physical").ToList();
            return warehouse;
        }

        public object GetSalesWarehouseOnly()
        {
            var warehouse = _entities.warehouses.Where(w => w.warehouse_id == 30).ToList();
            return warehouse;
        }

        public object GetWarehouseForDirectTransfer()
        {
            var warehouse = _entities.warehouses.Where(w => w.region_id == 1 && w.party_type_id == 1 && w.warehouse_type == "Physical").ToList();
            return warehouse;
        }

        public object GetWarehouseForTransferOrder()
        {
            var warehouse = _entities.warehouses.Where(w => w.party_type_id == 1 && w.warehouse_type == "Physical").OrderBy(w=>w.warehouse_name).ToList();
            return warehouse;
        }

        public warehouse GetWarehouseByPartyIdForAll(long party_id)
        {
            var warehouse = _entities.warehouses.FirstOrDefault(w => w.party_id == party_id);
            return warehouse;
        }


        public object GetAllWarehouseForGridLoad()
        {
            var warehouse = (from w in _entities.warehouses
                             join p in _entities.parties on w.party_id equals p.party_id into ps
                             from p in ps.DefaultIfEmpty()                     
                             //join e in _entities.employees on w.warehouse_incharge equals e.employee_id
                             //into pem
                             //from e in pem.DefaultIfEmpty()
                             join pt in _entities.party_type on w.party_type_id equals pt.party_type_id
                             into ptTem
                             from pt in ptTem.DefaultIfEmpty()
                             join reg in _entities.regions on w.region_id equals reg.region_id
                             into tempReg
                             from reg in tempReg.DefaultIfEmpty()
                             join are in _entities.areas on w.area_id equals are.area_id
                             into tempAre
                             from are in tempAre.DefaultIfEmpty()
                             join ter in _entities.territories on w.territory_id equals ter.territory_id
                             into tempTer
                             from ter in tempTer.DefaultIfEmpty()
                             select new
                             {
                                 warehouse_id = w.warehouse_id,
                                 warehouse_name = w.warehouse_name,
                                 warehouse_address = w.warehouse_address,
                                 warehouse_code = w.warehouse_code,
                                 warehouse_type = w.warehouse_type,
                                 region_name = reg.region_name,
                                 area_name = are.area_name,
                                 territory_name = ter.territory_name,
                                 warehouse_incharge = w.warehouse_incharge,
                                 //employee_name = e.employee_name,
                                 is_active = w.is_active,
                                 created_by = w.created_by,
                                 created_date = w.created_date,
                                 party_id = p.party_id,
                                 party_name = p.party_name,
                                 party_type_name = pt.party_type_name,
                                 party_type_id = pt.party_type_id
                               

                             }).OrderByDescending(e => e.warehouse_id).ToList();
            return warehouse;
        }

        public object GetWarehouseForEdit(long warehuse_id)
        {
            throw new NotImplementedException();
        }

       

        public bool CheckDuplicateWarehouse(string warehouse_name)
        {
            var checkduplicatewarehouse = _entities.warehouses.FirstOrDefault(w => w.warehouse_name == warehouse_name);
            bool return_type = checkduplicatewarehouse == null ? false : true;
            return return_type;
        }

        public long AddWarehouse(warehouse warehouse)
        {
            try
            {
                // generate warehouse Code
                long WarehouseSerial = _entities.warehouses.Max(rq => (long?)rq.warehouse_id) ?? 0;

                if (WarehouseSerial != 0)
                {
                    WarehouseSerial++;

                }
                else
                {
                    WarehouseSerial++;
                }
                var whStr = WarehouseSerial.ToString().PadLeft(7, '0');
                string warehouseCode = "WH-" + whStr;

               warehouse insert_warehouse = new warehouse
                {
                    warehouse_name = warehouse.warehouse_name,
                    warehouse_code = warehouseCode,                 
                    warehouse_address = warehouse.warehouse_address,
                    warehouse_type = warehouse.warehouse_type,
                    party_id = 1,
                    party_type_id = 1,
                    region_id = warehouse.region_id,
                    area_id = warehouse.area_id,
                    territory_id = warehouse.territory_id,
                    created_by = warehouse.created_by,
                    is_active = warehouse.is_active,
                    created_date = DateTime.Now                    
                    
                };
                _entities.warehouses.Add(insert_warehouse);
                _entities.SaveChanges();
                long last_insert_id = insert_warehouse.warehouse_id;
                return last_insert_id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public warehouse GetWarehouseById(long warehouse_id)
        {
            var warehouse = _entities.warehouses.Find(warehouse_id);
            return warehouse;
        }

        public bool EditWarehouse(warehouse warehouse)
        {
            try
            {
                warehouse whWarehouse = _entities.warehouses.Find(warehouse.warehouse_id);
                whWarehouse.warehouse_name = warehouse.warehouse_name;
                whWarehouse.warehouse_code = warehouse.warehouse_code;
                whWarehouse.warehouse_address = warehouse.warehouse_address;              
                whWarehouse.warehouse_type = warehouse.warehouse_type;
                whWarehouse.region_id = warehouse.region_id;
                whWarehouse.area_id = warehouse.area_id;
                whWarehouse.territory_id = warehouse.territory_id;
                whWarehouse.updated_by = warehouse.updated_by;
                whWarehouse.updated_date = DateTime.Now;
                whWarehouse.is_active = warehouse.is_active;
                whWarehouse.is_deleted = false;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteWarehouse(long warehouse_id, long? updated_by)
        {
            try
            {
                warehouse whwarehouse = _entities.warehouses.FirstOrDefault(w => w.warehouse_id == warehouse_id);
                whwarehouse.is_deleted = true;
                whwarehouse.updated_by = updated_by;
                whwarehouse.updated_date = DateTime.Now;    
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public long GetWarehouseByPartyIdUseInParty(warehouse warehouse)
        {
            //warehouse objWarehouse = _entities.warehouses.Find(warehouse.warehouse_id);
            //return objWarehouse;
            return 0;
        }
    }
}