using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private DMSEntities _entities;

        public DepartmentRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<department> GetAllDepartment()
        {
            var dept = _entities.departments.OrderBy(d=>d.department_name).ToList();
            return dept;
        }


        public department GetDepartmentByID(long department_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertDepartment(department oDepartment)
        {
            try
            {
                department insertdepartment = new department
                {
                    department_name = oDepartment.department_name,
                    is_active = oDepartment.is_active,
                  
                };
                _entities.departments.Add(insertdepartment);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDepartment(long department_id)
        {
            try
            {
                department objDepartment = _entities.departments.FirstOrDefault(d => d.department_id == department_id);
                _entities.departments.Attach(objDepartment);
                _entities.departments.Remove(objDepartment);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDepartment(department oDepartment)
        {
            try
            {
                department dep = _entities.departments.Find(oDepartment.department_id);
                dep.department_name = oDepartment.department_name;
                dep.is_active = oDepartment.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateDepartment(string depatmentName)
        {
            try
            {
                var checkDuplicateDept = _entities.departments.FirstOrDefault(d => d.department_name == depatmentName);

                bool return_type = checkDuplicateDept == null ? false : true;
                return return_type;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}