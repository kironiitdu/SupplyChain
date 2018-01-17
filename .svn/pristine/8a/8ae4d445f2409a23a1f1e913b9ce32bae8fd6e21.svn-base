using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IBankBranchRepository
    {
        object GetAllBankBranch();
        bool InsertBankBranch(BankBranchModel oBank);
        bool UpdateBankBranch(bank_branch oBank);
        bool DeleteBankBranch(long branch_id);
        BankBranchModel GetByBankBranchModelId(long branchId);

        object GetAllBankAccount();
        bool InsertAccount(bank_account account,long branchId,long createBy);
        bool editAccount(bank_account account, long createBy);
        bool deleteAccount(bank_account account);
        object GetAccount(long branchId);

    }
}
