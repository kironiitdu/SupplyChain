using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class SalesTypeRepository : ISalesTypeRepository
    {
        private DMSEntities _entities;

        public SalesTypeRepository()
        {
            this._entities = new DMSEntities();
        }


        public List<sales_type> GetAllsalSalesTypes()
        {
            var sales = _entities.sales_type.OrderBy(s =>s.sales_type1).ToList();
            return sales;
        }

        public sales_type GetSalesTypesById(long salesTypeId)
        {
            throw new NotImplementedException();
        }

        public bool InsertSalesType(sales_type objSalesType)
        {
            try
            {
                sales_type sales = new sales_type
                {
                    sales_type1 = objSalesType.sales_type1,
                    is_active = objSalesType.is_active,

                };
                _entities.sales_type.Add(sales);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSalesType(long salesTypeId)
        {
            try
            {
                sales_type objsaSalesType = _entities.sales_type.FirstOrDefault(d => d.sales_type_id == salesTypeId);
                _entities.sales_type.Attach(objsaSalesType);
                _entities.sales_type.Remove(objsaSalesType);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateSalesType(sales_type objSalesType)
        {
            try
            {
                sales_type sales = _entities.sales_type.Find(objSalesType.sales_type_id);
                sales.sales_type1 = objSalesType.sales_type1;
                sales.is_active = objSalesType.is_active;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicateSalesType(string salesType)
        {
            try
            {
                var checkDuplicateSales = _entities.sales_type.FirstOrDefault(d => d.sales_type1 == salesType);

                bool return_type = checkDuplicateSales == null ? false : true;
                return return_type;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}