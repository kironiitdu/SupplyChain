using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class InventoryAdjustmentRepository : IInventoryAdjustmentRepository
    {
        private DMSEntities _entities;

        public InventoryAdjustmentRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetSystemQuantityForAccessories(long warehouse_id, long product_id)
        {
            var data = _entities.inventory_stock.FirstOrDefault(u => u.warehouse_id == warehouse_id && u.product_id == product_id);
            return data;
        }

        public long PostAccessories(inventory_adjustment inventory_adjustment)
        {
            try
            {

                inventory_adjustment insertInventoryAdjustment = new inventory_adjustment
                {
                    adjustment_type = inventory_adjustment.adjustment_type,
                    warehouse_id = inventory_adjustment.warehouse_id,
                    contra_warehouse_id = inventory_adjustment.contra_warehouse_id,
                    product_id = inventory_adjustment.product_id,
                    system_quantity = inventory_adjustment.system_quantity,
                    physical_quantity = inventory_adjustment.physical_quantity,
                    adjustment_quantity = inventory_adjustment.adjustment_quantity,
                    status = inventory_adjustment.status,
                    created_by = inventory_adjustment.created_by,
                    created_date = inventory_adjustment.created_date,
                    updated_by = inventory_adjustment.created_by,
                    updated_date = inventory_adjustment.updated_date
                };
                _entities.inventory_adjustment.Add(insertInventoryAdjustment);
                _entities.SaveChanges();

                long last_insert_id = insertInventoryAdjustment.inventory_adjustment_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool PutAccessories(inventory_adjustment inventory_adjustment)
        {
            try
            {
                inventory_adjustment editInventoryAdjustment = _entities.inventory_adjustment.Find(inventory_adjustment.inventory_adjustment_id);
                editInventoryAdjustment.warehouse_id = inventory_adjustment.warehouse_id;
                editInventoryAdjustment.contra_warehouse_id = inventory_adjustment.contra_warehouse_id;
                editInventoryAdjustment.product_id = inventory_adjustment.product_id;
                editInventoryAdjustment.system_quantity = inventory_adjustment.system_quantity;
                editInventoryAdjustment.physical_quantity = inventory_adjustment.physical_quantity;
                editInventoryAdjustment.adjustment_quantity = inventory_adjustment.adjustment_quantity;
                editInventoryAdjustment.status = inventory_adjustment.status;
                editInventoryAdjustment.created_by = inventory_adjustment.created_by;
                editInventoryAdjustment.created_date = inventory_adjustment.created_date;
                editInventoryAdjustment.updated_by = inventory_adjustment.created_by;
                editInventoryAdjustment.updated_date = inventory_adjustment.updated_date;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetInventoryAdjustmentListForApprove()
        {
            var data = (from ia in _entities.inventory_adjustment
                        join pro in _entities.products on ia.product_id equals pro.product_id
                        join wa in _entities.warehouses on ia.warehouse_id equals wa.warehouse_id
                        join cowa in _entities.warehouses on ia.contra_warehouse_id equals cowa.warehouse_id
                        join col in _entities.colors on ia.color_id equals col.color_id into temp
                        from j in temp.DefaultIfEmpty()
                        join pv in _entities.product_version on ia.product_version_id equals pv.product_version_id into tempa
                        from x in tempa.DefaultIfEmpty()
                        select new
                        {
                            inventory_adjustment_id = ia.inventory_adjustment_id,
                            adjustment_type = ia.adjustment_type,
                            warehouse_id = wa.warehouse_id,
                            warehouse_name = wa.warehouse_name,
                            contra_warehouse_id = cowa.warehouse_id,
                            contra_warehouse_name = cowa.warehouse_name,
                            product_id = pro.product_id,
                            product_name = pro.product_name,
                            color_id = ia.color_id,
                            color_name = string.IsNullOrEmpty(j.color_name) ? "N/A" : j.color_name,
                            product_version_id = ia.product_version_id,
                            product_version_name = string.IsNullOrEmpty(x.product_version_name) ? "N/A" : x.product_version_name,
                            system_quantity = ia.system_quantity,
                            physical_quantity = ia.physical_quantity,
                            adjustment_quantity = ia.adjustment_quantity,
                            created_date = ia.created_date,
                            status = ia.status

                        }).Where(e => e.status == "Created" || e.status == "Updated").OrderByDescending(e => e.inventory_adjustment_id).ToList();

            return data;
        }

        public object GetAllInventoryAdjustment()
        {
            var data = (from ia in _entities.inventory_adjustment
                        join pro in _entities.products on ia.product_id equals pro.product_id
                        join wa in _entities.warehouses on ia.warehouse_id equals wa.warehouse_id
                        join cowa in _entities.warehouses on ia.contra_warehouse_id equals cowa.warehouse_id
                        join col in _entities.colors on ia.color_id equals col.color_id into temp
                        from j in temp.DefaultIfEmpty()
                        join pv in _entities.product_version on ia.product_version_id equals pv.product_version_id into tempa
                        from x in tempa.DefaultIfEmpty()
                        select new
                        {
                            inventory_adjustment_id = ia.inventory_adjustment_id,
                            adjustment_type = ia.adjustment_type,
                            warehouse_id = wa.warehouse_id,
                            warehouse_name = wa.warehouse_name,
                            contra_warehouse_id = cowa.warehouse_id,
                            contra_warehouse_name = cowa.warehouse_name,
                            product_id = pro.product_id,
                            product_name = pro.product_name,
                            color_id = ia.color_id,
                            color_name = string.IsNullOrEmpty(j.color_name) ? "N/A" : j.color_name,
                            product_version_id = ia.product_version_id,
                            product_version_name = string.IsNullOrEmpty(x.product_version_name) ? "N/A" : x.product_version_name,
                            system_quantity = ia.system_quantity,
                            physical_quantity = ia.physical_quantity,
                            adjustment_quantity = ia.adjustment_quantity,
                            created_date = ia.created_date,
                            status = ia.status

                        }).OrderByDescending(e => e.inventory_adjustment_id).ToList();

            return data;
        }

        public inventory_adjustment GetInventoryAdjustmentById(long inventory_adjustment_id)
        {
            var data = _entities.inventory_adjustment.Find(inventory_adjustment_id);
            return data;
        }

        public bool DeleteInventoryAdjustment(long inventory_adjustment_id)
        {
            try
            {
                inventory_adjustment oInventoryAdjustment = _entities.inventory_adjustment.Find(inventory_adjustment_id);
                _entities.inventory_adjustment.Attach(oInventoryAdjustment);
                _entities.inventory_adjustment.Remove(oInventoryAdjustment);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ApproveInventoryAdjustment(long inventory_adjustment_id, long user_id)
        {
            try
            {
                inventory_adjustment oInventoryAdjustment = _entities.inventory_adjustment.Find(inventory_adjustment_id);

                oInventoryAdjustment.status = "Approved";
                oInventoryAdjustment.approve_by = user_id;
                oInventoryAdjustment.approve_date = DateTime.Now;
                _entities.SaveChanges();

                var updateInventoty = new InventoryRepository();

                if (oInventoryAdjustment.system_quantity > oInventoryAdjustment.physical_quantity)
                {
                    updateInventoty.UpdateInventory("Inventory Adjustment", "IA-" + oInventoryAdjustment.inventory_adjustment_id + "", oInventoryAdjustment.warehouse_id ?? 0,
                                oInventoryAdjustment.contra_warehouse_id ?? 0, oInventoryAdjustment.product_id ?? 0, oInventoryAdjustment.color_id ?? 0, oInventoryAdjustment.product_version_id ?? 0, 1,
                                oInventoryAdjustment.adjustment_quantity ?? 0, user_id);
                }

                if (oInventoryAdjustment.system_quantity < oInventoryAdjustment.physical_quantity)
                {
                    updateInventoty.UpdateInventory("Inventory Adjustment", "IA-" + oInventoryAdjustment.inventory_adjustment_id + "", oInventoryAdjustment.contra_warehouse_id ?? 0,
                                oInventoryAdjustment.warehouse_id ?? 0, oInventoryAdjustment.product_id ?? 0, oInventoryAdjustment.color_id ?? 0, oInventoryAdjustment.product_version_id ?? 0, 1,
                                Math.Abs(oInventoryAdjustment.adjustment_quantity ?? 0), user_id);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}