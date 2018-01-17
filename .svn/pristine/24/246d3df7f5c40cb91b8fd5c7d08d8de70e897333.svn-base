using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IEmployeeRepository
    {
        object GetAllEmployees();
        object GetEmployeeByEmployeeName(string employee_name);
        List<employee> GetEmplyieesForDropdown();
        long AddEmployee(EmployeeModel employeeModel);
        EmployeeModel GetEmployeeById(long employee_id);
        bool EditEmployee(EmployeeModel employeeModel);
        bool DeleteEmployee(long employee_id);
        //List<employee> GetPartywiseEmplyieesForDropdown(long partyId);
        //object GetDropdownForSalesTarget();
        //object GetDropdownForPaymentRequest(string party_type_name);
    }
}
