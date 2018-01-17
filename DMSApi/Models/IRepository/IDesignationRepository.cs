using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IDesignationRepository
    {
        List<designation> GetAllDesignation();
        designation GetDesignationById(long sales_designation_id);
        object GetAllDesignationForGrid();
        long AddDesignation(designation designation);
        bool EditDesignation(designation designation);
        bool DeleteDesignation(long sales_designation_id);
        bool CheckDuplicateDesignation(string sales_designation);
    }
}
