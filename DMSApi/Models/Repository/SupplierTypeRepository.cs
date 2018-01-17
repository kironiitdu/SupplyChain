using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class SupplierTypeRepository : ISupplierTypeRepository
    {

        private DMSEntities _entities;

        public SupplierTypeRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<supplier_type> GetSupplierTypeListForGrid()
        {
            List<supplier_type> objSupplierType = _entities.supplier_type.OrderBy(st => st.supplier_type_name).Where(st => st.is_deleted == false).ToList();
            return objSupplierType;
        }

        public object GetAllSupplierType()
        {
            throw new NotImplementedException();
        }

        public bool AddSupplierType(supplier_type objSupplierType, long? created_by)
        {
            try
            {
                supplier_type insertSupplierType = new supplier_type
                {
                    supplier_type_name = objSupplierType.supplier_type_name,
                    created_by = objSupplierType.created_by,
                    created_date = DateTime.Now,
                    is_deleted = objSupplierType.is_deleted = false,
                    is_active = objSupplierType.is_active = true
                };
                _entities.supplier_type.Add(insertSupplierType);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool EditSupplierType(supplier_type objSupplierType,long? updated_by)
        {
            try
            {
                supplier_type objFindId = _entities.supplier_type.Find(objSupplierType.supplier_type_id);
                objFindId.supplier_type_name = objSupplierType.supplier_type_name;
                objFindId.updated_by = updated_by;
                objFindId.updated_date = DateTime.Now;
                objFindId.is_deleted = objSupplierType.is_deleted = false;
                objFindId.is_active = objSupplierType.is_active;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSupplierType(long supplier_type_id, long? updated_by)
        {
            try
            {
                supplier_type objSupplierType = _entities.supplier_type.FirstOrDefault(st => st.supplier_type_id == supplier_type_id);
                objSupplierType.is_deleted = true;
                objSupplierType.updated_by = updated_by;
                objSupplierType.updated_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateSupplierType(string supplier_type_name)
        {
            var checkDuplicateSupplierType = _entities.supplier_type.FirstOrDefault(st => st.supplier_type_name == supplier_type_name);
            bool return_type = checkDuplicateSupplierType == null ? false : true;
            return return_type;
        }

       
        public List<supplier_type> GetSupplierTypeListForDropDown()
        {
            List<supplier_type> objSupplierType = _entities.supplier_type.OrderByDescending(st => st.supplier_type_id).Where(st=>st.is_active==true).ToList();
            return objSupplierType;
        }
    }
}