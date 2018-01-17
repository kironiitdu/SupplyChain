using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class BankBranchRepository : IBankBranchRepository
    {
        private DMSEntities _entities;

        public BankBranchRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllBankBranch()
        {
            try
            {
                var list = (from e in _entities.bank_branch
                            join b in _entities.banks on e.bank_id equals b.bank_id
                            select new
                            {
                                e.bank_branch_name,
                                e.bank_branch_id,
                                e.bank_id,
                                b.bank_name
                            }).OrderBy(b=>b.bank_branch_name).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InsertBankBranch(StronglyType.BankBranchModel oBank)
        {
            try
            {
                var listOfAccount = oBank.oAccountList;

                var branch = new bank_branch();
                branch.bank_branch_name = oBank.oBranch.bank_branch_name;
                branch.bank_id = oBank.oBranch.bank_id;
                branch.created_by = oBank.oBranch.created_by;
                branch.created_date = DateTime.Now;

                _entities.bank_branch.Add(branch);
                _entities.SaveChanges();

                Int64 branchId = branch.bank_branch_id;

                // add account

                foreach (var bankAccount in listOfAccount)
                {
                    var account = new bank_account();
                    account.bank_account_name = bankAccount.bank_account_name;
                    account.bank_branch_id = branchId;
                    account.created_by = oBank.oBranch.created_by;
                    account.created_date = DateTime.Now;

                    _entities.bank_account.Add(account);
                    _entities.SaveChanges();
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateBankBranch(bank_branch oBank)
        {
            try
            {
                var bbb = _entities.bank_branch.Find(oBank.bank_branch_id);
                bbb.bank_branch_name = oBank.bank_branch_name;
                bbb.bank_id = oBank.bank_id;
                bbb.updated_by = oBank.updated_by;
                bbb.updated_date = DateTime.Now;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool DeleteBankBranch(long branch_id)
        {
            try
            {
                var check = _entities.bank_account.Where(a => a.bank_branch_id == branch_id).ToList();
                if (check.Count > 0)
                {
                    return false;
                }
                else
                {
                    var ttt = _entities.bank_branch.Find(branch_id);
                    _entities.bank_branch.Remove(ttt);
                    _entities.SaveChanges();
                    return true;

                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public StronglyType.BankBranchModel GetByBankBranchModelId(long branchId)
        {
            try
            {
                var send = new BankBranchModel();
                
                var branch = _entities.bank_branch.SingleOrDefault(a => a.bank_branch_id == branchId);

                var list = _entities.bank_account.Where(a => a.bank_branch_id == branch.bank_branch_id).ToList();
                send.oBranch = branch;
                send.oAccountList = list;

                return send;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public object GetAllBankAccount()
        {
            try
            {
                var list = (from e in _entities.bank_account
                            join b in _entities.bank_branch on e.bank_branch_id equals b.bank_branch_id
                            select new
                            {
                                e.bank_account_name,
                                e.bank_account_id,
                                e.bank_branch_id,
                                b.bank_branch_name
                            }).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool InsertAccount(bank_account account1, long branchId, long createBy)
        {
            try
            {
                var account = new bank_account();
                account.bank_account_name = account1.bank_account_name;
                account.bank_branch_id = branchId;
                account.created_by = createBy;
                account.created_date = DateTime.Now;

                _entities.bank_account.Add(account);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool editAccount(bank_account account, long createBy)
        {
            try
            {
                var acc = _entities.bank_account.Find(account.bank_account_id);

                acc.bank_account_name = account.bank_account_name;
                acc.updated_by = createBy;
                acc.updated_date = DateTime.Now;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool deleteAccount(bank_account account)
        {
            try
            {
                var acc = _entities.bank_account.Find(account.bank_account_id);

                _entities.bank_account.Remove(acc);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public object GetAccount(long branchId)
        {
            try
            {
                var ttt = _entities.bank_account.Where(a => a.bank_branch_id == branchId).ToList();
                return ttt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}