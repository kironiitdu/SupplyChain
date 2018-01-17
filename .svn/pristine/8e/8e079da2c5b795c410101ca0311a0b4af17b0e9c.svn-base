using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DMSApi.Models.crystal_models;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class ReturnRepository:IReturnRepository
    {
        private DMSEntities _entities;
        public ReturnRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<return_type> GetReturnType()
        {
            var returnType = _entities.return_type.ToList();

            return returnType;
        }

        //public object GetIMEIInformation(string imei_no, int user_id)
        public object GetIMEIInformation(long imei_no, long party_id)
        {
        //    try
        //    {
        //        //var userParty = _entities.users.FirstOrDefault(w => w.user_id == user_id);
        //        //int p_id = 0;
        //        string p_prefix = string.Empty;
        //        //p_id = (int) userParty.party_id;

        //        var userPartyType = (from p in _entities.parties
        //            join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
        //                             where (p.party_id == party_id)
        //            select new
        //            {
        //                party_prefix = pt.party_prefix

        //            }).FirstOrDefault();

        //        p_prefix = userPartyType.party_prefix;

        //        //check already returned but not receive(27.11.2016)
        //        var aaa = _entities.return_details.Where(w => w.imei_no == imei_no).FirstOrDefault();
        //        int bbb = 0;
        //        if (aaa != null)
        //        {
        //             bbb = (int)aaa.returned_qty;
        //        }
        //        else
        //        {
        //             bbb = 0;
        //        }


        //        if (bbb == 0)
        //        {
        //            //For MD and DBIS return
        //            if (p_prefix == "DEL" || p_prefix == "B2B")
        //            {
        //                var imeiInfo = (from rs in _entities.receive_serial_no_details
        //                    join pro in _entities.products on rs.product_id equals pro.product_id
        //                    join c in _entities.colors on rs.color_id equals c.color_id
        //                    join v in _entities.product_version on rs.product_version_id equals v.product_version_id
        //                    join b in _entities.brands on rs.brand_id equals b.brand_id
        //                    //join u in _entities.units on pro.unit_id equals u.unit_id
        //                    join p in _entities.parties on rs.party_id equals p.party_id
        //                    join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
        //                    join w in _entities.warehouses on rs.current_warehouse_id equals w.warehouse_id
        //                    join rd in _entities.requisition_details on rs.requisition_id equals rd.requisition_master_id
        //                    join inv in _entities.invoice_master on rd.requisition_master_id equals inv.requisition_master_id
        //                    where
        //                        (rs.imei_no == imei_no || rs.imei_no2==imei_no && rd.price > 0 && rs.party_id == party_id &&
        //                         rs.sales_status == false)

        //                    select new
        //                    {
        //                        receive_serial_no_details_id = rs.receive_serial_no_details_id,
        //                        product_id = rs.product_id,
        //                        product_name = pro.product_name,
        //                        color_id = rs.color_id,
        //                        color_name = c.color_name,
        //                        product_version_id = rs.product_version_id,
        //                        product_version_name=v.product_version_name,
        //                        brand_id = rs.brand_id,
        //                        brand_name = b.brand_name,
        //                        unit_id = pro.unit_id,
        //                        //unit_name = u.unit_name,
        //                        imei_no = rs.imei_no,
        //                        imei_no2 = rs.imei_no2,
        //                        price = rd.price,
        //                        party_id = p.party_id,
        //                        party_name = p.party_name,
        //                        party_type_id = pt.party_type_id,
        //                        party_prefix = pt.party_prefix,
        //                        current_warehouse_id = rs.current_warehouse_id,
        //                        warehouse_name = w.warehouse_name,
        //                        invoice_no = inv.invoice_no,
        //                        invoice_date = inv.invoice_date,
        //                        invoice_master_id = inv.invoice_master_id

        //                    }).OrderByDescending(rs => rs.receive_serial_no_details_id).FirstOrDefault();


        //                return imeiInfo;
        //            }

        //            //For Retailer Return (receive_serial_no_details=p_id)
        //            else
        //            {
        //                var imeiInfo = (from rs in _entities.receive_serial_no_details
        //                                join pro in _entities.products on rs.product_id equals pro.product_id
        //                                join c in _entities.colors on rs.color_id equals c.color_id
        //                                join v in _entities.product_version on rs.product_version_id equals v.product_version_id
        //                                join b in _entities.brands on rs.brand_id equals b.brand_id
        //                                join u in _entities.units on pro.unit_id equals u.unit_id
        //                                join p in _entities.parties on rs.party_id equals p.party_id
        //                                join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
        //                                join w in _entities.warehouses on rs.current_warehouse_id equals w.warehouse_id
        //                                join rd in _entities.requisition_details on rs.requisition_id equals
        //                                    rd.requisition_master_id
        //                                join inv in _entities.invoice_master on rd.requisition_master_id equals
        //                                    inv.requisition_master_id
        //                                where
        //                                    (rs.imei_no == imei_no && rd.price > 0 && rs.sales_status == false &&
        //                                     rs.is_return != true &&
        //                                     rs.retailer_id == party_id)
        //                                select new
        //                                {
        //                                    receive_serial_no_details_id = rs.receive_serial_no_details_id,
        //                                    product_id = rs.product_id,
        //                                    product_name = pro.product_name,
        //                                    color_id = rs.color_id,
        //                                    color_name = c.color_name,
        //                                    product_version_id = rs.product_version_id,
        //                                    product_version_name = v.product_version_name,
        //                                    brand_id = rs.brand_id,
        //                                    brand_name = b.brand_name,
        //                                    unit_id = pro.unit_id,
        //                                    unit_name = u.unit_name,
        //                                    imei_no = rs.imei_no,
        //                                    price = rd.price,
        //                                    party_id = p.party_id,
        //                                    party_name = p.party_name,
        //                                    party_tGetIMEIInformationype_id = pt.party_type_id,
        //                                    party_prefix = pt.party_prefix,
        //                                    current_warehouse_id = rs.current_warehouse_id,
        //                                    warehouse_name = w.warehouse_name,

        //                                    invoice_no = inv.invoice_no,
        //                                    invoice_date = inv.invoice_date,
        //                                    invoice_master_id = inv.invoice_master_id

        //                                }).OrderByDescending(rs => rs.receive_serial_no_details_id).FirstOrDefault();

        //                return imeiInfo;
        //            }

        //        }
        //        else
        //        {
        //            var imeiInfo = "";
        //            return imeiInfo;
        //        }
        //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
            return 0;
        }

        public int AddReturn(ReturnModel ReturnModel)
        {
            return 0;
            //try
            //{
            //    var ReturnMaster = ReturnModel.ReturnMasterData;
            //    var ReturnDetails = ReturnModel.ReturnDetailsList;

            //    int partyid = 0;
            //    int partytypeid = 0;
            //    string partyprefix = String.Empty;

            //    var partyInfo= ReturnDetails.FirstOrDefault();
            //    partyid = (int)partyInfo.party_id;
            //    partytypeid = (int)partyInfo.party_type_id;
            //    partyprefix = partyInfo.party_prefix;

            //    // generate return code
            //    int ReturnSerial = _entities.return_master.Max(rtm => (int?)rtm.return_master_id) ?? 0;

            //    if (ReturnSerial != 0)
            //    {
            //        ReturnSerial++;

            //    }
            //    else
            //    {
            //        ReturnSerial++;
            //    }
            //    var rtmStr = ReturnSerial.ToString().PadLeft(7, '0');
            //    string returncode = "RET-" + rtmStr;

            //    ReturnMaster.return_code = returncode;
            //    ReturnMaster.return_date = DateTime.Now;
            //    ReturnMaster.return_type = ReturnMaster.return_type;
            //    ReturnMaster.doa_varify_status = "Not Verified";
            //    ReturnMaster.doa_receive_status = "Not Received";
            //    ReturnMaster.party_id = partyid;

            //    _entities.return_master.Add(ReturnMaster);
            //    _entities.SaveChanges();

            //    long ReturnMasterId = ReturnMaster.return_master_id;

            //    //insert data in return_details

            //    foreach (var itm in ReturnDetails)
            //    {
            //        var returndetails = new return_details
            //        {
            //            return_master_id = ReturnMasterId,
            //            product_id = itm.product_id,
            //            brand_id = itm.brand_id,
            //            color_id = itm.color_id,
            //            product_version_id= itm.product_version_id,
            //            unit_id = itm.unit_id,
            //            price = itm.price,
            //            imei_no = itm.imei_no,
            //            imei_no2 = itm.imei_no2,
            //            item_status = "Returned",
            //            returned_qty = 1,
            //            verify_status = false,
            //            warehouse_id = itm.warehouse_id,
            //            invoice_no = itm.invoice_no,
            //            invoice_date = itm.invoice_date,
            //            invoice_master_id = itm.invoice_master_id
            //        };
            //        _entities.return_details.Add(returndetails);
            //        _entities.SaveChanges();
            //    }

            //    //update return_master by invoice_master_id
            //    var invMasterIdFromReturnDetails = _entities.return_details.Where(w => w.return_master_id==ReturnModel.ReturnMasterData.return_master_id).FirstOrDefault();
            //    long invMasterId = 0;
            //    invMasterId = (long)invMasterIdFromReturnDetails.invoice_master_id;

            //    var InvoiceMasterId = _entities.return_master.Find(ReturnModel.ReturnMasterData.return_master_id);
            //    InvoiceMasterId.invoice_master_id = invMasterId;
            //    _entities.SaveChanges();

            //    //02.03.2017
            //    foreach (var rtd in ReturnDetails)
            //    {
            //        receive_serial_no_details rcvDetails =
            //            _entities.receive_serial_no_details.Where(w => w.imei_no == rtd.imei_no || w.imei_no2==rtd.imei_no2).FirstOrDefault();

            //        if (rcvDetails != null)
            //        {
            //            rcvDetails.current_warehouse_id = 39;//39 is in transit, when aamra warehouse receive the imei then intransit will change into we warehouse id
            //            _entities.SaveChanges();
            //        }

            //    }

            //    return 1;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


        }

        public int PostReplace(ReturnModel ReturnModel)
        {
            //try
            //{
            //    var ReplaceMaster = ReturnModel.ReturnMasterData;
            //    var ReturnDetails = ReturnModel.ReturnDetailsList;


            //    foreach (var itm in ReturnDetails)
            //    {
            //        //update return_details table
            //        var returnDetails = _entities.return_details.Where(w=>w.imei_no == itm.imei_no).FirstOrDefault();
            //        long returnDetailsId=0;
            //        returnDetailsId = returnDetails.return_details_id;

            //        return_details rdetails = _entities.return_details.Find(returnDetailsId);
            //        rdetails.item_status = "Replaced";
            //        rdetails.replaced_by = ReplaceMaster.returned_by;//replaced by
            //        rdetails.replace_date = DateTime.Now;
            //        rdetails.replaced_imei_no = itm.replaced_imei_no;

            //        //update receive_serial_no tables
            //        long returnMasterId = 0;
            //        long partyId = 0;
            //        long warehouseId = 0;


            //        var rmasterid = _entities.return_details.Where(w => w.imei_no == itm.imei_no).FirstOrDefault();
            //        returnMasterId = (long)rmasterid.return_master_id;
            //        warehouseId = (long)rmasterid.warehouse_id;

            //        var partyid = _entities.return_master.Where(w => w.return_master_id == returnMasterId).FirstOrDefault();
            //        //partyId = (long)partyid.md_dbis_id;//comments on 28.02.2017(need later)

            //        var receiveSerialNoDetails = _entities.receive_serial_no_details.Where(w => w.imei_no == itm.replaced_imei_no).FirstOrDefault();
            //        long warehouseIdBfoReplace = 0;
            //        warehouseIdBfoReplace = (long)receiveSerialNoDetails.current_warehouse_id;

            //        long receiveSerialNoId = 0;
            //        receiveSerialNoId = (long)receiveSerialNoDetails.receive_serial_no_details_id;

            //        receive_serial_no_details rsn = _entities.receive_serial_no_details.Find(receiveSerialNoId);
            //        rsn.replaced_status = true;
            //        rsn.party_id = partyId;
            //        rsn.deliver_date = DateTime.Now;
            //        rsn.current_warehouse_id = warehouseId;


            //        _entities.SaveChanges();

            //        //update inventory
            //        long to_warehouse_id = 0;
            //        long from_warehouse_id = 0;

            //        var toWarehouse = _entities.return_details.Where(w => w.return_details_id == returnDetailsId).FirstOrDefault();
            //        to_warehouse_id = (long)toWarehouse.warehouse_id;
                   
            //        //28.02.2017(need to use this portion)

            //        InventoryRepository updInventoty = new InventoryRepository();
            //        updInventoty.UpdateInventory("REPLACE", "", warehouseIdBfoReplace, to_warehouse_id, itm.product_id ?? 0,
            //            itm.color_id ?? 0, itm.product_version_id ?? 0, itm.unit_id ?? 0, 1, ReplaceMaster.returned_by ?? 0);
            //    }
                
            //    return 1;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            return 0;
        }

        public object ReturnInvoiceReportById(long return_master_id)
        {
            try
            {
                string query =
                    "select party_type.party_prefix, party_type.party_type_name, party.party_code, party.party_name, party.address, " +
                    //" (select province_name from province where province.province_id=party.province_id) as province, " +
                    //" (select city_name from district_city where district_city.city_id= party.city_id) as city, " +
                    " party.proprietor_name, party.phone, party.mobile, party.email, "+
                    " (select region_name from region where region_id=party.region_id) as region_name, "+
                    " (select area_name from area where area_id=party.area_id) as area_name, "+
                    " (select territory_name from territory where territory_id=party.territory_id) as territory_name, "+
                    " return_master.return_code, return_master.return_date, " +
                    " return_details.imei_no, return_details.imei_no2, product.product_name, color.color_name,product_version.product_version_name, unit.unit_name, return_details.price, return_details.invoice_no, return_details.invoice_date " +
                    " from return_master " +
                    " inner join return_details on return_master.return_master_id=return_details.return_master_id " +
                    " inner join product on return_details.product_id=product.product_id " +
                    " inner join party on return_master.party_id=party.party_id " +
                    " inner join party_type on party.party_type_id=party_type.party_type_id " +
                    " inner join color on return_details.color_id=color.color_id " +
                    " inner join product_version on return_details.product_version_id =product_version.product_version_id " +
                    " inner join unit on return_details.unit_id=unit.unit_id " +
                    " where return_master.return_master_id="+return_master_id+" ";

                var reData = _entities.Database.SqlQuery<ReturnReportModel>(query).ToList();
                return reData;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //to load verified return for receive list
        public object GetAllReturnForReceive()
        {
            try
            {
                var returns = (from rm in _entities.return_master
                               join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                               join p in _entities.parties on rm.party_id equals p.party_id
                               where (rm.doa_varify_status=="Verified" && rm.doa_receive_status=="Not Received")
                               select new
                               {
                                   return_master_id = rm.return_master_id,
                                   return_code = rm.return_code,
                                   party_name = p.party_name,
                                   return_type_name = rt.return_type_name,
                                   return_date = rm.return_date,
                                   doa_varify_status = rm.doa_varify_status,
                                   doa_receive_status = rm.doa_receive_status

                               }).OrderByDescending(r => r.return_master_id).ToList();

                return returns;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object GetAllReturnForVerify()
        {
            try
            {
                var returns = (from rm in _entities.return_master
                               join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                               join p in _entities.parties on rm.party_id equals p.party_id
                               where (rm.doa_varify_status == "Not Verified")
                               select new
                               {
                                   return_master_id = rm.return_master_id,
                                   return_code = rm.return_code,
                                   party_name = p.party_name,
                                   return_type_name = rt.return_type_name,
                                   return_date = rm.return_date,
                                   doa_varify_status = rm.doa_varify_status,
                                   doa_receive_status = rm.doa_receive_status

                               }).OrderByDescending(r => r.return_master_id).ToList();

                return returns;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //load dealer, dealer demo, b2b return for verify list
        public object GetAllReturn()
        {
            try
            {
                var returns = (from rm in _entities.return_master
                    join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                    join p in _entities.parties on rm.party_id equals p.party_id
                    select new
                    {
                        return_master_id = rm.return_master_id,
                        return_code=rm.return_code,
                        party_name = p.party_name,
                        return_type_name = rt.return_type_name,
                        return_date = rm.return_date,
                        doa_varify_status = rm.doa_varify_status,
                        doa_receive_status = rm.doa_receive_status

                    }).OrderByDescending(r=>r.return_master_id).ToList();
                
                return returns;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        //load Retailer all returned
        public object GetAllRetailerReturn()
        {
            try
            {
                //commets on 28.02.2017(need later)
                //var retailerReturns = (from rm in _entities.return_master
                //               join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                //               join p in _entities.parties on rm.retailer_id equals p.party_id
                //               select new
                //               {
                //                   return_master_id = rm.return_master_id,
                //                   return_code = rm.return_code,
                //                   party_name = p.party_name,
                //                   return_type_name = rt.return_type_name,
                //                   return_date = rm.return_date
                //               }).OrderByDescending(r => r.return_master_id).ToList();

                //return retailerReturns;
                return null;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        //imei information which is replaceing &&  never deliver to any md or dbis
        public object GetIMEIForReplace(string replaced_imei, string replacing_imei)
        {
            //try
            //{
            //    //get party type
            //    //comments on 28.02.2017(need later)
            //    //var ptype = (from rm in _entities.return_master
            //    //    join rd in _entities.return_details on rm.return_master_id equals rd.return_master_id
            //    //    join p in _entities.parties on rm.md_dbis_id equals p.party_id
            //    //    join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
            //    //    select new
            //    //    {
            //    //        party_prefix = pt.party_prefix
            //    //    }).FirstOrDefault();

            //    //string partyType = ptype.party_prefix;
            //    string partyType = "DEL";//28.02.2017//to get error free but no need 

            //    //get from which warehouse party(MD/DBIS) was delivered 
            //    //MD/DBIS receive from which warehouse, he will replaced from that warehouse
            //    var delivery_warehouse = (from dm in _entities.delivery_master
            //        join dd in _entities.delivery_details on dm.delivery_master_id equals dd.delivery_master_id
            //        join rs in _entities.receive_serial_no_details on dm.delivery_master_id equals rs.deliver_master_id
            //        where (rs.imei_no == replaced_imei)
            //        select new
            //        {
            //            from_warehouse_id = dm.from_warehouse_id
            //        }).FirstOrDefault();

            //    int deliveryWarehouse = 0;
            //    deliveryWarehouse = (int)delivery_warehouse.from_warehouse_id;


            //    if (partyType == "MD")//MD price
            //    {
            //        var imeiinfo_for_replace = (from rs in _entities.receive_serial_no_details
            //            join pro in _entities.products on rs.product_id equals pro.product_id
            //            join c in _entities.colors on rs.color_id equals c.color_id
            //            join b in _entities.brands on rs.brand_id equals b.brand_id
            //            join u in _entities.units on pro.unit_id equals u.unit_id
            //            where
            //                (rs.imei_no == replacing_imei && (rs.party_id == 0 || rs.party_id == null) &&
            //                 rs.current_warehouse_id == deliveryWarehouse)
            //            //&& (rs.is_return==false || rs.is_return==null)
            //            select new
            //            {
            //                receive_serial_no_details_id = rs.receive_serial_no_details_id,
            //                product_id = rs.product_id,
            //                product_name = pro.product_name,
            //                color_id = rs.color_id,
            //                color_name = c.color_name,
            //                brand_id = rs.brand_id,
            //                brand_name = b.brand_name,
            //                unit_id = pro.unit_id,
            //                unit_name = u.unit_name,
            //                imei_no = rs.imei_no,
            //                price = pro.md_price

            //            }).FirstOrDefault();//.ToList();

            //    return imeiinfo_for_replace;
            //    }
            //    else//(DBIS price)
            //    {
            //        var imeiinfo_for_replace = (from rs in _entities.receive_serial_no_details
            //            join pro in _entities.products on rs.product_id equals pro.product_id
            //            join c in _entities.colors on rs.color_id equals c.color_id
            //            join b in _entities.brands on rs.brand_id equals b.brand_id
            //            join u in _entities.units on pro.unit_id equals u.unit_id
            //            where
            //                (rs.imei_no == replacing_imei && (rs.party_id == 0 || rs.party_id == null) &&
            //                 rs.current_warehouse_id == deliveryWarehouse)
            //            select new
            //            {
            //                receive_serial_no_details_id = rs.receive_serial_no_details_id,
            //                product_id = rs.product_id,
            //                product_name = pro.product_name,
            //                color_id = rs.color_id,
            //                color_name = c.color_name,
            //                brand_id = rs.brand_id,
            //                brand_name = b.brand_name,
            //                unit_id = pro.unit_id,
            //                unit_name = u.unit_name,
            //                imei_no = rs.imei_no,
            //                price = pro.bs_price

            //            }).FirstOrDefault();//.ToList();

            //        return imeiinfo_for_replace;

            //    }

            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
            return 0;

        }

        

        //receiving the verified returned imei
        public bool ReceivingVerifiedIMEI(long return_details_id)
        {
            try
            {
                return_details rdetails = _entities.return_details.Find(return_details_id);
                rdetails.verify_status = true;
                rdetails.verify_date=DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public ReturnModel GetReturnById(long return_master_id)
        {
            try
            {
                ReturnModel returnModel = new ReturnModel();
                returnModel.ReturnMasterData = _entities.return_master.Find(return_master_id);

                //////////////////////04.03.2017
                var party = _entities.requisition_master.Find(return_master_id);
                Int64 partyId = 0;
                if (party != null)
                {
                    partyId = (long)party.party_id;
                }
                party ppp = _entities.parties.Find(partyId);

                //var returnMaster = _entities.return_master.Find(return_master_id);
               // var returnMaster = _entities.return_master.Where(w => w.return_master_id == return_master_id).ToList();


                //foreach (var itm in returnMaster)
                //{
                //    returnModel.ReturnMasterData.party_id = itm.party_id??0;
                //}

                //returnModel.ReturnMasterData.return_master_id = returnMaster.return_master_id;
                //returnModel.ReturnMasterData.party_id = returnMaster.party_id;
                //returnModel.ReturnMasterData.created_by = returnMaster.created_by;
                //returnModel.ReturnMasterData.created_date = returnMaster.created_date;

                //returnModel.ReturnMasterData.doa_receive_status = returnMaster.doa_receive_status;
                //returnModel.ReturnMasterData.doa_receive_date = returnMaster.doa_receive_date;
                //returnModel.ReturnMasterData.doa_received_by = returnMaster.doa_received_by;

                //returnModel.ReturnMasterData.doa_verified_date = returnMaster.doa_verified_date;
                //returnModel.ReturnMasterData.doa_varify_status = returnMaster.doa_varify_status;
                //returnModel.ReturnMasterData.doa_verified_by = returnMaster.doa_verified_by;

                //returnModel.ReturnMasterData.doa_warehouse_id = returnMaster.doa_warehouse_id;

                //returnModel.ReturnMasterData.invoice_master_id = returnMaster.invoice_master_id;
                //returnModel.ReturnMasterData.is_active = returnMaster.is_active;
                //returnModel.ReturnMasterData.is_deleted = returnMaster.is_deleted;
                //returnModel.ReturnMasterData.remarks = returnMaster.remarks;

                //returnModel.ReturnMasterData.updated_date = returnMaster.updated_date;
                //returnModel.ReturnMasterData.updated_by = returnMaster.updated_by;


                //returnModel.ReturnMasterData.region_id = ppp.region_id ?? 0;
                //returnModel.ReturnMasterData.area_id = ppp.area_id ?? 0;
                //returnModel.ReturnMasterData.area_id = ppp.area_id ?? 0;
                //returnModel.ReturnMasterData.party_type_id = ppp.party_type_id ?? 0;
               
                
                
                ///////////////////////04.03.2017


                var returns = (from rm in _entities.return_master
                               join rd in _entities.return_details on rm.return_master_id equals rd.return_master_id
                               join pro in _entities.products on rd.product_id equals pro.product_id
                               join c in _entities.colors on rd.color_id equals c.color_id
                               //join v in _entities.product_version on rd.product_version_id equals v.product_version_id
                               //in u in _entities.units on rd.unit_id equals u.unit_id
                               join b in _entities.brands on rd.brand_id equals b.brand_id
                               join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                               join p in _entities.parties on rm.party_id equals p.party_id
                               join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
                               join v in _entities.product_version on rd.product_version_id equals v.product_version_id
                               where (rm.return_master_id == return_master_id)//10.11.2016
                               select new ReturnDetailsModel()
                               {
                                   return_master_id = rm.return_master_id,
                                   return_details_id = rd.return_details_id,
                                   product_id = rd.product_id,
                                   product_name = pro.product_name,
                                   party_id = rm.party_id,
                                   party_name = p.party_name,
                                   party_type_id = p.party_type_id,
                                   party_prefix = pt.party_prefix,
                                   imei_no = rd.imei_no,
                                   imei_no2 = rd.imei_no2,
                                   return_type_name = rt.return_type_name,
                                   brand_id = rd.brand_id,
                                   brand_name = b.brand_name,
                                   color_id = rd.color_id,
                                   color_name = c.color_name,
                                   product_version_id = rd.product_version_id,
                                   product_version_name = v.product_version_name,
                                   unit_id = rd.unit_id,
                                   //unit_name = u.unit_name,
                                   price = rd.price,

                                   //15.11.2016
                                   invoice_no = rd.invoice_no,
                                   invoice_date = rd.invoice_date
                                   
                               }).OrderBy(t => t.return_details_id).ToList();

                returnModel.ReturnDetailsList = returns;

                return returnModel;

            }
            catch (Exception)
            {
                
                throw;
            }

        }


        //verify by doa engineer
        public int VerifyReturn(ReturnModel ReturnModel)
        {
            try
            {
                var ReturnMaster = ReturnModel.ReturnMasterData;
                var ReturnDetails = ReturnModel.ReturnDetailsList;

                int ReturnDetailsId=0;

                //verify status update in return_master table
                return_master rtm = _entities.return_master.Find(ReturnMaster.return_master_id);
                rtm.doa_varify_status = "Verified";
                rtm.doa_verified_date = DateTime.Now;
                rtm.doa_verified_by = ReturnMaster.doa_verified_by;
                _entities.SaveChanges();

                //verify each imei in return_details table
                foreach (var itm in ReturnDetails)
                {
                    var returndetails = new return_details
                    {
                        return_details_id = itm.return_details_id
                    };

                    return_details abc = _entities.return_details.Find(itm.return_details_id);
                    abc.verify_status = true;
                    abc.verify_date = DateTime.Now;
                    _entities.SaveChanges();

                }

                return 1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public int VerifybyDoaEngineer(ReturnModel ReturnModel)
        {
            try
            {
                var ReturnMaster =ReturnModel.ReturnMasterData;
                var ReturnDetails = ReturnModel.ReturnDetailsList;

                return 1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //load return receive page
        public ReturnModel GetVerifiedReturnById(long return_master_id)
        {
            try
            {
                ReturnModel returnModel=new ReturnModel();
               
                returnModel.ReturnMasterData = _entities.return_master.Find(return_master_id);

                var verifiedReturns = (from rm in _entities.return_master
                               join rd in _entities.return_details on rm.return_master_id equals rd.return_master_id
                               join pro in _entities.products on rd.product_id equals pro.product_id
                               join c in _entities.colors on rd.color_id equals c.color_id
                               join v in _entities.product_version on rd.product_version_id equals v.product_version_id
                               join u in _entities.units on rd.unit_id equals u.unit_id
                               join b in _entities.brands on rd.brand_id equals b.brand_id
                               join rt in _entities.return_type on rm.return_type equals rt.return_type_id
                               join p in _entities.parties on rm.party_id equals p.party_id
                               join pt in _entities.party_type on p.party_type_id equals pt.party_type_id
                                       where (rd.verify_status == true && rm.doa_varify_status == "Verified" && rm.return_master_id == return_master_id)
                               select new ReturnDetailsModel()
                               {
                                   return_master_id = rm.return_master_id,
                                   return_details_id = rd.return_details_id,
                                   //return_code = rm.return_code,
                                   product_id = rd.product_id,
                                   product_name = pro.product_name,
                                   party_id = rm.party_id,
                                   party_name = p.party_name,
                                   party_type_id = p.party_type_id,
                                   party_prefix = pt.party_prefix,
                                   imei_no = rd.imei_no,
                                   imei_no2=rd.imei_no2,
                                   return_type_name = rt.return_type_name,
                                   //return_date = rm.return_date,
                                   brand_id = rd.brand_id,
                                   brand_name = b.brand_name,
                                   color_id = rd.color_id,
                                   color_name = c.color_name,
                                   product_version_id = rd.product_version_id,
                                   product_version_name = v.product_version_name,
                                   unit_id = rd.unit_id,
                                   unit_name = u.unit_name,
                                   price = rd.price,
                                 
                                   //15.11.2016
                                   invoice_no = rd.invoice_no,
                                   invoice_date = rd.invoice_date,
                                   invoice_master_id = rd.invoice_master_id
                               }).OrderBy(t => t.return_details_id).ToList();

                returnModel.ReturnDetailsList = verifiedReturns;

                return returnModel;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //receive verified imei
        public int ReceiveReturn(ReturnModel ReturnModel)
        {
            //try
            //{
            //    var ReturnMaster = ReturnModel.ReturnMasterData;
            //    var ReturnDetails = ReturnModel.ReturnDetailsList;

            //    string ReturnCode = ReturnMaster.return_code;
            //    //receiving in return_master table
            //    return_master rtm = _entities.return_master.Find(ReturnMaster.return_master_id);
            //    rtm.doa_receive_status = "Received";
            //    rtm.doa_receive_date = DateTime.Now;
            //    rtm.doa_received_by = ReturnMaster.doa_received_by;

            //    //receiving each verified imei in return_details table
            //    int counter = 0;
            //    foreach (var itm in ReturnDetails)
            //    {
            //        var returndetails = new return_details
            //        {
            //            return_details_id = itm.return_details_id
            //        };

            //        return_details rtd = _entities.return_details.Find(itm.return_details_id);
            //        rtd.doa_receive_date = DateTime.Now;
            //        rtd.doa_received_by = ReturnMaster.doa_received_by;
            //        rtd.item_status = "Received";

            //        //before update receive_serial_no_details keep the row to receive_serial_no_details table

            //        receive_serial_no_details serialtablebeforeUpdate = _entities.receive_serial_no_details.Where(r => r.imei_no == itm.imei_no).ToList().FirstOrDefault();
                    
            //        /////////////////////comments on 28.02.2017(need to use)////////////////////////////
            //        //receive_serial_no_details_log Update = new receive_serial_no_details_log
            //        //{
            //        //    receive_serial_no_details_id = serialtablebeforeUpdate.receive_serial_no_details_id,
            //        //    product_id = serialtablebeforeUpdate.product_id,
            //        //    brand_id = serialtablebeforeUpdate.brand_id,
            //        //    color_id = serialtablebeforeUpdate.color_id,
            //        //    imei_no = serialtablebeforeUpdate.imei_no,
            //        //    received_warehouse_id = serialtablebeforeUpdate.received_warehouse_id,
            //        //    received_date = serialtablebeforeUpdate.received_date,
            //        //    party_id = serialtablebeforeUpdate.party_id,
            //        //    deliver_date = serialtablebeforeUpdate.deliver_date,
            //        //    requisition_id = serialtablebeforeUpdate.requisition_id,
            //        //    grn_master_id = serialtablebeforeUpdate.grn_master_id,
            //        //    deliver_master_id = serialtablebeforeUpdate.deliver_master_id,
            //        //    sales_status = serialtablebeforeUpdate.sales_status,
            //        //    sales_date = serialtablebeforeUpdate.sales_date,
            //        //    is_gift = serialtablebeforeUpdate.is_gift,
            //        //    price_protected = serialtablebeforeUpdate.price_protected,
            //        //    retailer_id = serialtablebeforeUpdate.retailer_id,
            //        //    delivery_to_retailer_date = serialtablebeforeUpdate.delivery_to_retailer_date,
            //        //    is_return = serialtablebeforeUpdate.is_return,
            //        //    current_warehouse_id = serialtablebeforeUpdate.current_warehouse_id,
            //        //    replaced_status = serialtablebeforeUpdate.replaced_status
            //        //};
            //        //_entities.receive_serial_no_details_log.Add(Update);
            //        //_entities.SaveChanges();

            //        /////////////////////comments on 28.02.2017(need to use)////////////////////////////


            //        //update receive_serial_no_details table for Return Type: Defect and Good
            //        receive_serial_no_details serialtable = _entities.receive_serial_no_details.Where(r => r.imei_no == itm.imei_no).ToList().FirstOrDefault();
            //        serialtable.is_return = true;
            //        serialtable.party_id = 0;
            //        if (ReturnMaster.return_type == 2)
            //            serialtable.current_warehouse_id = 3;//3 is doa warehouse id if Return Type is Defect
            //        else
            //        {
            //            serialtable.current_warehouse_id = 1;// 1 is central warehouse id if Return is Good
            //        }
            //        counter++;

            //        _entities.SaveChanges();

            //        //update inventory
            //        int fromWarehouseId = 0;
            //        int toWarehouseId = 0;

            //        var warehouse = (from rd in _entities.return_details
            //                         join rs in _entities.receive_serial_no_details on rd.imei_no equals rs.imei_no
            //                         where (rd.return_details_id == itm.return_details_id)
            //                         select new
            //                         {
            //                             from_warehouse_id = rd.warehouse_id,
            //                             to_warehouse_id = rs.current_warehouse_id

            //                         }).FirstOrDefault();


            //        fromWarehouseId = (int)warehouse.from_warehouse_id;
            //        toWarehouseId = (int)warehouse.to_warehouse_id;

                    
            //        //insert inventory
            //        ///////////////////////comments on 28.02.2017(need to use this)
            //        InventoryRepository updInventoty = new InventoryRepository();
            //        updInventoty.UpdateInventory("RETURN", ReturnCode, fromWarehouseId, toWarehouseId, itm.product_id ?? 0,
            //            itm.color_id ?? 0,itm.product_version_id ?? 0, itm.unit_id ?? 0, 1, ReturnMaster.doa_received_by ?? 0);
            //        ///////////////////////comments on 28.02.2017(need to use this)
            //    }

            //    //insert party_journal table for good return only(27.11.2016)
            //    if (rtm.return_type == 1)//good return=1
            //    {
            //        //insert invoice_log table returned_qty && returned_amount for good product
            //        decimal totalReturnedAmount = _entities.return_details.Where(w => w.return_master_id == rtm.return_master_id).Sum(s => s.price) ?? 0;
            //        int totalReturnedQty = _entities.return_details.Where(w => w.return_master_id == rtm.return_master_id).Sum(s => s.returned_qty) ?? 0;
            //        decimal totalReturnedAmtContra = -totalReturnedAmount;

            //        var inVoiceLog = _entities.invoice_log.Find(rtm.invoice_master_id);
            //        if (inVoiceLog != null)
            //        {
            //            inVoiceLog.returned_qty = totalReturnedQty;
            //            inVoiceLog.returned_amount = totalReturnedAmount;
            //            _entities.SaveChanges();
            //        }

            //        PartyJournalRepository updPartyJournal = new PartyJournalRepository();
            //        updPartyJournal.PartyJournalEntry("RETURN", rtm.party_id ?? 0, totalReturnedAmtContra, "good return", rtm.doa_received_by ?? 0, rtm.return_code);
            //    }

            //    return 1;
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
            return 0;
        }

       


        //public bool UpdateApproveStatus(int requisition_master_id)
        //{
        //    try
        //    {
        //        requisition_master reqMaster = _entities.requisition_master.Find(requisition_master_id);
        //        reqMaster.status = "Approved";
        //        _entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
    }
}