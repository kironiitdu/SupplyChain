using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DMSEntities _entities;

        public SupplierRepository()
        {
            this._entities = new DMSEntities();
        }
        public List<supplier> GetAllSuppliers()
        {
            var suppliers = _entities.suppliers.OrderBy(s => s.supplier_name).Where(s => s.is_deleted != true).ToList();
            return suppliers;
        }

        public object GetAllInternationalSuppliers()
        {
            try
            {
                var suppliers = (from sup in _entities.suppliers
                            join st in _entities.supplier_type on sup.supplier_type_id equals st.supplier_type_id
                            select new
                            {
                                sup.supplier_name,
                                sup.supplier_id,
                                st.supplier_type_name,
                                st.supplier_type_id,
                                sup.is_deleted,
                            }).OrderBy(s => s.supplier_name).Where(s => s.is_deleted != true && s.supplier_type_name == "International").ToList();
                return suppliers;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public object GetAllLocalSuppliers()
        {
            try
            {
                var suppliers = (from sup in _entities.suppliers
                                 join st in _entities.supplier_type on sup.supplier_type_id equals st.supplier_type_id
                                 select new
                                 {
                                     sup.supplier_name,
                                     sup.supplier_id,
                                     st.supplier_type_name,
                                     st.supplier_type_id,
                                     sup.is_deleted,
                                 }).OrderByDescending(s => s.supplier_id).Where(s => s.is_deleted != true && s.supplier_type_name == "Local").ToList();
                return suppliers;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public long AddSupplier(supplier objSupplier)
        {
            try
            {
                int suppliersCode = _entities.suppliers.Max(sp => (int?)sp.supplier_id) ?? 0;
                suppliersCode++;

                var spStr = suppliersCode.ToString().PadLeft(7, '0');
                string SupplierCode = "SUP" + "-" + spStr;
                supplier insert_supplier = new supplier
                {

                    supplier_name = objSupplier.supplier_name,
                    supplier_code = SupplierCode,
                    supplier_type_id = objSupplier.supplier_type_id,
                    company_address = objSupplier.company_address,
                    factory_address = objSupplier.factory_address,
                    phone = objSupplier.phone,
                    mobile = objSupplier.mobile,
                    email = objSupplier.email,
                    email2 = objSupplier.email2,
                    contact_person = objSupplier.contact_person,
                    contact_person2 = objSupplier.contact_person2,
                    total_debit = objSupplier.total_debit,
                    total_credit = objSupplier.total_credit,
                    balance = objSupplier.balance,
                    vat_id = objSupplier.vat_id,
                    tin_no = objSupplier.tin_no,
                    receiving_company_name = objSupplier.receiving_company_name,
                    receiving_company_person = objSupplier.receiving_company_person,
                    receiving_company_contact_email = objSupplier.receiving_company_contact_email,
                    is_active = true,
                    is_deleted = objSupplier.is_deleted = false,
                    created_by = objSupplier.created_by,
                    created_date = objSupplier.created_date = DateTime.Now

                };
                _entities.suppliers.Add(insert_supplier);
                _entities.SaveChanges();
                long last_insert_id = insert_supplier.supplier_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public supplier GetSupplierById(long supplier_id)
        {
            var supplier = _entities.suppliers.Find(supplier_id);
            return supplier;
        }

        public bool EditSupplier(supplier objSupplier)
        {
            try
            {

                supplier sup = _entities.suppliers.Find(objSupplier.supplier_id);
                sup.supplier_name = objSupplier.supplier_name;
                sup.supplier_code = objSupplier.supplier_code;
                sup.supplier_type_id = objSupplier.supplier_type_id;
                sup.company_address = objSupplier.company_address;
                sup.factory_address = objSupplier.factory_address;
                sup.phone = objSupplier.phone;
                sup.mobile = objSupplier.mobile;
                sup.email = objSupplier.email;
                sup.email2 = objSupplier.email2;
                sup.contact_person = objSupplier.contact_person;
                sup.contact_person2 = objSupplier.contact_person2;
                sup.total_debit = objSupplier.total_debit;
                sup.total_credit = objSupplier.total_credit;
                sup.balance = objSupplier.balance;
                sup.vat_id = objSupplier.vat_id;
                sup.tin_no = objSupplier.tin_no;
                sup.is_active = objSupplier.is_active;
                sup.receiving_company_name = objSupplier.receiving_company_name;
                sup.receiving_company_person = objSupplier.receiving_company_person;
                sup.receiving_company_contact_email = objSupplier.receiving_company_contact_email;
                sup.updated_by = objSupplier.updated_by;
                sup.updated_date = DateTime.Now;

                _entities.SaveChanges();

                return true;




            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSupplier(long supplier_id)
        {
            throw new NotImplementedException();
        }


        public bool CheckDuplicateSupplier(string supplier_name)
        {
            var checkDuplicatesupplierName = _entities.suppliers.FirstOrDefault(s => s.supplier_name == supplier_name);
            bool returnSupplier = checkDuplicatesupplierName == null ? false : true;
            return returnSupplier;
        }


        public bool DeleteSupplier(long supplier_id, long updated_by)
        {
            try
            {
                supplier oSupplier = _entities.suppliers.FirstOrDefault(c => c.supplier_id == supplier_id);
                oSupplier.is_deleted = true;
                oSupplier.updated_by = updated_by;
                oSupplier.updated_date = DateTime.Now;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}