using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using DMSClient.Reports.crystal_models;
using InventoryStockModel = DMSApi.Models.crystal_models.InventoryStockModel;


namespace DMSApi.Models.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DMSEntities _entities;
        public InventoryRepository()
        {
            this._entities = new DMSEntities();
        }
        /// <summary>
        /// UpdateInventory
        /// </summary>
        /// <param name="transactionType"></param>
        /// <param name="documentCode"></param>
        /// <param name="fromWarehouse"></param>
        /// <param name="toWarehouse"></param>
        /// <param name="productId"></param>
        /// <param name="colorId"></param>
        /// <param name="uomId"></param>
        /// <param name="transactionQuantity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// //By KIron
        public bool UpdateInventory(string transactionType, string documentCode, long fromWarehouse, long toWarehouse, long productId, long colorId, long productVersionId, long uomId, int transactionQuantity, long userId)
        {
            try
            {
                if (transactionQuantity <= 0) return false;
                // get from warehouse balance
                var fromWarehouseClosingStock = GetWarehouseClosingStockByProductId(fromWarehouse, productId, colorId, productVersionId);
                // insert from warehouse inventory (decrease from warehouse)
                var objFromInventory = new inventory
                {
                    //inventory_id
                    transaction_date = DateTime.Now,
                    transaction_type = transactionType,
                    document_code = documentCode,
                    warehouse_id = fromWarehouse,
                    product_id = productId,
                    color_id = colorId,
                    product_version_id = productVersionId,
                    uom_id = uomId,
                    opening_stock = fromWarehouseClosingStock,
                    stock_in = 0,
                    stock_out = transactionQuantity,
                    closing_stock = fromWarehouseClosingStock - transactionQuantity,
                    created_by = userId,
                    //created_date = DateTime.Now.ToString(),
                    updated_by = 0,
                    //updated_date = ""

                };
                // INSERT inventory
                _entities.inventories.Add(objFromInventory);
                long invFromSaved = _entities.SaveChanges();

                // if inserted
                if (invFromSaved > 0)
                {
                    // For Updating Inventory_Stock
                    fromWarehouseClosingStock = GetWarehouseClosingStockByProductId(fromWarehouse, productId, colorId, productVersionId);
                    UpdateInventoryStock(fromWarehouse, productId, colorId, productVersionId, uomId, (int)fromWarehouseClosingStock, userId);
                    // get to warehouse balance
                    decimal toWarehouseClosingStock = GetWarehouseClosingStockByProductId(toWarehouse, productId, colorId, productVersionId);


                    // insert to warehouse inventory (increase to warehouse)
                    var objToInventory = new inventory
                    {
                        //inventory_id
                        transaction_date = DateTime.Now,
                        transaction_type = transactionType,
                        document_code = documentCode,
                        warehouse_id = toWarehouse,
                        product_id = productId,
                        color_id = colorId,
                        product_version_id = productVersionId,
                        uom_id = uomId,
                        opening_stock = toWarehouseClosingStock,
                        stock_in = transactionQuantity,
                        stock_out = 0,
                        closing_stock = toWarehouseClosingStock + transactionQuantity,
                        created_by = userId,
                        //created_date = DateTime.Now.ToString(),
                        updated_by = 0,
                        //updated_date = "",

                    };
                    // INSERT inventory
                    _entities.inventories.Add(objToInventory);
                    long invSaved = _entities.SaveChanges();
                    if (invSaved > 0)
                    {
                        // For Updating Inventory_Stock
                        toWarehouseClosingStock = GetWarehouseClosingStockByProductId(toWarehouse, productId, colorId, productVersionId);
                        UpdateInventoryStock(toWarehouse, productId, colorId, productVersionId, uomId, (int)toWarehouseClosingStock, userId);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }


        public long GetWarehouseClosingStockByProductId(long warehouseId, long productId, long colorId, long productVersionId)
        {
            try
            {
                var transaction =
                    _entities.inventories.Where(
                        i => i.warehouse_id == warehouseId && i.product_id == productId && i.color_id == colorId && i.product_version_id == productVersionId)
                        .OrderByDescending(o => o.inventory_id)
                        .FirstOrDefault();
                long closingStock = 0;
                if (transaction != null)
                {
                    closingStock = (long)(transaction.closing_stock ?? 0);
                }
                return closingStock;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        //added By Meraj 03/11/2016 
        public object GetProductInventoryExcelData(string from_date, string to_date, string product_id, string color_id)
        {
            try
            {
                string condition = "";

                #region "1# if product and color both  not selected"

                if (product_id == "0" && color_id == "0")
                {
                    condition = "";
                }

                #endregion

                #region "2# if product selected but color not selected"

                if (product_id != "0" && color_id == "0")
                {
                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inv.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or inv.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "inv.product_id=" + product_id + " and ";
                    }

                }

                #endregion

                #region "3# if product not selected but color selected"

                if (product_id == "0" && color_id != "0")
                {
                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inv.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or inv.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "inv.color_id=" + color_id + " and ";
                    }

                }

                #endregion

                #region "4# if product and color both selected"

                if (product_id != "0" && color_id != "0")
                {
                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inv.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or inv.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "inv.product_id=" + product_id + " and ";
                    }

                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inv.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or inv.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "inv.color_id=" + color_id + " and ";
                    }

                }

                #endregion

                string baseQuiry = "select A.product_id,A.color_id,A.warehouse_id,A.product_name,A.color_name,A.warehouse_name,A.warehouse_code,A.opening_stock ,B.closing_stock,to_char(to_date(B.transaction_date,'DD/MM/YYYY'),'YYYY-MM-DD')as transaction_date,A.province_name from (select inv.product_id,inv.opening_stock,p.product_name,inv.color_id,c.color_name,inv.warehouse_id ,w.warehouse_name,w.warehouse_code,prov.province_name from inventory inv " +
                                   "inner join product p on p.product_id=inv.product_id " +
                                   "inner join color c on c.color_id=inv.color_id " +
                                   "inner join warehouse w on w.warehouse_id=inv.warehouse_id " +
                                   "inner join province prov on w.province_id = prov.province_id where inv.inventory_id in (select min(inv.inventory_id) as start from inventory inv " +
                                   "inner join product p on p.product_id=inv.product_id " +
                                   "inner join color c on c.color_id=inv.color_id " +
                                   "inner join warehouse w on w.warehouse_id=inv.warehouse_id " +
                                   "inner join province prov on w.province_id = prov.province_id where " + condition + " (inv.warehouse_id=1 or inv.warehouse_id=2 or inv.warehouse_id=3) and to_date(inv.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY') group by inv.product_id,p.product_name,inv.color_id,c.color_name,inv.warehouse_id ,w.warehouse_name,w.warehouse_code))A , (select inv.product_id,inv.closing_stock,p.product_name,inv.color_id,c.color_name,inv.warehouse_id ,w.warehouse_name,w.warehouse_code,inv.transaction_date,prov.province_name from inventory inv " +
                                   "inner join product p on p.product_id=inv.product_id " +
                                   "inner join color c on c.color_id=inv.color_id " +
                                   "inner join warehouse w on w.warehouse_id=inv.warehouse_id " +
                                   "inner join province prov on w.province_id = prov.province_id where inv.inventory_id in (select max(inv.inventory_id) as start from inventory inv " +
                                   "inner join product p on p.product_id=inv.product_id " +
                                   "inner join color c on c.color_id=inv.color_id " +
                                   "inner join warehouse w on w.warehouse_id=inv.warehouse_id " +
                                   "inner join province prov on w.province_id = prov.province_id where " + condition + " (inv.warehouse_id=1 or inv.warehouse_id=2 or inv.warehouse_id=3) and to_date(inv.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY') group by inv.product_id,p.product_name,inv.color_id,c.color_name,inv.warehouse_id ,w.warehouse_name,w.warehouse_code))B where A.product_id = B.product_id and A.color_id = B.color_id and A.warehouse_id = B.warehouse_id and B.closing_stock!=0  order by A.warehouse_name asc";

                var data = _entities.Database.SqlQuery<ProductInventoryExcelModel>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object GetAdaProductInventoryDetailsExcel(long product_id, long color_id, long warehouse_id)
        {
            try
            {
                string query = "select pro.product_name,c.color_name,rsnd.imei_no from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where rsnd.product_id='" + product_id + "' and rsnd.color_id='" + color_id + "' and rsnd.current_warehouse_id='" + warehouse_id + "'";


                var poData = _entities.Database.SqlQuery<GrnReportModel>(query).ToList();

                return poData;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object GetAdaProductInventoryAllExcel()
        {
            try
            {
                string condition = "";

                var warehouse = _entities.warehouses.Where(w => w.party_type_id == 1 && w.warehouse_type == "Physical").ToList();
                long xxx = warehouse.Count;
                long counter = 1;

                foreach (var item in warehouse)
                {

                    if (xxx == counter)
                    {
                        condition += " rsnd.current_warehouse_id=" + item.warehouse_id + " ";
                    }
                    else
                    {
                        condition += " rsnd.current_warehouse_id=" + item.warehouse_id + " or ";
                    }

                    counter++;
                }

                string query = "select pro.product_name,c.color_name,rsnd.imei_no from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where " + condition + " order by c.color_name asc";


                var poData = _entities.Database.SqlQuery<GrnReportModel>(query).ToList();

                return poData;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object GetPartyWiseInventoryDetailsExcel(long product_id, long color_id, long warehouse_id, long party_id)
        {
            try
            {
                // string query = "select distinct pro.product_name ,c.color_name ,rsnd.imei_no from receive_serial_no_details rsnd inner join inventory_stock on rsnd.current_warehouse_id=inventory_stock.warehouse_id inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where rsnd.product_id='" + product_id + "' and rsnd.color_id='" + color_id + "' and inventory_stock.warehouse_id='" + warehouse_id + "'";

                //string query =
                //    "select pro.product_name ,color.color_name ,rsnd.imei_no ,inventory_stock.update_date FROM receive_serial_no_details rsnd inner join inventory_stock on rsnd.current_warehouse_id=inventory_stock.warehouse_id inner join product pro on rsnd.product_id = pro.product_id inner join color on rsnd.color_id =color.color_id where rsnd.product_id='" +
                //    product_id + "' and rsnd.color_id='" + color_id + "' and rsnd.party_id='" + party_id + "'";
                string query = "select distinct pro.product_name ,color.color_name ,rsnd.imei_no ,party.party_name FROM receive_serial_no_details rsnd inner join inventory_stock on rsnd.current_warehouse_id=inventory_stock.warehouse_id inner join product pro on rsnd.product_id = pro.product_id inner join color on rsnd.color_id =color.color_id inner join party on rsnd.party_id=party.party_id where rsnd.product_id='" + product_id + "' and rsnd.color_id='" + color_id + "' and rsnd.party_id='" + party_id + "' ";
                var poData = _entities.Database.SqlQuery<partyWiseStockExcelReportModel>(query).ToList();

                return poData;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object GetInventoryReport(long warehouse_id, long product_id, long color_id, string from_date, string to_date, long user_id)
        {
            try
            {
                if (from_date != null && to_date != null && user_id != null && product_id == 0 && color_id == 0 && warehouse_id != 0)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='165') as current_user_full_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'01/11/2016' as from_date ,'13/11/2016' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day AND warehouse.warehouse_id !=39 AND inventory.warehouse_id='" + warehouse_id + "'";
                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }
                if (from_date != null && to_date != null && user_id != null && product_id != 0 && color_id == 0 && warehouse_id != 0)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='165') as current_user_full_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'01/11/2016' as from_date ,'13/11/2016' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day AND warehouse.warehouse_id !=39 AND inventory.product_id='" + product_id + "' AND inventory.warehouse_id='" + warehouse_id + "'";
                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }
                if (from_date != null && to_date != null && user_id != null && product_id != 0 && color_id != 0 && warehouse_id == 0)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='165') as current_user_full_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'01/11/2016' as from_date ,'13/11/2016' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day AND warehouse.warehouse_id !=39 AND inventory.product_id='" + product_id + "' AND inventory.color_id='" + color_id + "'";
                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }

                if (from_date != null && to_date != null && user_id != null && product_id != 0 && color_id == 0 && warehouse_id == 0)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='165') as current_user_full_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'01/11/2016' as from_date ,'13/11/2016' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day AND warehouse.warehouse_id !=39 AND inventory.product_id='" + product_id + "'";
                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }
                if (from_date != null && to_date != null && user_id != null && product_id == 0 && color_id == 0 && warehouse_id == 0)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='165') as current_user_full_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'01/11/2016' as from_date ,'13/11/2016' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day AND warehouse.warehouse_id !=39";
                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }
                // Have to work on it??
                if (from_date != null && to_date != null && user_id != null && warehouse_id != null)
                {
                    string reportQuery = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,color.color_name ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'" + from_date + "' as from_date ,'" + to_date + "' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1' day and inventory.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' and color.color_id='" + color_id + "' order by inventory.inventory_id asc";
                    //string query = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,'" + from_date + "' as from_date  ,'" + to_date + "' as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE inventory.transaction_date BETWEEN'" + from_date + "' and '" + to_date + "'and inventory.warehouse_id=" + warehouse_id + " and product.product_id=" + product_id + "order by inventory.inventory_id asc";
                    // string query = "SELECT transaction_type ,transaction_date ,document_code ,product.product_name ,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock ,to_date('" + from_date + "', 'DD/MM/YYYY') as from_date ,to_date('" + to_date + "', 'DD//MM/YYYY') as to_date FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD//MM/YYYY') and inventory.warehouse_id=" + warehouse_id + " and product.product_id=" + product_id + " order by inventory.inventory_id asc"; 

                    var reData = _entities.Database.SqlQuery<InventoryReportModels>(reportQuery).ToList();
                    return reData;
                }
                if (from_date == null && to_date == null && user_id != null)
                {
                    string query =
                        "SELECT transaction_type ,transaction_date ,document_code ,product.product_name,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,color.color_name,warehouse.warehouse_name ,unit_name ,opening_stock ,stock_in ,stock_out ,closing_stock FROM inventory LEFT JOIN product ON inventory.product_id=product.product_id  LEFT JOIN color on inventory.color_id=color.color_id  LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory.uom_id=unit.unit_id WHERE inventory.warehouse_id=" + warehouse_id + " and product.product_id='" + product_id + "' and color.color_id='" + color_id + "' order by inventory.inventory_id asc";

                    var selectedWarehouseData = _entities.Database.SqlQuery<InventoryReportModels>(query).ToList();
                    return selectedWarehouseData;
                }





                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <param name="colorId"></param>
        /// <param name="uomId"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateInventoryStock(long warehouseId, long productId, long colorId, long productVersionId, long uomId, int stockQuantity, long userId)
        {
            try
            {
                var objInventoryStock = new inventory_stock
                {
                    warehouse_id = warehouseId,
                    product_id = productId,
                    color_id = colorId,
                    product_version_id = productVersionId,
                    uom_id = uomId,
                    stock_quantity = stockQuantity,
                    //update_by = userId,
                    //update_date = DateTime.Now.ToString()
                };

                var skuExists = _entities.inventory_stock.FirstOrDefault(i => i.warehouse_id == warehouseId && i.product_id == productId && i.color_id == colorId && i.product_version_id == productVersionId);
                if (skuExists == null)
                {
                    _entities.inventory_stock.Add(objInventoryStock);
                    long updateStock = _entities.SaveChanges();
                    if (updateStock > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    skuExists.stock_quantity = stockQuantity;
                    //skuExists.update_by = userId;
                    //skuExists.update_date = DateTime.Now.ToString();
                    long updateStock = _entities.SaveChanges();
                    if (updateStock > 0)
                    {
                        return true;
                    }
                }


                return false;
            }
            catch (Exception)
            {

                return false;

            }

        }


        public object GetInventoryStockReport(long warehouse_id, long product_id, long color_id, long user_id)
        {
            try
            {
                //Modified By Kiron 27/09/2016
                if (warehouse_id == 0 && product_id != 0 && color_id != 0 && user_id != 0)
                {
                    string query = "SELECT product.product_name ,color.color_name ,warehouse.warehouse_name ,unit.unit_name ,color.color_name ,stock_quantity ,inventory_stock.update_date,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase'and inventory_stock.product_id=" + product_id + "and inventory_stock.color_id=" + color_id + "  ";
                    var selectedWarehouseData = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return selectedWarehouseData;
                }
                if (warehouse_id == 0 && product_id != 0 && color_id == 0 && user_id != 0)
                {
                    string query = "SELECT product.product_name ,color.color_name ,warehouse.warehouse_name ,unit.unit_name ,color.color_name ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,stock_quantity ,inventory_stock.update_date FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase'and inventory_stock.product_id=" + product_id + "  ";
                    var selectedWarehouseData = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return selectedWarehouseData;
                }
                if (warehouse_id != 0 && product_id == 0 && color_id == 0 && user_id != 0)
                {
                    string query = "SELECT product.product_name ,color.color_name ,warehouse.warehouse_name ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,unit.unit_name ,color.color_name ,stock_quantity ,inventory_stock.update_date FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase'and inventory_stock.warehouse_id=" + warehouse_id + "  ";
                    var selectedWarehouseData = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return selectedWarehouseData;
                }
                //Have to work on it
                if (warehouse_id == 0 && product_id == 0 && color_id == 0 && user_id != 0)
                {
                    string query = "SELECT product.product_name ,color.color_name ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,inventory_stock.update_date ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit'";
                    var selectedWarehouseData = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return selectedWarehouseData;
                }
                else
                {
                    string reportQuery = "SELECT product.product_name ,product.product_id ,color.color_name ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,color.color_id ,warehouse.warehouse_name ,warehouse.warehouse_id ,unit.unit_name ,stock_quantity ,inventory_stock.update_date FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id";
                    var reData = _entities.Database.SqlQuery<InventoryStockModel>(reportQuery).ToList();
                    return reData;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        //added By Kiron 27/09/2016 
        public object LoadAllInventoryStock(long warehouse_id, long product_id, long color_id, long product_version_id)
        {
            try
            {
                if (warehouse_id != 0 && product_id != 0 && color_id != 0 && product_version_id != 0)
                {
                    string loadAllGridData = "SELECT inventory_stock_id ,product_category.product_category_name,product.product_name ,product.product_id ,color.color_name ,color.color_id ,warehouse.warehouse_name ,warehouse.warehouse_id ,unit.unit_name ,unit.unit_id,product_version.product_version_id,product_version.product_version_name ,stock_quantity FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product_category.product_category_id=product.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase' and warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' and color.color_id ='" + color_id + "'and product_version.product_version_id='" + product_version_id + "'";
                    var loadAllInventoryStockForGrid = _entities.Database.SqlQuery<InventoryStockModel>(loadAllGridData).ToList();
                    return loadAllInventoryStockForGrid;
                }
                if (warehouse_id != 0 && product_id != 0 && color_id != 0)
                {
                    string loadAllGridData = "SELECT inventory_stock_id ,product_category.product_category_name,product.product_name ,product.product_id ,color.color_name ,color.color_id ,warehouse.warehouse_name ,warehouse.warehouse_id ,unit.unit_name ,unit.unit_id,product_version.product_version_id,product_version.product_version_name ,stock_quantity FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product_category.product_category_id=product.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase' and warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' and color.color_id ='" + color_id + "'";
                    var loadAllInventoryStockForGrid = _entities.Database.SqlQuery<InventoryStockModel>(loadAllGridData).ToList();
                    return loadAllInventoryStockForGrid;
                }
                if (warehouse_id != 0 && product_id != 0)
                {
                    string loadAllGridData = "SELECT inventory_stock_id ,product_category.product_category_name,product.product_name ,product.product_id ,color.color_name ,color.color_id,product_version.product_version_id,product_version.product_version_name ,warehouse.warehouse_name ,warehouse.warehouse_id ,unit.unit_name ,unit.unit_id ,stock_quantity FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product_category.product_category_id=product.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase' and warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "'";
                    var loadAllInventoryStockForGrid = _entities.Database.SqlQuery<InventoryStockModel>(loadAllGridData).ToList();
                    return loadAllInventoryStockForGrid;
                }
                if (warehouse_id != 0)
                {
                    string loadAllGridData = "SELECT inventory_stock_id ,product_category.product_category_name,product.product_name ,product.product_id ,color.color_name ,product_version.product_version_id ,product_version.product_version_name ,color.color_id ,warehouse.warehouse_name ,warehouse.warehouse_id ,unit.unit_name ,unit.unit_id ,stock_quantity FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product_category.product_category_id=product.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id  LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse_name !='Purchase' and warehouse.warehouse_id='" + warehouse_id + "'";
                    var loadAllInventoryStockForGrid = _entities.Database.SqlQuery<InventoryStockModel>(loadAllGridData).ToList();
                    return loadAllInventoryStockForGrid;
                }
                return null;

            }
            catch (Exception)
            {

                return null;
            }
        }

        //Added By Kiron 21/10/2016 Sales Transaction
        public object DailySalesTransaction(long user_id, string from_date, string to_date)
        {

            try
            {
                if (from_date != null && to_date != null && user_id != null)
                {
                    string DailySalesTransactionReport =
                        "SELECT dm.delivery_date ,fromwa.warehouse_name as from_warehouse_name ,towa.warehouse_name as to_warehouse_name ,pro.product_name ,col.color_name ,dd.delivered_quantity ,dd.unit_price ,dd.line_total ,dd.is_gift ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,'" + from_date + "' as from_date ,'" + to_date +
                        "' as to_date FROM delivery_details dd INNER JOIN delivery_master dm on dd.delivery_master_id = dm.delivery_master_id INNER JOIN product pro on dd.product_id=pro.product_id INNER JOIN warehouse fromwa on dm.from_warehouse_id =fromwa.warehouse_id INNER JOIN warehouse towa on dm.to_warehouse_id=towa.warehouse_id INNER JOIN color col on dd.color_id = col.color_id WHERE to_date(dm.delivery_date , 'DD/MM/YYYY') BETWEEN to_date('" +
                        from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')+ interval '1 day '";
                    var DailySalesReportData =
                        _entities.Database.SqlQuery<DailySalesTransactionModel>(DailySalesTransactionReport).ToList();
                    return DailySalesReportData;
                }
                if (from_date == null && to_date == null && user_id != null)
                {
                    string DailySalesTransactionReport =
                       "select dm.delivery_date ,fromwa.warehouse_name as from_warehouse_name ,(select full_name from users where user_id='" +
                        user_id + "') as current_user_full_name ,towa.warehouse_name as to_warehouse_name ,pro.product_name ,col.color_name ,dd.delivered_quantity ,dd.unit_price ,dd.line_total ,dd.is_gift ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM delivery_details dd INNER JOIN delivery_master dm on dd.delivery_master_id = dm.delivery_master_id INNER JOIN product pro on dd.product_id=pro.product_id INNER JOIN warehouse fromwa on dm.from_warehouse_id =fromwa.warehouse_id INNER JOIN warehouse towa on dm.to_warehouse_id=towa.warehouse_id INNER JOIN color col on dd.color_id = col.color_id";
                    var DailySalesReportData =
                        _entities.Database.SqlQuery<DailySalesTransactionModel>(DailySalesTransactionReport).ToList();
                    return DailySalesReportData;
                }
                return null;

            }
            catch (Exception)
            {

                return null;
            }
        }



        public object DailySalesTransaction()
        {
            try
            {
                var DailySalesData =

                                            (from dd in _entities.delivery_details
                                             join dm in _entities.delivery_master on dd.delivery_master_id equals dm.delivery_master_id //into tmpDeliveryMaster from dm in tmpDeliveryMaster.DefaultIfEmpty()
                                             join pro in _entities.products on dd.product_id equals pro.product_id //into tempProduct from pro in tempProduct.DefaultIfEmpty()
                                             join warfrom in _entities.warehouses on dm.from_warehouse_id equals warfrom.warehouse_id //into temWar from warfrom in temWar.DefaultIfEmpty()
                                             join warto in _entities.warehouses on dm.to_warehouse_id equals warto.warehouse_id //into temWarTo from warto in temWarTo.DefaultIfEmpty()
                                             join col in _entities.colors on dd.color_id equals col.color_id
                                             //into temColor from col in temColor.DefaultIfEmpty()
                                             select new
                                             {
                                                 delivery_details_id = dd.delivery_details_id,
                                                 delivery_master_id = dm.delivery_master_id,
                                                 delivery_date = dm.delivery_date,
                                                 from_warehouse_name = warfrom.warehouse_name,
                                                 from_warehouse_id = warfrom.warehouse_id,
                                                 to_warehouse_name = warto.warehouse_name,
                                                 to_warehouse_id = warto.warehouse_id,
                                                 product_name = pro.product_name,
                                                 product_id = pro.product_id,
                                                 color_name = col.color_name,
                                                 color_id = col.color_id,
                                                 is_gift = dd.is_gift,
                                                 delivered_quantity = dd.delivered_quantity ?? 0,
                                                 unit_price = dd.unit_price ?? 0,
                                                 line_total = dd.line_total ?? 0


                                             }).OrderByDescending(d => d.delivery_date).ToList();

                return DailySalesData;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public object TraceIMEI(string imei_no)
        {
            try
            {
                //Sp_Find_IMEI @IMEI='357289070017627'
                //string imeiTrace =
                //    "SELECT receive_serial_no_details.imei_no ,product.product_name ,color.color_name ,warehouse.warehouse_name ,(select province_name from province INNER JOIN warehouse on warehouse.province_id= province.province_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_province ,(select city_name from district_city INNER JOIN warehouse on warehouse.city_id= district_city.city_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_district_city ,party.party_name ,party_type.party_type_name ,(select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=receive_serial_no_details.party_id) as party_province ,(select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=receive_serial_no_details.party_id) as party_district_city ,received_date as stock_in_since ,deliver_date as stock_in_party_since ,receive_serial_no_details.sales_status ,receive_serial_no_details.sales_date ,ret.party_name as retailer_name ,(select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=receive_serial_no_details.retailer_id) as retailer_province ,(select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=receive_serial_no_details.retailer_id) as retailer_district_city ,receive_serial_no_details.delivery_to_retailer_date ,receive_serial_no_details.is_return ,receive_serial_no_details.replaced_status ,return_details.replaced_imei_no ,(SELECT imei_no FROM return_details WHERE imei_no=receive_serial_no_details.imei_no and return_details.imei_no !='') as imei_replace_with ,to_char((SELECT replace_date FROM return_details WHERE replaced_imei_no=return_details.replaced_imei_no),'DD/MM/YYYY') as replace_date ,(SELECT warehouse_name FROM warehouse WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_name ,(SELECT province_name FROM province INNER JOIN warehouse on warehouse.province_id= province.province_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_province ,(SELECT city_name FROM district_city INNER JOIN warehouse on warehouse.city_id= district_city.city_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_district_city ,return_master.return_code ,to_char(return_master.return_date,'DD/MM/YYYY') as return_date ,return_type.return_type_name ,(SELECT party_name FROM party where party_id= return_master.md_dbis_id )as return_by_dbis ,(SELECT party_name FROM party where party_id= return_master.retailer_id )as return_by_retailer FROM receive_serial_no_details LEFT JOIN party on receive_serial_no_details.party_id=party.party_id LEFT JOIN party as ret on receive_serial_no_details.retailer_id=ret.party_id LEFT JOIN product on receive_serial_no_details.product_id=product.product_id LEFT JOIN color on receive_serial_no_details.color_id = color.color_id LEFT join warehouse on receive_serial_no_details.received_warehouse_id=warehouse.warehouse_id LEFT JOIN party_type on party.party_type_id= party_type.party_type_id LEFT JOIN return_details on receive_serial_no_details.imei_no=return_details.imei_no LEFT JOIN return_master ON return_details.return_master_id=return_master.return_master_id LEFT JOIN return_type ON return_master.return_type=return_type.return_type_id WHERE receive_serial_no_details.imei_no='" + imei_no + "'";
                //string imei = "SELECT receive_serial_no_details.imei_no ,product.product_name ,color.color_name ,product_version.product_version_name ,warehouse.warehouse_name ,(select region_name from region INNER JOIN warehouse on warehouse.region_id= region.region_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_province ,(select area_name from area INNER JOIN warehouse on warehouse.area_id= area.area_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_district_city ,party.party_name ,party_type.party_type_name ,(SELECT warehouse_name FROM warehouse WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_name ,(SELECT region_name FROM region INNER JOIN warehouse on warehouse.region_id= region.region_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_province ,(SELECT area_name FROM area INNER JOIN warehouse on warehouse.area_id= area.area_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_district_city ,received_date as stock_in_since ,deliver_date as stock_in_party_since ,receive_serial_no_details.sales_status ,receive_serial_no_details.sales_date ,receive_serial_no_details.delivery_to_retailer_date ,receive_serial_no_details.is_return ,receive_serial_no_details.replaced_status FROM receive_serial_no_details LEFT JOIN party on receive_serial_no_details.party_id=party.party_id LEFT JOIN party as ret on receive_serial_no_details.retailer_id=ret.party_id LEFT JOIN product on receive_serial_no_details.product_id=product.product_id LEFT JOIN color on receive_serial_no_details.color_id = color.color_id LEFT JOIN product_version on receive_serial_no_details.product_version_id=product_version.product_version_id LEFT JOIN warehouse on receive_serial_no_details.received_warehouse_id=warehouse.warehouse_id LEFT JOIN party_type on party.party_type_id= party_type.party_type_id WHERE receive_serial_no_details.imei_no='"+imei_no+"'";
                string imei = "SELECT receive_serial_no_details.imei_no,receive_serial_no_details.imei_no2 ,product.product_name ,color.color_name ,product_version.product_version_name ,warehouse.warehouse_name ,(select region_name from region INNER JOIN warehouse on warehouse.region_id= region.region_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_province ,(select area_name from area INNER JOIN warehouse on warehouse.area_id= area.area_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id) as grn_warehouse_district_city ,party.party_name ,party_type.party_type_name ,(SELECT warehouse_name FROM warehouse WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_name ,(SELECT region_name FROM region INNER JOIN warehouse on warehouse.region_id= region.region_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_province ,(SELECT area_name FROM area INNER JOIN warehouse on warehouse.area_id= area.area_id WHERE warehouse_id=receive_serial_no_details.current_warehouse_id) as current_warehouse_district_city ,received_date as stock_in_since ,deliver_date as stock_in_party_since ,receive_serial_no_details.sales_status ,receive_serial_no_details.sales_date ,receive_serial_no_details.delivery_to_retailer_date ,receive_serial_no_details.is_return ,receive_serial_no_details.replaced_status FROM receive_serial_no_details LEFT JOIN party on receive_serial_no_details.party_id=party.party_id LEFT JOIN party as ret on receive_serial_no_details.retailer_id=ret.party_id LEFT JOIN product on receive_serial_no_details.product_id=product.product_id LEFT JOIN color on receive_serial_no_details.color_id = color.color_id LEFT JOIN product_version on receive_serial_no_details.product_version_id=product_version.product_version_id LEFT JOIN warehouse on receive_serial_no_details.received_warehouse_id=warehouse.warehouse_id LEFT JOIN party_type on party.party_type_id= party_type.party_type_id WHERE receive_serial_no_details.imei_no='" + imei_no + "'or receive_serial_no_details.imei_no2='" + imei_no + "'";
                // string imei = "Sp_Find_IMEI @IMEI='" + imei_no + "'";
                var getIMEIData = _entities.Database.SqlQuery<IMEITraceModel>(imei).FirstOrDefault();
                return getIMEIData;
            }
            catch (Exception)
            {

                throw null;
            }
        }


        public object PartyWiseStock(long role_id, long party_id)
        {
            try
            {
                //var userId = (from us in _entities.users.Where(us => us.party_id == party_id)
                //              select new
                //              {
                //                  users = us.user_id
                //              }).ToList();

                if (role_id == 2)
                {
                    // string query = "SELECT product.product_name , product.product_id ,product.product_id ,color.color_name ,color.color_id ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,party.party_name ,inventory_stock.update_date ,(select full_name from users where user_id='"+user_id+"') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and warehouse.warehouse_name !='Central' and warehouse.warehouse_name !='Esen' and party.party_name !='Sales Customer' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity,update_date,party_name,product.product_id,color.color_id,warehouse.warehouse_id,product.product_id ";
                    string query = "SELECT product.product_name ,product.product_id ,color.color_name ,color.color_id ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,party.party_name ,party.party_id ,party_type.party_type_name as to_date ,inventory_stock.update_date ,(select full_name from users where user_id='165') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and warehouse.warehouse_name !='Esen' and party.party_name !='ADA Central' and warehouse.warehouse_name !='Central' and party.party_name !='Sales Customer' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity,update_date,party_name,to_date,warehouse.warehouse_id ,party.party_id,color.color_id,product.product_id";
                    var partyWiseStock = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return partyWiseStock;
                }
                if (role_id != 2)
                {


                    string query = "SELECT product.product_name ,product.product_id ,color.color_name ,color.color_id ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,party.party_name ,party.party_id ,party_type.party_type_name as to_date ,inventory_stock.update_date FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and warehouse.warehouse_name !='Central' and party.party_name !='ADA Central' and party.party_name !='Sales Customer' and party.party_id='" + party_id + "' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity,update_date,party_name,party.party_id,to_date,product.product_id,color.color_id,warehouse.warehouse_id";
                    var partyWiseStock = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return partyWiseStock;
                }
                return 0;

            }
            catch (Exception)
            {

                throw null;
            }
        }


        public object PartyWiseStockReport(long party_id, long user_id)
        {
            try
            {

                if (party_id == 0)
                {
                    string query = "SELECT product.product_name ,product.product_id ,color.color_name ,color.color_id ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,party.party_name ,party.party_id ,party_type.party_type_name as to_date ,inventory_stock.update_date ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and warehouse.warehouse_name !='Central' and warehouse.warehouse_name !='Esen' and party.party_name !='Sales Customer' and party.party_name !='Central' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity,update_date,party_name,to_date,warehouse.warehouse_id ,party.party_id,product.product_id,color.color_id";
                    var partyWiseStock = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return partyWiseStock;
                }


                if (party_id != 0)
                {
                    string query = "SELECT product.product_name ,product.product_id ,color.color_name ,color.color_id ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,party.party_name ,party.party_id ,party_type.party_type_name as to_date ,inventory_stock.update_date ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and warehouse.warehouse_name !='Central' and party.party_name !='Sales Customer' and party.party_name !='ADA Central' and party.party_id='" + party_id + "' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity,update_date,party_name,party.party_id,to_date,product.product_id,color.color_id,warehouse.warehouse_id";
                    var partyWiseStock = _entities.Database.SqlQuery<InventoryStockModel>(query).ToList();
                    return partyWiseStock;
                }

                return null;

            }
            catch (Exception)
            {

                throw;
            }

        }


        public object ImeiMovementCentralToParty(string from_date, string to_date)
        {
            try
            {
                string imeiMovement = "SELECT DISTINCT COALESCE((select country_name from country INNER JOIN warehouse on warehouse.country_id= country.country_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id),'*') as warehouse_country ,COALESCE((select province_name from province INNER JOIN warehouse on warehouse.province_id= province.province_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id),'*') as warehouse_province ,COALESCE((select city_name from district_city INNER JOIN warehouse on warehouse.city_id= district_city.city_id WHERE warehouse_id=receive_serial_no_details.received_warehouse_id),'*') as warehouse_district_city ,COALESCE(imei_no,'*') as imei_no ,COALESCE(party.party_code, 'POS Direct Sale' ) as party_code ,COALESCE(party.party_name, 'POS Direct Sale' )as party_name ,COALESCE((select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=receive_serial_no_details.party_id),'*') as customer_province ,COALESCE((select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=receive_serial_no_details.party_id),'*') as customer_district_city ,COALESCE((select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=receive_serial_no_details.party_id),'*') as ship_to_province ,COALESCE((select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=receive_serial_no_details.party_id),'*') as ship_to_district_city ,COALESCE(product.product_name,'*') as huawei_product_model ,COALESCE(product.product_name ,'*') as customer_product_model ,'ND' as color_name ,COALESCE(warehouse.warehouse_name ,'*') as warehouse_name ,COALESCE(party_type.party_type_name ,'*') as party_type_name ,'*' as retailer_name ,'*' as retailer_code ,'*' as retailer_province ,COALESCE((select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=receive_serial_no_details.retailer_id),'*') as retailer_district_city ,COALESCE(to_char(to_date(received_date,'DD/MM/YYYY'),'YYYY-MM-DD'),'*') as received_date_in_warehouse ,COALESCE(to_char(to_date(delivery_to_retailer_date ,'DD/MM/YYYY'),'YYYY-MM-DD'),'*') as delivery_to_retailer_date ,COALESCE(to_char(to_date(deliver_date ,'DD/MM/YYYY'),'YYYY-MM-DD'),'*') as deliver_date_to_party ,receive_serial_no_details.sales_status FROM receive_serial_no_details LEFT JOIN party on receive_serial_no_details.party_id=party.party_id LEFT JOIN product on receive_serial_no_details.product_id=product.product_id LEFT JOIN color on receive_serial_no_details.color_id = color.color_id LEFT JOIN warehouse on receive_serial_no_details.received_warehouse_id=warehouse.warehouse_id LEFT JOIN party_type on party.party_type_id= party_type.party_type_id WHERE to_date(receive_serial_no_details.deliver_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY') OR receive_serial_no_details.requisition_id=0 and receive_serial_no_details.deliver_master_id=0 AND receive_serial_no_details.sales_status=true";

                var getIMEIData = _entities.Database.SqlQuery<ImeiMovementCentralToPartyModel>(imeiMovement).ToList();
                return getIMEIData;
            }
            catch (Exception)
            {

                throw null;
            }

        }

        //No parameter for Grid
        public object ImeiMovementCentralToParty()
        {
            string imeiMovement = "select imei_no ,product.product_name ,color.color_name ,warehouse.warehouse_name ,warehouse.warehouse_address ,received_date as received_date_in_warehouse ,party.party_name ,party.party_code ,party.address as party_address ,party_type.party_type_name ,deliver_date as deliver_date_to_party ,(SELECT party_type.party_type_name FROM party_type WHERE party_type_id=receive_serial_no_details.retailer_id) as retailer_name ,(SELECT party.party_code FROM party WHERE party_type_id=receive_serial_no_details.retailer_id) as retailer_code ,(SELECT party.address FROM party WHERE party_type_id=receive_serial_no_details.retailer_id) as retailer_address ,delivery_to_retailer_date ,receive_serial_no_details.sales_status FROM receive_serial_no_details left JOIN party on receive_serial_no_details.party_id=party.party_id LEFT JOIN product on receive_serial_no_details.product_id=product.product_id LEFT JOIN color on receive_serial_no_details.color_id = color.color_id LEFT JOIN warehouse on receive_serial_no_details.received_warehouse_id=warehouse.warehouse_id LEFT JOIN party_type on party.party_type_id= party_type.party_type_id";
            var getIMEIData = _entities.Database.SqlQuery<ImeiMovementCentralToPartyModel>(imeiMovement).ToList();
            return getIMEIData;

        }

        //01-11-2016 By Kiron
        public object CustomerWisePSI(string product_id, string color_id, string from_date, string to_date)
        {

            try
            {


                string condition = "";
                //01 ==> When Both Not Select

                #region

                if (product_id == "0" && color_id == "0")
                {
                    condition = "";
                }

                #endregion


                //02 ==> When Both Selected 
                #region

                if (product_id != "0" && color_id != "0")
                {

                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inventory.product_id=" + item + "";
                            }
                            else
                            {
                                condition += " or inventory.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "inventory.product_id=" + product_id + " and ";
                    }
                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(inventory.color_id=" + item + "";
                            }
                            else
                            {
                                condition += " or inventory.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "inventory.color_id=" + color_id + " and ";
                    }
                }

                #endregion
                //Basic Query
                #region

                string customerPSIQuery = "SELECT party.party_name , party.party_code ,'" + from_date +
                                          "'as from_date,'" + to_date +
                                          "'as to_date , party_type.party_type_name , product.product_id, product.product_name, color.color_id, color.color_name ," +
                                          " opening_stock as previous_balance , stock_in , stock_out as sales , closing_stock as current_Stock FROM inventory " +
                                          " LEFT JOIN warehouse on inventory.warehouse_id=warehouse.warehouse_id " +
                                          " LEFT JOIN party on warehouse.party_id=party.party_id " +
                                          " LEFT JOIN party_type on party.party_type_id =party_type.party_type_id " +
                                          " LEFT JOIN product on inventory.product_id= product.product_id " +
                                          " LEFT JOIN color on inventory.color_id=color.color_id " +
                                          " WHERE " + condition + " warehouse.warehouse_id =1" +
                                          " AND  to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" +
                                          from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date +
                                          "', 'DD/MM/YYYY')" +
                                          " ORDER BY inventory_id";
                var psiAllData = _entities.Database.SqlQuery<CustomerWisePSIModel>(customerPSIQuery).ToList();
                #endregion

                var returnValue = new List<CustomerWisePSIModel>();
                // Product And Color List
                #region

                var intProductIdList = psiAllData.Select(p => p.product_id).Distinct().ToArray();
                var productIdList = Array.ConvertAll(intProductIdList, s => s.ToString());
                var intColorIdList = psiAllData.Select(c => c.color_id).Distinct().ToArray();
                var colorIdList = Array.ConvertAll(intColorIdList, s => s.ToString());

                #endregion
                //Generate each product row for single product
                #region

                foreach (string productId in productIdList)
                {
                    foreach (string colorId in colorIdList)
                    {
                        // working area -------------------------------->
                        long pid = long.Parse(productId);
                        long cid = long.Parse(colorId);
                        var psiData = psiAllData.Where(psi => psi.product_id == pid && psi.color_id == cid).ToList();
                        // working area --------------------------------//

                        var productColorSum = new CustomerWisePSIModel();

                        for (int i = 0; i < psiData.Count; i++)
                        {
                            if (i == 0)
                            {
                                productColorSum.party_id = psiData[i].party_id;
                                productColorSum.party_name = psiData[i].party_name;
                                productColorSum.product_id = psiData[i].product_id;
                                productColorSum.product_name = psiData[i].product_name;
                                productColorSum.color_id = psiData[i].color_id;
                                productColorSum.color_name = psiData[i].color_name;
                                productColorSum.previous_balance = psiData[i].previous_balance;
                                productColorSum.stock_in = psiData[i].stock_in;
                                productColorSum.sales = psiData[i].sales;
                                productColorSum.current_Stock = psiData[i].current_Stock;
                                productColorSum.from_date = psiData[i].from_date;
                                productColorSum.to_date = psiData[i].to_date;

                            }
                            else if (i == (psiData.Count - 1))
                            {
                                productColorSum.stock_in += psiData[i].stock_in;
                                productColorSum.sales += psiData[i].sales;
                                productColorSum.current_Stock = psiData[i].current_Stock;
                            }
                            else
                            {
                                productColorSum.stock_in += psiData[i].stock_in;
                                productColorSum.sales += psiData[i].sales;
                            }
                        }
                        if (psiData.Count > 0)
                        {
                            returnValue.Add(productColorSum);
                        }

                    }
                }


                #endregion

                return returnValue;

            }
            catch (Exception)
            {

                return 0;
            }
        }

        public object DeliverySummaryReportADAToMDDBIS(string party_type_id, string product_id, string color_id, string from_date, string to_date)
        {
            try
            {
                string condition = "";
                //01 ==> When Both Not Select
                #region
                if (product_id == "0" && color_id == "0" && party_type_id == "0")
                {
                    condition = "";
                }
                #endregion
                //02 ==> When Party_Type && Product Not NUll
                #region
                if (party_type_id != "0" && product_id != "0" && color_id == "0")
                {
                    if (party_type_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = party_type_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(party_type.party_type_id=" + item + "";
                            }
                            else
                            {
                                condition += " or party_type.party_type_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "party_type.party_type_id=" + party_type_id + " and ";
                    }
                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.product_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "delivery_details.product_id=" + product_id + " and ";
                    }
                }
                #endregion
                //03 ==> When Party_type && Color Not Null
                #region
                if (party_type_id != "0" && product_id == "0" && color_id != "0")
                {
                    if (party_type_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = party_type_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(party_type.party_type_id=" + item + "";
                            }
                            else
                            {
                                condition += " or party_type.party_type_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "party_type.party_type_id=" + party_type_id + " and ";
                    }

                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.color_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "delivery_details.color_id=" + color_id + " and ";
                    }
                }
                #endregion
                //04 ==> When Party_type Not Null
                #region
                if (party_type_id != "0" && product_id == "0" && color_id == "0")
                {
                    if (party_type_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = party_type_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(party_type.party_type_id=" + item + "";
                            }
                            else
                            {
                                condition += " or party_type.party_type_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "party_type.party_type_id=" + party_type_id + " and ";
                    }
                }
                #endregion
                //05 ==> When Product && Color Not Null
                #region
                if (party_type_id == "0" && product_id != "0" && color_id != "0")
                {
                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.product_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "delivery_details.product_id=" + product_id + " and ";
                    }
                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.color_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "delivery_details.color_id=" + color_id + " and ";
                    }
                }
                #endregion
                //06 ==> When Product Not Null
                #region
                if (party_type_id == "0" && product_id != "0" && color_id == "0")
                {
                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.product_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "delivery_details.product_id=" + product_id + " and ";
                    }
                }
                #endregion
                //07 ==> When Color_id Not Null
                #region
                if (party_type_id == "0" && product_id == "0" && color_id != "0")
                {
                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.color_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "delivery_details.color_id=" + color_id + " and ";
                    }
                }
                #endregion
                //08 ==> When Both Selected 
                #region
                if (product_id != "0" && color_id != "0" && party_type_id != "0")
                {

                    if (product_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = product_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.product_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "delivery_details.product_id=" + product_id + " and ";
                    }
                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(delivery_details.color_id=" + item + "";
                            }
                            else
                            {
                                condition += " or delivery_details.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "delivery_details.color_id=" + color_id + " and ";
                    }
                #endregion
                    //Party Type 
                    #region
                    if (party_type_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = party_type_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(party_type.party_type_id=" + item + "";
                            }
                            else
                            {
                                condition += " or party_type.party_type_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "party_type.party_type_id=" + party_type_id + " and ";
                    }
                    #endregion
                }
                #region "Main Query By: Kiron :05-11-2016"
                string deliveryReportQuery = "SELECT Distinct delivery_master.delivery_no ,'" + from_date + "'as from_date ,'" + to_date + "'as to_date " +
                                             ",delivery_master.delivery_date " +
                                             ",warehouse.warehouse_name as from_warehouse " +
                                             ",warehouse.warehouse_code as from_warehouse_code " +
                                             ",province.province_name as from_warehouse_province " +
                                             ",party.party_name " +
                                             ",party.party_code " +
                                             ",party_type.party_type_name " +
                                             ",(select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=delivery_master.party_id) as party_province " +
                                             ",(select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=delivery_master.party_id) as party_district_city " +
                                             ",(select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=delivery_master.party_id) as ship_to_province " +
                                             ",(select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=delivery_master.party_id) as ship_to_district_city " +
                                             ",product.product_name " +
                                             ",color.color_name " +
                                             ",delivery_details.delivered_quantity " +
                                             ",delivery_master.delivery_master_id " +
                                             "FROM delivery_details " +
                                             "LEFT JOIN delivery_master on delivery_details.delivery_master_id= delivery_master.delivery_master_id " +
                                             "LEFT JOIN party on delivery_master.party_id=party.party_id " +
                                             "LEFT JOIN party_type on party.party_type_id=party_type.party_type_id " +
                                             "LEFT JOIN product on delivery_details.product_id=product.product_id " +
                                             "LEFT JOIN color on delivery_details.color_id=color.color_id " +
                                             "LEFT JOIN warehouse ON delivery_master.from_warehouse_id=warehouse.warehouse_id " +
                                             "LEFT JOIN warehouse as warto ON delivery_master.to_warehouse_id=warto.warehouse_id " +
                                             "LEFT JOIN province on warehouse.province_id = province.province_id " +
                                             "LEFT JOIN district_city on warehouse.city_id=district_city.city_id " +
                                             "LEFT JOIN country on warehouse.country_id=country.company_id " +
                                             "WHERE " + condition + " to_date(delivery_master.delivery_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY')";
                var deliveryReportData = _entities.Database.SqlQuery<DeliverySummaryReportFromADAToPartyModel>(deliveryReportQuery).ToList();
                return deliveryReportData;
                #endregion
            }
            catch (Exception)
            {

                return 0;
            }


        }

        public object SellThroughBack(string from_date, string to_date)
        {
            try
            {
                string queryData = "select distinct COALESCE((select country_name from country INNER JOIN party on party.country_id= country.country_id WHERE party_id=return_master.md_dbis_id),'*') as country ,COALESCE((select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=return_master.md_dbis_id),'*') as ship_from_province ,COALESCE(party.party_name,'*') as party_name ,COALESCE(party.party_code,'*') as customer_code ,COALESCE(return_details.imei_no,'*' ) as imei_no ,COALESCE(product.product_name,'*') as huawei_product ,COALESCE(product.product_name,'*') as customer_product ,'*'as return_type_name ,'ND' as color_name ,COALESCE(to_char(return_master.return_date, 'YYYY-MM-DD'),'*') as shipment_date ,COALESCE((select province_name from province INNER JOIN party on party.province_id= province.province_id WHERE party_id=return_master.md_dbis_id),'*') as customer_province ,COALESCE((select city_name from district_city INNER JOIN party on party.city_id= district_city.city_id WHERE party_id=return_master.md_dbis_id),'*') as customer_district_city ,COALESCE(warehouse.warehouse_name,'*') as from_warehouse_name ,'*' as to_warehouse_name ,'*' as reserved_field from return_master left join return_details on return_master.return_master_id=return_details.return_master_id left join receive_serial_no_details on return_details.imei_no=return_details.imei_no and receive_serial_no_details.party_id=0 and is_return=true left join return_type on return_master.return_type=return_type_id left join product on return_details.product_id=product.product_id left join color on return_details.color_id=color.color_id left join party on return_master.md_dbis_id=party.party_id left join warehouse on return_details.warehouse_id=warehouse.warehouse_id left join warehouse as w2 on receive_serial_no_details.current_warehouse_id=w2.warehouse_id WHERE to_date(to_char(return_master.return_date, 'YYYY/MM/DD'), 'YYYY/MM/DD') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY')";
                var sellThroughBackQueryData = _entities.Database.SqlQuery<SellThroughBackModels>(queryData).ToList();
                return sellThroughBackQueryData;
            }
            catch (Exception)
            {

                return 0;
            }
        }



        public object GetAllDeliveredIMEIExcel()
        {
            string query = "select pro.product_name ,c.color_name ,rsnd.imei_no ,COALESCE(party.party_name,'Direct Sale') as party_name from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id inner join party on rsnd.party_id=party.party_id where rsnd.current_warehouse_id !=1 and rsnd.current_warehouse_id !=2 and rsnd.current_warehouse_id !=3 order by c.color_name asc";
            var allDeliveredIMEI = _entities.Database.SqlQuery<AllDeliveredIMEI>(query).ToList();
            return allDeliveredIMEI;
        }


        public object PSIDetails(string product_id, string color_id, string from_date, string to_date)
        {

            try
            {
                DateTime fff = Convert.ToDateTime(from_date);

                var kiron_Date = fff.AddDays(-28).ToString("dd/MM/yyyy");
                string query = "SELECT distinct '" + from_date + "'as from_date ,'" + to_date + "'as to_date ,product.product_id ,product.product_name ,product.product_code ,color.color_id ,color.color_name ,grn_details.po_quantity-grn_details.receive_quantity as intransit ,(SELECT COALESCE(sum(delivery_details.delivered_quantity),0) FROM delivery_details INNER JOIN delivery_master ON delivery_details.delivery_master_id=delivery_master.delivery_master_id WHERE delivery_details.product_id=grn_details.product_id AND delivery_details.color_id=grn_details.color_id OR delivery_master.delivery_date < '" + kiron_Date + "' ) as closing_inventory_before_from_date ,COALESCE((grn_details.receive_quantity)-(SELECT sum(delivery_details.delivered_quantity) FROM delivery_details WHERE delivery_details.product_id=grn_details.product_id AND delivery_details.color_id=grn_details.color_id ),0) as opening_balance ,grn_details.receive_quantity as stock_in ,(SELECT COALESCE(sum(delivered_quantity),0) FROM delivery_details WHERE delivery_details.product_id=grn_details.product_id AND delivery_details.color_id=grn_details.color_id)as delivery_qty ,COALESCE((grn_details.receive_quantity)-(SELECT sum(delivery_details.delivered_quantity) FROM delivery_details WHERE delivery_details.product_id=grn_details.product_id AND delivery_details.color_id=grn_details.color_id),0) as closing_balance ,(case when return_details.product_id=grn_details.product_id and return_details.color_id=grn_details.color_id then sum(returned_qty) else 0 end) as return_quantity FROM grn_details INNER JOIN product ON grn_details.product_id=product.product_id INNER JOIN color ON grn_details.color_id=color.color_id INNER JOIN grn_master ON grn_details.grn_master_id=grn_master.grn_master_id left join return_details on grn_details.product_id=return_details.product_id and grn_details.color_id=return_details.color_id left join return_master on return_details.return_master_id=return_master.return_master_id WHERE grn_details.receive_quantity !=0 AND to_date(grn_master.grn_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY') group by product.product_id ,product.product_name ,product.product_code,color.color_id ,color.color_name ,intransit,grn_details.product_id,grn_details.color_id,grn_details.receive_quantity,return_details.product_id,return_details.color_id";
                var psiDEtails = _entities.Database.SqlQuery<PSIDetailsModel>(query).ToList();
                return psiDEtails;

                //Need Not to go down.... Depricated code!!
                #region
                //string condition = "";
                ////01 ==> When Both Not Select

                //#region

                //if (product_id == "0" && color_id == "0")
                //{
                //    condition = "";
                //}

                //#endregion


                ////02 ==> When Both Selected 
                //#region

                //if (product_id != "0" && color_id != "0")
                //{

                //    if (product_id.Contains(","))
                //    {
                //        long counter = 0;
                //        string[] array = product_id.Split(',');
                //        foreach (var item in array)
                //        {
                //            if (counter == 0)
                //            {
                //                condition += "(inventory.product_id=" + item + "";
                //            }
                //            else
                //            {
                //                condition += " or inventory.product_id=" + item + "";
                //            }
                //            counter++;
                //        }
                //        condition += " ) and ";
                //    }
                //    else
                //    {
                //        condition = "inventory.product_id=" + product_id + " and ";
                //    }
                //    if (color_id.Contains(","))
                //    {
                //        long counter = 0;
                //        string[] array = color_id.Split(',');
                //        foreach (var item in array)
                //        {
                //            if (counter == 0)
                //            {
                //                condition += "(inventory.color_id=" + item + "";
                //            }
                //            else
                //            {
                //                condition += " or inventory.color_id=" + item + "";
                //            }
                //            counter++;
                //        }
                //        condition += " ) and ";
                //    }
                //    else
                //    {
                //        condition += "inventory.color_id=" + color_id + " and ";
                //    }
                //}

                //#endregion
                ////Basic Query
                //#region

                ////string customerPSIQuery = "SELECT DISTINCT party.party_name ,inventory.inventory_id , party.party_code ,'01/11/2016'as from_date ,'30/11/2016'as to_date , party_type.party_type_name , product.product_id , product.product_name , product.product_code , color.color_id , color.color_name , opening_stock as previous_balance , stock_in , stock_out as sales , closing_stock as current_Stock ,COALESCE((SELECT quantity FROM purchase_order_details WHERE purchase_order_details.product_id=inventory.product_id AND purchase_order_details.color_id=inventory.color_id ),'0') as po_quantity ,COALESCE((SELECT receive_qty FROM purchase_order_details WHERE purchase_order_details.product_id=inventory.product_id AND purchase_order_details.color_id=inventory.color_id ),'0') as grn_quantity ,(SELECT quantity FROM purchase_order_details WHERE purchase_order_details.product_id=inventory.product_id AND purchase_order_details.color_id=inventory.color_id )- (SELECT receive_qty FROM purchase_order_details WHERE purchase_order_details.product_id=inventory.product_id AND purchase_order_details.color_id=inventory.color_id ) as intransit ,COALESCE((SELECT COUNT(imei_no) FROM receive_serial_no_details WHERE receive_serial_no_details.product_id=inventory.product_id AND receive_serial_no_details.deliver_date <'"+xxx+"'),'0') as closing_inventory_before_from_date ,COALESCE((SELECT sum(returned_qty) FROM return_details LEFT JOIN return_master ON return_details.return_master_id=return_master.return_master_id WHERE return_details.product_id=inventory.product_id AND return_details.color_id=inventory.color_id AND to_date(to_char(return_master.return_date, 'DD/MM/YYYY'), 'DD/MM/YYYY') BETWEEN to_date('"+from_date+"', 'DD/MM/YYYY') and to_date('"+to_date+"', 'DD/MM/YYYY')),'0')as return_quntity_sum ,COALESCE(return_details.returned_qty,'0')as returned_qty ,COALESCE(delivery_details.delivered_quantity,'0') as delivered_quantity FROM inventory LEFT JOIN warehouse on inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN party on warehouse.party_id=party.party_id LEFT JOIN party_type on party.party_type_id =party_type.party_type_id LEFT JOIN product on inventory.product_id= product.product_id LEFT JOIN color on inventory.color_id=color.color_id LEFT JOIN return_details ON inventory.product_id=return_details.product_id AND inventory.color_id=return_details.color_id LEFT JOIN return_master ON return_details.return_master_id=return_master.return_master_id LEFT JOIN receive_serial_no_details ON inventory.product_id=receive_serial_no_details.product_id AND inventory.color_id=receive_serial_no_details.color_id LEFT JOIN delivery_master ON receive_serial_no_details.deliver_master_id=delivery_master.delivery_master_id LEFT JOIN delivery_details ON delivery_master.delivery_master_id=delivery_details.delivery_master_id WHERE warehouse.warehouse_id =1 AND to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('"+from_date+"', 'DD/MM/YYYY') AND to_date('"+to_date+"', 'DD/MM/YYYY') OR to_date(to_char(return_master.return_date, 'DD/MM/YYYY'), 'DD/MM/YYYY') BETWEEN to_date('"+from_date+"', 'DD/MM/YYYY') and to_date('"+to_date+"', 'DD/MM/YYYY') GROUP BY party.party_name,inventory.inventory_id, party.party_code, party_type.party_type_name, product.product_id, product.product_name , product.product_code, color.color_id, color.color_name,returned_qty,delivery_details.delivered_quantity ORDER BY inventory_id";
                //string customerPSIQuery =
                //    "SELECT party.party_name ,party.party_code,product.product_code ,'" + to_date + "'as from_date ,'" + from_date + "'as to_date ,party_type.party_type_name ,product.product_id ,product.product_name ,color.color_id ,color.color_name ,opening_stock as previous_balance ,stock_in ,stock_out as sales ,closing_stock as current_Stock ,purchase_order_details.quantity ,purchase_order_details.receive_qty ,purchase_order_details.quantity - purchase_order_details.receive_qty as intransit ,delivery_details.delivered_quantity ,return_details.returned_qty FROM inventory LEFT JOIN warehouse ON inventory.warehouse_id=warehouse.warehouse_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN party_type ON party.party_type_id =party_type.party_type_id LEFT JOIN product ON inventory.product_id= product.product_id LEFT JOIN color ON inventory.color_id=color.color_id INNER JOIN purchase_order_details ON inventory.product_id=purchase_order_details.product_id AND inventory.color_id=purchase_order_details.color_id INNER JOIN delivery_details ON inventory.product_id=delivery_details.product_id AND inventory.color_id=delivery_details.color_id INNER JOIN return_details ON inventory.product_id=return_details.product_id AND inventory.color_id=return_details.color_id WHERE warehouse.warehouse_id =1 AND to_date(inventory.transaction_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') AND to_date('" + to_date + "', 'DD/MM/YYYY') ORDER BY inventory_id";
                //var psiAllData = _entities.Database.SqlQuery<PSIDetailsModel>(customerPSIQuery).ToList();
                //#endregion

                //var returnValue = new List<PSIDetailsModel>();
                //// Product And Color List
                //#region

                //var intProductIdList = psiAllData.Select(p => p.product_id).Distinct().ToArray();
                //var productIdList = Array.ConvertAll(intProductIdList, s => s.ToString());
                //var intColorIdList = psiAllData.Select(c => c.color_id).Distinct().ToArray();
                //var colorIdList = Array.ConvertAll(intColorIdList, s => s.ToString());

                //#endregion
                ////Generate each product row for single product
                //#region

                //foreach (string productId in productIdList)
                //{
                //    foreach (string colorId in colorIdList)
                //    {
                //        // working area -------------------------------->
                //        long pid = long.Parse(productId);
                //        long cid = long.Parse(colorId);
                //        var psiData = psiAllData.Where(psi => psi.product_id == pid && psi.color_id == cid).ToList();
                //        // working area --------------------------------//

                //        var productColorSum = new PSIDetailsModel();

                //        for (long i = 0; i < psiData.Count; i++)
                //        {
                //            if (i == 0)
                //            {
                //                productColorSum.party_id = psiData[i].party_id;
                //                productColorSum.party_name = psiData[i].party_name;
                //                productColorSum.product_id = psiData[i].product_id;
                //                productColorSum.product_name = psiData[i].product_name;
                //                productColorSum.product_code = psiData[i].product_code;
                //                productColorSum.color_id = psiData[i].color_id;
                //                productColorSum.color_name = psiData[i].color_name;
                //                productColorSum.previous_balance = psiData[i].previous_balance;
                //                productColorSum.stock_in = psiData[i].stock_in;
                //                productColorSum.sales = psiData[i].sales;
                //                productColorSum.current_Stock = psiData[i].current_Stock;
                //                productColorSum.from_date = psiData[i].from_date;
                //                productColorSum.to_date = psiData[i].to_date;
                //                productColorSum.po_quantity = psiData[i].po_quantity;
                //                productColorSum.grn_quantity = psiData[i].grn_quantity;
                //                productColorSum.intransit = psiData[i].intransit;
                //                productColorSum.closing_inventory_before_from_date = psiData[i].closing_inventory_before_from_date;
                //                productColorSum.return_quntity_sum = psiData[i].return_quntity_sum;
                //                productColorSum.returned_qty = psiData[i].returned_qty;
                //                productColorSum.delivered_quantity = psiData[i].delivered_quantity;

                //            }
                //            else if (i == (psiData.Count - 1))
                //            {
                //                productColorSum.stock_in += psiData[i].stock_in;
                //                productColorSum.sales += psiData[i].sales;
                //                productColorSum.current_Stock += psiData[i].current_Stock;
                //                productColorSum.delivered_quantity += psiData[i].delivered_quantity;
                //                productColorSum.returned_qty += psiData[i].returned_qty;
                //            }
                //            else
                //            {
                //                productColorSum.stock_in += psiData[i].stock_in;
                //                productColorSum.sales += psiData[i].sales;
                //                productColorSum.delivered_quantity += psiData[i].delivered_quantity;
                //                productColorSum.returned_qty += psiData[i].returned_qty;
                //            }
                //        }
                //        if (psiData.Count > 0)
                //        {
                //            returnValue.Add(productColorSum);
                //        }

                //    }
                //}


                //#endregion

                //return returnValue;
                #endregion
            }
            catch (Exception)
            {

                return 0;
            }
        }

        //Added By Kiron:23-01-2017
        public object InventoryStockExcel(long product_id, long color_id, long product_version_id, long warehouse_id)
        {
            try
            {
                string query = "select pro.product_name ,c.color_name ,product_version.product_version_name ,rsnd.imei_no,rsnd.imei_no2 from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id inner join product_version on rsnd.product_version_id=product_version.product_version_id where rsnd.product_id='" + product_id + "' and rsnd.color_id='" + color_id + "' and rsnd.product_version_id='" + product_version_id + "' and rsnd.current_warehouse_id='" + warehouse_id + "'";


                var ExcelIMEI = _entities.Database.SqlQuery<InventoryStockExcelModel>(query).ToList();

                return ExcelIMEI;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public object GetAllInventoryStockPDF(long product_id, long color_id, long product_version_id, long warehouse_id, long user_id)
        {


            if (color_id != 0 && product_version_id != 0)
            {
                //string query = "SELECT product.product_name ,product.product_id ,color.color_name ,color.color_id ,product_version.product_version_name ,(SELECT STUFF((SELECT ',' + imei_no FROM receive_serial_no_details where receive_serial_no_details.product_id='"+product_id+"' and receive_serial_no_details.color_id='"+color_id+"' and receive_serial_no_details.product_version_id='"+product_version_id+"' FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,warehouse.warehouse_code ,party.party_name ,party.party_id ,region.region_name ,area.area_name ,inventory_stock.updated_date ,(select full_name from users where user_id='"+user_id+"') as current_user_full_name FROM inventory_stock inner join receive_serial_no_details on receive_serial_no_details.product_id=inventory_stock.product_id and receive_serial_no_details.product_version_id=inventory_stock.product_version_id and receive_serial_no_details.color_id=inventory_stock.color_id LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id left join region on warehouse.region_id=region.region_id left join area on warehouse.area_id=area.area_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and party.party_name !='Sales Customer' and warehouse.warehouse_id='"+warehouse_id+"' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity ,party_name,party.party_id,inventory_stock.updated_date ,product.product_id,color.color_id ,warehouse.warehouse_id,product_version.product_version_name,region.region_name ,area.area_name,warehouse.warehouse_code";
                //string query = "SELECT product.product_name ,product_category.product_category_name,product.product_id ,color.color_name ,color.color_id ,product_version.product_version_name ,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where receive_serial_no_details.product_id='" + product_id + "' and receive_serial_no_details.color_id='" + color_id + "' and receive_serial_no_details.product_version_id='" + product_version_id + "'and receive_serial_no_details.current_warehouse_id='" + warehouse_id + "'  FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,(SELECT STUFF((SELECT ' ' + imei_no2 FROM receive_serial_no_details where receive_serial_no_details.product_id='" + product_id + "' and receive_serial_no_details.color_id='" + color_id + "' and receive_serial_no_details.product_version_id='" + product_version_id + "'and receive_serial_no_details.current_warehouse_id='" + warehouse_id + "'  FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,warehouse.warehouse_code ,party.party_name ,party.party_id ,region.region_name ,area.area_name ,inventory_stock.updated_date ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM inventory_stock inner join receive_serial_no_details on receive_serial_no_details.product_id=inventory_stock.product_id and receive_serial_no_details.product_version_id=inventory_stock.product_version_id and receive_serial_no_details.color_id=inventory_stock.color_id LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product.product_category_id=product_category.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id left join region on warehouse.region_id=region.region_id left join area on warehouse.area_id=area.area_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and party.party_name !='Sales Customer' and warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' and product_version.product_version_id='" + product_version_id + "' and color.color_id='" + color_id + "' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity ,party_name,party.party_id,inventory_stock.updated_date ,product.product_id,color.color_id ,warehouse.warehouse_id,product_version.product_version_name,region.region_name ,product_category.product_category_name,area.area_name,warehouse.warehouse_code";
                string query = "SELECT product.product_name ,product_category.product_category_name,product.product_id ,color.color_name ,color.color_id ,product_version.product_version_name ,(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where receive_serial_no_details.product_id='" + product_id + "' and receive_serial_no_details.color_id='" + color_id + "' and receive_serial_no_details.product_version_id='" + product_version_id + "'and receive_serial_no_details.current_warehouse_id='" + warehouse_id + "'  FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no ,(SELECT STUFF((SELECT ' ' + imei_no2 FROM receive_serial_no_details where receive_serial_no_details.product_id='" + product_id + "' and receive_serial_no_details.color_id='" + color_id + "' and receive_serial_no_details.product_version_id='" + product_version_id + "'and receive_serial_no_details.current_warehouse_id='" + warehouse_id + "'  FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,warehouse.warehouse_code ,party.party_name ,party.party_id ,region.region_name ,area.area_name ,inventory_stock.updated_date ,(select full_name from users where user_id='" + user_id + "') as current_user_full_name FROM inventory_stock inner join receive_serial_no_details on receive_serial_no_details.product_id=inventory_stock.product_id and receive_serial_no_details.product_version_id=inventory_stock.product_version_id and receive_serial_no_details.color_id=inventory_stock.color_id LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product.product_category_id=product_category.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id left join region on warehouse.region_id=region.region_id left join area on warehouse.area_id=area.area_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' and product_version.product_version_id='" + product_version_id + "' and color.color_id='" + color_id + "' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity ,party_name,party.party_id,inventory_stock.updated_date ,product.product_id,color.color_id ,warehouse.warehouse_id,product_version.product_version_name,region.region_name ,product_category.product_category_name,area.area_name,warehouse.warehouse_code";
                var stockPdfImeiProducts = _entities.Database.SqlQuery<InventoryStockPDFModel>(query).ToList();
                return stockPdfImeiProducts;
            }

            else
            {

                //string query = "SELECT product.product_name ,product_category.product_category_name ,product.product_id ,color.color_name ,color.color_id ,product_version.product_version_name ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,warehouse.warehouse_code ,party.party_name ,party.party_id ,region.region_name ,area.area_name ,inventory_stock.updated_date ,(select full_name from users where user_id='1') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product.product_category_id=product_category.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN region on warehouse.region_id=region.region_id LEFT JOIN area on warehouse.area_id=area.area_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_name !='Purchase' and warehouse.warehouse_name !='In Transit' and party.party_name !='Sales Customer' and warehouse.warehouse_id='"+warehouse_id+"' and product.product_id='"+product_id+"' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity ,party_name,party.party_id,inventory_stock.updated_date ,product.product_id,color.color_id ,warehouse.warehouse_id,product_version.product_version_name,region.region_name ,product_category.product_category_name,area.area_name,warehouse.warehouse_code";
                string query = "SELECT product.product_name ,product_category.product_category_name ,product.product_id ,color.color_name ,color.color_id ,product_version.product_version_name ,unit.unit_name ,inventory_stock.stock_quantity ,warehouse.warehouse_name ,warehouse.warehouse_id ,warehouse.warehouse_code ,party.party_name ,party.party_id ,region.region_name ,area.area_name ,inventory_stock.updated_date ,(select full_name from users where user_id='1') as current_user_full_name FROM inventory_stock LEFT JOIN product ON inventory_stock.product_id=product.product_id LEFT JOIN color on inventory_stock.color_id=color.color_id LEFT JOIN product_category on product.product_category_id=product_category.product_category_id LEFT JOIN product_version on inventory_stock.product_version_id=product_version.product_version_id LEFT JOIN warehouse ON inventory_stock.warehouse_id=warehouse.warehouse_id LEFT JOIN region on warehouse.region_id=region.region_id LEFT JOIN area on warehouse.area_id=area.area_id LEFT JOIN party ON warehouse.party_id=party.party_id LEFT JOIN unit ON inventory_stock.uom_id=unit.unit_id LEFT JOIN party_type on party.party_type_id = party_type.party_type_id where warehouse.warehouse_id='" + warehouse_id + "' and product.product_id='" + product_id + "' GROUP BY warehouse.warehouse_name ,product_name,color_name,unit_name,stock_quantity ,party_name,party.party_id,inventory_stock.updated_date ,product.product_id,color.color_id ,warehouse.warehouse_id,product_version.product_version_name,region.region_name ,product_category.product_category_name,area.area_name,warehouse.warehouse_code";
                var stockPdfForAccsories = _entities.Database.SqlQuery<InventoryStockPDFModel>(query).ToList();
                return stockPdfForAccsories;
            }

            return null;
        }



    }
}