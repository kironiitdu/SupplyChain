using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankBranchController : ApiController
    {
       private IBankBranchRepository bankBranchRepository;

        public BankBranchController()
        {
            this.bankBranchRepository = new BankBranchRepository();
        }

        public BankBranchController(IBankBranchRepository bankBranchRepository)
        {
            this.bankBranchRepository = bankBranchRepository;
        }

        [HttpGet]
        public HttpResponseMessage GetAllBankBranch()
        {
            var branch = bankBranchRepository.GetAllBankBranch();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetAllBankAccoun()
        {
            var branch = bankBranchRepository.GetAllBankAccount();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }

        [HttpGet]
        public HttpResponseMessage GetAccount(long bank_branch_id)
        {
            var branch = bankBranchRepository.GetAccount(bank_branch_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }
        [HttpGet]
        public HttpResponseMessage GetByBankBranchModelId(long bank_branch_id)
        {
            var branch = bankBranchRepository.GetByBankBranchModelId(bank_branch_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }
        [HttpPost]
        public HttpResponseMessage InsertAccount(bank_account bankAccount, long branchId, long createBy)
        {
            var branch = bankBranchRepository.InsertAccount(bankAccount, branchId, createBy);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }
        [HttpPut]
        public HttpResponseMessage editAccount(bank_account account, long createBy)
        {
            var branch = bankBranchRepository.editAccount(account, createBy);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }
        [HttpDelete]
        public HttpResponseMessage deleteAccount(bank_account account)
        {
            var branch = bankBranchRepository.deleteAccount(account);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteBankBranch(bank_branch bank)
        {
            var branch = bankBranchRepository.DeleteBankBranch(bank.bank_branch_id);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }

        [HttpPut]
        public HttpResponseMessage UpdateBankBranch(bank_branch bank)
        {
            var branch = bankBranchRepository.UpdateBankBranch(bank);
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, branch, formatter);
        }

        [HttpPost]
        public HttpResponseMessage Post(BankBranchModel modelOfBankBranch)
        {
            try
            {
                bool save_bank_branch = bankBranchRepository.InsertBankBranch(modelOfBankBranch);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "Bank Branch save successfully" }, formatter);

            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }
    }
}