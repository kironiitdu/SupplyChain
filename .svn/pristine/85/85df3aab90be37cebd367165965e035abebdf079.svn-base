using DMSApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.Repository
{
    public class BankRepository : IBankRepository
    {
        private DMSEntities _entities;

        public BankRepository()
        {
            this._entities = new DMSEntities();
        }


        public List<bank> GetAllBank()
        {
            List<bank> bbranch = _entities.banks.Where(a=>a.is_deleted == false).OrderBy(b => b.bank_name).ToList();

            return bbranch;
        }

        public bool InsertBank(bank oBank)
        {
            try
            {


                _entities.banks.Add(oBank);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateBank(bank oBank)
        {
            try
            {
                bank entity = _entities.banks.Find(oBank.bank_id);
                entity.bank_name = oBank.bank_name;
                entity.is_active = oBank.is_active;
                entity.is_deleted = false;
                entity.updated_by = oBank.updated_by;
                entity.updated_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBank(long bank_id, long updated_by)
        {
            try
            {
                bank oBankBranch = _entities.banks.FirstOrDefault(bb => bb.bank_id == bank_id);

                var request = _entities.payment_request.Where(a => a.bank_id == bank_id).ToList();
                var receivePayment = _entities.receives.Where(a => a.bank_id == bank_id).ToList();
                if (request.Count > 0 || receivePayment.Count > 0)
                {
                    return false;
                }
                else
                {
                    oBankBranch.is_deleted = true;
                    oBankBranch.is_active = false;
                    oBankBranch.updated_by = updated_by;
                    oBankBranch.updated_date = DateTime.Now;
                    _entities.SaveChanges();
                    return true;
                }               
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DuplicateCheck(string bank_name)
        {
            try
            {
                var ttt = _entities.banks.Where(a => a.bank_name == bank_name && a.is_deleted == false).ToList();
                if (ttt.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }


        public bool DuplicateCheckEdit(long bank_id, string bName)
        {
            try
            {
                var ttt =
                    _entities.banks.Where(a => a.bank_id != bank_id && a.bank_name == bName && a.is_deleted == false)
                        .ToList();
                if (ttt.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}