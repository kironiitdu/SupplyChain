using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class TransferOrderRepository : ITransferOrderRepository
    {
        private DMSEntities _entities;

        public TransferOrderRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllTransferOrder()
        {
            var data = (from tom in _entities.transfer_order_master
                        join frmw in _entities.warehouses on tom.from_warehouse_id equals frmw.warehouse_id
                        join tow in _entities.warehouses on tom.to_warehouse_id equals tow.warehouse_id
                        select new
                        {
                            transfer_order_master_id = tom.transfer_order_master_id,
                            order_no = tom.order_no,
                            order_date = tom.order_date,
                            from_warehouse_id = tom.from_warehouse_id,
                            from_warehouse_name = frmw.warehouse_name,
                            to_warehouse_id = tom.from_warehouse_id,
                            to_warehouse_name = tow.warehouse_name,
                            status = tom.status,
                            created_by = tom.created_by,
                            created_date = tom.created_date,
                            updated_by = tom.updated_by,
                            updated_date = tom.updated_date,
                            is_active = tom.is_active,
                            is_deleted = tom.is_deleted

                        }).Where(e => e.is_active == true).OrderByDescending(e => e.transfer_order_master_id).ToList();

            return data;
        }

        public object GetAllDeliverableTransferOrder()
        {
            var data = (from tom in _entities.transfer_order_master
                        join frmw in _entities.warehouses on tom.from_warehouse_id equals frmw.warehouse_id
                        join tow in _entities.warehouses on tom.to_warehouse_id equals tow.warehouse_id
                        select new
                        {
                            transfer_order_master_id = tom.transfer_order_master_id,
                            order_no = tom.order_no,
                            order_date = tom.order_date,
                            status = tom.status,
                            from_warehouse_id = tom.from_warehouse_id,
                            from_warehouse_name = frmw.warehouse_name,
                            to_warehouse_id = tom.from_warehouse_id,
                            to_warehouse_name = tow.warehouse_name,
                            expected_transfer_date = tom.expected_transfer_date,
                            created_by = tom.created_by,
                            created_date = tom.created_date,
                            updated_by = tom.updated_by,
                            updated_date = tom.updated_date,
                            is_active = tom.is_active,
                            is_deleted = tom.is_deleted

                        }).Where(e =>e.is_active == true).OrderByDescending(e => e.transfer_order_master_id).ToList();

            return data;
        }

        public object GetTransferOrderReportById(long transfer_order_master_id)
        {
            try
            {
                string query = "select pro.product_name,tom.created_date,pcat.product_category_name ,col.color_name ,pro_v.product_version_name ,tod.quantity ,fw.warehouse_name as from_warehouse_name ,tw.warehouse_name as to_warehouse_name ,tom.order_no ,tom.order_date ,tom.expected_transfer_date ,tom.status ,tom.remarks ,usr.full_name From transfer_order_details tod inner join transfer_order_master tom on tod.transfer_order_master_id =tom.transfer_order_master_id inner join product pro on tod.product_id = pro.product_id inner join warehouse fw on tom.from_warehouse_id = fw.warehouse_id inner join warehouse tw on tom.to_warehouse_id = tw.warehouse_id left join product_category pcat on pro.product_category_id=pcat.product_category_id left join product_version pro_v on tod.product_version_id = pro_v.product_version_id left join color col on tod.color_id= col.color_id left join users usr on tom.created_by = usr.user_id where tod.transfer_order_master_id=" + transfer_order_master_id + "";

                var poData = _entities.Database.SqlQuery<ToReportModel>(query).ToList();
                return poData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddTransferOrder(TransferOrderModel transferOrderModel)
        {
            try
            {
                var toMaster = transferOrderModel.ToMasterData;
                var toDetailsList = transferOrderModel.ToDetailsList;

                // generate order_no
                long toSerial = _entities.transfer_order_master.Max(to => (long?)to.transfer_order_master_id) ?? 0;

                if (toSerial != 0)
                {
                    toSerial++;

                }
                else
                {
                    toSerial++;
                }
                var toStr = toSerial.ToString().PadLeft(7, '0');

                //string orderNo = "ORD/" + DateTime.Now.Year + "/" + poSerial;
                string orderNo = "TO-" + toStr;
                toMaster.order_no = orderNo;
                toMaster.order_date = Convert.ToDateTime(transferOrderModel.ToMasterData.order_date);
                toMaster.status = transferOrderModel.ToMasterData.status;
                toMaster.remarks = transferOrderModel.ToMasterData.remarks;
                toMaster.expected_transfer_date = transferOrderModel.ToMasterData.expected_transfer_date;
                toMaster.total_amount = transferOrderModel.ToMasterData.total_amount;
                toMaster.from_warehouse_id = transferOrderModel.ToMasterData.from_warehouse_id;
                toMaster.to_warehouse_id = transferOrderModel.ToMasterData.to_warehouse_id;
                toMaster.created_by = transferOrderModel.ToMasterData.created_by;
                toMaster.created_date = DateTime.Now;
                toMaster.updated_by = transferOrderModel.ToMasterData.updated_by;
                toMaster.updated_date = DateTime.Now;
                toMaster.is_active = true;
                toMaster.is_deleted = false;

                _entities.transfer_order_master.Add(toMaster);
                _entities.SaveChanges();

                long transferOrderMasterId = toMaster.transfer_order_master_id;

                foreach (var item in toDetailsList)
                {
                    var toDetails = new transfer_order_details
                    {
                        transfer_order_master_id = transferOrderMasterId,
                        product_id = item.product_id,
                        unit_id = item.unit_id,
                        unit_price = item.unit_price,
                        quantity = item.quantity,
                        product_version_id = item.product_version_id,
                        brand_id = item.brand_id,
                        color_id = item.color_id,
                        transfered_quantity = 0,
                        last_modified_date = DateTime.Now,
                        line_total = item.line_total,
                        size_id = item.size_id,
                        status = item.status,
                        product_category_id = item.product_category_id

                    };

                    _entities.transfer_order_details.Add(toDetails);
                    _entities.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public TransferOrderModel GetTransferOrderById(long transfer_order_master_id)
        {

            TransferOrderModel transferOrderModel = new TransferOrderModel();

            transferOrderModel.ToMasterData = _entities.transfer_order_master.Find(transfer_order_master_id);

            transferOrderModel.ToDetailsList =
                _entities.transfer_order_details
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                    .Join(_entities.product_category, jcat => jcat.jp.product_category_id,
                        cat => cat.product_category_id, (jcat, cat) => new { jcat, cat })
                    .Join(_entities.brands, jb => jb.jcat.jp.brand_id, b => b.brand_id, (jb, b) => new { jb, b })
                     .GroupJoin(_entities.colors, jc => jc.jb.jcat.jp.color_id, c => c.color_id, (jc, c) => new { jc, nc = c.FirstOrDefault() })
                    .Join(_entities.units, ju => ju.jc.jb.jcat.jp.unit_id, u => u.unit_id, (ju, u) => new { ju, u })
                // .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jb.jcat.jp.product_version_id, jpv => jpv.product_version_id, (jpv, pp) => new { jpv, pp })
                     .GroupJoin(_entities.product_version, jpv => jpv.ju.jc.jb.jcat.jp.product_version_id, pv => pv.product_version_id, (jpv, pv) => new { jpv, vc = pv.FirstOrDefault() })
                    .Where(k => k.jpv.ju.jc.jb.jcat.jp.transfer_order_master_id == transfer_order_master_id)
                    .Select(i => new TransferOrderDetailsModel
                    {
                        transfer_order_details_id = i.jpv.ju.jc.jb.jcat.jp.transfer_order_details_id,
                        product_category_id = i.jpv.ju.jc.jb.cat.product_category_id,
                        product_category_name = i.jpv.ju.jc.jb.cat.product_category_name,
                        product_id = i.jpv.ju.jc.jb.jcat.jp.product_id,
                        product_name = i.jpv.ju.jc.jb.jcat.p.product_name,
                        has_serial = (bool)i.jpv.ju.jc.jb.jcat.p.has_serial,
                        brand_id = i.jpv.ju.jc.jb.jcat.jp.brand_id,
                        brand_name = i.jpv.ju.jc.b.brand_name,
                        color_id = i.jpv.ju.nc.color_id,
                        color_name = string.IsNullOrEmpty(i.jpv.ju.nc.color_name) ? "" : i.jpv.ju.nc.color_name,
                        quantity = i.jpv.ju.jc.jb.jcat.jp.quantity,
                        product_version_id = i.vc.product_version_id,
                        product_version_name = string.IsNullOrEmpty(i.vc.product_version_name) ? "" : i.vc.product_version_name,
                        transfered_quantity = i.jpv.ju.jc.jb.jcat.jp.transfered_quantity,
                        unit_price = i.jpv.ju.jc.jb.jcat.jp.unit_price,
                        line_total = i.jpv.ju.jc.jb.jcat.jp.line_total,
                        transfer_order_master_id = i.jpv.ju.jc.jb.jcat.jp.transfer_order_master_id,
                        last_modified_date = i.jpv.ju.jc.jb.jcat.jp.last_modified_date,
                        unit_id = i.jpv.u.unit_id,
                        unit_name = i.jpv.u.unit_name,
                        status = i.jpv.ju.jc.jb.jcat.jp.status
                    }).OrderByDescending(p => p.transfer_order_details_id).ToList();


            return transferOrderModel;


        }

        public bool EditTransferOrder(TransferOrderModel transferOrderModel)
        {
            try
            {
                var toMaster = transferOrderModel.ToMasterData;
                var toDetailsList = transferOrderModel.ToDetailsList;
                transfer_order_master masterData = _entities.transfer_order_master.Find(toMaster.transfer_order_master_id);

                masterData.order_date = Convert.ToDateTime(transferOrderModel.ToMasterData.order_date);
                masterData.remarks = transferOrderModel.ToMasterData.remarks;
                masterData.expected_transfer_date = transferOrderModel.ToMasterData.expected_transfer_date;
                masterData.total_amount = transferOrderModel.ToMasterData.total_amount;
                masterData.from_warehouse_id = transferOrderModel.ToMasterData.from_warehouse_id;
                masterData.to_warehouse_id = transferOrderModel.ToMasterData.to_warehouse_id;
                masterData.created_by = transferOrderModel.ToMasterData.created_by;
                masterData.created_date = DateTime.Now;
                masterData.updated_by = transferOrderModel.ToMasterData.updated_by;
                masterData.updated_date = DateTime.Now;
                masterData.is_active = true;
                masterData.is_deleted = false;

                _entities.SaveChanges();

                foreach (var item in toDetailsList)
                {
                    transfer_order_details detailsData =
                        _entities.transfer_order_details.FirstOrDefault(pd => pd.transfer_order_master_id == toMaster.transfer_order_master_id && pd.transfer_order_details_id == item.transfer_order_details_id && pd.last_modified_date==item.last_modified_date);
                    if (detailsData != null)
                    {
                        detailsData.transfer_order_master_id = toMaster.transfer_order_master_id;
                        detailsData.product_id = item.product_id;
                        detailsData.unit_id = item.unit_id;
                        detailsData.unit_price = item.unit_price;
                        detailsData.quantity = item.quantity;
                        detailsData.product_version_id = item.product_version_id;
                        detailsData.brand_id = item.brand_id;
                        detailsData.color_id = item.color_id;
                        detailsData.last_modified_date = DateTime.Now;
                        detailsData.line_total = item.line_total;
                        detailsData.size_id = item.size_id;
                        detailsData.status = item.status;
                        detailsData.product_category_id = item.product_category_id;

                        _entities.SaveChanges();

                    }
                    else
                    {
                        var poDetails = new transfer_order_details
                        {
                            transfer_order_master_id = toMaster.transfer_order_master_id,
                            product_id = item.product_id,
                            unit_id = item.unit_id,
                            unit_price = item.unit_price,
                            quantity = item.quantity,
                            product_version_id = item.product_version_id,
                            brand_id = item.brand_id,
                            color_id = item.color_id,
                            transfered_quantity = 0,
                            last_modified_date = DateTime.Now,
                            line_total = item.line_total,
                            size_id = item.size_id,
                            status = item.status,
                            product_category_id = item.product_category_id
                        };

                        _entities.transfer_order_details.Add(poDetails);
                        _entities.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTransferOrder(long transfer_order_master_id)
        {
            try
            {
                //List<grn_master> grnList =
                //    _entities.grn_master.Where(g => g.purchase_order_master_id == purchase_order_master_id).ToList();
                //if (grnList.Count == 0)
                //{
                //    transfer_order_master oTransferOrderMaster = _entities.transfer_order_master.Find(transfer_order_master_id);
                //    oTransferOrderMaster.is_active = false;
                //    oTransferOrderMaster.is_deleted = true;
                //    _entities.SaveChanges();

                //    return true;
                //}
                //else
                //{
                    return false;
                //}


            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTransferOrderDetailsById(long transfer_order_details_id)
        {
            try
            {
                transfer_order_details oTransferOrderDetails = _entities.transfer_order_details.Find(transfer_order_details_id);
                _entities.transfer_order_details.Attach(oTransferOrderDetails);
                _entities.transfer_order_details.Remove(oTransferOrderDetails);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}