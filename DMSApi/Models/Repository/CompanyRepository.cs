using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMSApi.Models.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private DMSEntities _entities;

        public CompanyRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<company> GetAllCompany()
        {
            var company = _entities.companies.Where(b => b.is_active == true && b.is_deleted == false).OrderByDescending(com => com.company_id).ToList();
            return company;
        }

        public bool CheckDuplicateCompany(string company_name)
        {
            var checkDuplicateCompany = _entities.companies.FirstOrDefault(c => c.company_name == company_name);
            bool return_type = checkDuplicateCompany == null ? false : true;
            return return_type;

        }

        public long AddCompany(company objCompany)
        {
            try
            {
                _entities.companies.Add(objCompany);
                _entities.SaveChanges();
                long last_insert_id = objCompany.company_id;
                return last_insert_id;

            }
            catch (Exception ex)
            {
                return 0;
            }
              
        }

        public company GetCompanyById(long company_id)
        {
            var companyById = _entities.companies.Find(company_id);
            return companyById;
        }

        public bool EditCompany(company objCompany)
        {
            try
            {
                company com = _entities.companies.Find(objCompany.company_id);
                com.company_name = objCompany.company_name;
                com.company_code = objCompany.company_code;
                com.address = objCompany.address;
                com.mobile = objCompany.mobile;
                com.email = objCompany.email;
                com.zip_code = objCompany.zip_code;
                com.country_id = objCompany.country_id;
                com.city_id = objCompany.city_id;
                com.updated_by = objCompany.updated_by;
                com.updated_date = DateTime.Now;
                com.is_active = objCompany.is_active;
                com.is_deleted = false;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCompany(long company_id)
        {
            try
            {
                company objCompany = _entities.companies.FirstOrDefault(c => c.company_id == company_id);
                _entities.companies.Attach(objCompany);
                _entities.companies.Remove(objCompany);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public string GetCompanyCode(long company_id)
        {
            try
            {
                var firstOrDefault = _entities.companies.FirstOrDefault(c => c.company_id == company_id);
                if (firstOrDefault != null)
                {
                    string companyCode = firstOrDefault.company_code;
                    return companyCode;
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetCompanyName(long company_id)
        {
            try
            {
                string companyCode = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().company_name;
                return companyCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string GetCompanyFlagLogo(long company_id)
        {
            try
            {
                //string companyFlag = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().flag_path;
                //return companyFlag;
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    //    //public object GetAllBank(int company_id)
    //    //{
    //    //    var Bank = _entities.banks.Where(a => a.source_id == company_id).ToList();
    //    //    return Bank;
    //    //}


    //    public bool UpdateBank(CompanyModel oCompany)
    //    {
    //        try
    //        {
    //            //var last_insert_id = insert_company.company_id;

    //            bank banks = _entities.banks.Find(oCompany.bank_id);

    //           // banks.source_type = "Company";
    //            banks.bank_name = oCompany.bank_name;
    //          //  banks.bank_acc_no = oCompany.bank_acc_no;
    //          ////  banks.bank_acc_id = oCompany.bank_acc_id;
    //          //  banks.bank_branch_name = oCompany.bank_branch_name;
    //          //  banks.swift_code = oCompany.swift_code;

    //            _entities.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }


    //    public bool InsertBank(CompanyModel oCompany)
    //    {

    //        try
    //        {
    //            bank insert_bank = new bank
    //             {
    //             //    source_id = oCompany.company_id,
    //              //   source_type = "Company",
    //                 bank_name = oCompany.bank_name,

    //                 //bank_acc_no = oCompany.bank_acc_no,
    //                // bank_acc_id = oCompany.bank_acc_id,
    //                 //bank_branch_name = oCompany.bank_branch_name,
    //                 //swift_code = oCompany.swift_code
    //             };
    //            _entities.banks.Add(insert_bank);
    //            _entities.SaveChanges();

    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }


    //    public bool CheckDuplicateCompany(string company_name)
    //    {
    //        var checkDplicateCompany = _entities.companies.FirstOrDefault(co => co.company_name == company_name);

    //            bool return_type = checkDplicateCompany == null ? false : true;
    //           return return_type;
    //    }
      

        //public string GetCompanyFlagLogo(long company_id)
        //{
        //    try
        //    {
        //        string companyFlag = _entities.companies.Where(c => c.company_id == company_id).FirstOrDefault().flag_path;
        //        return companyFlag;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

    }
}