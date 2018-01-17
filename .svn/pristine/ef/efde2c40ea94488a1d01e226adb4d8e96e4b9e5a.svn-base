using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IDepartmentRepository
    {
        List<department> GetAllDepartment();
        department GetDepartmentByID(long department_id);

        bool InsertDepartment(department oDepartment);

        bool DeleteDepartment(long department_id);

        bool UpdateDepartment(department oDepartment);

        bool CheckDuplicateDepartment(string depatmentName);
    }
}
