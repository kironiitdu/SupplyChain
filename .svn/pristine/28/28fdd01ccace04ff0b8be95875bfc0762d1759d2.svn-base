using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class LocalPurchaseOrderRepository : ILocalPurchaseOrderRepository
    {
        private DMSEntities _entities;

        public LocalPurchaseOrderRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllLocalPurchaseOrders()
        {
            var data = (from pom in _entities.local_purchase_order_master
                        join sup in _entities.suppliers on pom.supplier_id equals sup.supplier_id
                        select new
                        {
                            purchase_order_master_id = pom.local_purchase_order_master_id,
                            order_no = pom.order_no,
                            order_date = pom.order_date,
                            supplier_id = pom.supplier_id,
                            created_date = pom.created_date,
                            supplier_name = sup.supplier_name,
                            vat_total = pom.vat_total,
                            tax_total = pom.tax_total,
                            is_active = pom.is_active,
                            is_deleted = pom.is_deleted,
                            approve_status = pom.approve_status,
                            total_amount_including_vattax = pom.total_amount_including_vattax,
                            total_amount_without_vattax = pom.total_amount_without_vattax

                        }).Where(e => e.is_active == true).OrderByDescending(e => e.purchase_order_master_id).ToList();

            return data;
        }

        public long AddLocalPurchaseOrder(LocalPurchaseOrderModel localPurchaseOrderModel)
        {
            try
            {
                var localPoMaster = localPurchaseOrderModel.LocalPoMasterData;
                var localPoDetailsList = localPurchaseOrderModel.LocalPoDetailsList;

                // generate order_no
                long poSerial = _entities.local_purchase_order_master.Max(po => (long?)po.local_purchase_order_master_id) ?? 0;

                if (poSerial != 0)
                {
                    poSerial++;
                }
                else
                {
                    poSerial++;
                }
                var poStr = poSerial.ToString().PadLeft(7, '0');

                string orderNo = "LPO-" + poStr;
                localPoMaster.local_purchase_order_reference_number = localPoMaster.local_purchase_order_reference_number;
                localPoMaster.supplier_id = localPoMaster.supplier_id;
                localPoMaster.currency_id = localPoMaster.currency_id;
                localPoMaster.total_amount_including_vattax = localPoMaster.total_amount_including_vattax;
                localPoMaster.status = localPoMaster.status;
                localPoMaster.company_id = localPoMaster.company_id;
                localPoMaster.order_date = DateTime.Now;
                localPoMaster.vat_total = localPoMaster.vat_total;
                localPoMaster.created_by = localPoMaster.created_by;
                localPoMaster.created_date = DateTime.Now;
                localPoMaster.updated_by = localPoMaster.updated_by;
                localPoMaster.updated_date = DateTime.Now;
                localPoMaster.remarks = localPoMaster.remarks;
                localPoMaster.is_active = true;
                localPoMaster.approve_status = "Not Approved";
                localPoMaster.is_deleted = false;
                localPoMaster.order_no = orderNo;
                localPoMaster.tax_total = localPoMaster.tax_total;
                localPoMaster.total_amount_without_vattax = localPoMaster.total_amount_without_vattax;

                _entities.local_purchase_order_master.Add(localPoMaster);
                _entities.SaveChanges();
                long localPurchaseOrderMasterId = localPoMaster.local_purchase_order_master_id;

                foreach (var item in localPoDetailsList)
                {
                    var localPoDetails = new local_purchase_order_details
                    {
                        local_purchase_order_master_id = localPurchaseOrderMasterId,
                        product_id = item.product_id,
                        unit_id = item.unit_id,
                        unit_price = item.unit_price,
                        quantity = item.quantity,
                        product_version_id = item.product_version_id,
                        amount = item.amount,
                        brand_id = item.brand_id,
                        color_id = item.color_id,
                        receive_qty = 0,
                        vat_amount = item.vat_amount,
                        last_modified_date = DateTime.Now,
                        tax_amount = item.tax_amount,
                        line_total = item.line_total,
                        size_id = item.size_id,
                        status = item.status,
                        vat_pcnt = item.vat_pcnt,
                        tax_pcnt = item.tax_pcnt,
                        product_category_id = item.product_category_id

                    };

                    _entities.local_purchase_order_details.Add(localPoDetails);
                    _entities.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool DeletePurchaseOrder(long local_purchase_order_master_id)
        {
            try
            {
                List<grn_master> grnList =
                    _entities.grn_master.Where(g => g.purchase_order_master_id == local_purchase_order_master_id).ToList();
                if (grnList.Count == 0)
                {
                    purchase_order_master oPurchaseOrderMaster = _entities.purchase_order_master.Find(local_purchase_order_master_id);
                    oPurchaseOrderMaster.is_active = false;
                    oPurchaseOrderMaster.is_deleted = true;
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}