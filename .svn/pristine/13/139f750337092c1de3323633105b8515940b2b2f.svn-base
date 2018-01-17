using DMSApi.Models.StronglyType;
using System.Collections.Generic;

namespace DMSApi.Models.IRepository
{
    public interface ICompanyRepository
    {
        List<company> GetAllCompany();
        bool CheckDuplicateCompany(string company_name);
        long AddCompany(company objCompany);
        company GetCompanyById(long company_id);
        bool EditCompany(company objCompany);
        bool DeleteCompany(long company_id);
        string GetCompanyCode(long company_id);

        string GetCompanyName(long company_id);

        //string GetCompanyFlagLogo(long company_id);

    }
}