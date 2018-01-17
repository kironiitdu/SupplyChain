using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class CiPlRepository : ICiPlRepository
    {
        private DMSEntities _entities;

        public CiPlRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllCiPl()
        {
            var data = (from ciplm in _entities.ci_pl_master
                        join sup in _entities.suppliers on ciplm.supplier_id equals sup.supplier_id
                        join pom in _entities.purchase_order_master on ciplm.purchase_order_master_id equals pom.purchase_order_master_id
                        select new
                        {
                            ci_pl_master_id = ciplm.ci_pl_master_id,
                            supplier_id = ciplm.supplier_id,
                            supplier_name = sup.supplier_name,
                            purchase_order_master_id = ciplm.purchase_order_master_id,
                            pi_number = pom.pi_number,
                            ci_no = ciplm.ci_no,
                            ref_no = ciplm.ref_no,
                            ci_date = ciplm.ci_date,
                            payment_term = ciplm.payment_term,
                            dc_no = ciplm.dc_no,
                            is_active = ciplm.is_active,
                            issue_date = ciplm.issue_date,
                            is_received = ciplm.is_received
                        }).Where(e => e.is_active == true).OrderByDescending(e => e.ci_pl_master_id).ToList();

            return data;
        }

        public bool CheckDuplicateCiNo(string ciNo)
        {
            var checkDuplicateciNo = _entities.ci_pl_master.FirstOrDefault(b => b.ci_no == ciNo);
            bool return_type = checkDuplicateciNo == null ? false : true;
            return return_type;
        }

        public long AddCiPl(CiPlModel ciPlModel)
        {
            try
            {
                var ciPlMaster = ciPlModel.CiPlMasterData;
                var ciPlDetailsList = ciPlModel.CiPlDetailsList;

                ciPlMaster.supplier_id = ciPlModel.CiPlMasterData.supplier_id;
                ciPlMaster.purchase_order_master_id = ciPlModel.CiPlMasterData.purchase_order_master_id;
                ciPlMaster.ci_no = ciPlModel.CiPlMasterData.ci_no;
                ciPlMaster.ref_no = ciPlModel.CiPlMasterData.ref_no;
                ciPlMaster.ci_date = Convert.ToDateTime(ciPlModel.CiPlMasterData.ci_date);
                ciPlMaster.payment_term = ciPlModel.CiPlMasterData.payment_term;
                ciPlMaster.dc_no = ciPlModel.CiPlMasterData.dc_no;
                ciPlMaster.issue_date = Convert.ToDateTime(ciPlModel.CiPlMasterData.issue_date);
                ciPlMaster.created_by = ciPlModel.CiPlMasterData.created_by;
                ciPlMaster.created_date = DateTime.Now;
                ciPlMaster.updated_by = ciPlModel.CiPlMasterData.updated_by;
                ciPlMaster.updated_date = DateTime.Now;
                ciPlMaster.is_active = true;
                ciPlMaster.is_received = false;

                _entities.ci_pl_master.Add(ciPlMaster);
                _entities.SaveChanges();
                long ciPlMasterId = ciPlMaster.ci_pl_master_id;

                foreach (var item in ciPlDetailsList)
                {
                    var ciPlDetails = new ci_pl_details
                    {
                        ci_pl_master_id = ciPlMasterId,
                        product_id = item.product_id,
                        unit_price = item.unit_price,
                        quantity = item.quantity,
                        amount = item.amount,
                        nw_kg_ctn = item.nw_kg_ctn,
                        gw_kg_ctn = item.gw_kg_ctn,
                        measurement = item.measurement

                    };

                    _entities.ci_pl_details.Add(ciPlDetails);
                    _entities.SaveChanges();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public CiPlModel GetCiPlById(long ci_pl_master_id)
        {
            var ciPlModel = new CiPlModel();

            ciPlModel.CiPlMasterData = _entities.ci_pl_master.Find(ci_pl_master_id);

            ciPlModel.CiPlDetailsList =
                _entities.ci_pl_details
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                    .Where(k => k.jp.ci_pl_master_id == ci_pl_master_id)
                    .Select(i => new CiPlDetailsModel
                    {
                        ci_pl_details_id = i.jp.ci_pl_details_id,
                        ci_pl_master_id = i.jp.ci_pl_master_id,
                        product_id = i.p.product_id,
                        product_name = i.p.product_name,
                        has_serial = i.p.has_serial??false,
                        quantity = i.jp.quantity,
                        unit_price = i.jp.unit_price,
                        amount = i.jp.amount,
                        nw_kg_ctn = i.jp.nw_kg_ctn,
                        gw_kg_ctn = i.jp.gw_kg_ctn,
                        measurement = i.jp.measurement
                    }).OrderByDescending(p => p.ci_pl_details_id).ToList();


            return ciPlModel;
        }

        public bool EditCiPl(CiPlModel ciPlModel)
        {
            try
            {
                var ciPlMaster = ciPlModel.CiPlMasterData;
                var ciPlDetailsList = ciPlModel.CiPlDetailsList;
                ci_pl_master masterData = _entities.ci_pl_master.Find(ciPlMaster.ci_pl_master_id);

                masterData.supplier_id = ciPlModel.CiPlMasterData.supplier_id;
                masterData.purchase_order_master_id = ciPlModel.CiPlMasterData.purchase_order_master_id;
                masterData.ci_no = ciPlModel.CiPlMasterData.ci_no;
                masterData.ref_no = ciPlModel.CiPlMasterData.ref_no;
                masterData.ci_date = Convert.ToDateTime(ciPlModel.CiPlMasterData.ci_date);
                masterData.payment_term = ciPlModel.CiPlMasterData.payment_term;
                masterData.dc_no = ciPlModel.CiPlMasterData.dc_no;
                masterData.issue_date = Convert.ToDateTime(ciPlModel.CiPlMasterData.issue_date);
                masterData.updated_by = ciPlModel.CiPlMasterData.updated_by;
                masterData.updated_date = DateTime.Now;
                masterData.is_active = true;

                _entities.SaveChanges();


                foreach (var item in ciPlDetailsList)
                {
                    ci_pl_details detailsData = _entities.ci_pl_details.FirstOrDefault(pd => pd.ci_pl_master_id == item.ci_pl_master_id && pd.ci_pl_details_id == item.ci_pl_details_id);
                    if (detailsData != null)
                    {
                        detailsData.ci_pl_master_id = ciPlMaster.ci_pl_master_id;
                        detailsData.product_id = item.product_id;
                        detailsData.unit_price = item.unit_price;
                        detailsData.quantity = item.quantity;
                        detailsData.amount = item.amount;
                        detailsData.nw_kg_ctn = item.nw_kg_ctn;
                        detailsData.gw_kg_ctn = item.gw_kg_ctn;
                        detailsData.measurement = item.measurement;
                        _entities.SaveChanges();

                    }
                    else
                    {
                        var ciPlDetails = new ci_pl_details
                        {
                            ci_pl_master_id = ciPlMaster.ci_pl_master_id,
                            product_id = item.product_id,
                            unit_price = item.unit_price,
                            quantity = item.quantity,
                            amount = item.amount,
                            nw_kg_ctn = item.nw_kg_ctn,
                            gw_kg_ctn = item.gw_kg_ctn,
                            measurement = item.measurement

                        };

                        _entities.ci_pl_details.Add(ciPlDetails);
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

        public bool DeleteCiPl(long ci_pl_master_id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCiPlDetailsById(long ci_pl_details_id)
        {
            try
            {
                ci_pl_details oCiPlDetailsDetails = _entities.ci_pl_details.Find(ci_pl_details_id);
                _entities.ci_pl_details.Attach(oCiPlDetailsDetails);
                _entities.ci_pl_details.Remove(oCiPlDetailsDetails);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetNewCiPl()
        {
            var data = (from ciplm in _entities.ci_pl_master
                        join sup in _entities.suppliers on ciplm.supplier_id equals sup.supplier_id
                        join pom in _entities.purchase_order_master on ciplm.purchase_order_master_id equals pom.purchase_order_master_id
                        select new
                        {
                            ci_pl_master_id = ciplm.ci_pl_master_id,
                            supplier_id = ciplm.supplier_id,
                            supplier_name = sup.supplier_name,
                            purchase_order_master_id = ciplm.purchase_order_master_id,
                            pi_number = pom.pi_number,
                            ci_no = ciplm.ci_no,
                            ref_no = ciplm.ref_no,
                            ci_date = ciplm.ci_date,
                            payment_term = ciplm.payment_term,
                            dc_no = ciplm.dc_no,
                            is_active = ciplm.is_active,
                            issue_date = ciplm.issue_date,
                            is_received = ciplm.is_received
                        }).Where(e => e.is_active == true && e.is_received==false).OrderByDescending(e => e.ci_pl_master_id).ToList();

            return data;
        }
    }
}