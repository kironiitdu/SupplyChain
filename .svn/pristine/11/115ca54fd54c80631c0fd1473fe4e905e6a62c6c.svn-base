using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ReceiveSerialNoDetailsRepository : IReceiveSerialNoDetailsRepository
    {
        private DMSEntities _entities;

        public ReceiveSerialNoDetailsRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetProductInformation(long imei_no, int party_id)
        {
            var data = (from rsnd in _entities.receive_serial_no_details
                        join pt in _entities.parties on rsnd.party_id equals pt.party_id
                        join pro in _entities.products on rsnd.product_id equals pro.product_id
                        join col in _entities.colors on rsnd.color_id equals col.color_id
                        where rsnd.imei_no == imei_no && rsnd.party_id == party_id && rsnd.sales_status == false
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

        public object GetProductInformationForCentral(long imei_no)
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

        public object GetProductInformationForDirectTransfer(long imei_no, int from_warehouse_id)
        {
            var imeiList = new List<object>();
            
            //if (imei_no.Contains(","))
            //{
            //    //split imei string based on comma---------------
            //    string[] array = imei_no.Split(',');
            //    foreach (var item in array)
            //    {
            //        if (item.Length == 15)
            //        {
            //            var data = (from rsnd in _entities.receive_serial_no_details
            //                        join pro in _entities.products on rsnd.product_id equals pro.product_id
            //                        join col in _entities.colors on rsnd.color_id equals col.color_id
            //                        join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
            //                        where
            //                            (rsnd.imei_no == item || rsnd.imei_no2 == item) && rsnd.sales_status == false &&
            //                            (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
            //                            rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
            //                        select new
            //                        {
            //                            receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
            //                            product_id = rsnd.product_id,
            //                            product_name = pro.product_name,
            //                            color_id = rsnd.color_id,
            //                            color_name = col.color_name,
            //                            product_version_id = rsnd.product_version_id,
            //                            product_version_name = pv.product_version_name,
            //                            imei_no = rsnd.imei_no,
            //                            imei_no2 = rsnd.imei_no2,
            //                            sales_status = rsnd.sales_status,
            //                            price = pro.mrp_price,
            //                        }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).FirstOrDefault();
            //            imeiList.Add(data);
            //        }
            //    }
            //    return imeiList;
            //}
            //else
            {
                var data = (from rsnd in _entities.receive_serial_no_details
                            join pro in _entities.products on rsnd.product_id equals pro.product_id
                            join col in _entities.colors on rsnd.color_id equals col.color_id
                            join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
                            where
                                (rsnd.imei_no == imei_no || rsnd.imei_no2 == imei_no) && rsnd.sales_status == false &&
                                (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
                                rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
                            select new
                            {
                                receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                                product_id = rsnd.product_id,
                                product_name = pro.product_name,
                                color_id = rsnd.color_id,
                                color_name = col.color_name,
                                product_version_id = rsnd.product_version_id,
                                product_version_name = pv.product_version_name,
                                imei_no = rsnd.imei_no,
                                imei_no2 = rsnd.imei_no2,
                                price = pro.mrp_price,
                                sales_status = rsnd.sales_status
                            }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).FirstOrDefault();
                imeiList.Add(data);

                return imeiList;
                //return data;
            }
        }

        public object GetProductInformationForScan(long imei_no, int from_warehouse_id)
        {
            List<object> imeiList = new List<object>();

            //check single imei or multiple imei by ","----------
            //if (imei_no.Contains(","))
            //{
            //    //split imei string based on comma---------------
            //    string[] array = imei_no.Split(',');

            //    //skip carton no from imei array-----------------
            //    //array = array.Skip(1).ToArray();


            //    foreach (var item in array)
            //    {
            //        if (item.Length == 15)
            //        {
            //            var data = (from rsnd in _entities.receive_serial_no_details
            //                        join pro in _entities.products on rsnd.product_id equals pro.product_id
            //                        join col in _entities.colors on rsnd.color_id equals col.color_id
            //                        join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
            //                        where
            //                            (rsnd.imei_no == item || rsnd.imei_no2 == item) && rsnd.sales_status == false &&
            //                            (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
            //                            rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
            //                        select new
            //                        {
            //                            receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
            //                            product_id = rsnd.product_id,
            //                            product_name = pro.product_name,
            //                            color_id = rsnd.color_id,
            //                            color_name = col.color_name,
            //                            product_version_id = rsnd.product_version_id,
            //                            product_version_name = pv.product_version_name,
            //                            imei_no = rsnd.imei_no,
            //                            imei_no2 = rsnd.imei_no2,
            //                            sales_status = rsnd.sales_status,
            //                            price = pro.mrp_price,
            //                        }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id);
            //            imeiList.Add(data);
            //        }
            //    }


            //    return imeiList;
            //}
            //else
            {
                var data = (from rsnd in _entities.receive_serial_no_details
                            join pro in _entities.products on rsnd.product_id equals pro.product_id
                            join col in _entities.colors on rsnd.color_id equals col.color_id
                            join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
                            where
                                (rsnd.imei_no == imei_no || rsnd.imei_no2 == imei_no) && rsnd.sales_status == false && 
                                (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
                                rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
                            select new
                            {
                                receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                                product_id = rsnd.product_id,
                                product_name = pro.product_name,
                                color_id = rsnd.color_id,
                                color_name = col.color_name,
                                product_version_id = rsnd.product_version_id,
                                product_version_name = pv.product_version_name,
                                imei_no = rsnd.imei_no,
                                imei_no2 = rsnd.imei_no2,
                                price = pro.mrp_price,
                                sales_status = rsnd.sales_status
                            }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).ToList();
                imeiList.Add(data);

                return imeiList;
            }
        }

        //public List<receive_serial_no_details> Getimeino()
        public object Getimeino()
        {
            try
            {
                //List<receive_serial_no_details> imeino = _entities.receive_serial_no_details.Where(w=>w.is_return==true && w.current_warehouse_id==3).ToList();
                //return imeino;
                //var imeino = (from rs in _entities.receive_serial_no_details
                //              join rtd in _entities.return_details on rs.imei_no equals rtd.imei_no
                //              where (rs.is_return == true && rtd.item_status != "Returned" && rtd.item_status != "Replaced")
                //              select new
                //              {
                //                  imei_no = rs.imei_no
                //              }).ToList();

                //return imeino;
                return 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public object GetImeiPartyNProductwise(int party_id, int product_id)
        {
            try
            {
                var imeiParty_Product_wise = (from rm in _entities.return_master
                                              join rd in _entities.return_details on rm.return_master_id equals rd.return_master_id

                                              where (rm.party_id == party_id && rd.product_id == product_id && rd.item_status != "Returned" && rd.item_status != "Replaced")


                                              //comments 28.02.2017(need later)
                                              //where (rm.md_dbis_id == party_id && rd.product_id == product_id && rd.item_status != "Returned" && rd.item_status != "Replaced")

                                              where (rm.party_id == party_id && rd.product_id == product_id && rd.item_status != "Returned" && rd.item_status != "Replaced")


                                              select new
                                              {
                                                  imei_no = rd.imei_no
                                              }).ToList();

                return imeiParty_Product_wise;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// MARUF:GET IMEI LIST BY CARTON NO
        /// DATE: 12.JUN.2017
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <param name="from_warehouse_id"></param>
        /// <returns></returns>
        public object GetImeiListByCartonNoFromWarehouse(string cartonNo, int from_warehouse_id)
        {
            try
            {
                var cartonTotal = _entities.receive_serial_no_details.Count(r => r.carton_no == cartonNo);
                var data = (from rsnd in _entities.receive_serial_no_details
                    join pro in _entities.products on rsnd.product_id equals pro.product_id
                    join col in _entities.colors on rsnd.color_id equals col.color_id
                    join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
                    where
                        (rsnd.carton_no == cartonNo) && rsnd.sales_status == false &&
                        (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
                        rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
                    select new
                    {
                        receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                        product_id = rsnd.product_id,
                        product_name = pro.product_name,
                        color_id = rsnd.color_id,
                        color_name = col.color_name,
                        product_version_id = rsnd.product_version_id,
                        product_version_name = pv.product_version_name,
                        imei_no = rsnd.imei_no,
                        imei_no2 = rsnd.imei_no2,
                        price = pro.mrp_price,
                        sales_status = rsnd.sales_status
                    }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).ToList();
                var returnValue = data.Cast<object>().ToList();
                var cartonTotalAvailable = returnValue.Count;
                if (cartonTotal < 10) // assuming min quantity for the smallest carton is 10
                {
                    return new Confirmation { output = "error", msg = "Not a valid Carton Number" };
                } 
                if (cartonTotalAvailable > 0)
                {
                    if (returnValue.Count == cartonTotal)
                    {
                        return new Confirmation { output = "success", msg = "success", returnvalue = returnValue };
                    }
                    else
                    {
                        return new Confirmation { output = "error", msg = "This carton has been broken / partially delivered." };
                    }
                }
                else
                {
                    return new Confirmation { output = "error", msg = "No IMEI available for the Carton Number "+cartonNo };
                }

            }
            catch (Exception)
            {
                return new Confirmation { output = "error", msg = "Exception occured while retrieving IMEI by carton no." };
            }
        }

        public object GetProductInfoByImeiFromWarehouse(long imei_no, int from_warehouse_id)
        {
            var imeiList = new List<object>();
            
            //if (imei_no.Contains(","))
            //{

            //    string[] array = imei_no.Split(',');
            //    string invalidImeis = "";
            //    foreach (var item in array)
            //    {
            //        var aItem = item.Trim();
            //        if (aItem.Length == 15)
            //        {
            //            var data = (from rsnd in _entities.receive_serial_no_details
            //                        join pro in _entities.products on rsnd.product_id equals pro.product_id
            //                        join col in _entities.colors on rsnd.color_id equals col.color_id
            //                        join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
            //                        where
            //                            (rsnd.imei_no == aItem || rsnd.imei_no2 == aItem) && rsnd.sales_status == false &&
            //                            (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
            //                            rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
            //                        select new
            //                        {
            //                            receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
            //                            product_id = rsnd.product_id,
            //                            product_name = pro.product_name,
            //                            color_id = rsnd.color_id,
            //                            color_name = col.color_name,
            //                            product_version_id = rsnd.product_version_id,
            //                            product_version_name = pv.product_version_name,
            //                            imei_no = rsnd.imei_no,
            //                            imei_no2 = rsnd.imei_no2,
            //                            sales_status = rsnd.sales_status,
            //                            price = pro.mrp_price,
            //                        }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).FirstOrDefault();
            //            imeiList.Add(data);
            //        }else if (aItem.Length == 0)
            //        {
            //            // do nothing
            //        }
            //        else
            //        {
            //            invalidImeis += aItem + ",";
            //        }
            //    }
            //    if (invalidImeis == "") // no invalid imeis
            //    {
            //        return new Confirmation {output = "success", msg = "success", returnvalue = imeiList};
            //    }
            //    else
            //    {
            //        return new Confirmation { output = "error", msg = "Invalid IMEIs :" + invalidImeis };
            //    }

            //}
            //else
            {
                //imei_no = imei_no.Trim();
                //if (imei_no.Length == 15)
                if(Math.Floor(Math.Log10(imei_no) + 1)==15)
                {
                    var data = (from rsnd in _entities.receive_serial_no_details
                        join pro in _entities.products on rsnd.product_id equals pro.product_id
                        join col in _entities.colors on rsnd.color_id equals col.color_id
                        join pv in _entities.product_version on rsnd.product_version_id equals pv.product_version_id
                        where
                            (rsnd.imei_no == imei_no || rsnd.imei_no2 == imei_no) && rsnd.sales_status == false &&
                            (rsnd.retailer_id == null || rsnd.retailer_id == 0) &&
                            rsnd.current_warehouse_id == from_warehouse_id && rsnd.sales_status == false
                        select new
                        {
                            receive_serial_no_details_id = rsnd.receive_serial_no_details_id,
                            product_id = rsnd.product_id,
                            product_name = pro.product_name,
                            color_id = rsnd.color_id,
                            color_name = col.color_name,
                            product_version_id = rsnd.product_version_id,
                            product_version_name = pv.product_version_name,
                            imei_no = rsnd.imei_no,
                            imei_no2 = rsnd.imei_no2,
                            price = pro.mrp_price,
                            sales_status = rsnd.sales_status
                        }).OrderByDescending(rsnd => rsnd.receive_serial_no_details_id).FirstOrDefault();
                    imeiList.Add(data);
                    return new Confirmation { output = "success", msg = "success", returnvalue = imeiList };
                }
                else
                {
                    return new Confirmation { output = "error", msg = "Invalid IMEI No." };
                }
            }
        }
    }
}