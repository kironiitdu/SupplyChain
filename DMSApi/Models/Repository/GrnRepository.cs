using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;


using System.Transactions;
using System.Data.Objects;
using System.Data;

namespace DMSApi.Models.Repository
{
    public class GrnRepository : IGrnRepository
    {
        private DMSEntities _entities;

        public GrnRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllGrns()
        {
            var users = (from grn in _entities.grn_master
                         join sup in _entities.suppliers on grn.supplier_id equals sup.supplier_id
                         select new
                         {
                             grn_master_id = grn.grn_master_id,
                             grn_no = grn.grn_no,
                             grn_date = grn.grn_date,
                             supplier_id = grn.supplier_id,
                             supplier_name = sup.supplier_name,
                             vat_total = grn.vat_total,
                             tax_total = grn.tax_total,
                             created_date = grn.created_date,
                             total_amount_including_vattax = grn.total_amount_including_vattax,
                             total_amount_without_vattax = grn.total_amount_without_vattax

                         }).OrderByDescending(e => e.grn_master_id).ToList();

            return users;
        }

        public object GetGrnReportById(long grn_master_id)
        {
            try
            {
                string query = "select distinct gm.grn_no,gm.lot_no,gm.grn_date,usr.full_name,pom.order_no,sup.supplier_name,wh.warehouse_name," +
                               "pro.product_name, pc.product_category_name,bd.brand_name,col.color_name, " +
                               "(SELECT STUFF((SELECT ' ' + imei_no FROM receive_serial_no_details where receive_serial_no_details.grn_master_id= " + grn_master_id + " and gd.product_id=receive_serial_no_details.product_id and gd.color_id=receive_serial_no_details.color_id and gd.product_version_id=receive_serial_no_details.product_version_id FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no," +
                               "(SELECT STUFF((SELECT ' ' + imei_no2 FROM receive_serial_no_details where receive_serial_no_details.grn_master_id= " + grn_master_id + " and gd.product_id=receive_serial_no_details.product_id and gd.color_id=receive_serial_no_details.color_id and gd.product_version_id=receive_serial_no_details.product_version_id FOR XML PATH('')) ,1,1,'') AS Txt)as imei_no2, " +
                               "gm.remarks, gm.vat_total,gm.tax_total,gm.total_amount_including_vattax,gd.receive_quantity, " +
                               "(gd.pi_quantity-gd.receive_quantity) as receivable_quantity, gd.unit_price,gd.vat_pcnt,gd.tax_pcnt,gd.vat_amount,gd.tax_amount," +
                               "gd.amount,gd.line_total " +
                               "FROM grn_master gm " +
                               "inner join grn_details gd on gm.grn_master_id =gd.grn_master_id " +
                               "left join receive_serial_no_details rsnd on gd.product_id =rsnd.product_id " +
                               "and gd.color_id =rsnd.color_id and gd.grn_master_id =rsnd.grn_master_id " +
                               "inner join product pro on gd.product_id = pro.product_id " +
                               "left join color col on gd.color_id= col.color_id " +
                               "inner join warehouse wh on gm.warehouse_id= wh.warehouse_id " +
                               "inner join supplier sup on gm.supplier_id= sup.supplier_id " +
                               "inner join purchase_order_master pom on gm.purchase_order_master_id= pom.purchase_order_master_id " +
                               "inner join brand bd on pro.brand_id= bd.brand_id " +
                               "inner join product_category pc on pro.product_category_id= pc.product_category_id " +
                               "left join users usr on gm.created_by = usr.user_id where gm.grn_master_id=" + grn_master_id +
                               "group by gm.grn_no,gm.grn_date,usr.full_name,pom.order_no,sup.supplier_name,wh.warehouse_name,pro.product_name,col.color_name, " +
                               "gm.remarks, gm.vat_total,gm.tax_total, gm.total_amount_including_vattax,gd.receive_quantity,gd.pi_quantity-gd.receive_quantity, " +
                               "gd.unit_price,gd.vat_pcnt,gd.tax_pcnt,gd.vat_amount,gd.tax_amount,gd.amount ,bd.brand_name, pc.product_category_name,gd.line_total," +
                               "gm.lot_no,gd.product_id,gd.color_id,gd.product_version_id";

                var poData = _entities.Database.SqlQuery<GrnReportModel>(query).ToList();
                return poData;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public object GetGrnExcelReportByGrnMasterId(long grn_master_id)
        {
            try
            {
                string query = "select pro.product_name,c.color_name,rsnd.imei_no,rsnd.imei_no2 from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where rsnd.grn_master_id='"+grn_master_id+"' order by pro.product_name";

                var poData = _entities.Database.SqlQuery<GrnReportModel>(query).ToList();
                return poData;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object GetGrnExcelReportByGrnMasterIdProductIdColorId(long grn_master_id, long product_id, long color_id)
        {
            try
            {
                string query = "select pro.product_name,c.color_name,rsnd.imei_no from receive_serial_no_details rsnd inner join product pro on rsnd.product_id = pro.product_id inner join color c on c.color_id =rsnd.color_id where rsnd.grn_master_id='" + grn_master_id + "' and rsnd.product_id='" + product_id + "' and rsnd.color_id='" + color_id+ "'";

                var poData = _entities.Database.SqlQuery<GrnReportModel>(query).ToList();

                return poData;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public object GetGrnExcelData(string from_date, string to_date, string product_id, string color_id)
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
                                condition += "(gd.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or gd.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "gd.product_id=" + product_id + " and ";
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
                                condition += "(gd.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or gd.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "gd.color_id=" + color_id + " and ";
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
                                condition += "(gd.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or gd.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "gd.product_id=" + product_id + " and ";
                    }

                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(gd.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or gd.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "gd.color_id=" + color_id + " and ";
                    }

                }

                #endregion

                string baseQuiry = "select gm.grn_master_id,p.product_id,c.color_id,gm.grn_no,to_char(to_date(gm.grn_date,'DD/MM/YYYY'),'YYYY-MM-DD') as grn_date,to_char(to_timestamp(gm.created_date,'MM/DD/YYYY HH12:MI:SS'),'YYYY-MM-DD HH12:MI:SS') as created_date,pm.order_no ,p.product_name,gd.product_id,c.color_name,gd.color_id,gd.receive_quantity  from grn_master gm inner join purchase_order_master pm on pm.purchase_order_master_id=gm.purchase_order_master_id inner join grn_details gd on gd.grn_master_id=gm.grn_master_id inner join product p on p.product_id=gd.product_id inner join color c on c.color_id=gd.color_id WHERE " + condition + " to_date(gm.grn_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY') and gd.receive_quantity !=0 order by gm.grn_no asc";

                var data = _entities.Database.SqlQuery<GrnExcelModel>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public object GetProductGrnDetailsData(string from_date, string to_date, string product_id, string color_id)
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
                                condition += "(rsnd.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or rsnd.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "rsnd.product_id=" + product_id + " and ";
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
                                condition += "(rsnd.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or rsnd.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "rsnd.color_id=" + color_id + " and ";
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
                                condition += "(rsnd.product_id=" + item + "";
                            }
                            else
                            {
                                condition += "or rsnd.product_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition = "rsnd.product_id=" + product_id + " and ";
                    }

                    if (color_id.Contains(","))
                    {
                        long counter = 0;
                        string[] array = color_id.Split(',');
                        foreach (var item in array)
                        {
                            if (counter == 0)
                            {
                                condition += "(rsnd.color_id=" + item + "";
                            }
                            else
                            {
                                condition += "or rsnd.color_id=" + item + "";
                            }
                            counter++;
                        }
                        condition += " ) and ";
                    }
                    else
                    {
                        condition += "rsnd.color_id=" + color_id + " and ";
                    }

                }

                #endregion

                string baseQuiry = "select p.product_id,p.product_name,c.color_id,c.color_name,rsnd.imei_no from receive_serial_no_details rsnd inner join product p on p.product_id=rsnd.product_id inner join color c on c.color_id=rsnd.color_id WHERE " + condition + "  to_date(rsnd.received_date, 'DD/MM/YYYY') BETWEEN to_date('" + from_date + "', 'DD/MM/YYYY') and to_date('" + to_date + "', 'DD/MM/YYYY')";

                var data = _entities.Database.SqlQuery<GrnReportModel>(baseQuiry).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public long AddGrn(GrnModel grnModel)
        {
            
            try
            {

                var grnMaster = grnModel.GrnMasterData;
                var grnDetailsList = grnModel.GrnDetailsList;
                var receiveSerialNoDetails = grnModel.ReceiveSerialNoDetails;
                bool isDuplicateImei = false;

                foreach (var item in receiveSerialNoDetails)
                {
                    long imeiForDuplicateSearch = item.imei_no??0;

                    var xxx = _entities.receive_serial_no_details.Where(r => r.imei_no == imeiForDuplicateSearch).ToList();
                    if (xxx.Count > 0)
                    {
                        isDuplicateImei=true;
                    }
                }

                if (!isDuplicateImei)
                {
                    // generate order_no
                    long grnSerial = _entities.grn_master.Max(po => (long?)po.grn_master_id) ?? 0;
                    grnSerial++;
                    var grnStr = grnSerial.ToString().PadLeft(7, '0');

                    // SAVING GRN MASTER
                    string grnNo = "GRN-" + grnStr;
                    grnMaster.supplier_id = grnModel.GrnMasterData.supplier_id;
                    grnMaster.total_amount_including_vattax = grnModel.GrnMasterData.total_amount_including_vattax;
                    grnMaster.status = grnModel.GrnMasterData.status;
                    grnMaster.company_id = grnModel.GrnMasterData.company_id;
                    grnMaster.grn_date = grnModel.GrnMasterData.grn_date;
                    grnMaster.purchase_order_master_id = grnModel.GrnMasterData.purchase_order_master_id;
                    grnMaster.vat_total = grnModel.GrnMasterData.vat_total;
                    grnMaster.warehouse_id = grnModel.GrnMasterData.warehouse_id;
                    grnMaster.created_by = grnModel.GrnMasterData.created_by;
                    grnMaster.created_date = DateTime.Now;
                    grnMaster.updated_by = grnModel.GrnMasterData.updated_by;
                    grnMaster.updated_date = DateTime.Now;
                    grnMaster.remarks = grnModel.GrnMasterData.remarks;
                    grnMaster.lot_no = grnModel.GrnMasterData.lot_no;
                    grnMaster.lc_id = 0;
                    grnMaster.grn_no = grnNo;
                    grnMaster.tax_total = grnModel.GrnMasterData.tax_total;
                    grnMaster.total_amount_without_vattax = grnModel.GrnMasterData.total_amount_without_vattax;
                    grnMaster.mrr_status = false;

                    _entities.grn_master.Add(grnMaster);
                    _entities.SaveChanges();

                    // Maruf: 28.Feb.2016: Update CiPl received = true
                    //var ciPlMaster = _entities.ci_pl_master.Find(grnModel.GrnMasterData.purchase_order_master_id);
                    var ciPlMaster = _entities.ci_pl_master.FirstOrDefault(c => c.purchase_order_master_id == grnModel.GrnMasterData.purchase_order_master_id);
                    ciPlMaster.is_received = true;
                    _entities.SaveChanges();

                    long grnMasterId = grnMaster.grn_master_id;
                    foreach (var item in grnDetailsList)
                    {
                        var grnDetails = new grn_details
                        {
                            product_id = item.product_id,
                            grn_master_id = grnMasterId,
                            unit_id = item.unit_id,
                            unit_price = item.unit_price,
                            pi_quantity = item.pi_quantity,
                            amount = item.amount,
                            brand_id = item.brand_id,
                            color_id = item.color_id,
                            product_version_id = item.product_version_id,
                            receive_quantity = item.receive_quantity,
                            vat_amount = item.vat_amount,
                            tax_amount = item.tax_amount,
                            line_total = item.line_total,
                            size_id = item.size_id,
                            vat_pcnt = item.vat_pcnt,
                            tax_pcnt = item.tax_pcnt
                        };

                        _entities.grn_details.Add(grnDetails);
                        // _entities.SaveChanges();
                        // kiron 04.09.2016
                        long saved = _entities.SaveChanges();
                        if (saved > 0)
                        {
                            // update inventory
                            InventoryRepository updateInventoty = new InventoryRepository();

                            // Maruf: 04.Apr.2017: move product from purchase(29) WH to GRN(4) WH; it will move to central after MRR process
                            updateInventoty.UpdateInventory("GRN", grnMaster.grn_no, 29,
                                4, grnDetails.product_id ?? 0, grnDetails.color_id ?? 0,grnDetails.product_version_id??0, grnDetails.unit_id ?? 0,
                                grnDetails.receive_quantity ?? 0, grnMaster.created_by ?? 0);
                        }
                        long purchaseOrderDetailsId = item.purchase_order_details_id;
                        purchase_order_details poDetails = _entities.purchase_order_details.Find(purchaseOrderDetailsId);
                        poDetails.receive_qty += item.receive_quantity;
                        _entities.SaveChanges();
                    }
                     
                    foreach (var item in receiveSerialNoDetails)
                    {
                        int c = 0;
                        var receiveSerialNoDetailsData = new receive_serial_no_details
                        {
                            product_id = item.product_id,
                            grn_master_id = grnMasterId,
                            brand_id = item.brand_id,
                            color_id = item.color_id,
                            product_version_id =item.product_version_id, 
                            imei_no = item.imei_no,
                            imei_no2 = item.imei_no2,
                            received_warehouse_id = 4, // 4-GRN WH; will be moved to central after MRR
                            current_warehouse_id = 4, // 4-GRN WH; will be moved to central after MRR
                            received_date = item.received_date,
                            requisition_id = 0,
                            deliver_master_id = 0,
                            sales_status = false,
                            price_protected = false,
                            carton_no = item.carton_no

                        };
                        _entities.receive_serial_no_details.Add(receiveSerialNoDetailsData);
                        c++;
                        if (c == 100)
                        {
                            c = 0;
                            _entities.SaveChanges();
                        }
                    }
                    _entities.SaveChanges();
                    return 1;
                }

               
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public GrnModel GetGrnById(long grn_master_id)
        {
            throw new NotImplementedException();
        }

        public bool EditGrn(GrnModel grnModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGrn(long grn_master_id)
        {
            throw new NotImplementedException();
        }

        public Confirmation GetModelIdsByNames(List<string> models)
        {
            var modelIdNames = new List<ModelIdName>();
            foreach (var model in models)
            {
                var myProduct = _entities.products.FirstOrDefault(p => p.product_name == model);
                if (myProduct == null)
                {
                    return new Confirmation { output = "error", msg = "Model '" + model + "' not exists in Inventory. First add the model in product entry section." };
                }
                else
                {
                    var modelIdName = new ModelIdName
                    {
                        productId = myProduct.product_id,
                        productName = myProduct.product_name
                    };
                    modelIdNames.Add(modelIdName);
                }
            }
            return new Confirmation { output = "success", msg = "All models available.", returnvalue = modelIdNames };
        }

        public Confirmation GetColorIdsByNames(List<string> colors)
        {
            var colorIdNames = new List<ColorIdName>();
            foreach (var color in colors)
            {
                var myColor = _entities.colors.FirstOrDefault(c => c.color_name == color);
                if (myColor == null)
                {
                    return new Confirmation { output = "error", msg = "Color '" + color + "' not exists in System. First add the color in color entry section." };
                }
                else
                {
                    var colorIdName = new ColorIdName
                    {
                        colorId = myColor.color_id,
                        colorName = myColor.color_name
                    };
                    colorIdNames.Add(colorIdName);
                }
            }
            return new Confirmation { output = "success", msg = "All colors are available.", returnvalue = colorIdNames };

        }


        public object GetProductInformation(long imei_no)
        {
            var data = (from rsnd in _entities.receive_serial_no_details
                        join pro in _entities.products on rsnd.product_id equals pro.product_id
                        join col in _entities.colors on rsnd.color_id equals col.color_id
                        where
                            (rsnd.party_id == null || rsnd.party_id == 0) && rsnd.imei_no == imei_no &&
                            rsnd.sales_status == false && rsnd.received_warehouse_id == 1
                        select new
                        {
                            receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                            product_id = rsnd.product_id,
                            product_name = pro.product_name,
                            color_id = rsnd.color_id,
                            color_name = col.color_name,
                            imei_no = rsnd.imei_no,
                            price = pro.mrp_price,
                        }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).ToList();
            return data;
        }
    }

    class ModelIdName
    {
        public long productId { get; set; }
        public string productName { get; set; }
    }

    class ColorIdName
    {
        public long colorId { get; set; }
        public string colorName { get; set; }
    }

    
}