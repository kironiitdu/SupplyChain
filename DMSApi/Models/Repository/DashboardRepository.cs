﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using iTextSharp.text;

namespace DMSApi.Models.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private DMSEntities _entities;

        public DashboardRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetPoStatus()
        {
            //var totalPo = _entities.purchase_order_master.Count();
            var totalApproved = _entities.purchase_order_master.Count(p => p.approve_status == "Approved");
            var totalUnApproved = _entities.purchase_order_master.Count(p => p.approve_status == "Not Approved");

            List<PoStatus> poStatusList = new List<PoStatus>();

            //PoStatus poStatusTotalPo= new PoStatus
            //{
            //    Name = "Total PO",
            //    Value = totalPo
            //};
            //poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Approved",
                Value = totalApproved
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Unapproved",
                Value = totalUnApproved
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);


            return poStatusList;
        }

        public object GetPoStatusPanel()
        {
            var totalPo = _entities.purchase_order_master.Count();
            var totalApproved = _entities.purchase_order_master.Count(p => p.approve_status == "Approved");
            var totalUnApproved = _entities.purchase_order_master.Count(p => p.approve_status == "Not Approved");

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total PO",
                Value = totalPo
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Approved",
                Value = totalApproved
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Unapproved",
                Value = totalUnApproved
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);


            return poStatusList;
        }

        public object GetPiStatusPanel()
        {
            var totalPi = _entities.purchase_order_master.Count(p => p.pi_number != null);
            var totalApproved = _entities.purchase_order_master.Count(p => p.pi_number != null && p.approve_status == "Approved");
            var totalUnApproved = _entities.purchase_order_master.Count(p => p.pi_number != null && p.approve_status == "Not Approved");

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total PI",
                Value = totalPi
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Approved",
                Value = totalApproved
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Unapproved",
                Value = totalUnApproved
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);


            return poStatusList;
        }

        public object GetGrnStatusPanel()
        {
            var receivedGrnNo = _entities.grn_master.Count();
            var receivableGrnNo = _entities.ci_pl_master.Count(c => c.is_received == false);

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Total Grn No",
                Value = receivableGrnNo + receivedGrnNo
            };

            poStatusList.Add(poStatusTotalUnapprovedPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Received Grn No",
                Value = receivedGrnNo
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Receivable Grn No",
                Value = receivableGrnNo
            };
            poStatusList.Add(poStatusTotalPo);

            return poStatusList;
        }

        public object GetToStatusPanel()
        {
            var totalTo = _entities.transfer_order_master.Count();
            var totalPending = _entities.transfer_order_master.Count(p => p.status == "Created");
            var totalDelivered = _entities.transfer_order_master.Count(p => p.status == "Delivered");

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total TO",
                Value = totalTo
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Pending",
                Value = totalPending
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Delivered",
                Value = totalDelivered
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);


            return poStatusList;
        }

        public object GetStockStatusPanel()
        {
            var totalSellable = _entities.receive_serial_no_details.Count(r => r.current_warehouse_id == 1 && r.sales_status == false);
            var totalNonSellable = _entities.receive_serial_no_details.Count(r => r.current_warehouse_id == 2 || r.current_warehouse_id == 3 || r.current_warehouse_id == 13 || r.current_warehouse_id == 14 || r.current_warehouse_id == 15);
            var totalStock = totalSellable + totalNonSellable;

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total Stock",
                Value = totalStock
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Sellable",
                Value = totalSellable
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Non Sellable",
                Value = totalNonSellable
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);


            return poStatusList;
        }

        public object GetUserInfoStatusPanel()
        {
            var totalActiveUser = _entities.users.Count(r => r.is_active == true);
            var totalInActiveUser = _entities.users.Count(r => r.is_active == false);
            var totalUser = totalActiveUser + totalInActiveUser;

            List<PoStatus> poStatusList = new List<PoStatus>();

            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total User",
                Value = totalUser
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "Active User",
                Value = totalActiveUser
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "Inactive User",
                Value = totalInActiveUser
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);

            return poStatusList;
        }

        public object GetApprovalRequisitionStatus()
        {
            var totalAccPending = _entities.requisition_master.Count(r => r.status == "Not Forwarded");
            var totalHosPending = _entities.requisition_master.Count(r => r.status == "Forward to HOS" && r.finance_status == "Not Approved");
            var totalHopPending = _entities.requisition_master.Count(r => r.status == "Forward to HOS" && r.finance_status == "Approved" && r.forward_2_status == null);

            List<PoStatus> poStatusList = new List<PoStatus>();
            PoStatus poStatusTotalPo = new PoStatus
            {
                Name = "Total Acc Pending",
                Value = totalAccPending
            };
            poStatusList.Add(poStatusTotalPo);

            PoStatus poStatusTotalApprovedPo = new PoStatus
            {
                Name = "HOS Pending",
                Value = totalHosPending
            };
            poStatusList.Add(poStatusTotalApprovedPo);

            PoStatus poStatusTotalUnapprovedPo = new PoStatus
            {
                Name = "HOP Pending",
                Value = totalHopPending
            };
            poStatusList.Add(poStatusTotalUnapprovedPo);

            return poStatusList;
        }

        public object GetRequisitionStatus()
        {
            string query = "select count(requisition_master_id) as RequisitionQuantity ,MONTH(CAST(requisition_date AS DATE)) as RequisitionDate ,DATENAME(month,requisition_date) as RequisitionMonth ,YEAR(CAST(requisition_date AS DATE)) as RequisitionYear from requisition_master GROUP BY MONTH(CAST(requisition_date AS DATE)), YEAR(CAST(requisition_date AS DATE)) ,DATENAME(month,requisition_date) order by requisitionYear";
            var data = _entities.Database.SqlQuery<RequisitionStatus>(query).ToList();
            return data;
        }

        public object GetRequisitionStatusLine()
        {
            string query = "select count(requisition_master_id) as RequisitionQuantity ,MONTH(CAST(requisition_date AS DATE)) as RequisitionDate ,DATENAME(month,requisition_date) as RequisitionMonth ,YEAR(CAST(requisition_date AS DATE)) as RequisitionYear from requisition_master GROUP BY MONTH(CAST(requisition_date AS DATE)), YEAR(CAST(requisition_date AS DATE)) ,DATENAME(month,requisition_date) order by requisitionYear";
            var data = _entities.Database.SqlQuery<RequisitionStatus>(query).ToList();
            return data;
        }


        public object GetSystemMemoryConsumption()
        {
            string query = "SELECT TableId, TableName,CONVERT(bigint,rows) AS NumberOfRows, CONVERT(bigint,left(reserved,len(reserved)-3)) AS SizeinKB FROM RowCountsAndSizes ORDER BY NumberOfRows DESC,SizeinKB DESC,TableName";
            var data = _entities.Database.SqlQuery<SystemMemoryConsumtion>(query).ToList();
            return data;
        }
        public object GetTotalSystemMemoryConsumptionStatus()
        {
            string query = "select sum (CONVERT(bigint,left(reserved,len(reserved)-3))) AS TotalSizeinKB from RowCountsAndSizes";
            var data = _entities.Database.SqlQuery<SystemTotalMemoryConsumtion>(query).ToList();
            return data;
        }

        public object TopTenDealerChart(string fromDate, string toDate)
        {
            string query = "select top 10 SUM(invoice_total) as invoice_total,im.party_id,p.party_name,a.area_name,r.region_name,t.territory_name,pt.party_type_name from invoice_master im LEFT JOIN party p ON p.party_id = im.party_id LEFT JOIN area a ON a.area_id = p.area_id LEFT JOIN region r ON r.region_id = p.region_id LEFT JOIN territory t ON t.territory_id = p.territory_id LEFT JOIN party_type pt ON pt.party_type_id = p.party_type_id where pt.party_type_name='Dealer' and im.invoice_date between '" + fromDate + "' and '" + toDate + "' group by im.party_id,p.party_name,a.area_name,r.region_name,t.territory_name,pt.party_type_name order by invoice_total desc";
            var data = _entities.Database.SqlQuery<TopTenDealer>(query).ToList();
            return data;
        }

        public object BestSellingProducts(string fromDate, string toDate)
        {
            string query = "select '" + fromDate + "' as from_date,'" + toDate + "' as to_date,p.product_id,p.product_name,c.color_name,pv.product_version_name,SUM(id.quantity)as quantity from invoice_details id LEFT JOIN product P ON P.product_id = id.product_id LEFT JOIN color c ON c.color_id = id.color_id LEFT JOIN product_version pv ON pv.product_version_id = id.product_version_id LEFT JOIN invoice_master im ON im.invoice_master_id = id.invoice_master_id where im.invoice_date between '" + fromDate + "' and '" + toDate + "' group by p.product_name,c.color_name,pv.product_version_name,p.product_id order by quantity desc";
            var data = _entities.Database.SqlQuery<BestSellingProduct>(query).ToList();
            return data;
        }

        public object GetProductLiftingStatus(DateTime from_date, DateTime to_date, string region_id, string area_id, string territory_id)
        {
            try
            {
                DateTime toDate = to_date.AddDays(1);

                string condition = "";

                #region "1# if region, area and territory all not selected"

                if (region_id == "0" && area_id == "0" && territory_id == "0")
                {
                    condition = "";
                }

                #endregion

                #region "2# if region selected but area and territory not selected"

                if (region_id != "0" && area_id == "0" && territory_id == "0")
                {
                    condition = " and r.region_id=" + region_id + " ";
                }

                #endregion

                #region "3# if region and area selected but territory not selected"

                if (region_id != "0" && area_id != "0" && territory_id == "0")
                {
                    condition = " and r.region_id=" + region_id + " and a.area_id=" + area_id + " ";
                }

                #endregion

                #region "4# if region, area and territory all selected"

                if (region_id != "0" && area_id != "0" && territory_id != "0")
                {
                    condition = " and r.region_id=" + region_id + " and a.area_id=" + area_id + " and t.territory_id=" + territory_id + " ";
                }

                #endregion

                string baseQuiry = "select '" + from_date + "' as from_date,'" + toDate + "' as to_date, pro.product_id,pro.product_name,a.area_id,a.area_name,r.region_id,r.region_name,t.territory_id,t.territory_name,SUM(dd.delivered_quantity) as lifting_quantity " +
                                   "from delivery_details dd LEFT JOIN delivery_master dm ON dm.delivery_master_id = dd.delivery_master_id LEFT JOIN product pro ON pro.product_id = dd.product_id " +
                                   "LEFT JOIN party pa ON pa.party_id = dm.party_id LEFT JOIN area a ON a.area_id = pa.area_id " +
                                   "LEFT JOIN region r ON r.region_id = pa.region_id LEFT JOIN territory t ON t.territory_id = pa.territory_id " +
                                   "where dm.delivery_date between '" + from_date + "' and '" + toDate + "' " + condition + " group by pro.product_id,pro.product_name,a.area_id,a.area_name,r.region_id,r.region_name,t.territory_id,t.territory_name order by lifting_quantity desc";

                var data = _entities.Database.SqlQuery<ProductLifting>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public object GetTopTenDealerReport(DateTime from_date, DateTime to_date)
        {
            DateTime toDate = to_date.AddDays(1);
            try
            {
                string query = "select top 10 '" + from_date + "' as from_date,'" + toDate + "' as to_date,SUM(invoice_total) as invoice_total,im.party_id,p.party_name,a.area_name,r.region_name,t.territory_name,pt.party_type_name from invoice_master im LEFT JOIN party p ON p.party_id = im.party_id LEFT JOIN area a ON a.area_id = p.area_id LEFT JOIN region r ON r.region_id = p.region_id LEFT JOIN territory t ON t.territory_id = p.territory_id LEFT JOIN party_type pt ON pt.party_type_id = p.party_type_id where pt.party_type_name='Dealer' and im.invoice_date between '" + from_date + "' and '" + toDate + "' group by im.party_id,p.party_name,a.area_name,r.region_name,t.territory_name,pt.party_type_name order by invoice_total desc";
                var data = _entities.Database.SqlQuery<TopTenDealer>(query).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BestSellingProductsReport(DateTime from_date, DateTime to_date)
        {
            DateTime toDate = to_date.AddDays(1);
            try
            {
                string query = "select '" + from_date + "' as from_date,'" + toDate + "' as to_date,p.product_id,p.product_name,c.color_name,pv.product_version_name,SUM(id.quantity)as quantity from invoice_details id LEFT JOIN product P ON P.product_id = id.product_id LEFT JOIN color c ON c.color_id = id.color_id LEFT JOIN product_version pv ON pv.product_version_id = id.product_version_id LEFT JOIN invoice_master im ON im.invoice_master_id = id.invoice_master_id where im.invoice_date between '" + from_date + "' and '" + toDate + "' group by p.product_name,c.color_name,pv.product_version_name,p.product_id order by quantity desc";
                var data = _entities.Database.SqlQuery<BestSellingProduct>(query).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetSalesTrendStatus(DateTime from_date, DateTime to_date, string product_id)
        {
            try
            {
                DateTime toDate = to_date.AddDays(1);
                string condition = "";

                #region "1# if product selected"
                if (product_id != "0")
                {
                    condition = " and id.product_id=" + product_id + " ";
                }
                #endregion

                string baseQuiry = "select '" + from_date + "' as from_date,'" + toDate + "' as to_date, SUM(id.delivered_quantity)as quantity, CONVERT(VARCHAR(19),im.delivery_date,10) as delivery_date from delivery_details id LEFT JOIN product P ON P.product_id = id.product_id LEFT JOIN color c ON c.color_id = id.color_id LEFT JOIN product_version pv ON pv.product_version_id = id.product_version_id LEFT JOIN delivery_master im ON im.delivery_master_id = id.delivery_master_id  " +
                                   "where im.delivery_date between '" + from_date + "' and '" + toDate + "' " + condition + " group by CONVERT(VARCHAR(19),im.delivery_date,10)";

                var data = _entities.Database.SqlQuery<SalesTrendStatus>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }

    public class SalesTrendStatus
    {
        public int? quantity { get; set; }
        public string delivery_date { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }

    public class BestSellingProduct
    {
        public int? quantity { get; set; }
        public long? product_id { get; set; }
        public string product_name { get; set; }
        public string color_name { get; set; }
        public string product_version_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }

    public class ProductLifting
    {
        public long? product_id { get; set; }
        public string product_name { get; set; }
        public long? region_id { get; set; }
        public string region_name { get; set; }
        public long? area_id { get; set; }
        public string area_name { get; set; }
        public long? territory_id { get; set; }
        public string territory_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int? lifting_quantity { get; set; }
    }

    public class TopTenDealer
    {
        public decimal? invoice_total { get; set; }
        public string party_name { get; set; }
        public long? party_id { get; set; }
        public string area_name { get; set; }
        public string region_name { get; set; }
        public string territory_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }

    public class PoStatus
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class RequisitionStatus
    {
        public int RequisitionQuantity { get; set; }
        public string RequisitionMonth { get; set; }
        public int RequisitionYear { get; set; }
    }

    public class SystemMemoryConsumtion
    {

        public int TableId { get; set; }
        public string TableName { get; set; }
        public long NumberOfRows { get; set; }
        public long SizeinKB { get; set; }
    }
    public class SystemTotalMemoryConsumtion
    {
        public long TotalSizeinKB { get; set; }
    }
}