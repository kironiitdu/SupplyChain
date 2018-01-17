using DMSApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.Repository
{
    public class WarehouseTypeRepository:IWarehouseTypeRepository
    {
          private DMSEntities _entities;

        public WarehouseTypeRepository()
        {
            this._entities = new DMSEntities();
        }

        //public object GetAllWarehouseType()
        //{
        //    List<warehouse_type> warehousetypes = _entities.warehouse_type.ToList();
        //    return warehousetypes;
        //}

        //public bool InsertWarehouseType(warehouse_type oWarehouseType)
        //{
        //    try
        //    {
        //        warehouse_type insert_warehouse_type = new warehouse_type
        //        {
        //            warehouse_type_name = oWarehouseType.warehouse_type_name,
        //            company_id = oWarehouseType.company_id,
        //            created_by = oWarehouseType.created_by,
        //            created_date = oWarehouseType.created_date,
        //            branch_id = oWarehouseType.branch_id
        //        };
        //        _entities.warehouse_type.Add(insert_warehouse_type);
        //        _entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public bool UpdateWarehouseType(warehouse_type oWarehouseType)
        //{
        //    try
        //    {
        //        warehouse_type ware = _entities.warehouse_type.Find(oWarehouseType.warehouse_type_id);
        //        ware.warehouse_type_name = oWarehouseType.warehouse_type_name;
        //        ware.company_id = oWarehouseType.company_id;
        //        ware.created_by = oWarehouseType.created_by;
        //        ware.created_date = oWarehouseType.created_date;
        //        ware.branch_id = oWarehouseType.branch_id;

        //        _entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public bool DeleteWarehouseType(long warehouse_type_id)
        //{
        //    try
        //    {
        //        warehouse_type oWarehouseType = _entities.warehouse_type.FirstOrDefault(wt => wt.warehouse_type_id == warehouse_type_id);
        //        _entities.warehouse_type.Attach(oWarehouseType);
        //        _entities.warehouse_type.Remove(oWarehouseType);
        //        _entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}